using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab2
{
    public class SecondBuffer
    {
        public BitArray[] framesArray;

        private Semaphore _signalFromFirstSt;
        private Semaphore _signalToSecondSt;

        private BitArray[] _receivedFrames;
        private BitArray[] _sentFrames;

        private PostDataFromSecondBufWt _postFrames;


        public SecondBuffer(ref Semaphore firstStToSecondBuf, ref Semaphore secondBufToSecondSt)
        {
            _signalFromFirstSt = firstStToSecondBuf;
            _signalToSecondSt = secondBufToSecondSt;
        }

        public void SendFramesToSecondSt(object obj)
        {
            _postFrames = (PostDataFromSecondBufWt)obj;

            _signalFromFirstSt.WaitOne();
            _sentFrames = _receivedFrames;
            _postFrames(_sentFrames);
            ConsoleHelper.WriteToConsole("буфер 2", "отправил кадры станции 2");
            _signalToSecondSt.Release();


            _signalFromFirstSt.WaitOne();
            _sentFrames = _receivedFrames;
            _postFrames(_sentFrames);
            ConsoleHelper.WriteToConsole("буфер 2", "отправил кадры станции 2");
            _signalToSecondSt.Release();
        }

        public void ReceiveFrames(BitArray[] frames)
        {
            _receivedFrames = frames;
        }
    }
}
