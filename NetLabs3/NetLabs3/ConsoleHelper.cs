using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetsLab3
{
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

                if (array.Length == 1)
                {
                    if (array[0] == true)
                        Console.Write("1");
                    else
                        Console.Write("0");
                }

                else
                {

                    string str = FrameHelper.BitArrayToString(array);

                    Console.Write(str);
                }

                Console.WriteLine();
            }

        }
    }
}
