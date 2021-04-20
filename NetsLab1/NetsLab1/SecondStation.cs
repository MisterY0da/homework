using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class SecondStation
    {
        private Semaphore _semaphore;
        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToFirstWT _post;

        private PostReceiptToFirst _postReceipt;
        private BitArray _receivedReceipt;
        private BitArray _sendReceipt;

        private bool _receivedMesShown = false;

        public SecondStation(ref Semaphore semaphore)
        {
            _semaphore = semaphore;
        }
        public void SecondThreadMain(Object obj)
        {
            _postReceipt = (PostReceiptToFirst)obj;
            _sendReceipt = new BitArray(1);

            _post = (PostToFirstWT)obj;
            _sendMessage = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                if (i % 2 == 0)
                    _sendMessage[i] = false;
                else
                    _sendMessage[i] = true;
            }


            ConsoleHelper.WriteToConsole("2 станция", "Начинаю работу.");

            _semaphore.WaitOne();
            if (_receivedMessage != null)
            {
                ConsoleHelper.WriteToConsole("2 станция ", "Данные получены.");
                _sendReceipt[0] = true;
                _postReceipt(_sendReceipt);
                ConsoleHelper.WriteToConsoleArray("2 станция полученные данные", _receivedMessage);
                _receivedMesShown = true;

                ConsoleHelper.WriteToConsole("2 станция", "Готовлю данные для передачи.");
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("2 станция", "Данные переданы.");
                ConsoleHelper.WriteToConsoleArray("2 станция полученная квитанция", _receivedReceipt);
            }

            else
            {
                ConsoleHelper.WriteToConsole("2 станция", "Готовлю данные для передачи.");
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("2 станция", "Данные переданы.");
                ConsoleHelper.WriteToConsoleArray("2 станция полученная квитанция", _receivedReceipt);
                ConsoleHelper.WriteToConsole("2 станция", "Жду передачи данных.");
            }
            _semaphore.Release();

            _semaphore.WaitOne();
            if (_receivedMesShown == false)
            {
                ConsoleHelper.WriteToConsole("2 станция ", "Данные получены.");
                _sendReceipt[0] = true;
                _postReceipt(_sendReceipt);
                ConsoleHelper.WriteToConsoleArray("2 станция полученные данные", _receivedMessage);
            }
            _semaphore.Release();

            ConsoleHelper.WriteToConsole("2 станция", "Завершаю работу.");

        }

        public void ReceiveData(BitArray array)
        {
            _receivedMessage = array;
        }

        public void ReceiveReceipt(BitArray array)
        {
            _receivedReceipt = array;
        }
    }
}
