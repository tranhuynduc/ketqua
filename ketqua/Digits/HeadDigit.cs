using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    public class HeadDigit
    {
        public int numDigit = 1;
        public static string fileContinuily = Variables.RESULT_DIRECTORY + "so-lien-tiep.txt";
        public static string fileMax = Variables.RESULT_DIRECTORY + "so-dau-lau-nhat.txt";
        public static string fileAppend = Variables.RESULT_DIRECTORY + "dau-cuoi-tong.txt";
        public static string fileAppendFull = Variables.RESULT_DIRECTORY + "dau-cuoi-tong-full.txt";
        public static bool isGetFull = false;
        public static bool isGetAll = false;
        public static int indexData = 0;
        public static int count40 = 0;
        public string currentNumber = "";
        public static string[] data;
        public HeadDigit(int numDigit = 1)
        {
            this.numDigit = numDigit;
        }
        public static string CURRENT_HEAD_DIGIT_FILE = Variables.CURRENT_DIRECTORY + "so-dau.txt";
        public static string CURRENT_HEAD_DIGIT_FULL_FILE = Variables.CURRENT_DIRECTORY + "so-dau-full.txt";

        public static bool OnlyOnceCheck(string input)
        {
            return input.Distinct().Count() == input.Length;
        }

        public static string RemoveDuplicates(string input)
        {
            var a = input.Distinct();
            var b = input.ToCharArray().Distinct().ToArray();
            return new string(input.ToCharArray().Distinct().ToArray());
        }

        public void StartCalculateMax()
        {
            //string[] lines = Utils.ReadFile(CURRENT_HEAD_DIGIT_FILE);
            string[] lines = Variables.cn.GetData(ConnectDatabase.IDataType.SpecialTwoDigit, 100);
            string[] data = lines[0].Split(' ');
            if (data.Length < 2)
            {
                data = data[0].Split('\t');
            }
            int length = data.Length;
            string[] head = new string[length];
            string[] tail = new string[length];
            string[] sum = new string[length];
            for (int i = 0; i < length; i++)
            {
                string number = data[i];
                head[i] = number.Substring(0, 1);
                tail[i] = number.Substring(1, 1);
                sum[i] = ((Int32.Parse(head[i]) + Int32.Parse(tail[i])) % 10).ToString();
            }

            CalculateMax(head, "Đầu max:" + numDigit);
            CalculateMax(tail, "Đuôi max:" + numDigit);
            CalculateMax(sum, "tổng max:" + numDigit);
        }

        public void CalculateMax(string[] arrData, string message = "")
        {
            int length = arrData.Length;
            int[,] table = new int[10, length];
            for (int i = 0; i < length; i++)
            {
                int value = Int32.Parse(arrData[i]);
                for (int j = 0; j < 10; j++)
                {
                    if (j == value || i == 0)
                    {
                        table[j, i] = 0;
                    } else
                    {
                        table[j, i] = table[j, i - 1] + 1;
                    }
                }
            }
            FindLatest(table, 10 - numDigit);
            string[] tempWrite = new string[length + 1];
            int count = 0;
            for (int i = 10 -numDigit + 1; i < length + 1; i++)
            {
                tempWrite[i] = arrData[i-1] + '\t' + currentNumber + "@" + count.ToString();
                if (i == length)
                {
                    break;
                }
                int foundIndex = currentNumber.IndexOf(arrData[i]);
                if (foundIndex != -1)
                {
                    count = 0;
                    FindLatest(table, i);
                } else
                {
                    count++;
                }
            }

            string[] finalResult = new string[10];
            finalResult[0] = '\n' + message;
            for (int i = 1; i < 10; i++)
            {
                finalResult[i] = tempWrite[length - 9 + i ].Replace("@", new string('\t', i));
            }

            Utils.WriteFile(finalResult, fileMax, true);
            //Utils.WriteTable(table, Variables.RESULT_DIRECTORY + "test-2.txt");
        }

        private void FindLatest(int[,] table, int column)
        {
            currentNumber = "";
            int[] temp = new int[10];
            for (int i = 0; i < 10; i++)
            {
                temp[i] = table[i, column];
            }
            int[] sortedCopy = temp.OrderBy(i => i).Reverse().ToArray();
            int[,] tempTable = new int[10, 2];
            for (int i = 0; i < 10; i++)
            {
                int value = sortedCopy[i];
                int position = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (table[j, column] == value)
                    {
                        position = j;
                        break;
                    }
                }
                tempTable[i, 0] = position;
                tempTable[i, 1] = value;
            }
            for (int i = 0; i < numDigit; i++)
            {
                currentNumber += tempTable[i,0];
            }

        }

        private int[,] SortTable(int[,] table, string message = "")
        {
            int row = table.GetLength(0);
            int column = table.GetLength(1);
            int[] temp = new int[10];

            for (int i = 0; i < 10; i++)
            {
                temp[i] = table[i, column - 1];
            }

            int[] sortedCopy = temp.OrderBy(i => i).Reverse().ToArray();
            int[,] tempTable = new int[10, 2];
            for (int i = 0; i < 10; i++)
            {
                int value = sortedCopy[i];
                int position = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (table[j, 1] == value)
                    {
                        position = j;
                        break;
                    }
                }
                tempTable[i, 0] = table[position, 0];
                tempTable[i, 1] = value;
            }
            return tempTable;
        }
        public void Start(bool isFull = false)
        {
            string[] lines = Variables.cn.GetData(ConnectDatabase.IDataType.SpecialTwoDigit, isFull ? 5000 : 100);

            string[] data = lines[0].Split(' ');
            if (data.Length < 2)
            {
                data = data[0].Split('\t');
            }
            int length = data.Length;
            string[] head = new string[length];
            string[] tail = new string[length];
            string[] sum = new string[length];
            for (int i = 0; i < length; i++)
            {
                string number = data[i];
                head[i] = number.Substring(0, 1);
                tail[i] = number.Substring(1, 1);
                sum[i] = ((Int32.Parse(head[i]) + Int32.Parse(tail[i])) % 10).ToString();
            }

            string[] headData = GenerateData(head, "Đầu " + numDigit );
            string[] tailData = GenerateData(tail, "Đuôi " + numDigit);
            string[] sumData = GenerateData(sum, "Tổng " + numDigit);

            Utils.WriteFile(headData, fileAppend, true);
            Utils.WriteFile(tailData, fileAppend, true);
            Utils.WriteFile(sumData, fileAppend, true);
        }



        public string[] GenerateData(string[] strArr, string message)
        {
            string tempStr = "";
            int length = strArr.Length;
            int tempCount = numDigit;
            var count = numDigit;
            

            for (int i = 0; i < tempCount; i++)
            {
               
 
                tempStr += strArr[i];
                if (!OnlyOnceCheck(tempStr))
                {
                    tempStr = tempStr.Remove(tempStr.IndexOf(strArr[i]),1);
                    tempCount++;
                    count++;
                }
            }

            int offSet = count;
            string[] result = new string[length - count + 1];
            result[0] = tempStr;
            int index = 0;

            int[] tempInt = new int[length - count + 1];
            tempInt[0] = 0;
            for (int i = count; i < length; i++)
            {
                string value = strArr[i];
                int foundIndex = result[index].IndexOf(value);
                index++;
                if (foundIndex == -1)
                {
                    result[index] = result[index - 1].Remove(0, 1) + value;
                    if (index == 1)
                    {
                        tempInt[index - 1] = 1;
                    } else
                    {
                        tempInt[index - 1] = tempInt[index - 2] + 1;
                    }
                }
                else
                {
                    result[index] = result[index - 1].Remove(foundIndex, 1) + value;
                    tempInt[index - 1] = 0;
                }
            }
            if (isGetFull)
            {
                return GetMax(tempInt, message);
            } 
            return CalculateResult(result, tempInt, message, offSet);
        }

        public string[] GetMax(int[] intArr, string message)
        {
            var max = 0;
            bool isCounting = false;
            int tempInt = 40;
            count40 = 0;
            for (int i = 0; i < intArr.Length; i++)
            {

                if (max < intArr[i])
                {
                    max = intArr[i];
                    
                }


                if (intArr[i] > tempInt)
                {
                    if (!isCounting)
                    {
                        isCounting = true;
                        count40++;
                    }
                } else
                {
                    isCounting = false;
                }
       
            }
            string[] temp = new string[1];
            temp[0] = message + " === Max: " + max.ToString();

            if (numDigit == 1)
            {
                temp[0] += " so lan xuat hien tren 40: " + count40;
            }
            return temp;
        }

        public string[] CalculateResult(string[] strArr, int[] intArray, string message, int offset = 0)
        {
            string[] tempArray = new string[strArr.Length + 1];
            if (isGetAll)
            {
                tempArray[0] = data[indexData];
                indexData++;
            } else
            {
                tempArray[0] = message;
            }
            for (int i = 0; i < strArr.Length - 1; i++)
            {
                string count = intArray[i] == 0 ? "x" : intArray[i].ToString();
                tempArray[i + 1] = strArr[i] + new string('\t', i + offset + 1) + count;
            }
            int tempCount = 1;
            string[] newTempArray = new String[4];
            newTempArray[0] = tempArray[0];
            for ( int i = strArr.Length - 3; i < strArr.Length; i++)
            {
                newTempArray[tempCount] = tempArray[i].Replace("\t", "").Insert(numDigit, new String('\t', tempCount));
                tempCount++;
            }
            return newTempArray;
        }
        public void ContinuesDegits()
        {
            string[] lines = Variables.cn.GetData(ConnectDatabase.IDataType.SpecialTwoDigit, 100);

            string[] data = lines[0].Split(' ');
            if (data.Length < 2)
            {
                data = data[0].Split('\t');
            }
            int length = data.Length;
            string[] head = new string[length];
            string[] tail = new string[length];
            string[] sum = new string[length];
            for (int i = 0; i < length; i++)
            {
                string number = data[i];
                head[i] = number.Substring(0, 1);
                tail[i] = number.Substring(1, 1);
                sum[i] = ((Int32.Parse(head[i]) + Int32.Parse(tail[i])) % 10).ToString();
            }

            CalculateContinuityDigits(head, "Đầu " + numDigit);
            CalculateContinuityDigits(tail, "Đuôi " + numDigit);
            CalculateContinuityDigits(sum, "Tổng " + numDigit);

        }

        private string[] GetContinuityMax(int[,] table)
        {
            int length = table.GetLength(1);
            string[] maxData = new string[10];
            for (int i = 0; i< 10; i++)
            {
                int max = 0; 
                for (int j = 0; j < length; j++)
                {
                    int v = table[i, j];
                    if (max < v)
                    {
                        max = v;
                    }
                }
                maxData[i] = max.ToString();
            }
            return maxData;
        }

        private void CalculateContinuityDigits(string[] arrData, string message)
        {
            int[,] table = new int[10, arrData.Length + 1];
            for (int i = 0; i < arrData.Length; i++)
            {
                int currentData = Int32.Parse(arrData[i]);
                for (int j = 0; j < 10; j++)
                {
                    table[j, i + 1] = table[j, i] + 1;

                }
                for (int j = currentData + 10; j > currentData + 10 - numDigit; j--)
                {
                    int temp = j % 10;
                    table[temp, i + 1] = 0;
                }
            }
            if (numDigit == 1)
            {
                WriteMax(table, message);
            }
            BeforeWriteContinuityDigits(table, message);
        }

        private void WriteMax(int[,] table, string message = "")
        {
            int length = table.GetLength(1);
            int[,] arrString = new int[10, 2];
            for (int index = 0; index < 10; index++)
            {
                arrString[index, 0] = index;
                arrString[index, 1] = table[index, length - 1];
            }

            string[] writeData = new string[10];

            int[,] sortedTable = SortTable(arrString, message);

            string[] resultWriteData = GetMaxDigit(sortedTable, message);
            for (int i = 0; i < 10; i++)
            {
                writeData[i] = sortedTable[i, 0].ToString() + '\t' + sortedTable[i, 1].ToString();
                
            }

            
            Utils.WriteFile(writeData, Variables.RESULT_DIRECTORY + "dau-duoi-lau-nhat.txt", true);
            Utils.WriteFile(resultWriteData, Variables.RESULT_DIRECTORY + "dau-duoi-lau-nhat.txt", true);

        }

        

        private string[] GetMaxDigit(int[,] table, string message = "")
        {
            string[] resultWriteData = new string[12];
            resultWriteData[0] = message;
            for (int i = 0; i < 7; i++)
            {
                string temp = "";
                for (int j = 0; j <= i; j++)
                {
                    temp += table[j, 0].ToString();
                }
                resultWriteData[i + 1] = temp;
            }
            return resultWriteData;
        }



        private void BeforeWriteContinuityDigits(int[,] table, string message = "")
        {
            string[] arrayDigits = GenerateDigits(numDigit);
            string[] writeData = new string[arrayDigits.Length + 1];
            int length = table.GetLength(1);
            string[] maxData = GetContinuityMax(table);
            writeData[0] = message;
            int count = length > 10 ? 10 : length;
            for (int i = 0; i < 10; i++)
            {
                string temp = "";
                for (int j = length - count; j < length; j++)
                {
                    temp += (table[i, j] == 0 ? "x" : table[i,j].ToString()) + '\t';
                }

                writeData[i + 1] = arrayDigits[i] + '\t' + temp + "max = " + maxData[i] + '\t' + '\t';
            }

            Utils.WriteFile(writeData, fileContinuily, true);
        }
        public string[] GenerateDigits(int num)
        {
            string temp = "";
            string[] arrTemp = new string[10];
            for (int i = 0; i <= 9; i++)
            {
                temp = "";
                for (int j = i; j < i + num; j++)
                {
                    int t = j % 10;
                    temp += t.ToString();
                }
                arrTemp[i] = temp;
            }

            return arrTemp;
        }

        
    }

    class OneHead : HeadDigit
    {
        public OneHead()
        {
            numDigit = 1;
        }
        
    }

    class TwoHead : HeadDigit
    {
        public TwoHead()
        {
            numDigit = 2;
        }
    }


    class ThreeHead : HeadDigit
    {
        public ThreeHead()
        {
            numDigit = 3;
        }
    }


    class FourHead : HeadDigit
    {
        public FourHead()
        {
            numDigit = 4;
        }
    }


    class FiveHead : HeadDigit
    {
        public FiveHead()
        {
            numDigit = 5;
        }
    }

    class SixHead : HeadDigit
    {
        public SixHead()
        {
            numDigit = 6;
        }
    }

    class SevenHead : HeadDigit
    {
        public SevenHead()
        {
            numDigit = 7;
        }
    }
}
