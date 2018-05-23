﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ketqua.Digits
{
    class TwoDigits : Digit
    {
        public TwoDigits()
        {
            this.amountOfNumber = Variables.AMOUNT_OF_TWO_DEGITS;
            this.filePath = Variables.DATABASE_FOLDER_PATH + "data-two-digits.txt";
    }
        public override string[] GenerateNumber()
        {
            int numRows = Variables.AMOUNT_OF_TWO_DEGITS;
            string[] strArray = new string[numRows];
            int count = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = i + 1; j < 100; j++)
                {
                    strArray[count] = Utils.Pad2(i) + '_' + Utils.Pad2(j);
                    count++;
                }
            }
            return strArray;
        }

        public override string[] MixNumber(string[] strArray, bool isSkip = true)
        {
            int start = isSkip ? 1 : 0;
            int arrayLength = strArray.Length;
            int count = 0;
            string[] tempArray = new string[Variables.AMOUNT_OF_TWO_DEGITS];

            for (int i = start; i < arrayLength; i++)
            {
                for (int j = i + 1; j < arrayLength; j++)
                {
                    tempArray[count] = strArray[i] + '_' + strArray[j];
                    count++;
                }
            }
            return tempArray;
        }

    }
}
