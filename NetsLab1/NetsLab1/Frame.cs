using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetsLab1
{
    public class Frame
    {
        private BitArray _frame;
        public const int DATASIZEBLOCKBITSCOUNT = 7;
        public const int PARITYBLOCKBITSCOUNT = 8;

        public Frame(BitArray data)
        {
            BitArray binaryDataSize = DecimalToBinary(data.Length);
            bool[] verticalParity = GetVerticalParity(data);

            _frame = FillFrame(data, binaryDataSize, verticalParity);           
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

        private BitArray FillFrame(BitArray data, BitArray binaryDataSize, bool[] verticalParity)
        {
            for (int i = 0; i < data.Length; i++)
            {
                _frame[i] = data[i];
            }

            for (int i = data.Length; i < data.Length + binaryDataSize.Length; i++)
            {
                _frame[i] = binaryDataSize[i];
            }

            for (int i = data.Length + binaryDataSize.Length;
                i < data.Length + binaryDataSize.Length + verticalParity.Length; i++)
            {
                _frame[i] = verticalParity[i];
            }

            return _frame;
        }

        private bool[] GetVerticalParity(BitArray data)
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

        public BitArray GetFrame()
        {
            return _frame;
        }
    }
}
