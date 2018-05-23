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

        private string folderPath = @"D:\project\02-form\database\";
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("=============start===============");
            Console.WriteLine(Variables.CURRENT_DIRECTORY);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
        }
    }
}
