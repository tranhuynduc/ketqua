using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    class DropDigits
    {
        public static string FILE_DATA_DROP_FORWARD_DITGITS= Variables.DATABASE_DIRECTORY + "data-lo-roi-xuoi.txt";
        public static string FILE_DATA_DROP_BACKWARD_DITGITS = Variables.DATABASE_DIRECTORY + "data-lo-roi-nguoc.txt";
        public static string FILE_DATA_RESULT_DROP_DITGITS = Variables.DATABASE_DIRECTORY + "data-kq-lo-roi.txt";
        //public ConnectDatabase cn = new ConnectDatabase();
        public static string fileResultDropDigits = Variables.RESULT_DIRECTORY + "ket-qua-lo-roi.txt";
        public static int[] currentDropDigitCount;
        public static long[] arrayRatingForward;
        public static long[] arrayRatingBackward;
        public static int ratingIndex = 0;
        public static bool isRatingForward = true;

        public void StartDropDigits()
        {
            currentDropDigitCount = null;

            // Read data
            //string[] dataDropForwardDigits = Utils.ReadFile(FILE_DATA_DROP_FORWARD_DITGITS);
            //string[] dataDropBackwardDigits = Utils.ReadFile(FILE_DATA_DROP_BACKWARD_DITGITS);
            //string[] dataResultDropDigits = Utils.ReadFile(FILE_DATA_RESULT_DROP_DITGITS);

            string[] dataDropForwardDigits = Variables.cn.GetData(ConnectDatabase.IDataType.DropDigitForward, 25);
            string[] dataDropBackwardDigits = Variables.cn.GetData(ConnectDatabase.IDataType.DropDigitBackward, 25);
            string[] dataResultDropDigits = Utils.ReduceDuplicateNumberInArray(Variables.cn.GetData(ConnectDatabase.IDataType.AllTwoDigit, 25));
            currentDropDigitCount = new int[dataDropForwardDigits[0].Length];

            for (int i = 1; i < dataDropForwardDigits.Length; i++)
            {
                ////isRatingForward = true;
                ratingIndex = i;
                if (i == 1)
                {
                    arrayRatingForward = new long[dataDropForwardDigits.Length];
                    CalcuateDropDataDigits(dataDropForwardDigits[i], dataDropForwardDigits[i - 1], dataResultDropDigits[i], "xuoi", dataResultDropDigits[i + 1]);
                } else if (i == dataDropBackwardDigits.Length - 1) {
                    CalcuateDropDataDigits(dataDropForwardDigits[i], dataDropForwardDigits[i - 1], dataResultDropDigits[i], "");
                } else
                {
                    CalcuateDropDataDigits(dataDropForwardDigits[i], dataDropForwardDigits[i - 1], dataResultDropDigits[i], "", dataResultDropDigits[i +1]);
                }
            }

            currentDropDigitCount = new int[dataDropBackwardDigits[0].Length];

            for (int i = 1; i < dataDropBackwardDigits.Length; i++)
            {
                isRatingForward = false;
                ratingIndex = i;
                if (i == 1)
                {
                    arrayRatingBackward = new long[dataDropBackwardDigits.Length];
                    CalcuateDropDataDigits(dataDropBackwardDigits[i], dataDropBackwardDigits[i - 1], dataResultDropDigits[i], "nguoc", dataResultDropDigits[i + 1]);
                }
                else if (i == dataDropBackwardDigits.Length - 1)
                {
                    CalcuateDropDataDigits(dataDropBackwardDigits[i], dataDropBackwardDigits[i - 1], dataResultDropDigits[i], "");
                } else
                {
                    CalcuateDropDataDigits(dataDropBackwardDigits[i], dataDropBackwardDigits[i - 1], dataResultDropDigits[i], "", dataResultDropDigits[i + 1]);

                }
            }
        }

        private long CalculateRating(long[] array)
        {
            long tempLong = 0;
            for (int i = 0; i < array.Length; i++)
            {
                tempLong += array[i];
            }
            return tempLong / array.Length;
        }

        public void CalcuateDropDataDigits(string current, string last, string result, string message = "", string next = "")
        {
            string[] currentArray = current.Split('\t');
            string[] lastArray = last.Split('\t');
            string[] resultArray = result.Split('\t');
            string[] nextArray = next.Split('\t');
            int[] temp = new int[lastArray.Length];
            string[] writeData = new string[3];
            string check = CheckAppear(current);
            string[] checkArray = CheckAppearArray(current);
            int count = 0;
            string number = "";
            for (int i = 0; i < checkArray.Length; i++)
            {
                if (nextArray.Contains(checkArray[i]))
                {
                    count++;
                    number += checkArray[i] + " ";
                }
            }
            long tempLong = 0;

            if (isRatingForward)
            {
                arrayRatingForward[ratingIndex] = (count * 100 / checkArray.Length);
                if (next == "")
                {
                    tempLong = CalculateRating(arrayRatingForward);
                }
            }
            else
            {

                arrayRatingBackward[ratingIndex] = (count * 100 / checkArray.Length);
                if (next == "")
                {
                    tempLong = CalculateRating(arrayRatingBackward);
                }
            }

            if (message != "")
            {
                message += '\n';
            }
            
            writeData[0] =(tempLong == 0 ? "" : ("Tổng tỉ lệ xuất hiện trung bình: " + tempLong.ToString() + "%\n")) + message + currentArray[0] +  "\t" + "Xuất hiện >= 3:\t\t\t\t" + check + "\t\t\t\t\t\t\t\t\t\t\t Về ngày sau:  " + number + "\t\t\t\t\t\t\t\tTỉ lệ: " + count + "/" + checkArray.Length + "\t\t\t\tTỉ lệ: " + (checkArray.Length == 0 ? "" : ((long)(count * 100/checkArray.Length)).ToString()) + "%";
            writeData[1] = current;
            writeData[2] = "\t";
            for (int i = 1; i < lastArray.Length; i++)
            {
                if (resultArray.Contains(lastArray[i]))
                {
                    temp[i] = 0;
                    currentDropDigitCount[i] = 0;
                } else
                {
                    currentDropDigitCount[i]++;
                    temp[i] = currentDropDigitCount[i];
                }
                if(checkArray.Contains(currentArray[i]))
                {
                    writeData[2] += temp[i].ToString() + "**" + '\t';
                }
                else
                {
                    writeData[2] += temp[i].ToString() + '\t';
                }
                
            }
            
            Utils.WriteFile(writeData, fileResultDropDigits, true);
        }

        private string CheckAppear(string data)
        {
            string[] dataArray = data.Split('\t').Skip(1).ToArray();
            var query = dataArray.GroupBy(r => r)
                .Select(grp => new
                {
                    Value = grp.Key,
                    Count = grp.Count()
                });
            int length = query.Count();
            string[] number = new string[length];
            int count = 0;
            string temp = "";
            foreach (var item in query)
            {
                Console.WriteLine("Value: {0}, Count: {1}", item.Value, item.Count);
                if (item.Count >= 3)
                {
                    temp += item.Value + " "; 
                    number[count] = item.Value;
                    count++;
                }
            }

            return temp;
        }

        private string[] CheckAppearArray(string data)
        {
            string[] dataArray = data.Split('\t').Skip(1).ToArray();
            var query = dataArray.GroupBy(r => r)
                .Select(grp => new
                {
                    Value = grp.Key,
                    Count = grp.Count()
                });
            int length = query.Count();
            string[] number = new string[length];
            int count = 0;
            string temp = "";
            foreach (var item in query)
            {
                Console.WriteLine("Value: {0}, Count: {1}", item.Value, item.Count);
                if (item.Count >= 3)
                {
                    temp += item.Value + " ";
                    number[count] = item.Value;
                    count++;
                }
            }
            var len = 0;
            for (int i = 0; i < number.Length; i++)
            {
                if (number[i] == null)
                {
                    len = i;
                    break;
                }
            }


            return number.Take(len).ToArray();
        }
    }
}
