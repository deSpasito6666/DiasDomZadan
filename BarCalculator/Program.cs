using System.Text.Json;
using System;
using System.IO;
using System.Xml;

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

            Console.WriteLine("Введите имя файла (например, data.json):");
            string usrflname = Console.ReadLine();
            User user = new User(usrname, dat, usrdat, usrflname);
            List<BarInfo> bars = new List<BarInfo>();

            while (true)
            {
                Console.WriteLine("Введите название бара (или 'exit' для завершения ввода):");
                string barName = Console.ReadLine();
                if (barName.ToLower() == "exit")
                    break;

                Console.WriteLine("Введите участников через запятую:");
                string[] participants = Console.ReadLine().Split(',');

                Console.WriteLine("Введите общий счет:");
                decimal totalBill = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Кто платил за всех?");
                string payer = Console.ReadLine();

                Dictionary<string, decimal> expenses = new Dictionary<string, decimal>();

                foreach (var participant in participants)
                {
                    Console.WriteLine($"Сколько потратил {participant.Trim()}?");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    expenses[participant.Trim()] = amount;
                }

                BarInfo bar = new BarInfo
                {
                    BarName = barName,
                    Participants = new List<string>(participants),
                    TotalBill = totalBill,
                    Payer = payer,
                    Expenses = expenses
                };

                bars.Add(bar);

                Console.WriteLine("Бар успешно добавлен!");
            }
            Console.WriteLine("Сохранение результатов....");
            string filePath = Path.Combine(usrdat, usrflname);
            SaveToJson(bars, filePath);
            static void SaveToJson(List<BarInfo> bars, string filePath)
            {
                try
                {
                    string jsonData = JsonSerializer.Serialize(bars);
                    File.WriteAllText(filePath, jsonData);
                    Console.WriteLine("Данные успешно сохранены в файл JSON!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при записи файла: {ex.Message}");
                }
            }
        }
    }

    public class User
    {
        public string Username { get; }
        public string Data { get; }
        public string UserDataPath { get; }
        public string UserFilename { get; }
        public string UserBarName { get; }
        public User(string username, string data, string userDataPath, string userFilename)
        {
            Username = username;
            Data = data;
            UserDataPath = userDataPath;
            UserFilename = userFilename;

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
        }
    }
    public class BarInfo
    {
        public string BarName { get; set; }
        public List<string> Participants { get; set; }
        public decimal TotalBill { get; set; }
        public string Payer { get; set; }
        public Dictionary<string, decimal> Expenses { get; set; }
    }
}