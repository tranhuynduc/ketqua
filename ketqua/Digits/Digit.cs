using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    public abstract class Digit
    {
        protected int amountOfNumber;
        protected string filePath = Variables.DATABASE_FOLDER_PATH + "data.txt";
        Variables.Digits digitType;
        abstract public string[] GenerateNumber();
        abstract public string[] MixNumber(string[] strArray, bool isSkip = true);

        private void WriteData(int[] data, Variables.Digits digitType)
        {
            Console.WriteLine("Conver Data Single....");
            string[] dataArray = GenerateNumber();
            int length = dataArray.Length;
            string[] tempArray = new string[length];

            for (int i = 0; i < length; i++)
            {
                tempArray[i] = dataArray[i] + '\t' + data[i];
            }
            Console.WriteLine("Conver Data Completed");
            Utils.WriteDataToFile(tempArray, filePath);
        }

        private void WriteData(int[,] table, Variables.Digits digitsType)
        {
            Console.WriteLine("Conver Table To Data Array.....: ");
            string[] dataArray = GenerateNumber();
            int numRows = table.GetLength(0);
            int numColumns = table.GetLength(1);
            string[] tempArray = new string[numRows];

            for (int i = 0; i < numRows; i++)
            {
                string tempString = dataArray[i];
                for (int j = 0; j < numColumns; j++)
                {
                    tempString += "\t" + table[i, j];
                }

                tempArray[i] = tempString;
            }
            Console.WriteLine("Conver Table To Data Completed.");
            Utils.WriteDataToFile(tempArray, filePath);

        }

        private void CalculateSingle()
        {
            Console.WriteLine("===== Calculate Single ======= ");
            Variables.START_TIME = Utils.GetTime();
            string[] lines = Utils.ReadFile();
            int lineLength = lines.Length;
            string[] dataArray = GenerateNumber();
            int dataLength = dataArray.Length;
            int[] tempDataArray = new int[dataLength];

            for (int column  = 0; column < lineLength; column++)
            {
                Console.WriteLine("Line: " + column);
                string[] result = MixNumber(Utils.ReduceDuplicateNumberInArray(lines[column].Split('\t')));
                int resultLength = result.Length;
                int count = 0;
                string lineVal = "";
                for (int row = 0; row < dataLength; row++)
                {
                    lineVal = dataArray[row];

                    if (count < resultLength && lineVal == result[count])
                    {
                        tempDataArray[row] = 0;
                        count++;
                    }
                    else
                    {
                        tempDataArray[row] = tempDataArray[row] + 1;
                    }

                }
            }

            WriteData(tempDataArray, digitType);
        }

        private void calcualteTable()
        {
            Console.WriteLine("===== Calculate Table ======= ");
            Variables.START_TIME = Utils.GetTime();

            string[] lines = Utils.ReadFile();
            int lineLength = lines.Length;
            string[] dataArray = GenerateNumber();
            int dataLength = dataArray.Length;
            int[,] tempDataArray = new int[dataLength, lineLength];

            for (int column = 0; column < lineLength; column++)
            {
                Console.WriteLine("Line: " + column);
                string[] result = MixNumber(Utils.ReduceDuplicateNumberInArray(lines[column].Split('\t')));
                int resultLength = result.Length;
                int count = 0;
                string lineVal = "";
                for (int row = 0; row < dataLength; row++)
                {
                    lineVal = dataArray[row];

                    if (count < resultLength && lineVal == result[count])
                    {
                        tempDataArray[row, column] = 0;
                        count++;
                    }
                    else
                    {
                        tempDataArray[row, column] = tempDataArray[row, column - 1] + 1;
                    }
                }
            }
            WriteData(tempDataArray, digitType);
        }

        private void calcualteLimitTable(int numColumns = 5)
        {
            Console.WriteLine("===== Calculate Limit Table ======= ");
            Variables.START_TIME = Utils.GetTime();

            string[] lines = Utils.ReadFile();
            int lineLength = lines.Length;
            string[] dataArray = GenerateNumber();
            int dataLength = dataArray.Length;
            int[,] tempDataTable = new int[dataLength, lineLength];
            int[] tempDataArray = new int[dataLength];
            bool isReset = ((lineLength + 1) % numColumns) == 0;


            for (int column = 0; column < lineLength; column++)
            {
                Console.WriteLine("Line: " + column);
                string[] result = MixNumber(Utils.ReduceDuplicateNumberInArray(lines[column].Split('\t')));
                int resultLength = result.Length;
                int count = 0;
                int columnCount = 0;
                string lineVal = "";
                for (int row = 0; row < dataLength; row++)
                {
                    lineVal = dataArray[row];
     
                    if (count < resultLength && lineVal == result[count])
                    {
                        tempDataArray[row] = 0;
                        count++;
                    }
                    else
                    {
                        tempDataArray[row] = tempDataArray[row] + 1;
                    }

                    if (lineLength - column <= numColumns)
                    {
                        tempDataTable[row, columnCount] = tempDataArray[column];
                        columnCount++;
                    }

                }
            }
            WriteData(tempDataTable, digitType);
        }
    }
}
