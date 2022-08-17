using System;
using System.Threading;

namespace CleverenceTask2
{
    class Program
    {
        private static void MyEventHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Handler started");
            Thread.Sleep(2000);
            Console.WriteLine("Handler ended");
        }
        static void Main(string[] args)
        {
            EventHandler h = new EventHandler(MyEventHandler);
            AsyncCaller ac = new AsyncCaller(h);
            if (ac.Invoke(5000, null, EventArgs.Empty))
            {
                Console.WriteLine("Completed successfully");
            }
            else
            {
                Console.WriteLine("Time is out!");
            }
        }
    }
}
