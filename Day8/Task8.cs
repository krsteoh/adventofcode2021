using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8
{
    public static  class Task8
    {
        const string Day = "Day8";
        const string FilePath = @"Day8\d81.txt";
        static List<int> inputList = new List<int> {};

        public static void Start()
        {
            Console.WriteLine(Day + "-  Task1 Test Result = " + DoTask1(inputList));
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Result = ", Day), DoTask1);

            Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(inputList));
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task2 Result = ", Day), DoTask2);
        }

        public static string DoTask1(List<int> l)
        {
            return "";
        }

        public static string DoTask2(List<int> l)
        {
            return "";
        }
        public static string DoTask1()
        {
            return DoTask1(System.IO.File.ReadAllText(string.Format("{0}{1}", Config.LocalPath, FilePath)).Split(',').Select(t => int.Parse(t)).ToList());
        }
        public static string DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllText(string.Format("{0}{1}", Config.LocalPath, FilePath)).Split(',').Select(t => int.Parse(t)).ToList());
        }
    }
}
