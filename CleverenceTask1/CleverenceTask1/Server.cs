using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CleverenceTask1
{
    public static class Server
    {
        static int count;
        static ReaderWriterLock readerWriterLock = new ReaderWriterLock();

        public static void GetCount()
        {
            readerWriterLock.AcquireReaderLock(Timeout.InfiniteTimeSpan);
            Console.WriteLine("Reader threadId: " + Thread.CurrentThread.ManagedThreadId + "\tcount = " + count);
            readerWriterLock.ReleaseReaderLock();
        }

        public static void AddToCount(int value)
        {
            readerWriterLock.AcquireWriterLock(Timeout.InfiniteTimeSpan);
            count += value;
            Console.WriteLine("Writer threadId: " + Thread.CurrentThread.ManagedThreadId + "\tadded " + value);
            readerWriterLock.ReleaseWriterLock();
        }
    }
}
