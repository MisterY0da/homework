using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class SecondBuffer
    {
        const int FRAMESCOUNT = 4;
        public BitArray[] framesArray = new BitArray[FRAMESCOUNT];

        private Semaphore _signalFromFirst;
        private Semaphore _signalToSecond;
        private Semaphore _signalFromSecond;
        private Semaphore _signalToFirst;

        private BitArray _receivedFrame;
        private BitArray _sentFrame;
        private BitArray _receivedReceipt;
        private BitArray _sentReceipt;

        private PostDataToSecondWt _postFrame;
        private PostReceiptToFirstWt _postReceipt;


        public SecondBuffer(ref Semaphore signalFromFirst, ref Semaphore signalToSecond, ref Semaphore signalFromSecond, ref Semaphore signalToFirst)
        {
            _signalFromFirst = signalFromFirst;
            _signalToSecond = signalToSecond;
            _signalFromSecond = signalFromSecond;
            _signalToFirst = signalToFirst;
        }


        public void SendReceiptToFirst(object obj)
        {
            _postReceipt = (PostReceiptToFirstWt)obj;
            _sentReceipt = new BitArray(1);

            _signalFromSecond.WaitOne();            
            _sentReceipt[0] = true;
            _postReceipt(_sentReceipt);
            ConsoleHelper.WriteToConsole("буфер 2", "отправил квитанцию станции 1");
            _signalToFirst.Release();
        }

        public void SendFrameToSecond(object obj)
        {
            _postFrame = (PostDataToSecondWt)obj;

            _signalFromFirst.WaitOne();
            _sentFrame = _receivedFrame;
            _postFrame(_sentFrame);
            ConsoleHelper.WriteToConsole("буфер 2", "отправил кадр станции 2");
            _signalToSecond.Release();
        }

        public void ReceiveFrame(BitArray frame)
        {
            _receivedFrame = frame;
        }

        public void ReceiveReceipt(BitArray receipt)
        {
            _receivedReceipt = receipt;
        }

    }
}
