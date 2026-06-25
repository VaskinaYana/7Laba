using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace ConsoleApp5
{
    public static class TasksOneToFive
    {
        private const string BasePath = @"C:\Users\MSI\Desktop\Яна\ЯП\7Laba\";

        private static void EnsureFolderExists()
        {
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
        }

        public static void RunTask1()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 1");
            string filePath = BasePath + "task1_numbers.txt";
            int digit = ReadDigitFromKeyboard("Введите цифру (0-9) для поиска окончания:");
            GenerateTask1File(filePath, 20);
            int sum = SumNumbersEndingWithDigit(filePath, digit);
            Console.WriteLine($"Сумма чисел, оканчивающихся на {digit}: {sum}");
        }

        private static void GenerateTask1File(string path, int count)
        {
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < count; i++)
                {
                    int num = rand.Next(1, 1000);
                    sw.WriteLine(num);
                }
            }
            Console.WriteLine($"Создан файл: {path}");
        }

        private static int SumNumbersEndingWithDigit(string path, int digit)
        {
            int sum = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (int.TryParse(line.Trim(), out int number))
                    {
                        int lastDigit = Math.Abs(number % 10);
                        if (lastDigit == digit)
                        {
                            sum += number;
                        }
                    }
                }
            }
            return sum;
        }

        private static int ReadDigitFromKeyboard(string message)
        {
            int digit;
            while (true)
            {
                Console.Write(message + " ");
                string input = Console.ReadLine();
                if (int.TryParse(input, out digit) && digit >= 0 && digit <= 9)
                {
                    return digit;
                }
                Console.WriteLine("Ошибка: введите цифру от 0 до 9.");
            }
        }

        public static void RunTask2()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 2");
            string filePath = BasePath + "task2_numbers.txt";
            GenerateTask2File(filePath, 5, 4);
            int diff = DifferenceFirstMin(filePath);
            Console.WriteLine($"Разность первого и минимального элементов: {diff}");
        }

        private static void GenerateTask2File(string path, int rows, int cols)
        {
            Random rand = new Random();
            using (StreamWriter sw = new StreamWriter(path))
            {
                for (int i = 0; i < rows; i++)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int j = 0; j < cols; j++)
                    {
                        sb.Append(rand.Next(1, 100));
                        if (j < cols - 1)
                            sb.Append(' ');
                    }
                    sw.WriteLine(sb.ToString());
                }
            }
            Console.WriteLine($"Создан файл: {path}");
        }

        private static int DifferenceFirstMin(string path)
        {
            int? first = null;
            int? min = null;

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string part in parts)
                    {
                        if (int.TryParse(part, out int number))
                        {
                            if (!first.HasValue)
                                first = number;

                            if (!min.HasValue || number < min.Value)
                                min = number;
                        }
                    }
                }
            }

            if (first.HasValue && min.HasValue)
                return first.Value - min.Value;
            else
                throw new InvalidOperationException("Файл не содержит чисел.");
        }

        public static void RunTask3()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 3");
            string source = BasePath + "task3_source.txt";
            string dest = BasePath + "task3_result.txt";
            GenerateTask3File(source);
            CopyLinesWithoutPunctuation(source, dest);
            Console.WriteLine($"Результат сохранен в файл: {dest}");
        }

        private static void GenerateTask3File(string path)
        {
            string[] lines = new string[]
            {
                "Привет, мир!",
                "Это строка без знаков препинания",
                "А здесь? точка и запятая;",
                "И снова нормальная строка",
                "Последняя строка.",
                "Строка без знаков препинания и пробелов"
            };
            File.WriteAllLines(path, lines);
            Console.WriteLine($"Создан файл: {path}");
        }

        private static void CopyLinesWithoutPunctuation(string source, string dest)
        {
            List<string> resultLines = new List<string>();
            char[] punctuation = new char[] { '.', ',', '!', '?', ';', ':', '-', '(', ')', '"', '\'' };

            using (StreamReader sr = new StreamReader(source))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    bool hasPunct = false;
                    for (int i = 0; i < line.Length; i++)
                    {
                        char c = line[i];
                        for (int j = 0; j < punctuation.Length; j++)
                        {
                            if (c == punctuation[j])
                            {
                                hasPunct = true;
                                break;
                            }
                        }
                        if (hasPunct)
                            break;
                    }

                    if (!hasPunct)
                    {
                        resultLines.Add(line);
                    }
                }
            }

            File.WriteAllLines(dest, resultLines);
        }

        public static void RunTask4()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 4");
            string source = BasePath + "task4_source.bin";
            string dest = BasePath + "task4_result.bin";
            GenerateTask4File(source, 20);
            RemoveDuplicatesBinary(source, dest);
            Console.WriteLine($"Результат сохранен в файл: {dest}");
        }

        private static void GenerateTask4File(string path, int count)
        {
            Random rand = new Random();
            using (BinaryWriter bw = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    int num = rand.Next(1, 15);
                    bw.Write(num);
                }
            }
            Console.WriteLine($"Создан бинарный файл: {path}");
        }

        private static void RemoveDuplicatesBinary(string source, string dest)
        {
            List<int> unique = new List<int>();

            using (BinaryReader br = new BinaryReader(File.Open(source, FileMode.Open)))
            {
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    int num = br.ReadInt32();
                    bool exists = false;
                    for (int i = 0; i < unique.Count; i++)
                    {
                        if (unique[i] == num)
                        {
                            exists = true;
                            break;
                        }
                    }
                    if (!exists)
                    {
                        unique.Add(num);
                    }
                }
            }

            using (BinaryWriter bw = new BinaryWriter(File.Open(dest, FileMode.Create)))
            {
                for (int i = 0; i < unique.Count; i++)
                {
                    bw.Write(unique[i]);
                }
            }
        }

        public struct Toy
        {
            public string Name;
            public int Price;
            public int AgeFrom;
            public int AgeTo;
        }

        public static void RunTask5()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 5");
            string filePath = BasePath + "task5_toys.xml";
            GenerateTask5File(filePath);
            string mostExpensive = GetMostExpensiveToyForAge(filePath, 2, 3);
            Console.WriteLine($"Самая дорогая игрушка для детей 2-3 лет: {mostExpensive}");
        }

        private static void GenerateTask5File(string path)
        {
            Toy[] toys = new Toy[]
            {
                new Toy { Name = "Мяч", Price = 300, AgeFrom = 2, AgeTo = 6 },
                new Toy { Name = "Кубики", Price = 450, AgeFrom = 1, AgeTo = 4 },
                new Toy { Name = "Пазл", Price = 200, AgeFrom = 3, AgeTo = 7 },
                new Toy { Name = "Машинка", Price = 600, AgeFrom = 2, AgeTo = 4 },
                new Toy { Name = "Лото", Price = 150, AgeFrom = 4, AgeTo = 10 },
                new Toy { Name = "Пирамидка", Price = 350, AgeFrom = 1, AgeTo = 3 }
            };

            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(fs, toys);
            }
            Console.WriteLine($"Создан XML-файл: {path}");
        }

        private static string GetMostExpensiveToyForAge(string path, int ageFrom, int ageTo)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Toy[]));
            Toy[] toys;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                toys = (Toy[])serializer.Deserialize(fs);
            }

            int maxPrice = -1;
            string name = "Нет подходящих игрушек";

            for (int i = 0; i < toys.Length; i++)
            {
                Toy toy = toys[i];
                if (toy.AgeFrom <= ageFrom && toy.AgeTo >= ageTo)
                {
                    if (toy.Price > maxPrice)
                    {
                        maxPrice = toy.Price;
                        name = toy.Name;
                    }
                }
            }

            return name;
        }
    }
}