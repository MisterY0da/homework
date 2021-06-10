using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetsLab3
{
    public class FrameHelper
    {        
        public const int DATASIZEBLOCKBITSCOUNT = 7;
        public const int PARITYBLOCKBITSCOUNT = 8;
        public const int FRAMENUMBERBLOCKBITSCOUNT = 3;

        public static BitArray FillFrame(BitArray data, int frameNumber)
        {
            BitArray binaryDataSize = DecimalToBinary(data.Length, DATASIZEBLOCKBITSCOUNT);
            bool[] verticalParity = GetVerticalParity(data);
            BitArray binaryFrameNumber = new BitArray(FRAMENUMBERBLOCKBITSCOUNT);
            binaryFrameNumber = DecimalToBinary(frameNumber, FRAMENUMBERBLOCKBITSCOUNT);

            int frameLength = data.Length + binaryDataSize.Length + verticalParity.Length + binaryFrameNumber.Length;

            

            BitArray frame = new BitArray(frameLength);

            for (int i = 0; i < data.Length; i++)
            {
                frame[i] = data[i];
            }

            for (int i = data.Length; i < data.Length + binaryDataSize.Length; i++)
            {
                frame[i] = binaryDataSize[i - data.Length];
            }

            for (int i = data.Length + binaryDataSize.Length;
                i < data.Length + binaryDataSize.Length + verticalParity.Length; i++)
            {
                frame[i] = verticalParity[i - data.Length - binaryDataSize.Length];
            }

            for(int i = data.Length + binaryDataSize.Length + verticalParity.Length; 
                i < data.Length + binaryDataSize.Length + verticalParity.Length + binaryFrameNumber.Length; i++)
            {
                frame[i] = binaryFrameNumber[i - data.Length - binaryDataSize.Length - verticalParity.Length];
            }

            return frame;
        }

        public static BitArray DecimalToBinary(int decimalNumber, int bitsCount)
        {
            BitArray binaryNumber = new BitArray(bitsCount, false);
            for (int i = 0; i < binaryNumber.Length; i++)
            {
                if (decimalNumber % 2 == 1)
                {
                    binaryNumber[(binaryNumber.Length - 1) - i] = true;
                }
                decimalNumber /= 2;
            }

            return binaryNumber;
        }

        public static int getIntFromBitArray(BitArray bitArray)
        {

            int[] array = new int[1];

            BitArray reversedBitArray = new BitArray(bitArray.Length);

            for (int i = 0; i < bitArray.Length; i++)
            {
                reversedBitArray[i] = bitArray[(bitArray.Length - 1) - i];
            }
            reversedBitArray.CopyTo(array, 0);
            return array[0];

        }


        public static BitArray GetBinaryFrameNumber(BitArray frame)
        {
            int startIndex = frame.Length - FRAMENUMBERBLOCKBITSCOUNT;

            BitArray frameNumber = new BitArray(FRAMENUMBERBLOCKBITSCOUNT);

            for(int i = 0; i < FRAMENUMBERBLOCKBITSCOUNT; i++)
            {
                frameNumber[i] = frame[startIndex + i];
            }

            return frameNumber;
        }

        public static bool[] GetVerticalParity(BitArray data)
        {
            bool[] verticalParity = new bool[PARITYBLOCKBITSCOUNT];
            for (int i = 0; i < PARITYBLOCKBITSCOUNT; i++)
            {
                int columnBitsSum = 0;
                for (int j = i; j < data.Length; j += 8)
                {
                    if (data[j] == true)
                    {
                        columnBitsSum += 1;
                    }
                }

                if (columnBitsSum % 2 != 0)
                {
                    verticalParity[i] = true;
                }
                else
                {
                    verticalParity[i] = false;
                }
            }

            return verticalParity;
        }


        public static BitArray GetFrameData(BitArray _frame)
        {
            int dataSize = _frame.Length - DATASIZEBLOCKBITSCOUNT - PARITYBLOCKBITSCOUNT - FRAMENUMBERBLOCKBITSCOUNT;
            BitArray dataReceived = new BitArray(dataSize);

            for (int i = 0; i < dataSize; i++)
            {
                dataReceived[i] = _frame[i];
            }

            return dataReceived;
        }


        public static string BitArrayToString(BitArray bitArray)
        {
            byte[] bytes = new byte[(bitArray.Length - 1) / 8 + 1];
            bitArray.CopyTo(bytes, 0);
            string bytesStr = Encoding.UTF8.GetString(bytes);

            return bytesStr;
        }
    }
}
