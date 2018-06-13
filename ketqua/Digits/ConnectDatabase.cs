using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
namespace ketqua.Digits
{
    class ConnectDatabase
    {
        private const string DATA_BASE_NAME = "digit";
        private const string COLLECTION_NAME = "db";
        private const string XML_URL = "http://xskt.com.vn/rss-feed/mien-bac-xsmb.rss";
        private const string COMMAND_CONNECT_MONGODB = "/d/01-project/MongoDB/bin/mongod.exe";
        private static IMongoCollection<BsonDocument> collection = null;
        private static IMongoCollection<BsonDocument> _collection = null;
        private IMongoDatabase database;
        private string DATA_DIGITS_PATH = Variables.DATABASE_DIRECTORY + "database.json";

        public ConnectDatabase()
        {
            MakeConnection();

        }
        public void StartInsert()
        {
            if (collection == null)
            {
                MakeConnection();
                return;
            }
            GetDataFromFeedRss();
        }

        public enum IDataType {
            Special,
            SpecialTwoDigit,
            AllTwoDigit,
            DropDigitForward,
            DropDigitBackward


        };
        public string[] GetData(IDataType dataType = IDataType.Special, int offset = 5000)
        {
            if (collection == null)
            {
                MakeConnection();
                return null;
            }
            List<BsonDocument> list;

            if (offset == 0)
            {
                list = _collection.Find(p => true).ToList();
                
            } else
            {
                var filter = Builders<BsonDocument>.Filter.Gt("offset", Utils.GetOffSetDate() - offset);
                list = _collection.Find(filter).ToList();
            }



            switch (dataType)
            {
                case IDataType.Special:
                    return GetSpecial(list);
                case IDataType.AllTwoDigit:
                    return GetAllTwoDigits(list);
                case IDataType.SpecialTwoDigit:
                    return GetSpecialTwoDigits(list);
                case IDataType.DropDigitForward:
                    return GetDropDigit(list);
                case IDataType.DropDigitBackward:
                    return GetDropDigit(list, false);
                default:
                    return null;
            }
        }

        private string[] GetSpecial(List<BsonDocument> list)
        {
            string[] array = new string[1];
            string temp = "";
            int length = list.Count();
            int count = 0;
            foreach (var item in list)
            {
                temp += item.GetValue("special");
                temp += (++count == length ? "" : "\t");
            }
            array[0] = temp;
            return array;
        }

        private string[] GetAllTwoDigits(List<BsonDocument> list)
        {
            string temp = "";
            int length = list.Count();
            int count = 0;
            string[] array = new string[length];

            foreach (var item in list)
            {
                temp = "";
                string value = item.GetValue("digits").ToString();
                string[] tempArray = value.Split(' ');
                int len = tempArray.Length;
                for (int i = 0; i < len; i++)
                {
                    string current = tempArray[i];
                    temp += current.Substring(current.Length - 2);
                    temp += (i == len - 1 ? "" : "\t");

                }
                string[] tempArr = temp.Split('\t');
                Array.Sort(tempArr, StringComparer.InvariantCulture);
                temp = string.Join("\t", tempArr);
                array[count] = item.GetValue("date") + "\t" + temp;
                count++;
            }
            return array;
        }

        private string[] GetDropDigit(List<BsonDocument> list, bool isForward = true)
        {
            string temp = "";
            int length = list.Count();
            int count = 0;
            string[] array = new string[length];

            foreach (var item in list)
            {
                temp = "";
                string value = item.GetValue("digits").ToString();
                string[] tempArray = value.Split(' ');
                int len = tempArray.Length;
                for (int i = 0; i < len; i++)
                {
                    temp += CutTwoDigitsFromString(tempArray[i], isForward);
                }
                temp = temp.Trim().Replace(' ', '\t');
                array[count] = item.GetValue("date").ToString() + '\t' + temp;
                count++;
            }
            return array;
        }

        private string CutTwoDigitsFromString(string str, bool isForward)
        {
            if (!isForward)
            {
                char[] array = str.ToCharArray();
                Array.Reverse(array);
                str = new String(array);
            }
            int len = str.Length;
            string temp = "";
            for (int i = 0; i < len - 1; i++)
            {
                temp += str.Substring(i, 2) + " ";
            }
            return temp;
        }

