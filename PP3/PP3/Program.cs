using System;
using System.Diagnostics;
using System.Threading;

namespace PP3
{
    class Program
    {
        static void Main(string[] args)
        {
            Serial();
            MultiThreaded();
        }


        public static void Serial()
        {
            double start = 0.0;

            double end = 9.0;

            double epsilon = 0.001;

            double totalValue = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            CalculateIntegralRecursively(start, end, epsilon, ref totalValue);
            stopwatch.Stop();

            Console.WriteLine("serial: " + Math.Round(totalValue, 2));
            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Serial time: " + ts.Milliseconds + "ms\n");
        }

        public static void MultiThreaded()
        {
            double start = 0.0;

            double end = 9.0;

            double epsilon = 0.001;

            double part1 = 0;
            double part2 = 0;
            double part3 = 0;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Thread t1 = new Thread(state => CalculateIntegralRecursively(start, 3.00, epsilon, ref part1));
            t1.Start();
            Thread t2 = new Thread(state => CalculateIntegralRecursively(start + 3.00, 6.00, epsilon, ref part2));
            t2.Start();
            Thread t3 = new Thread(state => CalculateIntegralRecursively(start + 6.00, end, epsilon, ref part3));
            t3.Start();            
            
            stopwatch.Stop();

            Thread.Sleep(1);

            double totalValue = part1 + part2 + part3;
            

            Console.WriteLine("multi: " + Math.Round(totalValue, 2));

            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Multithreaded time: " + ts.Milliseconds + "ms\n");
        }


        static void CalculateIntegralRecursively(double start, double end, double epsilon, ref double sum)
        {
            if (start >= end)
            {
                sum += Parabola(end) * epsilon;
            }

            else
            {
                sum += Parabola(start + epsilon) * (epsilon);
                CalculateIntegralRecursively(start + epsilon, end, epsilon, ref sum);
            }
        }

        static double Parabola(double x)
        {
            return x * x;
        }
    }
}
