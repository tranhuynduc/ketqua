using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    public abstract class Digit
    {
        public enum Digits
        {
            TWO_DIGITS = 2,
            THREE_DIGITS = 3,
            FOURS_DIGITS = 4,
        }
        protected int amountOfNumber;
        protected string filePath;
        protected string dataPath;
        protected int limitDataLine = 0;
        protected int numberPerMessage = 1;

        public Digit()
        {
            filePath = Variables.RESULT_DIRECTORY;

        }
        public Digits digitType;
        abstract public string[] GenerateNumber();
        abstract public string[] MixNumber(string[] strArray, bool isSkip = true);

        private void WriteData(int[] data, Digits digitType)
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


        private void WriteMaxData(int[] data)
        {
            string[] dataArray = GenerateNumber();
            int length = dataArray.Length;
            string[] tempArray = new string[length];

            for (int i = 0; i < length; i++)
            {
                tempArray[i] = dataArray[i] + '\t' + data[i];
            }
            int number = (int)digitType;
            string tempPath = Variables.RESULT_DIRECTORY + "highest-" + number + ".txt";
            Utils.WriteDataToFile(tempArray, tempPath);


        }
        private void WriteData(int[,] table, Digits digitsType)
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

        public void CalculateSingle()
        {
            Console.WriteLine("===== Calculate Single ======= ");
            Variables.START_TIME = Utils.GetTime();
            string[] lines = Utils.ReadFile();
            int lineLength = lines.Length;
            string[] dataArray = GenerateNumber();
            int dataLength = dataArray.Length;
            int[] tempDataArray = new int[dataLength];
            int[] highestArray = new int[dataLength];

            for (int column = 0; column < lineLength; column++)
            {
                Console.WriteLine("Line: " + column);
                string[] result = MixNumber(Utils.ReduceDuplicateNumberInArray(lines[column].Split('\t')));
                int resultLength = result.Length;
                int count = 0;
                string lineVal = "";
                int tmpVal = 0;
                for (int row = 0; row < dataLength; row++)
                {
                    lineVal = dataArray[row];
                    tmpVal = tempDataArray[row];
                    if (count < resultLength && lineVal == result[count])
                    {
                        tempDataArray[row] = 0;
                        
                        count++;
                    }
                    else
                    {
                        
                        tempDataArray[row] = tmpVal + 1;
                    }

                    if (highestArray[row] < tmpVal)
                    {
                        highestArray[row] = tmpVal;
                    }

                }
            }

            WriteData(tempDataArray, digitType);
            WriteMaxData(highestArray);
        }

        public void calculateTable()
        {
            Console.WriteLine("===== Calculate Table ======= ");
            Variables.START_TIME = Utils.GetTime();

            string[] baseLines = Utils.ReadFile();
            int lineLength = baseLines.Length;
            string[] lines = new String[limitDataLine];
            if (limitDataLine != 0 && lineLength > limitDataLine)
            {
                lines = Utils.SubArray(baseLines, lineLength - limitDataLine - 1, limitDataLine);
                lineLength = limitDataLine;
            }
            else
            {
                lines = baseLines;
            }
            string[] dataArray = GenerateNumber();
            int dataLength = dataArray.Length;
            int[,] tempDataArray = new int[dataLength, lineLength];
            int[] highestArray = new int[lineLength];
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
                        if (column == 0)
                        {

                            tempDataArray[row, column] = 1;
                            continue;
                        }

                        tempDataArray[row, column] = tempDataArray[row, column - 1] + 1;

                    }

                    if (highestArray[row] < tempDataArray[row,column]) {
                        highestArray[row] = tempDataArray[row, column];
                    }
                }
            }
            WriteData(tempDataArray, digitType);
            WriteMaxData(highestArray);

        }

        public void calcualteLimitTable(int numColumns = 5)
        {
            Console.WriteLine("===== Calculate Limit Table ======= ");
            Variables.START_TIME = Utils.GetTime();

            string[] lines = Utils.ReadFile();
            int lineLength = lines.Length;
            string[] dataArray = GenerateNumber();
            int dataLength = dataArray.Length;
            int[,] tempDataTable = new int[dataLength, numColumns];
            int[] tempDataArray = new int[dataLength];
            bool isReset = ((lineLength + 1) % numColumns) == 0;

            bool isStart = false;
            int columnCount = 0;

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
                        tempDataArray[row] = 0;
                        count++;
                    }
                    else
                    {
                        tempDataArray[row] = tempDataArray[row] + 1;
                    }

                    if (isStart)
                    {
                        
                        tempDataTable[row, columnCount - 1] = tempDataArray[row];
                    }
                    

                }
                if (lineLength - column <= numColumns)
                {
                    isStart = true;
                    columnCount++;
                }
            }
            WriteData(tempDataTable, digitType);
        }

        public void GetResultOfDay()
        {
            Console.WriteLine("===== Calculate Single ======= ");
            Variables.START_TIME = Utils.GetTime();
            string[] lines = Utils.ReadFile(Variables.DATABASE_NEW_FILE);
            string[] result = MixNumber(Utils.ReduceDuplicateNumberInArray(lines[0].Split('\t')));
            Utils.WriteDataToFile(result, filePath);
        }

        public void GetCorrectNumber()
        {
            int number = (int)digitType;
            string path = Variables.DATABASE_DIRECTORY + "play-number-" + number + ".txt";
            string pathCorrect = Variables.RESULT_DIRECTORY + "correct-number-" + number + ".txt";
            string pathWrong = Variables.RESULT_DIRECTORY + "wrong-number-" + number + ".txt";
            Console.WriteLine("===== Calculate Single ======= ");
            Variables.START_TIME = Utils.GetTime();
            string[] playNumberOfDay = Utils.ReadFile(path);
            int lineLength = playNumberOfDay.Length;
            string[] resultOfDay = Utils.ReadFile(filePath);
            int dataLength = resultOfDay.Length;
            int[] tempDataArray = new int[lineLength];
            int count = 0;
            for (int line = 0; line < lineLength; line++)
            {
                string playNumber = playNumberOfDay[line];
                tempDataArray[line] = 1;
                for (int row = 0; row < dataLength; row++)
                {
                    if (playNumber == resultOfDay[row])
                    {
                        tempDataArray[line] = 0;
                        count++;
                    }
                }
            }

            string[] correctNumber = new string[count];
            string[] wrongNumber = new string[lineLength - count];
            int correct = 0;
            int wrong = 0;
            for (int i = 0; i < lineLength; i++)
            {
                if (tempDataArray[i] == 0)
                {
                    correctNumber[correct] = playNumberOfDay[i] + '\t' + tempDataArray[i];
                    correct++;
                }
                else
                {
                    wrongNumber[wrong] = playNumberOfDay[i] + '\t' + tempDataArray[i];
                    wrong++;
                }
            }

            Utils.WriteDataToFile(correctNumber, pathCorrect);
            Utils.WriteDataToFile(wrongNumber, pathWrong);
        }

        public string GetHighestValue()
        {
            return "";
        }

        public void GenerateMessage()
        {
            string path = Variables.DATABASE_DIRECTORY + "message-number-" + (int)digitType + ".txt";
            string pathResult = Variables.RESULT_DIRECTORY + "result-message-number-" + (int)digitType + ".txt";
            string[] lines = Utils.ReadFile(path);
            int length = lines.Length;
            string[] messages = new string[(length / numberPerMessage) + 2];
            string message = "";
            string baseMesssage = message += "Lô xiên " + (int)digitType;
            string endMessage = "x2.";
            int count = -1;
            int numLine = (length / numberPerMessage + 1);
            message += "Lô xiên " + (int)digitType;

            string tempString = "";
            for (int i = 0; i < lines.Length; i++)
            {
                int rows = i % (numberPerMessage + 1);
                if (rows == 0)
                {
                    count++;
                    messages[count] = baseMesssage;
                }
                var s = lines[i];
                messages[count] += "." + GetMessage(s);

                if (rows == 23)
                {
                    messages[count] += endMessage;
                }
            }

            messages[count] += endMessage;
            Utils.WriteDataToFile(messages, pathResult);
        }

        abstract public string GetMessage(string str);
    }
}