        private string[] GetSpecialTwoDigits(List<BsonDocument> list)
        {
            string[] array = GetSpecial(list)[0].Split('\t');
            string temp = "";
            string[] tempArray = new string[1];
            for (int i = 0;  i< array.Length; i++)
            {
                string current = array[i];
                temp += current.Substring(current.Length - 2);
                temp += (i == array.Length- 1 ? "" : "\t");
            }
            tempArray[0] = temp;
            return tempArray;
        }

        private void GetDataFromFeedRss(string url = XML_URL)
        {
            XmlTextReader reader = new XmlTextReader(url);
            string title = "";
            string result = "";
            while (reader.Read())
            {
                if (reader.Name == "item" )
                {
                    bool isComplete = false;
                    while (reader.Read())
                    {
                        
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element: // The node is an element.
                                if (reader.Name == "title")
                                {
                                    reader.Read();
                                    title = reader.Value;
                                } else if (reader.Name == "description")
                                {
                                    reader.Read();
                                    result = reader.Value;
                                    isComplete = true;
                                }
                                break;
                        }

                        if (isComplete)
                        {
                            break;
                        }
                    }
                    break;

                }
            }

            var index = title.IndexOf('/');
            string[] dateArray = title.Substring(index - 2, 5).Split('/');
            DateTime d1 = new DateTime(2000, 01,01);
            DateTime d2 = new DateTime(Int32.Parse(Configs.CURRENT_YEAR), Int32.Parse(dateArray[1]), Int32.Parse(dateArray[0]));
            var offset = (d2 - d1).TotalDays;
            string date = dateArray[0] + "-" + dateArray[1] + "-" + Configs.CURRENT_YEAR;
            string[] resultArr = result.Split('\n');
            string[] arr = new string[0];
            for (int i = 0; i < resultArr.Length; i++)
            {
                string self = resultArr[i];
                if (self == "")
                {
                    continue;
                }
                var number = self.Split(':')[1];
                number.Split('-');
                arr = arr.Concat(number.Split('-')).ToArray();
            }
            string digits = "";
            for (int i = 0; i < arr.Length; i++)
            {
                digits += arr[i].Trim() + " ";
            }

            digits = digits.Trim();
            string special = arr[0].Trim();
            //IDigitsItem document = new IDigitsItem(date, digits, special, Convert.ToInt32(offset));
            var document = new BsonDocument
            {
                { "date", date },
                { "digits", digits },
                { "special", special },
                { "offset", Convert.ToInt32(offset)}
            };
            InserNewData(document);
        }

        
        private void InsertDataAsync()
        {
            int count = 0;

            using (var streamReader = new StreamReader(DATA_DIGITS_PATH))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    count++;
                    using (var jsonReader = new JsonReader(line))
                    {
                        var context = BsonDeserializationContext.CreateRoot(jsonReader);
                        var document = collection.DocumentSerializer.Deserialize(context);
                        collection.InsertOne(document);
                        Console.WriteLine("waiting");

                    }
                }
            }
            Console.WriteLine("complete" + count.ToString());
        }

        private void InserNewData(BsonDocument dataObject)
        {
            if (collection == null)
            {
                MakeConnection();
                return;
            }
            var filter = Builders<BsonDocument>.Filter.Eq("date", dataObject.GetValue("date"));
            var document = collection.Find(filter);
            if (document.Count() == 0)
            {
                collection.InsertOne(dataObject);
                Utils.ShowMessage(Utils.ReplaceMessage(Variables.MSG_DATA_IMPORTED, "ngày" + dataObject.GetValue("date")));
            }
            else
            {
                Utils.ShowMessage(Utils.ReplaceMessage(Variables.MSG_DATA_EXISTED, "ngày" + dataObject.GetValue("date")));
            }
        }
        private void MakeConnection()
        {
            if (collection != null)
            {
                return;
            }                    
            var client = new MongoClient();

            
            database = client.GetDatabase(DATA_BASE_NAME);
            bool isMongoLive = database.RunCommandAsync((Command<BsonDocument>)"{ping:1}").Wait(1000);
            Thread.Sleep(1000);
            if (isMongoLive)
            {
                collection = database.GetCollection<BsonDocument>(COLLECTION_NAME);
                _collection = database.GetCollection<BsonDocument>(COLLECTION_NAME);
            }
            else
            {
                 System.Windows.Forms.Clipboard.SetText(COMMAND_CONNECT_MONGODB);

                Utils.ShowMessage("Xin hãy connect với dâtbase \t lệnh đã được copy vào clipbaord. Mở gitbash va nhấn slift + insert");
            }
        }


    }
}
