using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class FirstBuffer
    {
        /*const int FRAMESCOUNT = 4;
        public BitArray[] framesArray = new BitArray[FRAMESCOUNT];*/

        private Semaphore _signalFromSecond;
        private Semaphore _signalToFirst;
        private Semaphore _signalFromFirst;
        private Semaphore _signalToSecond;
        

        private BitArray _receivedFrame;
        private BitArray _sentFrame;
        private BitArray _receivedReceipt;
        private BitArray _sentReceipt;

        private PostReceiptToSecondWt _postReceipt;
        private PostDataToFirstWt _postFrame;


        public FirstBuffer(ref Semaphore signalFromSecond, ref Semaphore signalToFirst, ref Semaphore signalFromFirst, ref Semaphore signalToSecond)
        {
            _signalFromSecond = signalFromSecond;
            _signalToFirst = signalToFirst;
            _signalFromFirst = signalFromFirst;
            _signalToSecond = signalToSecond;           
        }


        public void SendReceiptToSecond(object obj)
        {
            _postReceipt = (PostReceiptToSecondWt)obj;
            _sentReceipt = new BitArray(1);

            _signalFromFirst.WaitOne();
            _sentReceipt[0] = true;
            _postReceipt(_sentReceipt);
            ConsoleHelper.WriteToConsole("буфер 1", "отправил квитанцию станции 2");
            _signalToSecond.Release();
        }

        public void SendFrameToFirst(object obj)
        {
            _postFrame = (PostDataToFirstWt)obj;

            _signalFromSecond.WaitOne();
            _sentFrame = _receivedFrame;
            _postFrame(_sentFrame);
            ConsoleHelper.WriteToConsole("буфер 1", "отправил кадр станции 1");
            _signalToFirst.Release();
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
