using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class FirstBuffer
    {
        public const int FRAMESCOUNT = 3;
        public BitArray[] framesArray = new BitArray[FRAMESCOUNT];

        private Semaphore _signalFromSecond;
        private Semaphore _signalToFirst;
        private Semaphore _signalFromFirst;
        private Semaphore _signalToSecond;
        

        private BitArray[] _receivedFrames;
        private BitArray[] _sentFrames;
        private BitArray _receivedReceipt;
        private BitArray _sentReceipt;

        private PostReceiptToSecondWt _postReceipt;
        private PostDataToFirstWt _postFrames;


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

        public void SendFramesToFirst(object obj)
        {
            _postFrames = (PostDataToFirstWt)obj;

            _signalFromSecond.WaitOne();
            _sentFrames = _receivedFrames;
            _postFrames(_sentFrames);
            ConsoleHelper.WriteToConsole("буфер 1", "отправил кадры станции 1");
            _signalToFirst.Release();
        }

        public void ReceiveFrames(BitArray[] frames)
        {
            _receivedFrames = frames;
        }

        public void ReceiveReceipt(BitArray receipt)
        {
            _receivedReceipt = receipt;
        }

    }
}
