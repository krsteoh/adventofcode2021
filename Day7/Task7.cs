using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7
{
   public class Task7
    {
        const string Day = "Day7";
        const string FilePath = @"Day7\d71.txt";
        static List<int> inputList = new List<int> { 16, 1, 2, 0, 4, 2, 7, 1, 2, 14 };

        public static void Start()
        {
            Console.WriteLine(Day + "-  Task1 Test Result = " + DoTask1(inputList));
            Console.WriteLine(Day + "-  Task1 Result = " + DoTask1());

            Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(inputList));
            Console.WriteLine(Day + "-  Task2 Result = " + DoTask2());
        }

        public static string DoTask1(List<int> l)
        {
            return DoTask(l, CalcRate1);
        }

        public static string DoTask2(List<int> l)
        {
            return DoTask(l, CalcRate2);
        }
        public static string DoTask1()
        {
            return DoTask1(System.IO.File.ReadAllText(string.Format("{0}{1}", Config.LocalPath, FilePath)).Split(',').Select(t => int.Parse(t)).ToList());
        }
        public static string DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllText(string.Format("{0}{1}", Config.LocalPath, FilePath)).Split(',').Select(t => int.Parse(t)).ToList());
        }
        private static int CalcRate1(int num, Elem item)
        {
            return Math.Abs(num - item.Value) ;
        }
        private static int CalcRate2(int num, Elem item)
        {
            int dif = Math.Abs(num - item.Value);
            int rate = 0;
            for (int i = 1; i <= dif; i++)
             rate += i;
            
            return rate;
        }

        private static string DoTask(List<int> l,Func<int,Elem,int> calculateRate)
        {
            var list = l.GroupBy(t => t).Select(t => new Elem { Value = t.Key, Count = t.Count() }).ToList();
            int maxNum = l.Max();
            int minNum = l.Min();
            int minVal = -1;
            int foundN = -1;
            for (int i = minNum; i <= maxNum; i++)
            {
                var val = list.Sum(t => calculateRate(i, t) * t.Count);
                if (minVal == -1 || minVal > val)
                {
                    minVal = val;
                    foundN = i;
                }
            }
            return string.Format("{0}->{1}", foundN, minVal);
        }
        public struct Elem
        {
            public int Value { get; set; }
            public int Count { get; set; }
        }
    }
}
