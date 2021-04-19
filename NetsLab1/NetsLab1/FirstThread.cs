using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class FirstThread
    {
        private Semaphore _semaphore;
        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToSecondWT _post;

        private int _receiptToSecond = 0;
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
            if (_dataIsReceived == true)
            {
                ConsoleHelper.WriteToConsole("1 поток ", "Данные получены.");
                _receiptToSecond = 1;
                ConsoleHelper.WriteToConsoleArray("1 поток полученные данные", _receivedMessage);
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
                ConsoleHelper.WriteToConsole("1 поток ", "Данные получены.");
                ConsoleHelper.WriteToConsoleArray("1 поток полученные данные", _receivedMessage);
            }
            _semaphore.Release();

            ConsoleHelper.WriteToConsole("1 поток", "Завершаю работу.");
        }
        public void ReceiveData(BitArray array)
        {
            _receivedMessage = array;
            _dataIsReceived = true;
        }

        public int GetReceiptValue()
        {
            return _receiptToSecond;
        }

    }
}
