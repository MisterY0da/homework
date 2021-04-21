using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class SecondStation
    {
        private Semaphore _mainSemaphore;
        private Semaphore _giveReceiptSem;
        private Semaphore _askReceiptSem;

        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToFirstWT _post;

        private PostReceiptToFirstWT _postReceipt;
        private BitArray _receivedReceipt;
        private BitArray _sendReceipt;

        private bool _receivedMesShown = false;

        public SecondStation(ref Semaphore semaphore, ref Semaphore giveReceiptSem, ref Semaphore askReceiptSem)
        {
            _mainSemaphore = semaphore;
            _giveReceiptSem = giveReceiptSem;
            _askReceiptSem = askReceiptSem;
        }


        public void SecondStationReceipt(object obj)
        {
            _postReceipt = (PostReceiptToFirstWT)obj;
            _sendReceipt = new BitArray(1);

            _giveReceiptSem.WaitOne();
            _sendReceipt[0] = true;
            _postReceipt(_sendReceipt);
            _giveReceiptSem.Release();
        }

        public void SecondStationMessage(Object obj)
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


            ConsoleHelper.WriteToConsole("2 станция", "Начинаю работу.");

            _mainSemaphore.WaitOne();
            if (_receivedMessage != null)
            {                
                ConsoleHelper.WriteToConsoleArray("2 станция полученные данные", _receivedMessage);
                _receivedMesShown = true;

                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("2 станция", "Отправил данные.");
                

                _askReceiptSem.Release();                

                _askReceiptSem.WaitOne();
                ConsoleHelper.WriteToConsoleArray("2 станция полученная квитанция", _receivedReceipt);
                ConsoleHelper.WriteToConsole("2 станция", "Данные дошли до получателя.");
                _askReceiptSem.Release();


            }

            else
            {
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("2 станция", "Отправил данные.");
                
                _askReceiptSem.Release();                

                _askReceiptSem.WaitOne();
                ConsoleHelper.WriteToConsoleArray("2 станция полученная квитанция", _receivedReceipt);
                ConsoleHelper.WriteToConsole("2 станция", "Данные дошли до получателя.");
                _askReceiptSem.Release();



                ConsoleHelper.WriteToConsole("2 станция", "Жду ответа.");
            }
            _mainSemaphore.Release();

            _mainSemaphore.WaitOne();
            if (_receivedMesShown == false)
            {
                ConsoleHelper.WriteToConsoleArray("2 станция полученные данные", _receivedMessage);
            }
            _mainSemaphore.Release();


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
