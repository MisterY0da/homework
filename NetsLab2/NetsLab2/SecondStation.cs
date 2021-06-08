using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace NetsLab2
{
    public class SecondStation
    {
        const int windowSize = 8;

        private Semaphore _mainSemaphore;
        private Semaphore _signalFromSecondBuffer;
        private Semaphore _signalToFirstBuffer;
        private Semaphore _signalFromFirstSt;
        private Semaphore _signalToFirstSt;

    

        private PostDataToFirstBufWt _postFrames;
        private BitArray[] _sentFrames;
        private BitArray _receivedReceipt;

        private SecondStPostToFirstStWt _postReceipt;
        private BitArray _sentReceipt;
        private BitArray[] _receivedFrames;

        public static bool frameIsMissed = false;

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

            _postFrames = (PostDataToFirstBufWt)obj;
            SendPortionOfData(ref _sentFrames, ref _postFrames, 0, windowSize);
            ConsoleHelper.WriteToConsole("станция 2", "отправил кадры буферу 1");

            _signalToFirstBuffer.Release();

            _signalFromFirstSt.WaitOne();

            ConsoleHelper.WriteToConsoleArray("станция 2 полученный RR-ответ", _receivedReceipt);
            SendPortionOfData(ref _sentFrames, ref _postFrames, windowSize, windowSize);
            ConsoleHelper.WriteToConsole("станция 2", "отправил кадры буферу 1");

            _signalToFirstBuffer.Release();

            _signalFromFirstSt.WaitOne();

            ConsoleHelper.WriteToConsoleArray("станция 2 полученный RNR-ответ", _receivedReceipt);

            _mainSemaphore.Release();

        }

        public void SendPortionOfData(ref BitArray[] sentFrames, ref PostDataToFirstBufWt postFrames, int startSendingInd, int portionSize)
        {
            BitArray[] portion = new BitArray[portionSize];

            Array.Copy(sentFrames, startSendingInd, portion, 0, portionSize);

            postFrames(portion);
        }

        public void SendReceiptToFirstSt(Object obj)
        {
            Stopwatch framesReceptionWatch = new Stopwatch();
            _postReceipt = (SecondStPostToFirstStWt)obj;
            _sentReceipt = new BitArray(1);

            framesReceptionWatch.Start();
            _signalFromSecondBuffer.WaitOne();
            framesReceptionWatch.Stop();
            TimeSpan framesReceptionTime = framesReceptionWatch.Elapsed;

            if (framesReceptionTime.Milliseconds > 40)
            {
                _sentReceipt[0] = false;
                ConsoleHelper.WriteToConsole("станция 2", "отправляю REJ-запрос");
            }

            else
            {
                _sentReceipt[0] = true;
                for (int i = 0; i < _receivedFrames.Length; i++)
                {
                    Random rnd = new Random();

                    bool dataIsValid = (rnd.Next(1, 20) % 6) != 0;

                    if (dataIsValid == true)
                    {
                        ConsoleHelper.WriteToConsoleArray("станция 2 полученный кадр №" + FrameHelper.getIntFromBitArray(FrameHelper.GetBinaryFrameNumber(_receivedFrames[i])), 
                            FrameHelper.GetFrameData(_receivedFrames[i]));
                    }
                    else
                    {
                        ConsoleHelper.WriteToConsole("станция 2 полученный кадр №" + FrameHelper.getIntFromBitArray(FrameHelper.GetBinaryFrameNumber(_receivedFrames[i])),
                            "данные повреждены");
                        ConsoleHelper.WriteToConsole("станция 2", "отправляю REJ-запрос");
                        frameIsMissed = true;
                        _signalToFirstSt.Release();
                        //_sentReceipt[0] = false;
                        break;
                    }
                }
            }

            _postReceipt(_sentReceipt);

            if (frameIsMissed == true)
            {
                _signalFromSecondBuffer.WaitOne();
                for (int i = 0; i < _receivedFrames.Length; i++)
                {
                    ConsoleHelper.WriteToConsoleArray("станция 2 полученный кадр №" + FrameHelper.getIntFromBitArray(FrameHelper.GetBinaryFrameNumber(_receivedFrames[i])),
                            FrameHelper.GetFrameData(_receivedFrames[i]));
                }
            }

            ConsoleHelper.WriteToConsole("станция 2", "отправил RR-ответ станции 1");
            _signalToFirstSt.Release();

            Stopwatch framesReceptionWatch2 = new Stopwatch();
            _sentReceipt = new BitArray(1);

            framesReceptionWatch2.Start();

            _signalFromSecondBuffer.WaitOne();
            framesReceptionWatch2.Stop();
            TimeSpan framesReceptionTime2 = framesReceptionWatch2.Elapsed;

            if (framesReceptionTime2.Milliseconds > 40)
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
            ConsoleHelper.WriteToConsole("станция 2", "отправил RNR-ответ станции 1");

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
