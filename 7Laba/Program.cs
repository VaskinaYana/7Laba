using System;

namespace ConsoleApp5
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Файлы будут созданы в папке:");
            Console.WriteLine(@"C:\Users\MSI\Desktop\Яна\ЯП\7Laba\");
            Console.WriteLine();

            TasksOneToFive.RunTask1();
            TasksOneToFive.RunTask2();
            TasksOneToFive.RunTask3();
            TasksOneToFive.RunTask4();
            TasksOneToFive.RunTask5();

            TasksSixToTen.RunTask6();
            TasksSixToTen.RunTask7();
            TasksSixToTen.RunTask8();
            TasksSixToTen.RunTask9();
            TasksSixToTen.RunTask10();

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}