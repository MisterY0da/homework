using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class SecondStation
    {
        private Semaphore _mainSemaphore;
        private Semaphore _signalToSecondBuffer;
        private Semaphore _signalFromSecondBuffer;
        private Semaphore _signalToFirstBuffer;
        private Semaphore _signalFromFirstBuffer;

        private PostDataToFirstBufferWt _postFrame;
        private BitArray[] _sentFrames;
        private BitArray _receivedReceipt;

        private PostReceiptToSecondBufferWt _postReceipt;
        private BitArray _sentReceipt;
        private BitArray[] _receivedFrames;

        public SecondStation(ref Semaphore signalFromSecondBuffer, ref Semaphore signalToSecondBuffer, 
            ref Semaphore signalToFirstBuffer, ref Semaphore signalFromFirstBuffer, ref Semaphore mainSemaphore)
        {           
            _signalFromSecondBuffer = signalFromSecondBuffer;
            _signalToSecondBuffer = signalToSecondBuffer;
            _signalToFirstBuffer = signalToFirstBuffer;
            _signalFromFirstBuffer = signalFromFirstBuffer;
            _mainSemaphore = mainSemaphore;
        }

        public void SendFramesToFirst(object obj)
        {
            _sentFrames = new BitArray[SecondBuffer.FRAMESCOUNT];
            GenerateSomeFrames();

            _mainSemaphore.WaitOne();
            _postFrame = (PostDataToFirstBufferWt)obj;
            

            _postFrame(_sentFrames);
            ConsoleHelper.WriteToConsole("станция 2", "отправил кадры буферу 1");
            _signalToFirstBuffer.Release();

            _signalFromFirstBuffer.WaitOne();
            ConsoleHelper.WriteToConsoleArray("станция 2 полученная квитанция", _receivedReceipt);
            _mainSemaphore.Release();

        }

        public void SendReceiptToSecondBuffer(Object obj)
        {
            _postReceipt = (PostReceiptToSecondBufferWt)obj;
            _sentReceipt = new BitArray(1);

            _signalFromSecondBuffer.WaitOne();
            for (int i = 0; i < _receivedFrames.Length; i++)
            {
                ConsoleHelper.WriteToConsoleArray("станция 2 полученный кадр №" + i, _receivedFrames[i]);
            }
            _sentReceipt[0] = true;
            _postReceipt(_sentReceipt);
            ConsoleHelper.WriteToConsole("станция 2", "отправил квитанцию буферу 2");
            _signalToSecondBuffer.Release();
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
            BitArray firstFrameData = new BitArray(20);
            BitArray secondFrameData = new BitArray(20);
            BitArray thirdFrameData = new BitArray(30);
            BitArray forthFrameData = new BitArray(30);

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

            for (int i = 0; i < forthFrameData.Length; i++)
            {
                forthFrameData[i] = someData[i + firstFrameData.Length + secondFrameData.Length + thirdFrameData.Length];
            }

            BitArray frame1 = Frame.FillFrame(firstFrameData);
            BitArray frame2 = Frame.FillFrame(secondFrameData);
            BitArray frame3 = Frame.FillFrame(thirdFrameData);
            BitArray frame4 = Frame.FillFrame(forthFrameData);

            _sentFrames[0] = frame1;
            _sentFrames[1] = frame2;
            _sentFrames[2] = frame3;
            _sentFrames[3] = frame4;
        }

        public BitArray GenerateSomeData(int bitsCount)
        {
            BitArray someData = new BitArray(bitsCount);
            for (int i = 0; i < 100; i++)
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

            for (int i = frame.Length - 1; i > frame.Length - 8; i--)
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
            for (int i = frame.Length - 1; i > frame.Length - 8; i--)
            {
                parity[i] = frame[i];
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
