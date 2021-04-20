using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class FirstStation
    {
        private Semaphore _semaphore;
        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToSecondWT _post;

        private PostReceiptToSecond _postReceipt;
        private BitArray _receivedReceipt;
        private BitArray _sendReceipt;

        private bool _receivedMesShown = false;

        public FirstStation(ref Semaphore semaphore)
        {
            _semaphore = semaphore;
        }



        public void FirstThreadMain(object obj)
        {
            _postReceipt = (PostReceiptToSecond)obj;
            _sendReceipt = new BitArray(1);

            _post = (PostToSecondWT)obj;
            _sendMessage = new BitArray(56);
            for (int i = 0; i < 56; i++)
            {
                if (i % 2 == 0)
                    _sendMessage[i] = true;
                else
                    _sendMessage[i] = false;
            }


            ConsoleHelper.WriteToConsole("1 станция", "Начинаю работу.");


            _semaphore.WaitOne();
            if (_receivedMessage != null)
            {
                ConsoleHelper.WriteToConsole("1 станция ", "Данные получены.");
                _sendReceipt[0] = true;
                _postReceipt(_sendReceipt);
                ConsoleHelper.WriteToConsoleArray("1 станция полученные данные", _receivedMessage);
                _receivedMesShown = true;

                ConsoleHelper.WriteToConsole("1 станция", "Готовлю данные для передачи.");
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("1 станция", "Данные переданы.");
                ConsoleHelper.WriteToConsoleArray("1 станция полученная квитанция", _receivedReceipt);
            }
            else
            {
                ConsoleHelper.WriteToConsole("1 станция", "Готовлю данные для передачи.");
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("1 станция", "Данные переданы.");
                ConsoleHelper.WriteToConsoleArray("1 станция полученная квитанция", _receivedReceipt);
                ConsoleHelper.WriteToConsole("1 станция", "Жду передачи данных.");
            }
            _semaphore.Release();

            _semaphore.WaitOne();
            if (_receivedMesShown == false)
            {
                ConsoleHelper.WriteToConsole("1 станция ", "Данные получены.");
                _sendReceipt[0] = true;
                _postReceipt(_sendReceipt);
                ConsoleHelper.WriteToConsoleArray("1 станция полученные данные", _receivedMessage);
            }
            _semaphore.Release();

            ConsoleHelper.WriteToConsole("1 станция", "Завершаю работу.");
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
