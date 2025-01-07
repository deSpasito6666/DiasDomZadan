using System.Text.Json;
using System;
using System.Collections.Generic;
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

            Console.WriteLine("Введите имя файла (например, data.json):");
            string usrflname = Console.ReadLine();

            List<BarInfo> bars = new List<BarInfo>();
            Dictionary<string, decimal> participantBalances = new Dictionary<string, decimal>();

            while (true)
            {
                Console.WriteLine("Введите название бара (или 'exit' для завершения ввода):");
                string barName = Console.ReadLine();
                if (barName.ToLower() == "exit")
                    break;

                Console.WriteLine("Введите участников через запятую:");
                string[] participants = Console.ReadLine().Split(',');

                foreach (var participant in participants)
                {
                    if (!participantBalances.ContainsKey(participant.Trim()))
                    {
                        participantBalances[participant.Trim()] = 0;
                    }
                }

                Console.WriteLine("Введите общий счет:");
                decimal totalBill = decimal.Parse(Console.ReadLine());

                Console.WriteLine("Кто платил за всех?");
                string payer = Console.ReadLine().Trim();

                Dictionary<string, decimal> expenses = new Dictionary<string, decimal>();
                decimal totalExpenses = 0;


                foreach (var participant in participants)
                {
                    Console.WriteLine($"Сколько потратил {participant.Trim()}?");
                    decimal amount = decimal.Parse(Console.ReadLine());
                    expenses[participant.Trim()] = amount;
                    totalExpenses += amount;
                }

                if (totalExpenses != totalBill)
                {
                    Console.WriteLine("Внимание: Сумма расходов участников не совпадает с общим счетом!");
                }

                foreach (var participant in participants)
                {
                    string trimmedParticipant = participant.Trim();
                    if (trimmedParticipant == payer)
                    {
                        participantBalances[payer] -= totalBill;
                    }
                    else
                    {
                        participantBalances[payer] += expenses[trimmedParticipant];
                    }
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


            string filePath = Path.Combine(usrdat, usrflname);
            SaveToJson(bars, filePath);

            Console.
WriteLine($"\nПользователь: {usrname}");
            Console.WriteLine($"Дата: {dat}");
            Console.WriteLine($"Файл сохранен по пути: {filePath}");

            Console.WriteLine("\nИтоговый баланс участников:");
            foreach (var balance in participantBalances)
            {
                if (balance.Value > 0)
                    Console.WriteLine($"{balance.Key} должен {Math.Abs(balance.Value):0.00}");
                else if (balance.Value == 0)
                    Console.WriteLine($"{balance.Key} должен вернуть {balance.Value}:0.00");
                else
                    Console.WriteLine($"{balance.Key} ему/ей должны.");
            }
        }

        static void SaveToJson(List<BarInfo> bars, string filePath)
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(bars, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("Данные успешно сохранены в файл JSON!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи файла: {ex.Message}");
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
