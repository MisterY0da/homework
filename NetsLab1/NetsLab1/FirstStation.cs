using System;
using System.Collections;
using System.Collections.Generic;
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

        private PostDataToSecondBufferWt _postFrame;
        private BitArray _sentFrame;
        private BitArray _receivedReceipt;


        private PostReceiptToFirstBufferWt _postReceipt;
        private BitArray _sentReceipt;
        private BitArray _receivedFrame;


        public FirstStation(ref Semaphore signalToSecondBuffer, ref Semaphore signalFromSecondBuffer, 
            ref Semaphore signalToFirstBuffer, ref Semaphore signalFromFirstBuffer, ref Semaphore mainSemaphore)
        {
            _signalToSecondBuffer = signalToSecondBuffer;
            _signalFromSecondBuffer = signalFromSecondBuffer;
            _signalToFirstBuffer = signalToFirstBuffer;
            _signalFromFirstBuffer = signalFromFirstBuffer;
            _mainSemaphore = mainSemaphore;
        }



        public void SendFrameToSecond(object obj)
        {
            _mainSemaphore.WaitOne();
            _postFrame = (PostDataToSecondBufferWt)obj;
            _sentFrame = new BitArray(16);
            for (int i = 0; i < 16; i++)
            {
                if (i % 2 == 0)
                    _sentFrame[i] = true;
                else
                    _sentFrame[i] = false;
            }

            _postFrame(_sentFrame);

            ConsoleHelper.WriteToConsole("станция 1", "отправил кадр буферу 2");
            _signalToSecondBuffer.Release();

            _signalFromSecondBuffer.WaitOne();
            ConsoleHelper.WriteToConsoleArray("станция 1 полученная квитанция", _receivedReceipt);
            _mainSemaphore.Release();
        }

        public void SendReceiptToFirstBuffer(Object obj)
        {
            _postReceipt = (PostReceiptToFirstBufferWt)obj;
            _sentReceipt = new BitArray(1);

            _signalFromFirstBuffer.WaitOne();
            ConsoleHelper.WriteToConsoleArray("станция 1 принятый кадр", _receivedFrame);
            _sentReceipt[0] = true;
            _postReceipt(_sentReceipt);
            ConsoleHelper.WriteToConsole("станция 1", "отправил квитанцию буферу 1");
            _signalToFirstBuffer.Release();
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
