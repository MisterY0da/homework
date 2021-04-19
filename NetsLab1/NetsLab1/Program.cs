using System;
using System.Collections;
using System.Threading;

namespace NetsLab1
{
    public delegate void PostToFirstWT(BitArray message);
    public delegate void PostToSecondWT(BitArray message);   
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteToConsole("Главный поток", "");
            Semaphore semaphore = new Semaphore(1, 1);
            FirstThread firstThread = new FirstThread(ref semaphore);
            SecondThread secondThread = new SecondThread(ref semaphore);
            Thread threadFirst = new Thread(new ParameterizedThreadStart(firstThread.FirstThreadMain));
            Thread threadSecond = new Thread(new ParameterizedThreadStart(secondThread.SecondThreadMain));
            PostToFirstWT postToFirstWt = new PostToFirstWT(firstThread.ReceiveData);
            PostToSecondWT postToSecondWt = new PostToSecondWT(secondThread.ReceiveData);
            threadFirst.Start(postToSecondWt);
            threadSecond.Start(postToFirstWt);
            Console.ReadLine();

        }
    }   
}
