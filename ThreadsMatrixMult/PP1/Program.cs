using System;
using System.Diagnostics;
using System.Threading;

namespace PP1
{
    class Program
    {
        static void Main(string[] args)
        {
            MultithreadedMultiplication(100);

            SerialMultiplication(100);            
        }

        static void MultithreadedMultiplication(int matrixSize)
        {
            

            int n = matrixSize;
            int[,] A = new int[n, n];
            InitializeMatrix(A, n, n);
            //printMatrix(A, n, n);

            Console.WriteLine("\n");

            int[,] B = new int[n, n];
            InitializeMatrix(B, n, n);
            //printMatrix(B, n, n);

            int[,] C = new int[n, n];

            int threadsCount = 4;
            int rowsPerThread = n / threadsCount;

            Thread[] threadsArray = new Thread[threadsCount];


            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int threadInd = 0; threadInd < threadsCount; threadInd++)
            {
                int startInd = threadInd * rowsPerThread;
                int endInd = startInd + rowsPerThread - 1;
                threadsArray[threadInd] = new Thread(state => CalculateRows(A, B, C, startInd, endInd, n));
                threadsArray[threadInd].Start();
            }
            stopwatch.Stop();


            Console.WriteLine("\n");
            //printMatrix(C, n, n);            

            TimeSpan ts = stopwatch.Elapsed;
            Console.WriteLine("Multithreaded multiplication time: " + ts.Milliseconds + "ms\n");
        }

        static void SerialMultiplication(int matrixSize)
        {
            

            int n = matrixSize;

            int [,] A = new int[n, n];

            InitializeMatrix(A, n, n);
            //printMatrix(A, n, n);

            Console.WriteLine("\n");

            int [,] B = new int[n, n];
            InitializeMatrix(B, n, n);
            //printMatrix(B, n, n);

            int [,]C = new int[n, n];



            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            CalculateRows(A, B, C, 0, n - 1, n);
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

        static void CalculateRows(int[,] matrix1, int[,] matrix2, int[,] matrixProduct, int startIndex, int endIndex, int rowLength)
        {

            for (int rowIndex = startIndex; rowIndex <= endIndex; rowIndex++)
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
}







