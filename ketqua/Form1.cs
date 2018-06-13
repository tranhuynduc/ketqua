using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ketqua.Digits;
namespace ketqua
{
    public partial class Form1 : Form
    {
        private TwoDigits twoDigits;
        private ThreeDigits threeDigits;
        private FourDigits fourDigits;
        public Form1()
        {
            InitializeComponent();
            twoDigits = new TwoDigits();
            threeDigits = new ThreeDigits();
            fourDigits = new FourDigits();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("=============start===============");
            Console.WriteLine(Variables.CURRENT_DIRECTORY);
        }

        private void TwoDigitClicked(object sender, EventArgs e)
        {
            twoDigits.calculateTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            threeDigits.calcualteLimitTable(10);
        }

        private void GetDataOfDay(object sender, EventArgs e)
        {
            twoDigits.GetResultOfDay();
            threeDigits.GetResultOfDay();
            fourDigits.GetResultOfDay();
        }

        //private void FindCorrectNumber(object sender, EventArgs e)
        //{
        //    twoDigits.GetCorrectNumber();
        //    threeDigits.GetCorrectNumber();
        //}

        private void button3_Click(object sender, EventArgs e)
        {
            fourDigits.CalculateSingle();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            threeDigits.CalculateSingle();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            twoDigits.CalculateSingle();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            fourDigits.CalculateSingle();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            twoDigits.CalculateSingle();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            threeDigits.CalculateSingle();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            fourDigits.CalculateSingle();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            twoDigits.GenerateMessage();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            threeDigits.GenerateMessage();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            fourDigits.GenerateMessage();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            twoDigits.GetCorrectNumber();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            threeDigits.GetCorrectNumber();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fourDigits.GetCorrectNumber();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Variables.CURRENT_OPEN_DIRECTORY;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string sFileName = openFileDialog.FileName;
                Variables.CURRENT_FILE_NAME = sFileName;
                Variables.CURRENT_OPEN_DIRECTORY = Path.GetDirectoryName(sFileName);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            File.Delete(HeadDigit.fileAppend);

            for (int i = 1; i <= 7; i++)
            {
                
                HeadDigit hd = new HeadDigit(i);
    
                hd.Start();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            string temp = HeadDigit.fileAppend;
            string temp2 = HeadDigit.CURRENT_HEAD_DIGIT_FILE;
            HeadDigit.isGetFull = true;
            HeadDigit.fileAppend = HeadDigit.fileAppendFull;
            HeadDigit.CURRENT_HEAD_DIGIT_FILE = HeadDigit.CURRENT_HEAD_DIGIT_FULL_FILE;
            File.Delete(HeadDigit.fileAppend);

            for (int i = 1; i <= 7; i++)
            {
                HeadDigit hd = new HeadDigit(i);


                hd.Start(true);
            }
            HeadDigit.isGetFull = false;
            Console.WriteLine("eng======");
            HeadDigit.fileAppend = temp;
            HeadDigit.CURRENT_HEAD_DIGIT_FILE = temp2;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            HeadDigit.isGetAll = true;
            HeadDigit.indexData = 0;
            HeadDigit.data = Utils.ReadFile(HeadDigit.fileAppendFull);
            
            File.Delete(HeadDigit.fileAppend);

            for (int i = 1; i <= 7; i++)
            {

                HeadDigit hd = new HeadDigit(i);

                hd.Start();
            }
            HeadDigit.isGetAll = false;
        }

        private void button21_Click(object sender, EventArgs e)
        {

            File.Delete(HeadDigit.fileContinuily);

            for (int i = 1; i <= 7; i++)
            {
                HeadDigit hd = new HeadDigit(i);
                hd.ContinuesDegits();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            File.Delete(HeadDigit.fileContinuily);
            string temp2 = HeadDigit.CURRENT_HEAD_DIGIT_FILE;
            HeadDigit.CURRENT_HEAD_DIGIT_FILE = HeadDigit.CURRENT_HEAD_DIGIT_FULL_FILE;

            for (int i = 1; i <= 7; i++)
            {
                HeadDigit hd = new HeadDigit(i);
                hd.ContinuesDegits();
            }
            HeadDigit.CURRENT_HEAD_DIGIT_FILE = temp2;

        }

        private void button23_Click(object sender, EventArgs e)
        {
            File.Delete(HeadDigit.fileMax);
            for (int i = 1; i <= 7; i++)
            {
                HeadDigit hd = new HeadDigit(i);
                hd.StartCalculateMax();
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            DropDigits dg = new DropDigits();
            File.Delete(DropDigits.fileResultDropDigits);
            dg.StartDropDigits();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            ConnectDatabase cn = new ConnectDatabase();
            cn.StartInsert();
        }
    }
}
