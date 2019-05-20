using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Accounting
{
    [Serializable()]
    public class Account
    {

        public string fio { get; set; }
        public DateTime StDate { get; set; }
        public int days { get; set; }
        public List<DateTime> usedDates = new List<DateTime>();



        private static object lofile = new object();

        public static String LogFileDir()
        {
            return new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName + @"\db";
        }

        public static String LogfileName(string name)
        {
            String path = new System.IO.FileInfo(Assembly.GetEntryAssembly().Location).DirectoryName;
            return path + @"\db\db-" + name + "";
        }

        public static void SaveToFile(String name, List<Account> accounts)
        {
            lock (lofile)
            {
                try
                {
                    String dir_name = LogFileDir();
                    if (!Directory.Exists(dir_name))
                    {
                        Directory.CreateDirectory(dir_name);
                    } //создать директорию если не существует
                }
                catch (Exception ex)
                {
                }
                try
                {
                    using (Stream serializationStream =
                        new FileStream(LogfileName(name), FileMode.Create, FileAccess.Write))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(serializationStream, accounts);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Ошибка при записи в локальную базу данных!");
                }

                lofile = new object();
            }
        }

        public static List<Account> LoadFromFile(string name)
        {
            lock (lofile)
            {
                String fn = LogfileName(name);


                if (!File.Exists(fn))
                    return new List<Account>();


                try
                {
                    using (Stream serializationStream = new FileStream(fn, FileMode.Open, FileAccess.Read))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        List<Account> sav = (List<Account>) formatter.Deserialize(serializationStream);
                        List<Account> res = new List<Account>();
                        foreach (Account acc in sav)
                            if (acc != null)
                            {

                                res.Add(acc);
                            }

                        return res;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return new List<Account>();
                }
            }


        }
    }
}
