using System;
using System.IO;

namespace BarCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите имя пользователя:");
            string usrname = Console.ReadLine();

            Console.WriteLine("Введите дату:");
            string dat = Console.ReadLine();

            Console.WriteLine("Введите путь к данным (например, C:\\Users\\ВашПользователь\\Documents):");
            string usrdat = Console.ReadLine();

            Console.WriteLine("Введите имя файла (например, data.txt):");
            string usrflname = Console.ReadLine();
            Console.WriteLine("Введите имя заведения:");
            string barname = Console.ReadLine();
            User user = new User(usrname, dat, usrdat, usrflname, barname);
        }
    }

    public class User
    {
        public string Username { get; }
        public string Data { get; }
        public string UserDataPath { get; }
        public string UserFilename { get; }
        public string UserBarName { get; }
        public User(string username, string data, string userDataPath, string userFilename, string userBarName)
        {
            Username = username;
            Data = data;
            UserDataPath = userDataPath;
            UserFilename = userFilename;
            UserBarName = userBarName;

            // Формируем полный путь к файлу
            string fullFilePath = Path.Combine(UserDataPath, UserFilename);

            try
            {
                FileInfo fileInfo = new FileInfo(fullFilePath);

                // Проверяем, существует ли файл
                if (fileInfo.Exists)
                {
                    Console.WriteLine("Файл уже существует. Пересоздаю его...");
                    fileInfo.Delete(); // Удаляем старый файл
                }

                // Создаем новый файл
                using (FileStream fs = File.Create(fullFilePath))
                {
                    Console.WriteLine($"Файл успешно создан по пути: {fullFilePath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании файла: {ex.Message}");
            }
            File.WriteAllTextAsync(fullFilePath, UserBarName);
        }
    }
}
