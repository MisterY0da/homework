using System;
using System.Collections;
using System.Threading;

namespace NetsSkeleton
{
    public delegate void PostToFirstWT(BitArray message);
    public delegate void PostToSecondWT(BitArray message);   
    public class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteToConsole("Главный поток", "");
            //Semaphore firstReceiveSemaphore = new Semaphore(0, 1);
            //Semaphore secondReceiveSemaphore = new Semaphore(0, 1);
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

    public static class ConsoleHelper
    {
        public static object LockObject = new Object();
        public static void WriteToConsole(string info, string write)
        {
            lock (LockObject)
            {
                Console.WriteLine(info + " : " + write);
            }

        }
        public static void WriteToConsoleArray(string info, BitArray array)
        {
            lock (LockObject)
            {
                Console.Write(info + " : ");
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == true)
                        Console.Write("1");
                    else
                        Console.Write("0");

                }
                Console.WriteLine();
            }

        }
    }
    public class FirstThread
    {
        private Semaphore _semaphore;
        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToSecondWT _post;

        private string _receipt;
        private bool _dataIsReceived = false;

        public FirstThread(ref Semaphore semaphore)
        {
            _semaphore = semaphore;
        }
        public void FirstThreadMain(object obj)
        {
            _post = (PostToSecondWT)obj;
            _sendMessage = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                if (i % 2 == 0)
                    _sendMessage[i] = true;
                else
                    _sendMessage[i] = false;
            }
            

            ConsoleHelper.WriteToConsole("1 поток", "Начинаю работу.");


            _semaphore.WaitOne();
            if(_dataIsReceived == true)
            {
                ConsoleHelper.WriteToConsole("1 поток квитанция 2-му", _receipt);
                ConsoleHelper.WriteToConsoleArray("1 поток", _receivedMessage);
                _dataIsReceived = false;

                ConsoleHelper.WriteToConsole("1 поток", "Готовлю данные для передачи.");
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("1 поток", "Данные переданы.");
            }
            else
            {
                ConsoleHelper.WriteToConsole("1 поток", "Готовлю данные для передачи.");
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("1 поток", "Данные переданы.");
                ConsoleHelper.WriteToConsole("1 поток", "Жду передачи данных.");
            }            
            _semaphore.Release();            

            _semaphore.WaitOne();
            if (_dataIsReceived == true)
            {
                ConsoleHelper.WriteToConsole("1 поток квитанция 2-му", _receipt);
                ConsoleHelper.WriteToConsoleArray("1 поток", _receivedMessage);
            }
            _semaphore.Release();

            ConsoleHelper.WriteToConsole("1 поток", "Завершаю работу.");
        }
        public void ReceiveData(BitArray array)
        {
            _receivedMessage = array;
            _dataIsReceived = true;
            _receipt = "1";
        }

    }
    public class SecondThread
    {
        private Semaphore _semaphore;
        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToFirstWT _post;

        private string _receipt;
        private bool _dataIsReceived = false;

        public SecondThread(ref Semaphore semaphore)
        {
            _semaphore = semaphore;
        }
        public void SecondThreadMain(Object obj)
        {
            _post = (PostToFirstWT)obj;
            _sendMessage = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                if (i % 2 == 0)
                    _sendMessage[i] = false;
                else
                    _sendMessage[i] = true;
            }
            

            ConsoleHelper.WriteToConsole("2 поток", "Начинаю работу.");

            _semaphore.WaitOne();
            if (_dataIsReceived == true)
            {
                ConsoleHelper.WriteToConsole("2 поток квитация 1-му", _receipt);
                ConsoleHelper.WriteToConsoleArray("2 поток", _receivedMessage);
                _dataIsReceived = false;

                ConsoleHelper.WriteToConsole("2 поток", "Готовлю данные для передачи.");
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("2 поток", "Данные переданы.");
            }

            else
            {
                ConsoleHelper.WriteToConsole("2 поток", "Готовлю данные для передачи.");
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("2 поток", "Данные переданы.");
                ConsoleHelper.WriteToConsole("2 поток", "Жду передачи данных.");
            }
            _semaphore.Release();

            _semaphore.WaitOne();
            if (_dataIsReceived == true)
            {
                ConsoleHelper.WriteToConsole("2 поток квитация 1-му", _receipt);
                ConsoleHelper.WriteToConsoleArray("2 поток", _receivedMessage);
            }
            _semaphore.Release();

            ConsoleHelper.WriteToConsole("2 поток", "Завершаю работу.");

        }

        public void ReceiveData(BitArray array)
        {
            _receivedMessage = array;
            _dataIsReceived = true;
            _receipt = "1";
        }
    }

}
