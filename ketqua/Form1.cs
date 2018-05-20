using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ketqua
{
    public partial class Form1 : Form
    {
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
                                if (str == "01_10_11_14")
                                {
                                    bool debug = false;
                                }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] arrayString = { "01", "10", "11", "14", "19", "23", "26", "32", "34", "40", "45", "46", "54", "55", "61", "64", "66", "67", "73", "78", "82", "82", "84", "84", "87", "88", "90" };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] arrayString = { "01", "10", "11", "14", "19", "23", "26", "32", "34", "40", "45", "46", "54", "55", "61", "64", "66", "67", "73", "78", "82", "82", "84", "84", "87", "88", "90" };
            string date = "20-05-2018";
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
