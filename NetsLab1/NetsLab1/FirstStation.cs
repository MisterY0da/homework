using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace NetsLab1
{
    public class FirstStation
    {
        private Semaphore _mainSemaphore;
        private Semaphore _giveReceiptSem;
        private Semaphore _askReceiptSem;

        private BitArray _receivedMessage;
        private BitArray _sendMessage;
        private PostToSecondWT _post;

        private PostReceiptToSecondWT _postReceipt;
        private BitArray _receivedReceipt;
        private BitArray _sendReceipt;

        private bool _receivedMesShown = false;

        public FirstStation(ref Semaphore semaphore, ref Semaphore giveReceiptSem, ref Semaphore askReceiptSem)
        {
            _mainSemaphore = semaphore;
            _giveReceiptSem = giveReceiptSem;
            _askReceiptSem = askReceiptSem;
        }

        public void FirstStationReceipt(object obj)
        {
            _postReceipt = (PostReceiptToSecondWT)obj;
            _sendReceipt = new BitArray(1);

            _giveReceiptSem.WaitOne();
            _sendReceipt[0] = true;
            _postReceipt(_sendReceipt);
            _giveReceiptSem.Release();
        }

        public void FirstStationMessage(object obj)
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


            ConsoleHelper.WriteToConsole("1 станция", "Начинаю работу.");


            _mainSemaphore.WaitOne();
            if (_receivedMessage != null)
            {                
                ConsoleHelper.WriteToConsoleArray("1 станция полученные данные", _receivedMessage);
                _receivedMesShown = true;

                
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("1 станция", "Отправил данные.");

                _askReceiptSem.Release();
                

                _askReceiptSem.WaitOne();
                ConsoleHelper.WriteToConsoleArray("1 станция полученная квитанция", _receivedReceipt);
                ConsoleHelper.WriteToConsole("1 станция", "Данные дошли до получателя.");
                _askReceiptSem.Release();
            }
            else
            {
                
                _post(_sendMessage);
                ConsoleHelper.WriteToConsole("1 станция", "Отправил данные.");

                _askReceiptSem.Release();
                

                _askReceiptSem.WaitOne();
                ConsoleHelper.WriteToConsoleArray("1 станция полученная квитанция", _receivedReceipt);
                ConsoleHelper.WriteToConsole("1 станция", "Данные дошли до получателя.");
                _askReceiptSem.Release();

                ConsoleHelper.WriteToConsole("1 станция", "Жду ответа.");
            }
            _mainSemaphore.Release();

            _mainSemaphore.WaitOne();
            if (_receivedMesShown == false)
            {
                ConsoleHelper.WriteToConsoleArray("1 станция полученные данные", _receivedMessage);
            }
            _mainSemaphore.Release();


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
