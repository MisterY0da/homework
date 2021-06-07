﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace NetsLab2
{
    public class SecondStation
    {
        private Semaphore _mainSemaphore;
        private Semaphore _signalFromSecondBuffer;
        private Semaphore _signalToFirstBuffer;
        private Semaphore _signalFromFirstSt;
        private Semaphore _signalToFirstSt;

    

        private PostDataToFirstBufWt _postFrame;
        private BitArray[] _sentFrames;
        private BitArray _receivedReceipt;

        private PostReceiptToFirstStWt _postReceipt;
        private BitArray _sentReceipt;
        private BitArray[] _receivedFrames;

        public SecondStation(ref Semaphore secondBufToSecondSt, ref Semaphore secondStToFirstBuf, 
            ref Semaphore firstStToSecondSt, ref Semaphore secondStToFirstSt, ref Semaphore mainSemaphore)
        {           
            _signalFromSecondBuffer = secondBufToSecondSt;           
            _signalToFirstBuffer = secondStToFirstBuf;
            _signalFromFirstSt = firstStToSecondSt;
            _signalToFirstSt = secondStToFirstSt;
            
            _mainSemaphore = mainSemaphore;
        }

        public void SendFramesToFirstBuf(object obj)
        {
            SplitDataOnFragments();

            _mainSemaphore.WaitOne();
            _postFrame = (PostDataToFirstBufWt)obj;
            

            _postFrame(_sentFrames);
            ConsoleHelper.WriteToConsole("станция 2", "отправил кадры буферу 1");
            _signalToFirstBuffer.Release();

            _signalFromFirstSt.WaitOne();
            ConsoleHelper.WriteToConsoleArray("станция 2 полученная квитанция", _receivedReceipt);
            _mainSemaphore.Release();

        }

        public void SendReceiptToFirstSt(Object obj)
        {
            Stopwatch framesReceptionWatch = new Stopwatch();
            _postReceipt = (PostReceiptToFirstStWt)obj;
            _sentReceipt = new BitArray(1);

            framesReceptionWatch.Start();
            _signalFromSecondBuffer.WaitOne();
            framesReceptionWatch.Stop();
            TimeSpan framesReceptionTime = framesReceptionWatch.Elapsed;

            if (framesReceptionTime.Milliseconds > 20)
            {
                _sentReceipt[0] = false;
                ConsoleHelper.WriteToConsole("станция 2", "Данные не получены");
            }

            else
            {
                _sentReceipt[0] = true;
                for (int i = 0; i < _receivedFrames.Length; i++)
                {
                    if (ValidData(_receivedFrames[i]) == true)
                    {
                        ConsoleHelper.WriteToConsoleArray("станция 2 полученный кадр №" + FrameHelper.getIntFromBitArray(FrameHelper.GetBinaryFrameNumber(_receivedFrames[i])), 
                            FrameHelper.GetFrameData(_receivedFrames[i]));
                    }
                    else
                    {
                        ConsoleHelper.WriteToConsole("станция 2 полученный кадр №" + FrameHelper.getIntFromBitArray(FrameHelper.GetBinaryFrameNumber(_receivedFrames[i])),
                            "данные повреждены");
                    }
                }
            }

            _postReceipt(_sentReceipt);
            ConsoleHelper.WriteToConsole("станция 2", "отправил квитанцию станции 1");
            _signalToFirstSt.Release();
        }

        public void ReceiveFrames(BitArray[] frames)
        {
            _receivedFrames = frames;
        }

        public void ReceiveReceipt(BitArray receipt)
        {
            _receivedReceipt = receipt;
        }


        public void SplitDataOnFragments()
        {
            const int frameSizeInBits = 16;

            BitArray data = GenerateSomeData();
            BitArray[] dataFragments;

            if (data.Length % frameSizeInBits == 0)
            {
                dataFragments = new BitArray[data.Length / frameSizeInBits];
            }

            else if (data.Length < frameSizeInBits)
            {
                dataFragments = new BitArray[1];
            }

            //when data.Length % frameSize == 0 && data.Length > frameSize
            else
            {
                dataFragments = new BitArray[(data.Length / frameSizeInBits) + 1];
            }

            for (int fragmentInd = 0; fragmentInd < dataFragments.Length; fragmentInd++)
            {
                if (data.Length % frameSizeInBits == 0)
                {
                    dataFragments[fragmentInd] = new BitArray(frameSizeInBits);
                }
                else
                {
                    int newFrameSize = dataFragments.Length % frameSizeInBits;
                    dataFragments[fragmentInd] = new BitArray(newFrameSize);
                }
            }

            for (int fragmentInd = 0; fragmentInd < dataFragments.Length; fragmentInd++)
            {
                BitArrayCopy(data, fragmentInd * frameSizeInBits, dataFragments[fragmentInd]);
            }

            _sentFrames = new BitArray[dataFragments.Length];

            for (int frameInd = 0; frameInd < _sentFrames.Length; frameInd++)
            {
                _sentFrames[frameInd] = FrameHelper.FillFrame(dataFragments[frameInd], frameInd);
            }
        }


        public static void BitArrayCopy(BitArray sourceArray, int sourceIndex, BitArray destinationArray)
        {
            for (int i = 0; i < destinationArray.Length; i++)
            {
                destinationArray[i] = sourceArray[sourceIndex];
                sourceIndex++;
            }
        }

        public BitArray GenerateSomeData()
        {
            string str = "message from the second station!";

            byte[] strInBytes = Encoding.UTF8.GetBytes(str);

            BitArray someData = new BitArray(strInBytes);

            return someData;
        }

        public static bool ValidData(BitArray frame)
        {
            BitArray receivedData = FrameHelper.GetFrameData(frame);
            BitArray expectedParity = GetExpectedFrameParity(frame);
            BitArray receivedParity = CalculateFrameParity(receivedData);

            for (int i = 0; i < receivedParity.Length; i++)
            {
                if (receivedParity[i] != expectedParity[i])
                {
                    return false;
                }
            }

            return true;
        }


        public static BitArray GetExpectedFrameParity(BitArray frame)
        {
            BitArray parity = new BitArray(FrameHelper.PARITYBLOCKBITSCOUNT);
            for (int i = frame.Length - FrameHelper.PARITYBLOCKBITSCOUNT - FrameHelper.FRAMENUMBERBLOCKBITSCOUNT;
                i < frame.Length - FrameHelper.FRAMENUMBERBLOCKBITSCOUNT; i++)
            {
                parity[i - (frame.Length - FrameHelper.PARITYBLOCKBITSCOUNT - FrameHelper.FRAMENUMBERBLOCKBITSCOUNT)] = frame[i];
            }

            return parity;
        }

        public static BitArray CalculateFrameParity(BitArray receivedData)
        {
            BitArray calculatedParity = new BitArray(FrameHelper.PARITYBLOCKBITSCOUNT);

            for (int i = 0; i < 8; i++)
            {
                int columnBitsSum = 0;
                for (int j = i; j < receivedData.Length; j += 8)
                {
                    if (receivedData[j] == true)
                    {
                        columnBitsSum += 1;
                    }
                }

                if (columnBitsSum % 2 != 0)
                {
                    calculatedParity[i] = true;
                }
                else
                {
                    calculatedParity[i] = false;
                }
            }

            return calculatedParity;
        }
    }
}