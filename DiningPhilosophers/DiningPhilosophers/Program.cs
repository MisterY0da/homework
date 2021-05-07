using System;
using System.Threading;

namespace DiningPhilosophers
{
    public class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                bool[] forksAvailable = new bool[5] { true, true, true, true, true };

                for (int i = 0; i < 5; i++)
                {
                    Philosopher p = new Philosopher(i, ref forksAvailable);
                }

                Thread.Sleep(1000);
            }
        }
    }


    public class Philosopher
    {
        private int _number;
        private bool[] _forksAvailable;
        static Semaphore philosopherHungry = new Semaphore(2, 2);
        Thread thread;

        public Philosopher(int number, ref bool[] forksAvailable)
        {
            _number = number;
            _forksAvailable = forksAvailable;
            thread = new Thread(Eat);            
            thread.Start();
        }

        public void Eat()
        {
            int leftForkIndex = _number;
            int rightForkIndex = _number - 1;
            if(rightForkIndex < 0)
            {
                rightForkIndex = 4;
            }

            Console.WriteLine("Philosopher #" + _number + " is thinking");

            if (_forksAvailable[leftForkIndex] == true && _forksAvailable[rightForkIndex] == true)
            {
                _forksAvailable[leftForkIndex] = false;
                _forksAvailable[rightForkIndex] = false;
                philosopherHungry.WaitOne();
                Console.WriteLine("Philosopher #" + _number + " is taking forks #" + leftForkIndex + " and #" + rightForkIndex);
                Console.WriteLine("Philosopher #" + _number + " is eating");                
                philosopherHungry.Release();
                Console.WriteLine("Philosopher #" + _number + " has put forks and begun thinking again");
            }
        }
    }
}
