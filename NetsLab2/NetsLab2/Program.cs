using System;
using System.Collections;
using System.Threading;

namespace NetsLab2
{
    public delegate void PostDataToSecondBufWt(BitArray[] frames);
    public delegate void PostDataFromSecondBufWt(BitArray[] frames);
    public delegate void SecondStPostToFirstStWt(BitArray message);


    public delegate void PostDataToFirstBufWt(BitArray[] frames);
    public delegate void PostDataFromFirstBufWt(BitArray[] frames);
    public delegate void FirstStPostToSecondStWt(BitArray message);

    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteToConsole("Главный поток", "");


            Semaphore mainSemaphore = new Semaphore(1, 1);
            Semaphore firstStToSecondBuf = new Semaphore(0, 1);
            Semaphore secondBufToSecondSt = new Semaphore(0, 1);
            Semaphore secondStToFirstSt = new Semaphore(0, 1);

            Semaphore secondStToFirstBuf = new Semaphore(0, 1);
            Semaphore firstBufToFirstSt = new Semaphore(0, 1);
            Semaphore firstStToSecondSt = new Semaphore(0, 1);


            FirstStation firstStation = new FirstStation(ref firstStToSecondBuf, ref firstBufToFirstSt, ref secondStToFirstSt, ref firstStToSecondSt, ref mainSemaphore);
            FirstBuffer firstBuffer = new FirstBuffer(ref secondStToFirstBuf, ref firstBufToFirstSt);            
            SecondStation secondStation = new SecondStation(ref secondBufToSecondSt, ref secondStToFirstBuf, ref firstStToSecondSt, ref secondStToFirstSt, ref mainSemaphore);
            SecondBuffer secondBuffer = new SecondBuffer(ref firstStToSecondBuf, ref secondBufToSecondSt);


            Thread firstThread = new Thread(new ParameterizedThreadStart(firstStation.SendFramesToSecondBuf));
            Thread secondThread = new Thread(new ParameterizedThreadStart(secondBuffer.SendFramesToSecondSt));
            Thread thirdThread = new Thread(new ParameterizedThreadStart(secondStation.SendReceiptToFirstSt));
            Thread forthThread = new Thread(new ParameterizedThreadStart(secondStation.SendFramesToFirstBuf));
            Thread fifthThread = new Thread(new ParameterizedThreadStart(firstBuffer.SendFramesToFirstSt));
            Thread sixthThread = new Thread(new ParameterizedThreadStart(firstStation.SendReceiptToSecondSt));


            PostDataToSecondBufWt postDataToSecondBufferWt = new PostDataToSecondBufWt(secondBuffer.ReceiveFrames);
            PostDataFromSecondBufWt postDataFromSecondBufWt = new PostDataFromSecondBufWt(secondStation.ReceiveFrames);
            SecondStPostToFirstStWt postReceiptToFirstStWt = new SecondStPostToFirstStWt(firstStation.ReceiveReceipt);


            PostDataToFirstBufWt postDataToFirstBufferWt = new PostDataToFirstBufWt(firstBuffer.ReceiveFrames);
            PostDataFromFirstBufWt postDataFromFirstBufWt = new PostDataFromFirstBufWt(firstStation.ReceiveFrames);
            FirstStPostToSecondStWt postReceiptToSecondtStWt = new FirstStPostToSecondStWt(secondStation.ReceiveReceipt);


            firstThread.Start(postDataToSecondBufferWt);
            secondThread.Start(postDataFromSecondBufWt);
            thirdThread.Start(postReceiptToFirstStWt);
            forthThread.Start(postDataToFirstBufferWt);
            fifthThread.Start(postDataFromFirstBufWt);
            sixthThread.Start(postReceiptToSecondtStWt);

            Console.ReadLine();
        }
    }   
}
