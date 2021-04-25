using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class FirstStation
    {
        private Semaphore _mainSemaphore;
        private Semaphore _signalToSecondBuffer;
        private Semaphore _signalFromSecondBuffer;
        private Semaphore _signalToFirstBuffer;
        private Semaphore _signalFromFirstBuffer;

        private PostDataToSecondBufferWt _postFrames;
        private BitArray[] _sentFrames;
        private BitArray _receivedReceipt;


        private PostReceiptToFirstBufferWt _postReceipt;
        private BitArray _sentReceipt;
        private BitArray[] _receivedFrames;


        public FirstStation(ref Semaphore signalToSecondBuffer, ref Semaphore signalFromSecondBuffer, 
            ref Semaphore signalToFirstBuffer, ref Semaphore signalFromFirstBuffer, ref Semaphore mainSemaphore)
        {
            _signalToSecondBuffer = signalToSecondBuffer;
            _signalFromSecondBuffer = signalFromSecondBuffer;
            _signalToFirstBuffer = signalToFirstBuffer;
            _signalFromFirstBuffer = signalFromFirstBuffer;
            _mainSemaphore = mainSemaphore;
        }



        public void SendFramesToSecond(object obj)
        {
            _sentFrames = new BitArray[FirstBuffer.FRAMESCOUNT];
            GenerateSomeFrames();

            _mainSemaphore.WaitOne();
            _postFrames = (PostDataToSecondBufferWt)obj;
            _postFrames(_sentFrames);

            ConsoleHelper.WriteToConsole("станция 1", "отправил кадры буферу 2");
            _signalToSecondBuffer.Release();

            _signalFromSecondBuffer.WaitOne();
            ConsoleHelper.WriteToConsoleArray("станция 1 полученная квитанция", _receivedReceipt);
            _mainSemaphore.Release();
        }

        

        public void SendReceiptToFirstBuffer(Object obj)
        {
            Stopwatch framesReceptionWatch = new Stopwatch();
            _postReceipt = (PostReceiptToFirstBufferWt)obj;
            _sentReceipt = new BitArray(1);

            framesReceptionWatch.Start();
            _signalFromFirstBuffer.WaitOne();
            framesReceptionWatch.Stop();
            TimeSpan framesReceptionTime = framesReceptionWatch.Elapsed;

            if (framesReceptionTime.Milliseconds > 20)
            {
                _sentReceipt[0] = false;
                ConsoleHelper.WriteToConsole("станция 1", "Данные не получены");
            }

            else
            {
                _sentReceipt[0] = true;
                for (int i = 0; i < _receivedFrames.Length; i++)
                {
                    if (ValidData(_receivedFrames[i]) == true)
                    {
                        ConsoleHelper.WriteToConsoleArray("станция 1 полученный кадр №" + i, _receivedFrames[i]);
                    }
                    else
                    {
                        ConsoleHelper.WriteToConsole("станция 1 полученный кадр №" + i, "данные повреждены");
                    }
                }
            }
                      
            _postReceipt(_sentReceipt);
            ConsoleHelper.WriteToConsole("станция 1", "отправил квитанцию буферу 1");
            _signalToFirstBuffer.Release();
        }

        public void ReceiveFrames(BitArray[] frames)
        {
            _receivedFrames = frames;
        }

        public void ReceiveReceipt(BitArray receipt)
        {
            _receivedReceipt = receipt;
        }


        public void GenerateSomeFrames()
        {
            BitArray someData = GenerateSomeData(100);
            BitArray firstFrameData = new BitArray(40);
            BitArray secondFrameData = new BitArray(40);
            BitArray thirdFrameData = new BitArray(20);
            for (int i = 0; i < firstFrameData.Length; i++)
            {
                firstFrameData[i] = someData[i];
            }

            for (int i = 0; i < secondFrameData.Length; i++)
            {
                secondFrameData[i] = someData[i + firstFrameData.Length];
            }

            for (int i = 0; i < thirdFrameData.Length; i++)
            {
                thirdFrameData[i] = someData[i + firstFrameData.Length + secondFrameData.Length];
            }

            BitArray frame1 = Frame.FillFrame(firstFrameData);
            BitArray frame2 = Frame.FillFrame(secondFrameData);
            BitArray frame3 = Frame.FillFrame(thirdFrameData);

            _sentFrames[0] = frame1;
            _sentFrames[1] = frame2;
            _sentFrames[2] = frame3;
        }

        public BitArray GenerateSomeData(int bitsCount)
        {
            BitArray someData = new BitArray(bitsCount);
            for (int i = 0; i < bitsCount; i++)
            {
                if (i % 3 == 0 || i % 4 == 0)
                {
                    someData[i] = true;
                }
                else
                {
                    someData[i] = false;
                }
            }

            return someData;
        }


        public static bool ValidData(BitArray frame)
        {
            BitArray receivedData = GetFrameData(frame);
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
            BitArray parity = new BitArray(Frame.PARITYBLOCKBITSCOUNT);
            for (int i = frame.Length - 8; i < frame.Length; i++)
            {
                parity[i - (frame.Length - 8)] = frame[i];
            }

            return parity;
        }

        public static BitArray CalculateFrameParity(BitArray receivedData)
        {
            BitArray calculatedParity = new BitArray(Frame.PARITYBLOCKBITSCOUNT);

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

        public static BitArray GetFrameData(BitArray _frame)
        {
            int dataSize = _frame.Length - Frame.DATASIZEBLOCKBITSCOUNT - Frame.PARITYBLOCKBITSCOUNT;
            BitArray dataReceived = new BitArray(dataSize);

            for (int i = 0; i < dataSize; i++)
            {
                dataReceived[i] = _frame[i];
            }

            return dataReceived;
        }

    }
}
