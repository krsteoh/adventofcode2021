using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6
{
   public static class Task6
    {
        const string Day = "Day6";
        const string FilePath = @"Day6\d61.txt";
        static List<int> inputList = new List<int> { 3, 4, 3, 1, 2 };



        public static void Start()
        {
            Console.WriteLine(Day + "-  Task1 Test Result = " + DoTask1(inputList,18,true));
            Console.WriteLine(Day + "-  Task1 Result = " + DoTask1());

            //Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(inputList));
            //Console.WriteLine(Day + "-  Task2 Result = " + DoTask2());
        }



        public static int DoTask1(List<int> l,int days,bool print)
        {
            int add = 0;

            for (int i = 1; i <= days; i++)
            {
                for (int n = 0; n < l.Count; n++)
                {
                    l[n]--;
                    if (l[n] == -1)
                    {
                        l[n] = 6;
                        add++;
                    }
                }

                for (int x = 0; x < add; x++)
                {
                    l.Add(8);
                }
                add = 0;
                if (print) PrintList(l);
            }
            return l.Count();
        }

        public static int DoTask2(List<string> l)
        {
            return 0;
        }
        public static int DoTask1()
        {
            
            return DoTask1(System.IO.File.ReadAllText(string.Format("{0}{1}", Config.LocalPath, FilePath)).Split(',').Select(t=>int.Parse(t)).ToList(),80,false);
        }
        public static int DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }
        private static void PrintList(List<int> l)
        {
            Console.WriteLine(l.Select(t => t.ToString()).Aggregate((i, j) => string.Format("{0} {1}", i, j)));
        }
    }
}
