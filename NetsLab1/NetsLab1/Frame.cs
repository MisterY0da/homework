using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetsLab1
{
    public class Frame
    {        
        public const int DATASIZEBLOCKBITSCOUNT = 7;
        public const int PARITYBLOCKBITSCOUNT = 8;

        public static BitArray FillFrame(BitArray data)
        {
            BitArray binaryDataSize = DecimalToBinary(data.Length);
            bool[] verticalParity = GetVerticalParity(data);
            int frameLength = data.Length + binaryDataSize.Length + verticalParity.Length;
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

            return frame;
        }

        public static BitArray DecimalToBinary(int decimalNumber)
        {
            BitArray binaryNumber = new BitArray(DATASIZEBLOCKBITSCOUNT, false);
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
    }
}
