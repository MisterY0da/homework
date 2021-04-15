using System;
using System.Collections;
using System.Threading;

namespace NetsSkeleton
{
    public delegate void PostToFirstWT(BitArray message);
    public delegate void PostToSecondWT(BitArray message);
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleHelper.WriteToConsole("Главный поток", "");
            Semaphore firstReceiveSemaphore = new Semaphore(0, 1);
            Semaphore secondReceiveSemaphore = new Semaphore(0, 1);
            FirstThread firstThread = new FirstThread(ref secondReceiveSemaphore, ref firstReceiveSemaphore);
            SecondThread secondThread = new SecondThread(ref firstReceiveSemaphore, ref secondReceiveSemaphore);
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
        private Semaphore _sendSemaphore;
        private Semaphore _receiveSemaphore;
        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToSecondWT _post;

        public FirstThread(ref Semaphore sendSemaphore, ref Semaphore receiveSemaphore)
        {
            _sendSemaphore = sendSemaphore;
            _receiveSemaphore = receiveSemaphore;
        }
        public void FirstThreadMain(object obj)
        {
            _post = (PostToSecondWT)obj;
            ConsoleHelper.WriteToConsole("1 поток", "Начинаю работу.Готовлю данные для передачи.");
            _sendMessage = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                if (i % 2 == 0)
                    _sendMessage[i] = true;
                else
                    _sendMessage[i] = false;
            }
            _post(_sendMessage);
            _sendSemaphore.Release();
            ConsoleHelper.WriteToConsole("1 поток", "Данные переданы");
            ConsoleHelper.WriteToConsole("1 поток", "Жду передачи данных");
            _receiveSemaphore.WaitOne();
            ConsoleHelper.WriteToConsole("1 поток", "Данные получены.");
            ConsoleHelper.WriteToConsoleArray("1 поток", _receivedMessage);
            ConsoleHelper.WriteToConsole("1 поток", "Завершаю работу.");


        }
        public void ReceiveData(BitArray array)
        {
            _receivedMessage = array;
        }

    }
    public class SecondThread
    {
        private Semaphore _sendSemaphore;
        private Semaphore _receiveSemaphore;
        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToFirstWT _post;

        public SecondThread(ref Semaphore sendSemaphore, ref Semaphore receiveSemaphore)
        {
            _sendSemaphore = sendSemaphore;
            _receiveSemaphore = receiveSemaphore;
        }
        public void SecondThreadMain(Object obj)
        {
            _post = (PostToFirstWT)obj;
            ConsoleHelper.WriteToConsole("2 поток", "Начинаю работу.Жду передачи данных.");
            _receiveSemaphore.WaitOne();
            ConsoleHelper.WriteToConsole("2 поток", "Данные полученны");
            ConsoleHelper.WriteToConsoleArray("2 поток", _receivedMessage);
            ConsoleHelper.WriteToConsole("2 поток", "Подготавливаю данные.");
            _sendMessage = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                if (i % 2 == 0)
                    _sendMessage[i] = false;
                else
                    _sendMessage[i] = true;
            }
            _post(_sendMessage);
            _sendSemaphore.Release();
            ConsoleHelper.WriteToConsole("2 поток", "Данные переданы");
            ConsoleHelper.WriteToConsole("2 поток", "Заканчиваю работу");

        }
        public void ReceiveData(BitArray array)
        {
            _receivedMessage = array;
        }
    }

}
