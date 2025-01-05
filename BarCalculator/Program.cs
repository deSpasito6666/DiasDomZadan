using BarCalculator;
using System;
using System.IO;

namespace BarCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя");
            string usrname = Console.ReadLine();

            Console.WriteLine("Введите дату:");
            string dat = Console.ReadLine();

            Console.WriteLine("Введите путь к данным:");
            string usrdat = Console.ReadLine();

            Console.WriteLine("Введите имя файла:");
            string usrflname = Console.ReadLine();

            User user = new User(usrname, dat, usrdat, usrflname);

        }
    }

    public class User
    {
        public string Username { get; }
        public string Data { get; }
        public string UserDataPath { get; }
        public string UserFilename { get; }

        public User(string username, string data, string userDataPath, string userFilename)
        {
            Username = username;
            Data = data;
            UserDataPath = userDataPath;
            UserFilename = userFilename;
            FileInfo fileInfo = new FileInfo(UserFilename);
            if (fileInfo.Exists)
            {
                Console.WriteLine("Файл уже существует, пересоздаю его....");
                fileInfo.Delete();
                File.Create($"{UserFilename}");
                Console.WriteLine("Готово");
            }
            else
            {
                File.Create($"{UserFilename}");
                Console.WriteLine("Файл создан");
            }
        }
    }
} 



//Планы на 6 января
// надо сделать так чтобы создавался файл в указанный путь
// надо сделать метод для бара и записывать масиив
//дедлайн 9 января