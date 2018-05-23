using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    class Variables
    {
        public static string CURRENT_DIRECTORY = System.IO.Directory.GetCurrentDirectory();
        public const string FOLDER_PATH = @"D:\project\02-form\";
        public const string DIRECTORY_PATH = @"D:\project\02-form\";
        public const string DATABASE_FOLDER_PATH= FOLDER_PATH + @"database\";
        public const string DATABASE_FILE_PATH = DATABASE_FOLDER_PATH + "data.txt";
        public const string DATABASE_FOLDER_NAME = "database";

        // Timmer
        public static string START_TIME = "";
        public static string END_TIME = "";



        // number
        public const int AMOUNT_OF_TWO_DEGITS = 4950;
        public const int AMOUNT_OF_THREE_DEGITS = 161700;
        public const int AMOUNT_OF_FOUR_DEGITS = 3921225;


        public enum Digits
        {
            TWO_DIGITS,
            THREE_DIGITS,
            FOURS_DIGITS,
        }
    }
}
