using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
{
   public static class Task10
    {
        const int day = 10;
        static string Day = string.Format("Day{0}", day);
        static string FilePath = string.Format(@"Day{0}\d{0}1.txt", day);
        static string TestFilePath = string.Format(@"Day{0}\d{0}test.txt", day);

        public static void Start()
        {
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask1Test);
            //GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Result = ", Day), DoTask1);
            //GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test);
            //GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task2 Result = ", Day), DoTask2);
        }
        public static string DoTask2(List<string> l)
        {
            return null;
        }

        public static string DoTask1(List<string> l)
        {
            return null;
        }
        public static string DoTask1Test()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, TestFilePath)).ToList());
        }
        public static string DoTask1()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }
        public static string DoTask2Test()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, TestFilePath)).ToList());
        }

        public static string DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }
    }
}
