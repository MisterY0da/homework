using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab3
{
    public class FirstBuffer
    {
        public BitArray[] framesArray;

        private Semaphore _signalFromSecondSt;
        private Semaphore _signalToFirstSt;
        

        private BitArray[] _receivedFrames;
        private BitArray[] _sentFrames;

        private PostDataFromFirstBufWt _postFrames;


        public FirstBuffer(ref Semaphore secondStToFirstBuf, ref Semaphore firstBufToFirstSt)
        {
            _signalFromSecondSt = secondStToFirstBuf;
            _signalToFirstSt = firstBufToFirstSt;         
        }

        public void SendFramesToFirstSt(object obj)
        {
            _postFrames = (PostDataFromFirstBufWt)obj;

            _signalFromSecondSt.WaitOne();
            _sentFrames = _receivedFrames;
            _postFrames(_sentFrames);
            ConsoleHelper.WriteToConsole("буфер 1", "отправил кадры станции 1");
            _signalToFirstSt.Release();


            _signalFromSecondSt.WaitOne();
            _sentFrames = _receivedFrames;
            _postFrames(_sentFrames);
            ConsoleHelper.WriteToConsole("буфер 1", "отправил кадры станции 1");
            _signalToFirstSt.Release();
        }

        public void ReceiveFrames(BitArray[] frames)
        {
            _receivedFrames = frames;
        }
    }
}
