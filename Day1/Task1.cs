using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day1
{
    public static class Task1
    {
        const string Day = "Day1";
        const string FilePath = @"Day1\d11.txt";
        static List<int> testList = new List<int> { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
        public static void Start()
        {
            Console.WriteLine(Day + "-  Task1 Test Result = " + DoTask1(testList));
            Console.WriteLine(Day + "-  Task1 Result = " + DoTask1());

            Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(testList));
            Console.WriteLine(Day + "-  Task2 Result = " + DoTask2());
        }
       
        public static int DoTask1(List<int>l)
        {
            int c = 0;
            for (int i = 1; i < l.Count;i++) if (l[i] > l[i - 1]) c ++;
            return c;
        }

        public static int DoTask1()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).Select(t => int.Parse(t)).ToList());
        }

        public static int DoTask2(List<int> l)
        {
            List<int> agg = new List<int>();
            for (int i = 2; i < l.Count; i++) agg.Add(l[i - 2] + l[i - 1] + l[i]);
            return DoTask1(agg);
        }
        public static int DoTask2()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).Select(t => int.Parse(t)).ToList());
        }
    }
}
