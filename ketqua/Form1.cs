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

        private void FindCorrectNumber(object sender, EventArgs e)
        {
            twoDigits.GetCorrectNumber();
            threeDigits.GetCorrectNumber();
        }

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
    }
}
