using System;
using System.Threading;

namespace CleverenceTask1
{
    class Program
    {
        static void Main(string[] args)
        {
            int clientsCount = 30;
            var rnd = new Random();

            Thread[] clientsThreads = new Thread[clientsCount];

            for(int i = 0; i < clientsCount; i++)
            {
                if(i % 5 == 0)
                {
                    clientsThreads[i] = new Thread(state => Server.AddToCount(rnd.Next(1, 10)));
                }
                else
                {
                    clientsThreads[i] = new Thread(state => Server.GetCount());
                }
            }

            foreach(var th in clientsThreads)
            {
                th.Start();
                Thread.Sleep(10);
            }
           
            Thread.Sleep(2000);
        }
    }
}
