using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    public class Utils
    {
        // Utils
        public static string GetTime()
        {
            return DateTime.Now.ToString("h:mm:ss tt");
            
        }
        // Read/Write File
        public static string[] ReadFile(string path = Variables.DIRECTORY_PATH)
        {
            Console.WriteLine("Read File: " + path);
            string[] lines = System.IO.File.ReadAllLines(path);
            Console.WriteLine("Read File End.");
            return lines;
        }

        public static void WriteDataToFile(string[] strArray, string path = Variables.DATABASE_FOLDER_PATH)
        {
            Directory.CreateDirectory(Variables.DATABASE_FOLDER_NAME);
            int length = strArray.Length;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                for (int i = 0; i < length; i++)
                {
                    file.WriteLine(strArray[i]);
                }
            }
            Console.WriteLine("start: " + Variables.START_TIME);
            Console.WriteLine("end: " + DateTime.Now.ToString("h:mm:ss tt"));
            Console.WriteLine("============ Complete Write Data Single =============== ");
        }

        public static void WriteFile(string[] arrayData, string path)
        {
            Console.WriteLine("Write File Start.");
            int length = arrayData.Length;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                for (int i = 0; i < length; i++)
                {
                    file.WriteLine(arrayData[i]);
                }
            }
            Console.WriteLine("Write File Completed: " + path);
        }

        // Utils for number
        public static string Pad2(int number)
        {
            return (number < 10 ? "0" : "") + number;
        }

        // Working with array 
        public static string[] ReduceDuplicateNumberInArray(string[] arrayString)
        {
            arrayString = arrayString.Distinct().ToArray();
            return arrayString;
        }

        //private void calculateFourNumber()
        //{
        //    string[] lines = ReadFile();
        //    int lineLength = lines.Length;
        //    int numColumns = 5;
        //    string[] dataArray = generateFourNumber();
        //    int dataLength = dataArray.Length;
        //    //string[] tempDataArray = new string[] { };
        //    //string[,] tempDataArray = makeTable(dataArray, lineLength);

        //    int[,] tempDataArray = makeTableInt(dataLength, numColumns);

        //    for (int i = 0; i < lineLength; i++)
        //    {
        //        Console.WriteLine("Line: " + i);
        //        string[] result = mixFourNumber(reduceArray(lines[i].Split('\t')));
        //        int resultLength = result.Length;
        //        int count = 0;
        //        int currentColumn = i % numColumns + 1;
        //        string lineVal = "";
        //        for (int ii = 0; ii < dataLength; ii++)
        //        {
        //            bool isReset = ((lineLength + 1) % numColumns) == 0;
        //            lineVal = dataArray[ii];
        //            if (lineVal == "01_11_17_19")
        //            {
        //                Console.WriteLine("debug");
        //            }

        //            if (count < resultLength && lineVal == result[count])
        //            {
        //                tempDataArray[ii, currentColumn] = 0;
        //                count++;
        //            }
        //            else
        //            {
        //                tempDataArray[ii, currentColumn] = tempDataArray[ii, currentColumn - 1] + 1;
        //            }

        //            if (isReset)
        //            {
        //                tempDataArray[ii, 0] = tempDataArray[ii, numColumns];
        //            }
        //        }
        //    }

        //    WriteData(tempDataArray, false);
        //}



        // Working With Data
        //private string[] ConvertTableToDataArray(int[,] table, Variables.Digits digitsType, bool showAll = false, int offset = 10)
        //{
        //    Console.WriteLine("Conver Table To Data Array.....: ");
        //    string[] dataArray = GenerateTwoNumber();
        //    int lines = 0;
        //    int start = 0;
        //    switch (digitsType)
        //    {
        //        case Variables.Digits.TWO_DIGITS:
        //            lines = Variables.AMOUNT_OF_TWO_DEGITS;
        //            break;
        //        case Variables.Digits.THREE_DIGITS:
        //            dataArray = GenerateThreeNumber();
        //            lines = Variables.AMOUNT_OF_THREE_DEGITS;
        //            break;
        //        case Variables.Digits.FOURS_DIGITS:
        //            dataArray = GenerateFourNumber();
        //            lines = Variables.AMOUNT_OF_FOUR_DEGITS;
        //            break;
        //        default:
        //            dataArray = GenerateTwoNumber();
        //            break;
        //    }
        //    int numRows = table.GetLength(0);
        //    int numColumns = table.GetLength(1);

        //    string[] tempArray = new string[numRows];
        //    if (!showAll)
        //    {
        //        start = numColumns - offset;
        //    }

        //    for (int i = 0; i < numRows; i++)
        //    {
        //        string tempString = dataArray[i];
        //        for (int j = start; j < numColumns; j++)
        //        {
        //            tempString += "\t" + table[i, j];
        //        }

        //        tempArray[i] = tempString;
        //    }
        //    Console.WriteLine("Conver Table To Data Completed.");
        //    return tempArray;
        //}


        //private string[] ConvertDataSingle(int[] data, Variables.Digits digitType)
        //{
        //    Console.WriteLine("Conver Data Single....");
        //    string[] dataArray = GenerateTwoNumber();

        //    switch (digitType)
        //    {
        //        case Variables.Digits.TWO_DIGITS:
        //            break;
        //        case Variables.Digits.THREE_DIGITS:
        //            dataArray = GenerateThreeNumber();
        //            break;
        //        case Variables.Digits.FOURS_DIGITS:
        //            dataArray = GenerateFourNumber();
        //            break;
        //        default:
        //            dataArray = GenerateTwoNumber();
        //            break;
        //    }
        //    int length = dataArray.Length;
        //    string[] tempArray = new string[length];

        //    for (int i = 0; i < length; i++)
        //    {
        //        tempArray[i] = dataArray[i] + '\t' + data[i];
        //    }

        //    return tempArray;
        //}

        // private void WriteDataSingle(int[,] table = null, Variables.Digits digitType = Variables.Digits.TWO_DIGITS)
        //{
        //    Console.WriteLine("Start Write Data Single. Digits Type: ");
        //    string[] strArray = ConvertTableToDataArray(table, digitType);
        //    WriteDataToFile(strArray);

        //}

        //private void WriteDataSingle(int[] dataArray = null, Variables.Digits digitType = Variables.Digits.TWO_DIGITS)
        //{
        //    Console.WriteLine("Start Write Data Single. Digits Type: ");
        //    string[] strArray = ConvertDataSingle(dataArray, digitType);
        //    WriteDataToFile(strArray);
        //}

        // mix Number // 

        //public string[] MixTwoNumber(string[] strArray, bool isSkip = true)
        //{
        //    int start = isSkip ? 1 : 0;
        //    int arrayLength = strArray.Length;
        //    int count = 0;
        //    string[] tempArray = new string[Variables.AMOUNT_OF_TWO_DEGITS];

        //    for (int i = start; i < arrayLength; i++)
        //    {
        //        for (int j = i + 1; j < arrayLength; j++)
        //        {
        //            tempArray[count] = strArray[i] + '_' + strArray[j];
        //            count++;
        //        }
        //    }
        //    return tempArray;
        //}

        //public string[] MixThreeNumber(string[] strArray, bool isSkip = true)
        //{
        //    int start = isSkip ? 1 : 0;
        //    int arrayLength = strArray.Length;
        //    int count = 0;
        //    string[] tempArray = new string[Variables.AMOUNT_OF_THREE_DEGITS];

        //    for (int i = start; i < arrayLength; i++)
        //    {
        //        for (int j = i + 1; j < arrayLength; j++)
        //        {
        //            for (int k = j + 1; k < arrayLength; k++)
        //            {
        //                tempArray[count] = strArray[i] + '_' + strArray[j] + '_' + strArray[k];
        //                count++;
        //            }
        //        }
        //    }
        //    return tempArray;
        //}

        //public string[] MixFourNumber(string[] strArray, bool isSkip = true)
        //{
        //    int start = isSkip ? 1 : 0;
        //    int arrayLength = strArray.Length;
        //    int count = 0;
        //    string[] tempArray = new string[Variables.AMOUNT_OF_FOUR_DEGITS];
        //    for (int i = start; i < arrayLength; i++)
        //    {
        //        for (int j = i + 1; j < arrayLength; j++)
        //        {
        //            for (int k = j + 1; k < arrayLength; k++)
        //            {
        //                for (int l = k + 1; l < arrayLength; l++)
        //                {
        //                    tempArray[count] = strArray[i] + '_' + strArray[j] + '_' + strArray[k] + '_' + strArray[l];
        //                    count++;
        //                }
        //            }
        //        }
        //    }
        //    return tempArray;
        //}

        //// Gernerate Number

        //private string[] GenerateTwoNumber()
        //{
        //    int numRows = Variables.AMOUNT_OF_TWO_DEGITS;
        //    string[] strArray = new string[numRows];
        //    int count = 0;
        //    for (int i = 0; i < 100; i++)
        //    {
        //        for (int j = i + 1; j < 100; j++)
        //        {
        //            strArray[count] = Pad2(i) + '_' + Pad2(j);
        //            count++;
        //        }
        //    }
        //    return strArray;
        //}

        //private string[] GenerateThreeNumber()
        //{
        //    int numRows = Variables.AMOUNT_OF_THREE_DEGITS;
        //    string[] strArray = new string[numRows];
        //    int count = 0;
        //    for (int i = 0; i < 100; i++)
        //    {
        //        for (int j = i + 1; j < 100; j++)
        //        {
        //            for (int k = j + 1; k < 100; k++)
        //            {

        //                strArray[count] = Pad2(i) + '_' + Pad2(j) + '_' + Pad2(k);
        //                count++;
        //            }
        //        }
        //    }
        //    return strArray;
        //}

        //private string[] GenerateFourNumber()
        //{
        //    int numRows = Variables.AMOUNT_OF_FOUR_DEGITS;
        //    string[] strArray = new string[numRows];
        //    int count = 0;
        //    for (int i = 0; i < 100; i++)
        //    {
        //        for (int j = i + 1; j < 100; j++)
        //        {
        //            for (int k = j + 1; k < 100; k++)
        //            {
        //                for (int l = k + 1; l < 100; l++)
        //                {
        //                    strArray[count] = Pad2(i) + '_' + Pad2(j) + '_' + Pad2(k) + '_' + Pad2(l);
        //                    count++;
        //                }
        //            }
        //        }
        //    }
        //    return strArray;
        //}
    }
}
