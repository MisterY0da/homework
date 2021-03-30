using System;
using System.Diagnostics;
using System.Threading;

namespace PP1
{
    class Program
    {
        static void Main(string[] args)
        {
            MultithreadedMultiplication(500);

            SerialMultiplication(500);            
        }

        static void MultithreadedMultiplication(int matrixSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = matrixSize;
            int[,] A = new int[n, n];
            InitializeMatrix(A, n, n);
            //printMatrix(A, n, n);

            Console.WriteLine("\n");

            int[,] B = new int[n, n];
            InitializeMatrix(B, n, n);
            //printMatrix(B, n, n);

            int[,] C = new int[n, n];

            Thread[] threadsArray = new Thread[n];

            for (int i = 0; i < n; i++)
            {
                int rowIndex = i;
                threadsArray[i] = new Thread(state => CalculateRow(A, B, C, rowIndex, n));
                threadsArray[i].Start();
            }

            stopwatch.Stop();

            Console.WriteLine("\n");
            //printMatrix(C, n, n);            

            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Multithreaded multiplication time: " + ts.Milliseconds + "ms\n");
        }

        static void SerialMultiplication(int matrixSize)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int n = matrixSize;

            int [,] A = new int[n, n];

            InitializeMatrix(A, n, n);
            //printMatrix(A, n, n);

            Console.WriteLine("\n");

            int [,] B = new int[n, n];
            InitializeMatrix(B, n, n);
            //printMatrix(B, n, n);

            int [,]C = new int[n, n];


            for (int i = 0; i < n; i++)
            {
                CalculateRow(A, B, C, i, n);
            }

            stopwatch.Stop();

            Console.WriteLine("\n");
            //printMatrix(C, n, n);

            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Serial multiplication time: " + ts.Milliseconds + "ms\n");
        }


        static void InitializeMatrix(int[,] matrix, int rows, int cols)
        {
            Random rnd = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = rnd.Next(1, 3);
                }
            }
        }

        static void printMatrix(int[,] matrix, int rows, int cols)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(matrix[i, j] + "  ");
                }
                Console.Write("\n");
            }
        }

        static void CalculateRow(int[,] matrix1, int[,] matrix2, int[,] matrixProduct, int rowIndex, int rowLength)
        {
            for (int j = 0; j < rowLength; j++)
            {
                int tempValue = 0;
                for (int i = 0; i < rowLength; i++)
                {
                    tempValue += matrix1[rowIndex, i] * matrix2[i, j];
                }
                matrixProduct[rowIndex, j] = tempValue;
            }
            
        }
    }
}







