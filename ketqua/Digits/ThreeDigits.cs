using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    class ThreeDigits : Digit
    {
        public ThreeDigits()
        {
            this.amountOfNumber = Variables.AMOUNT_OF_THREE_DEGITS;
            this.filePath = filePath + "data-three-digits.txt";
            this.digitType = Digits.THREE_DIGITS;
            this.limitDataLine = 1000;
            this.numberPerMessage = 14;
        }

        public override string[] GenerateNumber()
        {
            int numRows = Variables.AMOUNT_OF_THREE_DEGITS;
            string[] strArray = new string[numRows];
            int count = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = i + 1; j < 100; j++)
                {
                    for (int k = j + 1; k < 100; k++)
                    {

                        strArray[count] = Utils.Pad2(i) + '_' + Utils.Pad2(j) + '_' + Utils.Pad2(k);
                        count++;
                    }
                }
            }
            return strArray;
        }

        public override string[] MixNumber(string[] strArray, bool isSkip = true)
        {
            int start = isSkip ? 1 : 0;
            int arrayLength = strArray.Length;
            int count = 0;
            for (int i = start; i < arrayLength; i++)
            {
                for (int j = i + 1; j < arrayLength; j++)
                {
                    for (int k = j + 1; k < arrayLength; k++)
                    {
                        count++;
                    }
                }
            }

            string[] tempArray = new string[count];
            count = 0;
            for (int i = start; i < arrayLength; i++)
            {
                for (int j = i + 1; j < arrayLength; j++)
                {
                    for (int k = j + 1; k < arrayLength; k++)
                    {
                        tempArray[count] = strArray[i] + '_' + strArray[j] + '_' + strArray[k];
                        count++;
                    }
                }
            }
            return tempArray;
        }

        public override string GetMessage(string s)
        {
            return s.Substring(0, 2) + " " + s.Substring(3, 2) + " " + s.Substring(6, 2);
        }
    }
}
