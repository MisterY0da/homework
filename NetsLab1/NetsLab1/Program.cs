using System;
using System.Collections;
using System.Threading;

namespace NetsLab1
{
    public delegate void PostToFirstWT(BitArray message);
    public delegate void PostToSecondWT(BitArray message);

    /*public delegate void PostDataToFirstBuffer(BitArray message);
    public delegate void PostDataToSecondBuffer(BitArray message);
    public delegate void PostDataToFirst(BitArray message);
    public delegate void PostDataToSecond(BitArray message);

    public delegate void PostReceiptToFirstBuffer(BitArray message);
    public delegate void PostReceiptToSecondBuffer(BitArray message);*/

    public delegate void PostReceiptToFirst(BitArray message);
    public delegate void PostReceiptToSecond(BitArray message);
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteToConsole("Главный поток", "");
            Semaphore semaphore = new Semaphore(1, 1);

            FirstStation firstStation = new FirstStation(ref semaphore);
            SecondStation secondStation = new SecondStation(ref semaphore);

            Thread firstThread = new Thread(new ParameterizedThreadStart(firstStation.FirstThreadMain));
            Thread secondThread = new Thread(new ParameterizedThreadStart(secondStation.SecondThreadMain));
            Thread thirdThread = new Thread(new ParameterizedThreadStart(firstStation.FirstThreadMain));
            Thread forthThread = new Thread(new ParameterizedThreadStart(secondStation.SecondThreadMain));


            PostToFirstWT postToFirstWt = new PostToFirstWT(firstStation.ReceiveData);
            PostToSecondWT postToSecondWt = new PostToSecondWT(secondStation.ReceiveData);

            PostReceiptToFirst postReceiptToFirst = new PostReceiptToFirst(firstStation.ReceiveReceipt);
            PostReceiptToSecond postReceiptToSecond = new PostReceiptToSecond(secondStation.ReceiveReceipt);

            firstThread.Start(postToSecondWt);

            secondThread.Start(postToFirstWt);

            thirdThread.Start(postReceiptToSecond);

            
            forthThread.Start(postReceiptToFirst);

            Console.ReadLine();

        }
    }   
}
