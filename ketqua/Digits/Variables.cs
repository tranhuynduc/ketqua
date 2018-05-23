using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ketqua.Digits
{
    class Variables
    {
        public const string FOLDER_PATH = @"D:\project\02-form\";
        public const string DIRECTORY_PATH = @"D:\project\02-form\";
        public const string DATABASE_FOLDER_PATH = FOLDER_PATH + @"database\";
        public const string DATABASE_FILE_PATH = DATABASE_FOLDER_PATH + "data.txt";
        public const string DATABASE_FOLDER_NAME = @"database\";
        public const string RESULT_FOLDER_NAME = @"result\";
        public const string DATABASE_FILE_NAME = "data.txt";
        public const string DATABASE_NEW_FILE_NAME = "data-new.txt";
        public const string PLAY_NUMBER_FILE_NAME = "play-number.txt";
        public const string CORRECT_FILE_NAME = "coorect.txt";
        public const string WRONG_FILE_NAME = "wrong.txt";

        public static string CURRENT_DIRECTORY = System.IO.Directory.GetCurrentDirectory() + @"\";
        public static string DATABASE_DIRECTORY = CURRENT_DIRECTORY + DATABASE_FOLDER_NAME;
        public static string RESULT_DIRECTORY = DATABASE_DIRECTORY + RESULT_FOLDER_NAME;
        public static string DATABASE_FILE = DATABASE_DIRECTORY + DATABASE_FILE_NAME;
        public static string PLAY_NUMBER_FILE = DATABASE_DIRECTORY + PLAY_NUMBER_FILE_NAME;
        public static string DATABASE_NEW_FILE = DATABASE_DIRECTORY + DATABASE_NEW_FILE_NAME;
        public static string CORRECT_FILE = RESULT_DIRECTORY + CORRECT_FILE_NAME;
        public static string WRONG_FILE = RESULT_DIRECTORY + WRONG_FILE_NAME;



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
