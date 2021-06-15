using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace PP3
{
    class Program
    {
        static Semaphore sem = new Semaphore(1, 1);
        static double eps = 0.0000001;

        static double iterationsToCreateThreads = (1 / eps) / 2;

        static double resultMult;
        static double resultSerial;

        static int allIterations = 0;

        static int threadIndex = 0;

        const int threadsCount = 2;

        static double[] sumsArr = new double[threadsCount];

        static void Main(string[] args)
        {
            for(int i = 0; i < sumsArr.Length; i++)
            {
                sumsArr[i] = 0;
            }

            GetSerialInfo();

            GetMultInfo();

            
            Console.WriteLine("threads count: " + threadIndex);

            for(int i = 0; i < sumsArr.Length; i++)
            {
                Console.WriteLine("thread #" + i + " local sum: " + sumsArr[i]);
            }

            Console.ReadLine();
        }


        static void GetSerialInfo()
        {
            Stopwatch watchSerial = new Stopwatch();
            watchSerial.Start();
            SerialIntegration(2, 3, ref resultSerial);
            watchSerial.Stop();

            Console.WriteLine("serial time: " + watchSerial.ElapsedTicks + " ticks");
            Console.WriteLine("serial result: " + resultSerial + "\n");
        }

        static void GetMultInfo()
        {
            Stopwatch watchMult = new Stopwatch();
            watchMult.Start();
            MultIntegration(2, 3, ref sumsArr[0]);
            watchMult.Stop();
            Console.WriteLine("Multithreaded time: " + watchMult.ElapsedTicks + " ticks");



            Thread.Sleep(1000);

            for(int i = 0; i < sumsArr.Length; i++)
            {
                resultMult += sumsArr[i];
            }

            Console.WriteLine("multithreaded result: " + resultMult + "\n");
        }

        static void MultIntegration(double startPoint, double endPoint, ref double res)
        {
            //allIterations++;

            double left = startPoint;
            double right = endPoint;
            double length = right - left;
            double middle = left + length / 2;

            

            if (length < eps)
            {
                res += Parabola(middle) * length;
            }

            else if (threadIndex < threadsCount)
            {
                threadIndex += 2;
                Thread leftThread = new Thread(state => MultIntegration(left, middle, ref sumsArr[threadIndex - 2]));
                leftThread.Start();

                Thread rightThread = new Thread(state => MultIntegration(middle, right, ref sumsArr[threadIndex - 1]));
                rightThread.Start();
            }

            else
            {
                MultIntegration(left, middle, ref res);
                MultIntegration(middle, right, ref res);
            }

            
        }

        static void SerialIntegration(double startPoint, double endPoint, ref double res)
        {
            
            double left = startPoint;
            double right = endPoint;
            double length = right - left;
            double middle = left + length / 2;

            

            if (length < eps)
            {
                res += Parabola(middle) * length;
            }

            else
            {
                SerialIntegration(left, middle, ref res);

                SerialIntegration(middle, right, ref res);
            }
        }


        static double Parabola(double x)
        {
            return x * x;
        }
    }
}
