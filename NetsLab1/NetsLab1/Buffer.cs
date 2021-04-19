using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NetsLab1
{
    public class Buffer
    {
        const int FRAMESCOUNT = 4;
        public BitArray[] framesArray = new BitArray[FRAMESCOUNT];

        public static bool ValidData(BitArray _frame)
        {            
            BitArray dataReceived = GetFrameData(_frame);
            BitArray expectedParity = GetExpectedFrameParity(_frame);
            BitArray receivedParity = new BitArray(Frame.PARITYBLOCKBITSCOUNT);

            //vertical parity check
            for (int i = 0; i < 8; i++)
            {
                int columnBitsSum = 0;
                for (int j = i; j < dataReceived.Length; j += 8)
                {
                    if (dataReceived[j] == true)
                    {
                        columnBitsSum += 1;
                    }
                }

                if (columnBitsSum % 2 != 0)
                {
                    receivedParity[i] = true;
                }
                else
                {
                    receivedParity[i] = false;
                }
            }



            for (int i = _frame.Length - 1; i > _frame.Length - 8; i--)
            {
                if(receivedParity[i] != expectedParity[i])
                {
                    return false;
                }
            }

            return true;
        }


        public static BitArray GetExpectedFrameParity(BitArray _frame)
        {
            BitArray parity = new BitArray(Frame.PARITYBLOCKBITSCOUNT);
            for(int i = _frame.Length - 1; i > _frame.Length - 8; i--)
            {
                parity[i] = _frame[i];
            }

            return parity;
        }

        public static BitArray GetFrameData(BitArray _frame)
        {
            int dataSize = _frame.Length - Frame.DATASIZEBLOCKBITSCOUNT - Frame.PARITYBLOCKBITSCOUNT;
            BitArray dataReceived = new BitArray(dataSize);

            for (int i = 0; i < dataSize; i++)
            {
                dataReceived[i] = _frame[i];
            }

            return dataReceived;
        }
    }
}
