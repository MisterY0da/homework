using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CleverenceTask1
{
    public static class Server
    {
        static int count;
        static object lockObject = new object();

        public static void GetCount()
        {
            Console.WriteLine("Читатель прочитал count = " + count + "\t id потока: " + Thread.CurrentThread.ManagedThreadId);
        }

        public static void AddToCount(int value)
        {
            lock (lockObject)
            {
                count += value;
            }

            Console.WriteLine("Писатель прибавил " + value + "\t\t id потока: " + Thread.CurrentThread.ManagedThreadId + ", count = "  + count);
        }
    }
}
