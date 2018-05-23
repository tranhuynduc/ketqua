using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    class FourDigits : Digit
    {
        public FourDigits ()
        {
            this.amountOfNumber = Variables.AMOUNT_OF_FOUR_DEGITS;
            this.filePath = Variables.DATABASE_FOLDER_PATH + "data-four-digits  .txt";
            this.digitType = Digits.FOURS_DIGITS;
        }
        public override string[] GenerateNumber()
        {
            int numRows = Variables.AMOUNT_OF_FOUR_DEGITS;
            string[] strArray = new string[numRows];
            int count = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = i + 1; j < 100; j++)
                {
                    for (int k = j + 1; k < 100; k++)
                    {
                        for (int l = k + 1; l < 100; l++)
                        {
                            strArray[count] = Utils.Pad2(i) + '_' + Utils.Pad2(j) + '_' + Utils.Pad2(k) + '_' + Utils.Pad2(l);
                            count++;
                        }
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
            string[] tempArray = new string[Variables.AMOUNT_OF_FOUR_DEGITS];
            for (int i = start; i < arrayLength; i++)
            {
                for (int j = i + 1; j < arrayLength; j++)
                {
                    for (int k = j + 1; k < arrayLength; k++)
                    {
                        for (int l = k + 1; l < arrayLength; l++)
                        {
                            tempArray[count] = strArray[i] + '_' + strArray[j] + '_' + strArray[k] + '_' + strArray[l];
                            count++;
                        }
                    }
                }
            }
            return tempArray;
        }
    }
}
