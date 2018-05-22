using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ketqua
{
    public partial class Form1 : Form
    {

        private string folderPath = @"D:\project\02-form\database\";
        public Form1()
        {
            InitializeComponent();
        }

        private string pad2(int number)
        {

            return (number < 10 ? "0" : "") + number;
        }

        private void calculate(string[] strArray, int num = 0)
        {
            string strPath = @"D:\project\02-form\ketqua\calculate.txt";
            int count = 0;
            int length = strArray.Length; 
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(strPath))
            {
                for (int i = 0; i < 100; i++)
                {
                    for (int j = i + 1; j < 100; j++)
                    {
                        for (int k = j + 1; k < 100; k++)
                        {
                            for (int l = k + 1; l < 100; l++)
                            {
                                var str = pad2(i) + '_' + pad2(j) + '_' + pad2(k) + '_' + pad2(l);
                                if (count < num && str == strArray[count])
                                {
                                    count++;
                                    file.WriteLine("0");

                                } else
                                {
                                    file.WriteLine("1");
                                }
                                
                            }
                        }
                    }
                }


            }
        }
        private void generateNumber()
        {
            var strPath = @"D:\project\02-form\ketqua\text.txt";

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(strPath))
            {
                for (int i = 0; i < 100; i++)
                {
                    for (int j = i + 1; j < 100; j++)
                    {
                        for (int k = j + 1; k < 100; k++)
                        {
                            for (int l = k + 1; l < 100; l++)
                            {
                                var str = pad2(i) + '_' + pad2(j) + '_' + pad2(k) + '_' + pad2(l);
                                file.WriteLine(str);
                            }
                        }
                    }
                }
            }
        }

        private string[,] makeTable(string[] dataArray, int numColumns) 
        {
            var numRows = dataArray.Length;
            string[,] array = new string[numRows, numColumns + 2];

            for (int i = 0; i < numRows; i++)
            {
                array[i, 0] = dataArray[i];
                array[i, 1] = "0";
            }

            return array;
  
        }

        private int[,] makeTableInt(int rows, int numColumns)
        {
            int[,] array = new int[rows, numColumns + 1];

            for (int i = 0; i < rows; i++)
            {
                array[i, 0] = 0;
            }

            return array;

        }

        private void calculateThreeNumber()
        {
            string[] lines = Read_File();
            int lineLength = lines.Length;
            string[] dataArray = generateThreeNumber();
            int dataLength = dataArray.Length;

            //string[] tempDataArray = new string[] { };
            //string[,] tempDataArray = makeTable(dataArray, lineLength);

            int[,] tempDataArray = makeTableInt(dataLength, lineLength);

            for (int i = 0; i < lineLength; i++)
            {
                Console.WriteLine("Line: " + i);
                string[] result = mixThreeNumber(reduceArray(lines[i].Split('\t')));
                int resultLength = result.Length;
                int count = 0;
                int currentColumn = i + 1;
                string lineVal = "";
                for (int ii = 0; ii < dataLength; ii++)
                {
                    lineVal = dataArray[ii];

                    if (count < resultLength && lineVal == result[count])
                    {
                        tempDataArray[ii, currentColumn] = 0;
                        count++;
                    }
                    else
                    {
                        tempDataArray[ii, currentColumn] = tempDataArray[ii, currentColumn - 1] + 1;
                    }
                }
            }

            WriteData(tempDataArray);
        }

        private void calculateFourNumber()
        {
            string[] lines = Read_File();
            int lineLength = lines.Length;
            int numColumns = 5;
            string[] dataArray = generateFourNumber();
            int dataLength = dataArray.Length;
            //string[] tempDataArray = new string[] { };
            //string[,] tempDataArray = makeTable(dataArray, lineLength);

            //int[,] tempDataArray = makeTableInt(dataLength, numColumns);
            int[] tempDataArray = new int[dataLength];

            for (int i = 0; i < lineLength; i++)
            {
                Console.WriteLine("Line: " + i);
                string[] result = mixFourNumber(reduceArray(lines[i].Split('\t')));
                int resultLength = result.Length;
                int count = 0;
                int currentColumn = i % numColumns + 1;
                string lineVal = "";
                for (int ii = 0; ii < dataLength; ii++)
                {
                    lineVal = dataArray[ii];

                    if (count < resultLength && lineVal == result[count])
                    {
                        tempDataArray[ii] = 0;
                        count++;
                    }
                    else
                    {
                        tempDataArray[ii] = tempDataArray[ii] + 1;
                    }

                }
            }

            WriteDataSingle(tempDataArray);
        }

        private void calculateFourNumberSeperate()
        {
            string[] lines = Read_File();
            int lineLength = lines.Length;
            string[] dataArray = generateFourNumber();
            int dataLength = dataArray.Length;

            //string[] tempDataArray = new string[] { };
            //string[,] tempDataArray = makeTable(dataArray, lineLength);

            int[,] tempDataArray = makeTableInt(dataLength, lineLength);

            for (int i = 0; i < lineLength; i++)
            {
                Console.WriteLine("Line: " + i);
                string[] result = mixFourNumber(reduceArray(lines[i].Split('\t')));
                int resultLength = result.Length;
                int count = 0;
                int currentColumn = i + 1;
                string lineVal = "";
                for (int ii = 0; ii < dataLength; ii++)
                {
                    lineVal = dataArray[ii];
                    if (lineVal == "01_11_17_19")
                    {
                        Console.WriteLine("debug");
                    }
                    if (count < resultLength && lineVal == result[count])
                    {
                        tempDataArray[ii, currentColumn] = 0;
                        count++;
                    }
                    else
                    {
                        tempDataArray[ii, currentColumn] = tempDataArray[ii, currentColumn - 1] + 1;
                    }
                }
            }

            WriteData(tempDataArray, false);
        }

        private string[] convertData(int[,] table, bool isThree = true)
        {
            Console.WriteLine("Conver Data");
            string[] dataArray = isThree ? generateThreeNumber() : generateFourNumber();
            int numRows = table.GetLength(0);
            int numColumns = table.GetLength(1);
            Console.WriteLine("Conver Data" + numRows + " ", + numColumns);
            string[] tempArray = new string[isThree ? countThreeNumber() : countFourNumber()];
            int start = isThree ? numColumns - 40 : 0;
            numColumns = isThree ? numColumns : numColumns - 1;

            for (int i = 0; i < numRows; i++)
            {
                string tempString = dataArray[i];
                for (int j = start; j < numColumns; j++)
                {
                    tempString += "\t" + table[i, j];
                }

                tempArray[i] = tempString;
            }

            return tempArray;
        }

        private string[] convertDataSingle(int[] data)
        {
            Console.WriteLine("Conver Data");
            string[] dataArray = generateFourNumber();
            int length = countFourNumber();
            string[] tempArray = new string[length];

            for (int i = 0; i < length; i++)
            {

                tempArray[i] = dataArray[i] + '\t' + data[i];
            }

            return tempArray;
        }


        private void WriteDataSingle(int[] dataArray = null)
        {
            Console.WriteLine("Write Data");
            string strPath = folderPath + @"result\result-single-4.txt";
            string[] arrayData = convertDataSingle(dataArray);

            int length = arrayData.Length;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(strPath))
            {
                for (int i = 0; i < length; i++)
                {
                    file.WriteLine(arrayData[i]);
                }
            }
            Console.WriteLine("start:" + start);
            Console.WriteLine("end: " + DateTime.Now.ToString("h:mm:ss tt"));
            System.Console.WriteLine("============Complete =============== ");
        }
        private void WriteData(int[,] table = null, bool isThree = true)
        {
            Console.WriteLine("Write Data");
            string strPath = folderPath + @"result\result-" + (isThree ? "3" : "4" ) + ".txt";
            string[] arrayData = convertData(table, isThree);
            int length = arrayData.Length;
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(strPath))
            {
                for (int i = 0; i < length; i++)
                {
                    file.WriteLine(arrayData[i]);
                }
            }
            Console.WriteLine("start:" + start);
            Console.WriteLine("end: " + DateTime.Now.ToString("h:mm:ss tt"));
            System.Console.WriteLine("============Complete =============== ");
        }

        private string[] mixThreeNumber(string[] strArray, bool isSkip = true)
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

        private string[] mixFourNumber(string[] strArray, bool isSkip = true)
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
                        for (int l = k + 1; l < 100; l++)
                        {
                            count++;
                        }
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
                        for (int l = k + 1; l < arrayLength; l++) {
                            tempArray[count] = strArray[i] + '_' + strArray[j] + '_' + strArray[k] + '_' + strArray[l];
                            count++;
                        }
                    }
                }
            }
            return tempArray;
        }
        private static int COUNT = 0;
        private int countThreeNumber()
        {
            COUNT = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = i + 1; j < 100; j++)
                {
                    for (int k = j + 1; k < 100; k++)
                    {
                        COUNT++;
                    }
                }
            }
            Console.WriteLine("Count:" + COUNT);
            return COUNT;
        }

        private int countFourNumber(bool needCount = false)
        {
            if (!needCount)
            {
                return 3921225;
            }
            COUNT = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = i + 1; j < 100; j++)
                {
                    for (int k = j + 1; k < 100; k++)
                    {
                        COUNT++;
                    }
                }
            }
            Console.WriteLine("Count:" + COUNT);
            return COUNT;
        }
        private string[] generateThreeNumber()
        {
            int numRows = countThreeNumber();
            string[] strArray = new string[numRows];
            int count = 0;
            for (int i = 0; i < 100; i++)
            {
                for (int j = i + 1; j < 100; j++)
                {
                    for (int k = j + 1; k < 100; k++)
                    {
                        strArray[count] = pad2(i) + '_' + pad2(j) + '_' + pad2(k);
                        count++;
                    }
                }
            }
            return strArray;
        }

        private string[] generateFourNumber()
        {
            int numRows = countFourNumber();
            string[] strArray = new string[numRows];
            int count = 0;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("index: " + i);

                for (int j = i + 1; j < 100; j++)
                {
                    for (int k = j + 1; k < 100; k++)
                    {
                        for (int l = k + 1; l < 100; l++)
                        {
                            strArray[count] = pad2(i) + '_' + pad2(j) + '_' + pad2(k) + '_' + pad2(l);
                            count++;
                        }
                    }
                }
            }
            return strArray;
        }
        private void generateNumberThree()
        {

            var strPath = @"D:\project\02-form\ketqua\text.txt";

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(strPath))
            {
                for (int i = 0; i < 100; i++)
                {
                    for (int j = i + 1; j < 100; j++)
                    {
                        for (int k = j + 1; k < 100; k++)
                        {
                            var str = pad2(i) + '_' + pad2(j) + '_' + pad2(k);
                            file.WriteLine(str);
                        }
                    }
                }
            }
        }

        private string start = "";
        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("=============start===============");
            start = DateTime.Now.ToString("h:mm:ss tt");
            Console.WriteLine(start);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            calculateThreeNumber();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            calculateFourNumber();
        }

        private string[] Read_File()
        {
            string path = @"D:\project\02-form\database\data.txt";
            Console.WriteLine("Contents start write  = ");
            string[] lines = System.IO.File.ReadAllLines(path);
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            return lines;
        }

        private string[] reduceArray(string[] arrayString)
        {
            arrayString = arrayString.Distinct().ToArray();
            return arrayString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] arrayString = { "01", "10", "11", "14", "19", "23", "26", "32", "34", "40", "45", "46", "54", "55", "61", "64", "66", "67", "73", "78", "82", "82", "84", "84", "87", "88", "90" };
            string[] array = new string[] { };
            int count = 0;
            arrayString = arrayString.Distinct().ToArray();
            int len = arrayString.Length;
           
            string[] newArray = new String[50000];
            
            for (int i = 0; i < len; i++)
            {
                for (int j = i + 1; j < len; j++)
                {
                    for (int k = j + 1; k < len; k++)
                    {
                        for (int l = k + 1; l < len; l++)
                        {

                            //array[0] = date;
                            //count++;
                            //array[count] = arrayString[i] + '_' + arrayString[j] + '_' + arrayString[k] + '_' + arrayString[l];
                            newArray[count] = (arrayString[i] + '_' + arrayString[j] + '_' + arrayString[k] + '_' + arrayString[l]);
                            count++;
                        }
                    }
                }
            }
            calculate(newArray, count);
            //var strPath = @"D:\project\02-form\ketqua\data.txt";
            //using (System.IO.StreamWriter file =
            //    new System.IO.StreamWriter(strPath))
            //{
            //    for (int i = 0; i < len; i++)
            //    {
            //        for (int j = i + 1; j < len; j++)
            //        {
            //            for (int k = j + 1; k < len; k++)
            //            {
            //                for (int l = j + 1; l < len; l++)
            //                {

            //                    //array[0] = date;
            //                    //count++;
            //                    //array[count] = arrayString[i] + '_' + arrayString[j] + '_' + arrayString[k] + '_' + arrayString[l];
            //                    file.WriteLine(arrayString[i] + '_' + arrayString[j] + '_' + arrayString[k] + '_' + arrayString[l]);
            //                }
            //            }
            //        }
            //    }
            //}

        }

        private string[] uniq_fast()
        {
            return null;
        }


    }
}
