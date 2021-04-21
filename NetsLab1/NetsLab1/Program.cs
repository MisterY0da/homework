using System;
using System.Collections;
using System.Threading;

namespace NetsLab1
{
    public delegate void PostToFirstWT(BitArray message);
    public delegate void PostToSecondWT(BitArray message);

    /*public delegate void PostDataToFirstBufferWT(BitArray message);
    public delegate void PostDataToSecondBufferWT(BitArray message);
    public delegate void PostDataToFirstWT(BitArray message);
    public delegate void PostDataToSecondWT(BitArray message);*/

    public delegate void PostReceiptToFirstWT(BitArray message);
    public delegate void PostReceiptToSecondWT(BitArray message);
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteToConsole("Главный поток", "");
            Semaphore semaphore = new Semaphore(1, 1);
            Semaphore firstReceiptSem = new Semaphore(0, 1);
            Semaphore secondReceiptSem = new Semaphore(0, 1);

            FirstStation firstStation = new FirstStation(ref semaphore, ref firstReceiptSem, ref secondReceiptSem);
            SecondStation secondStation = new SecondStation(ref semaphore, ref secondReceiptSem, ref firstReceiptSem);

            Thread firstThread = new Thread(new ParameterizedThreadStart(firstStation.FirstStationMessage));
            Thread secondThread = new Thread(new ParameterizedThreadStart(secondStation.SecondStationMessage));
            Thread thirdThread = new Thread(new ParameterizedThreadStart(firstStation.FirstStationReceipt));
            Thread forthThread = new Thread(new ParameterizedThreadStart(secondStation.SecondStationReceipt));


            PostToFirstWT postToFirstWt = new PostToFirstWT(firstStation.ReceiveData);
            PostToSecondWT postToSecondWt = new PostToSecondWT(secondStation.ReceiveData);

            PostReceiptToFirstWT postReceiptToFirstWt = new PostReceiptToFirstWT(firstStation.ReceiveReceipt);
            PostReceiptToSecondWT postReceiptToSecondWt = new PostReceiptToSecondWT(secondStation.ReceiveReceipt);

            firstThread.Start(postToSecondWt);

            secondThread.Start(postToFirstWt);

            thirdThread.Start(postReceiptToSecondWt);
            
            forthThread.Start(postReceiptToFirstWt);

            Console.ReadLine();
        }
    }   
}
