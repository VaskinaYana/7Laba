using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp5
{
    public static class TasksSixToTen
    {
        private const string BasePath = @"C:\Users\MSI\Desktop\Яна\ЯП\7Laba\";

        private static void EnsureFolderExists()
        {
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
        }

        public static void RunTask6()
        {
            Console.WriteLine("\n ЗАДАНИЕ 6");
            List<int> L1 = ReadListFromKeyboard("L1");
            List<int> L2 = ReadListFromKeyboard("L2");
            List<int> result = SymmetricDifference(L1, L2);
            Console.Write("Результат (элементы, входящие только в один список): ");
            PrintList(result);
        }

        private static List<int> ReadListFromKeyboard(string listName)
        {
            List<int> result = new List<int>();
            Console.WriteLine($"Введите элементы списка {listName} (целые числа, разделенные пробелом):");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return result;
            }

            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                if (int.TryParse(parts[i], out int number))
                {
                    result.Add(number);
                }
            }
            return result;
        }

        private static List<int> SymmetricDifference(List<int> list1, List<int> list2)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < list1.Count; i++)
            {
                int item = list1[i];
                bool found = false;
                for (int j = 0; j < list2.Count; j++)
                {
                    if (item == list2[j])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    bool already = false;
                    for (int k = 0; k < result.Count; k++)
                    {
                        if (result[k] == item)
                        {
                            already = true;
                            break;
                        }
                    }
                    if (!already)
                        result.Add(item);
                }
            }

            for (int i = 0; i < list2.Count; i++)
            {
                int item = list2[i];
                bool found = false;
                for (int j = 0; j < list1.Count; j++)
                {
                    if (item == list1[j])
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    bool already = false;
                    for (int k = 0; k < result.Count; k++)
                    {
                        if (result[k] == item)
                        {
                            already = true;
                            break;
                        }
                    }
                    if (!already)
                        result.Add(item);
                }
            }

            return result;
        }

        public static void RunTask7()
        {
            Console.WriteLine("\n ЗАДАНИЕ 7");
            List<int> list = ReadListFromKeyboard("список");
            Console.Write("Исходный список: ");
            PrintList(list);
            RemoveBetweenMinMax(list);
            Console.Write("Результат после удаления элементов между min и max: ");
            PrintList(list);
        }

        private static void RemoveBetweenMinMax(List<int> list)
        {
            if (list.Count < 3)
                return;

            int minIndex = 0;
            int maxIndex = 0;

            for (int i = 1; i < list.Count; i++)
            {
                if (list[i] < list[minIndex])
                    minIndex = i;
                if (list[i] > list[maxIndex])
                    maxIndex = i;
            }

            int start = Math.Min(minIndex, maxIndex);
            int end = Math.Max(minIndex, maxIndex);

            int removeCount = end - start - 1;
            if (removeCount > 0)
            {
                list.RemoveRange(start + 1, removeCount);
            }
        }

        public static void RunTask8()
        {
            Console.WriteLine("\n ЗАДАНИЕ 8");
            List<string> shows = ReadStringListFromKeyboard("названий телевизионных шоу");
            int n = ReadPositiveInt("Введите количество телезрителей: ");
            List<List<string>> viewerLikes = new List<List<string>>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Введите шоу, которые нравятся зрителю {i + 1} (через пробел):");
                List<string> likes = ReadStringListFromKeyboardLine();
                viewerLikes.Add(likes);
            }
            AnalyzeShows(shows, viewerLikes);
        }

        private static List<string> ReadStringListFromKeyboard(string description)
        {
            List<string> result = new List<string>();
            Console.WriteLine($"Введите {description} (через пробел):");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                {
                    result.Add(parts[i]);
                }
            }
            return result;
        }

        private static List<string> ReadStringListFromKeyboardLine()
        {
            List<string> result = new List<string>();
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                {
                    result.Add(parts[i]);
                }
            }
            return result;
        }

        private static int ReadPositiveInt(string message)
        {
            int number;
            while (true)
            {
                Console.Write(message);
                string input = Console.ReadLine();
                if (int.TryParse(input, out number) && number > 0)
                {
                    return number;
                }
                Console.WriteLine("Ошибка: введите положительное целое число.");
            }
        }

        private static void AnalyzeShows(List<string> allShows, List<List<string>> viewerLikes)
        {
            List<string> likedByAll = new List<string>();
            List<string> likedBySome = new List<string>();
            List<string> likedByNone = new List<string>();

            for (int i = 0; i < allShows.Count; i++)
            {
                string show = allShows[i];
                int count = 0;
                for (int j = 0; j < viewerLikes.Count; j++)
                {
                    List<string> viewer = viewerLikes[j];
                    bool found = false;
                    for (int k = 0; k < viewer.Count; k++)
                    {
                        if (viewer[k] == show)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found)
                        count++;
                }

                if (count == viewerLikes.Count)
                    likedByAll.Add(show);
                else if (count > 0)
                    likedBySome.Add(show);
                else
                    likedByNone.Add(show);
            }

            Console.WriteLine("\nРезультаты анализа:");
            Console.WriteLine("Нравятся всем: " + FormatList(likedByAll));
            Console.WriteLine("Нравятся некоторым: " + FormatList(likedBySome));
            Console.WriteLine("Не нравятся никому: " + FormatList(likedByNone));
        }

        private static string FormatList(List<string> list)
        {
            if (list.Count == 0)
                return "(нет)";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                    sb.Append(", ");
                sb.Append(list[i]);
            }
            return sb.ToString();
        }

        public static void RunTask9()
        {
            Console.WriteLine("\n ЗАДАНИЕ 9");
            Console.WriteLine("Введите текст на русском языке:");
            string text = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(text))
            {
                Console.WriteLine("Текст не введен.");
                return;
            }

            List<char> digits = FindDigitsInText(text);
            if (digits.Count == 0)
            {
                Console.WriteLine("В тексте нет цифр.");
            }
            else
            {
                Console.Write("Цифры, встречающиеся в тексте: ");
                for (int i = 0; i < digits.Count; i++)
                {
                    if (i > 0)
                        Console.Write(", ");
                    Console.Write(digits[i]);
                }
                Console.WriteLine();
            }
        }

        private static List<char> FindDigitsInText(string text)
        {
            List<char> result = new List<char>();
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (c >= '0' && c <= '9')
                {
                    bool alreadyExists = false;
                    for (int j = 0; j < result.Count; j++)
                    {
                        if (result[j] == c)
                        {
                            alreadyExists = true;
                            break;
                        }
                    }
                    if (!alreadyExists)
                    {
                        result.Add(c);
                    }
                }
            }
            return result;
        }

        public static void RunTask10()
        {
            EnsureFolderExists();
            Console.WriteLine("\n ЗАДАНИЕ 10");
            string filePath = BasePath + "task10_students.txt";
            GenerateTask10File(filePath);
            GenerateLogins(filePath);
        }

        private static void GenerateTask10File(string path)
        {
            string[] students = new string[]
            {
                "Иванова Мария",
                "Петров Сергей",
                "Бойцова Екатерина",
                "Петров Иван",
                "Иванова Наташа",
                "Сидоров Алексей",
                "Иванова Ольга",
                "Петров Дмитрий"
            };
            File.WriteAllLines(path, students);
            Console.WriteLine($"Создан файл с данными учеников: {path}");
        }

        private static void GenerateLogins(string path)
        {
            List<string> surnames = new List<string>();
            List<int> surnameCounts = new List<int>();
            List<string> logins = new List<string>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 2)
                    {
                        string surname = parts[0];
                        int index = -1;
                        for (int i = 0; i < surnames.Count; i++)
                        {
                            if (surnames[i] == surname)
                            {
                                index = i;
                                break;
                            }
                        }
                        if (index == -1)
                        {
                            surnames.Add(surname);
                            surnameCounts.Add(1);
                            logins.Add(surname);
                        }
                        else
                        {
                            surnameCounts[index]++;
                            logins.Add(surname + surnameCounts[index]);
                        }
                    }
                }
            }

            Console.WriteLine("\nСформированные логины:");
            for (int i = 0; i < logins.Count; i++)
            {
                Console.WriteLine(logins[i]);
            }
        }

        private static void PrintList(List<int> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                    Console.Write(" ");
                Console.Write(list[i]);
            }
            Console.WriteLine();
        }
    }
}