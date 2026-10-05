using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleApp5
{
    public static class TasksSixToTen
    {
        private const string BasePath = "C:\\Users\\MSI\\Desktop\\Яна\\ЯП\\7Laba\\";

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
            Console.WriteLine("Введите элементы списка " + listName + " (целые числа через пробел):");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return result;

            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                int number;
                if (int.TryParse(parts[i], out number))
                    result.Add(number);
            }
            return result;
        }

        private static List<int> SymmetricDifference(List<int> list1, List<int> list2)
        {
            HashSet<int> set1 = new HashSet<int>(list1);
            HashSet<int> set2 = new HashSet<int>(list2);

            List<int> result = new List<int>();

            for (int i = 0; i < list1.Count; i++)
            {
                int item = list1[i];
                if (!set2.Contains(item) && !result.Contains(item))
                    result.Add(item);
            }

            for (int i = 0; i < list2.Count; i++)
            {
                int item = list2[i];
                if (!set1.Contains(item) && !result.Contains(item))
                    result.Add(item);
            }

            return result;
        }

        private static void PrintList(List<int> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("(пусто)");
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0)
                    Console.Write(" ");
                Console.Write(list[i]);
            }
            Console.WriteLine();
        }

        public static void RunTask7()
        {
            Console.WriteLine("\n ЗАДАНИЕ 7");
            LinkedList<int> list = ReadLinkedListFromKeyboard();

            if (list.Count < 3)
            {
                Console.WriteLine("Недостаточно элементов (нужно минимум 3).");
                return;
            }

            Console.Write("Исходный список: ");
            PrintLinkedList(list);

            RemoveBetweenMinMax(list);

            Console.Write("Результат: ");
            PrintLinkedList(list);
        }

        private static LinkedList<int> ReadLinkedListFromKeyboard()
        {
            LinkedList<int> result = new LinkedList<int>();
            Console.WriteLine("Введите элементы списка (целые числа через пробел):");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                return result;

            string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; i++)
            {
                int number;
                if (int.TryParse(parts[i], out number))
                    result.AddLast(number);
            }
            return result;
        }

        private static void RemoveBetweenMinMax(LinkedList<int> list)
        {
            if (list.Count < 3)
                return;

            int minIndex = 0;
            int maxIndex = 0;
            int index = 0;
            int minValue = list.First.Value;
            int maxValue = list.First.Value;

            LinkedListNode<int> current = list.First;
            while (current != null)
            {
                if (current.Value < minValue)
                {
                    minValue = current.Value;
                    minIndex = index;
                }
                if (current.Value > maxValue)
                {
                    maxValue = current.Value;
                    maxIndex = index;
                }
                current = current.Next;
                index++;
            }

            int start = Math.Min(minIndex, maxIndex);
            int end = Math.Max(minIndex, maxIndex);

            if (end - start < 2)
                return;

            current = list.First;
            int pos = 0;
            while (current != null)
            {
                LinkedListNode<int> next = current.Next;
                if (pos > start && pos < end)
                    list.Remove(current);
                current = next;
                pos++;
            }
        }

        private static void PrintLinkedList(LinkedList<int> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("(пусто)");
                return;
            }
            LinkedListNode<int> current = list.First;
            while (current != null)
            {
                Console.Write(current.Value);
                if (current.Next != null)
                    Console.Write(" ");
                current = current.Next;
            }
            Console.WriteLine();
        }

        public static void RunTask8()
        {
            Console.WriteLine("\n ЗАДАНИЕ 8");

            List<string> shows = ReadStringListFromKeyboard("названий телевизионных шоу");
            int n = ReadPositiveInt("Введите количество телезрителей: ");

            List<HashSet<string>> viewerLikes = new List<HashSet<string>>();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Введите шоу, которые нравятся зрителю " + (i + 1) + " (через пробел):");
                viewerLikes.Add(ReadStringSetFromKeyboardLine());
            }

            AnalyzeShows(shows, viewerLikes);
        }

        private static List<string> ReadStringListFromKeyboard(string description)
        {
            List<string> result = new List<string>();
            Console.WriteLine("Введите " + description + " (через пробел):");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                    result.Add(parts[i]);
            }
            return result;
        }

        private static HashSet<string> ReadStringSetFromKeyboardLine()
        {
            HashSet<string> result = new HashSet<string>();
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                    result.Add(parts[i]);
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
                    return number;
                Console.WriteLine("Ошибка: введите положительное целое число.");
            }
        }

        private static void AnalyzeShows(List<string> allShows, List<HashSet<string>> viewerLikes)
        {
            HashSet<string> likedByAll = new HashSet<string>();
            HashSet<string> likedBySome = new HashSet<string>();
            HashSet<string> likedByNone = new HashSet<string>();

            for (int i = 0; i < allShows.Count; i++)
            {
                string show = allShows[i];
                int count = 0;
                for (int j = 0; j < viewerLikes.Count; j++)
                {
                    if (viewerLikes[j].Contains(show))
                        count++;
                }

                if (count == viewerLikes.Count)
                    likedByAll.Add(show);
                else if (count > 0)
                    likedBySome.Add(show);
                else
                    likedByNone.Add(show);
            }

            Console.WriteLine("\nНравятся всем: " + FormatSet(likedByAll));
            Console.WriteLine("Нравятся некоторым: " + FormatSet(likedBySome));
            Console.WriteLine("Не нравятся никому: " + FormatSet(likedByNone));
        }

        private static string FormatSet(HashSet<string> set)
        {
            if (set.Count == 0)
                return "(нет)";

            StringBuilder sb = new StringBuilder();
            bool first = true;
            foreach (string s in set)
            {
                if (!first)
                    sb.Append(", ");
                sb.Append(s);
                first = false;
            }
            return sb.ToString();
        }

        public static void RunTask9()
        {
            Console.WriteLine("\n ЗАДАНИЕ 9");
            EnsureFolderExists();

            string filePath = BasePath + "task9.txt";
            GenerateTask9File(filePath);
            Console.WriteLine("Создан файл с текстом: " + filePath);

            HashSet<char> digits = FindDigitsInFile(filePath);

            Console.WriteLine("Цифры, встречающиеся в тексте:");
            if (digits.Count == 0)
            {
                Console.WriteLine("  (нет)");
            }
            else
            {
                foreach (char c in digits)
                    Console.Write(c + " ");
                Console.WriteLine();
            }
        }

        private static void GenerateTask9File(string path)
        {
            string[] lines = new string[]
            {
                "Сегодня 25 сентября 2024 года.",
                "Температура воздуха 15 градусов.",
                "Код товара: 1234567890.",
                "Просто текст без цифр."
            };
            File.WriteAllLines(path, lines);
        }

        private static HashSet<char> FindDigitsInFile(string path)
        {
            HashSet<char> result = new HashSet<char>();

            using (StreamReader sr = new StreamReader(path))
            {
                string text = sr.ReadToEnd();
                for (int i = 0; i < text.Length; i++)
                {
                    char c = text[i];
                    if (c >= '0' && c <= '9')
                        result.Add(c);
                }
            }
            return result;
        }

        public static void RunTask10()
        {
            Console.WriteLine("\n ЗАДАНИЕ 10");
            EnsureFolderExists();

            string filePath = BasePath + "task10.txt";
            GenerateTask10File(filePath);
            Console.WriteLine("Создан файл с данными: " + filePath);

            FindOldestPerson(filePath);
        }

        private static void GenerateTask10File(string path)
        {
            string[] people = new string[]
            {
                "Иванов Сергей 27.03.1993",
                "Петров Иван 15.06.1985",
                "Сидорова Анна 01.01.1985",
                "Кузнецов Олег 12.12.2000",
                "Смирнова Елена 05.05.1990"
            };
            File.WriteAllLines(path, people);
        }

        private static void FindOldestPerson(string path)
        {
            Dictionary<string, DateTime> people = new Dictionary<string, DateTime>();

            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] parts = line.Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length < 3)
                        continue;

                    string surname = parts[0];
                    string firstName = parts[1];
                    string dateStr = parts[2];

                    DateTime birthDate;
                    if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy",
                        System.Globalization.CultureInfo.InvariantCulture,
                        System.Globalization.DateTimeStyles.None, out birthDate))
                    {
                        string key = surname + " " + firstName;
                        if (!people.ContainsKey(key))
                            people.Add(key, birthDate);
                    }
                }
            }

            if (people.Count == 0)
            {
                Console.WriteLine("Файл не содержит данных.");
                return;
            }

            DateTime oldest = DateTime.MaxValue;
            int countOldest = 0;

            foreach (KeyValuePair<string, DateTime> pair in people)
            {
                if (pair.Value < oldest)
                {
                    oldest = pair.Value;
                    countOldest = 1;
                }
                else if (pair.Value == oldest)
                {
                    countOldest++;
                }
            }

            if (countOldest == 1)
            {
                foreach (KeyValuePair<string, DateTime> pair in people)
                {
                    if (pair.Value == oldest)
                    {
                        Console.WriteLine("Самый старший: " + pair.Key + " (" + oldest.ToString("dd.MM.yyyy") + ")");
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Количество самых старших людей с одинаковой датой рождения: " + countOldest);
            }
        }
    }
}