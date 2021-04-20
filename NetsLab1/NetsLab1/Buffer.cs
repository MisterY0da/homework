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

        public static bool ValidData(BitArray frame)
        {            
            BitArray receivedData = GetFrameData(frame);
            BitArray expectedParity = GetExpectedFrameParity(frame);
            BitArray receivedParity = CalculateFrameParity(receivedData);
            
            for (int i = frame.Length - 1; i > frame.Length - 8; i--)
            {
                if(receivedParity[i] != expectedParity[i])
                {
                    return false;
                }
            }

            return true;
        }


        public static BitArray GetExpectedFrameParity(BitArray frame)
        {
            BitArray parity = new BitArray(Frame.PARITYBLOCKBITSCOUNT);
            for(int i = frame.Length - 1; i > frame.Length - 8; i--)
            {
                parity[i] = frame[i];
            }

            return parity;
        }

        public static BitArray CalculateFrameParity(BitArray receivedData)
        {
            BitArray calculatedParity = new BitArray(Frame.PARITYBLOCKBITSCOUNT);

            for (int i = 0; i < 8; i++)
            {
                int columnBitsSum = 0;
                for (int j = i; j < receivedData.Length; j += 8)
                {
                    if (receivedData[j] == true)
                    {
                        columnBitsSum += 1;
                    }
                }

                if (columnBitsSum % 2 != 0)
                {
                    calculatedParity[i] = true;
                }
                else
                {
                    calculatedParity[i] = false;
                }
            }

            return calculatedParity;
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
