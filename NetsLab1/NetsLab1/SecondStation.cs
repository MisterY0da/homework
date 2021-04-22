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
        private BitArray _sentFrame;
        private BitArray _receivedReceipt;

        private PostReceiptToSecondBufferWt _postReceipt;
        private BitArray _sentReceipt;
        private BitArray _receivedFrame;

        public SecondStation(ref Semaphore signalFromSecondBuffer, ref Semaphore signalToSecondBuffer, 
            ref Semaphore signalToFirstBuffer, ref Semaphore signalFromFirstBuffer, ref Semaphore mainSemaphore)
        {           
            _signalFromSecondBuffer = signalFromSecondBuffer;
            _signalToSecondBuffer = signalToSecondBuffer;
            _signalToFirstBuffer = signalToFirstBuffer;
            _signalFromFirstBuffer = signalFromFirstBuffer;
            _mainSemaphore = mainSemaphore;
        }

        public void SendFrameToFirst(object obj)
        {
            _mainSemaphore.WaitOne();
            _postFrame = (PostDataToFirstBufferWt)obj;
            _sentFrame = new BitArray(16);
            for (int i = 0; i < 16; i++)
            {
                if (i % 2 == 0)
                    _sentFrame[i] = false;
                else
                    _sentFrame[i] = true;
            }

            _postFrame(_sentFrame);
            ConsoleHelper.WriteToConsole("станция 2", "отправил кадр буферу 1");
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
            ConsoleHelper.WriteToConsoleArray("станция 2 принятый кадр", _receivedFrame);
            _sentReceipt[0] = true;
            _postReceipt(_sentReceipt);
            ConsoleHelper.WriteToConsole("станция 2", "отправил квитанцию буферу 2");
            _signalToSecondBuffer.Release();
        }

        public void ReceiveFrame(BitArray frame)
        {
            _receivedFrame = frame;
        }

        public void ReceiveReceipt(BitArray receipt)
        {
            _receivedReceipt = receipt;
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
