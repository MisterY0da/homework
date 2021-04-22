using System;
using System.Collections;
using System.Threading;

namespace NetsLab1
{
    public delegate void PostDataToSecondBufferWt(BitArray frame);
    public delegate void PostDataToSecondWt(BitArray frame);
    public delegate void PostReceiptToSecondBufferWt(BitArray receipt);
    public delegate void PostReceiptToFirstWt(BitArray receipt);


    public delegate void PostDataToFirstBufferWt(BitArray frame);
    public delegate void PostDataToFirstWt(BitArray frame);
    public delegate void PostReceiptToFirstBufferWt(BitArray receipt);
    public delegate void PostReceiptToSecondWt(BitArray receipt);

    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteToConsole("Главный поток", "");

            Semaphore dataSignalToSecondBuffer = new Semaphore(0, 1);
            Semaphore dataSignalToSecond = new Semaphore(0, 1);
            Semaphore receiptSignalToSecondBuffer = new Semaphore(0, 1);
            Semaphore receiptSignalToFirst = new Semaphore(0, 1);

            Semaphore dataSignalToFirstBuffer = new Semaphore(0, 1);
            Semaphore dataSignalToFirst = new Semaphore(0, 1);
            Semaphore receiptSignalToFirstBuffer = new Semaphore(0, 1);
            Semaphore receiptSignalToSecond = new Semaphore(0, 1);


            FirstStation firstStation = new FirstStation(ref dataSignalToSecondBuffer, ref receiptSignalToFirst, ref receiptSignalToFirstBuffer, ref dataSignalToFirst);
            FirstBuffer firstBuffer = new FirstBuffer(ref dataSignalToFirstBuffer, ref dataSignalToFirst, ref receiptSignalToFirstBuffer, ref receiptSignalToSecond);            
            SecondStation secondStation = new SecondStation(ref dataSignalToSecond, ref receiptSignalToSecondBuffer, ref dataSignalToFirstBuffer, ref receiptSignalToSecond);
            SecondBuffer secondBuffer = new SecondBuffer(ref dataSignalToSecondBuffer, ref dataSignalToSecond, ref receiptSignalToSecondBuffer, ref receiptSignalToFirst);


            Thread firstThread = new Thread(new ParameterizedThreadStart(firstStation.SendFrameToSecond));
            Thread secondThread = new Thread(new ParameterizedThreadStart(secondBuffer.SendFrameToSecond));
            Thread thirdThread = new Thread(new ParameterizedThreadStart(secondStation.SendReceiptToSecondBuffer));
            Thread forthThread = new Thread(new ParameterizedThreadStart(secondBuffer.SendReceiptToFirst));

            Thread fifthThread = new Thread(new ParameterizedThreadStart(secondStation.SendFrameToFirst));
            Thread sixthThread = new Thread(new ParameterizedThreadStart(firstBuffer.SendFrameToFirst));
            Thread seventhThread = new Thread(new ParameterizedThreadStart(firstStation.SendReceiptToFirstBuffer));
            Thread eighthThread = new Thread(new ParameterizedThreadStart(firstBuffer.SendReceiptToSecond));


            PostDataToSecondBufferWt postDataToSecondBufferWt = new PostDataToSecondBufferWt(secondBuffer.ReceiveFrame);
            PostDataToSecondWt postDataToSecondWt = new PostDataToSecondWt(secondStation.ReceiveFrame);
            PostReceiptToSecondBufferWt postReceiptToSecondBufferWt = new PostReceiptToSecondBufferWt(secondBuffer.ReceiveReceipt);
            PostReceiptToFirstWt postReceiptToFirstWt = new PostReceiptToFirstWt(firstStation.ReceiveReceipt);


            PostDataToFirstBufferWt postDataToFirstBufferWt = new PostDataToFirstBufferWt(firstBuffer.ReceiveFrame);
            PostDataToFirstWt postDataToFirstWt = new PostDataToFirstWt(firstStation.ReceiveFrame);
            PostReceiptToFirstBufferWt postReceiptToFirstBufferWt = new PostReceiptToFirstBufferWt(firstBuffer.ReceiveReceipt);
            PostReceiptToSecondWt postReceiptToSecondtWt = new PostReceiptToSecondWt(secondStation.ReceiveReceipt);


            /*firstThread.Start(postDataToSecondBufferWt);
            secondThread.Start(postDataToSecondWt);
            thirdThread.Start(postReceiptToSecondBufferWt);
            forthThread.Start(postReceiptToFirstWt);*/


            fifthThread.Start(postDataToFirstBufferWt);
            sixthThread.Start(postDataToFirstWt);
            seventhThread.Start(postReceiptToFirstBufferWt);
            eighthThread.Start(postReceiptToSecondtWt);


            Console.ReadLine();
        }
    }   
}
