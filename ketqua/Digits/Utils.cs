using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ketqua.Digits
{
    public class Utils
    {
        // Utils
        public static string GetTime()
        {
            return DateTime.Now.ToString("h:mm:ss tt");
            
        }

        public static bool OnlyOnceCheck(string input)
        {
            return input.Distinct().Count() == input.Length;
        }

        public static string[] SubArray(string[] data, int index, int length)
        {
            string[] result = new string[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }
        // Read/Write File

        public static void WriteTable(int[,] table, string path = null)
        {
            int row = table.GetLength(0);
            int column = table.GetLength(1);
            string[] writeData = new string[row];
            for (int i = 0; i < row; i++)
            {
                string temp = "";
                for (int j = 0; j < column; j++)
                {
                    temp += table[i, j].ToString() + '\t';
                }
                writeData[i] = temp;
            }
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {
                for (int i = 0; i < row; i++)
                {
                    file.WriteLine(writeData[i]);
                }
            }
            Console.WriteLine("Write File Completed: " + path);
        }
        public static string[] ReadFile(string path = null)
        {
            if (Variables.CURRENT_FILE_NAME != "")
            {
                path = Variables.CURRENT_FILE_NAME;
            } else if (path == null)
            {
                path = Variables.DATABASE_FILE;
            }
            Console.WriteLine("Read File: " + path);
            string[] lines = System.IO.File.ReadAllLines(path);
            Console.WriteLine("Read File End.");
            return lines;
        }

        public static bool FindItemInArray(string[] array, string item)
        {
            return array.Contains(item);
        }

        public static void WriteDataToFile(string str, string path = Variables.DATABASE_FOLDER_PATH, bool isSave = true)
        {
            Directory.CreateDirectory(Variables.RESULT_FOLDER_NAME);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
            {

                file.WriteLine(str);
            }
            Console.WriteLine("start: " + Variables.START_TIME);
            Console.WriteLine("end: " + DateTime.Now.ToString("h:mm:ss tt"));
            Console.WriteLine("============ Complete Write Data Single =============== ");
            Console.WriteLine("File Path: " + path);
            string message = "kết quả ở thư mục: " + path;
            string caption = "Hoàn Thành";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
        }

        //public static void WriteDataToFile(string str, string path = Variables.DATABASE_FOLDER_PATH)
        //{
        //    Directory.CreateDirectory(Variables.RESULT_FOLDER_NAME);
        //    using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
        //    {

        //        file.WriteLine(str);
        //    }
        //    Console.WriteLine("start: " + Variables.START_TIME);
        //    Console.WriteLine("end: " + DateTime.Now.ToString("h:mm:ss tt"));
        //    Console.WriteLine("============ Complete Write Data Single =============== ");
        //    Console.WriteLine("File Path: " + path);
        //}

        public static void WriteDataToFile(string[] strArray, string path = Variables.DATABASE_FOLDER_PATH, bool isSave = true)
        {
            Directory.CreateDirectory(Variables.RESULT_FOLDER_NAME);
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
            Console.WriteLine("File Path: " + path);
            string message = "kết quả ở thư mục: " + path;
            string caption = "Hoàn Thành";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);

        }

        public static void WriteFile(string[] arrayData, string path, bool isAppend = false)
        {
            Console.WriteLine("Write File Start.");
            int length = arrayData.Length;
            if (isAppend)
            {
                using (System.IO.StreamWriter file = File.AppendText(path))
                {
                    for (int i = 0; i < length; i++)
                    {
                        file.WriteLine(arrayData[i]);
                    }
                }
                return;
            }
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

        public static void ShowMessage(string message, string caption = "")
        {
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;

            result = MessageBox.Show(message, caption, buttons);
        }

        public static string ReplaceMessage(string message, string replace = "")
        {
            return message.Replace("%s", replace);
        }

        public static int GetOffSetDate()
        {
            DateTime d1 = new DateTime(2000, 01, 01);
            DateTime d2 = DateTime.Today;
            return Convert.ToInt32((d2 - d1).TotalDays);
        }
    }
}
