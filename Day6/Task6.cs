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
        static List<Byte> inputList = new List<Byte> { 3, 4, 3, 1, 2 };



        public static void Start()
        {
           Console.WriteLine(Day + "-  Task1 Test Result = " + DoTask1(inputList,18,true));
           Console.WriteLine(Day + "-  Task1 Result = " + DoTask1());

           Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(inputList,256));
           Console.WriteLine(Day + "-  Task2 Result = " + DoTask2());
        }

        public static long DoTask1(List<Byte> l,int days,bool print)
        {

            return Solution1(l, days, print);
        }

        public static long DoTask2(List<Byte> l, int days)
        {
            return Solution2(l, days);
        }
        public static long DoTask1()
        {
            
            return DoTask1(System.IO.File.ReadAllText(string.Format("{0}{1}", Config.LocalPath, FilePath)).Split(',').Select(t=> Byte.Parse(t)).ToList(),80,false);
        }
        public static long DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllText(string.Format("{0}{1}", Config.LocalPath, FilePath)).Split(',').Select(t => Byte.Parse(t)).ToList(), 256);
        }
        private static void PrintList(Byte[] l)
        {
            Console.WriteLine(l.Select(t => t.ToString()).Aggregate((i, j) => string.Format("{0} {1}", i, j)));
        }

        private static long Solution1(List<Byte> l, int days, bool print)
        {

            long add = 0;
            long count = l.LongCount();
            for (long i = 1; i <= days; i++)
            {

                Byte[] list = new Byte[count];
                list = l.ToArray();

                for (long n = 0; n < count; n++)
                {

                    list[n]--;
                    if (list[n] == 255)
                    {
                        list[n] = 6;
                        add++;
                    }
                }
                l = list.ToList();
                if (add > 0) count += add;

                for (long x = 0; x < add; x++)
                {

                    l.Add(8);
                }
                add = 0;
                if (print) PrintList(l.ToArray());
            }
            return l.LongCount();
        }

        private static long Solution2(List<Byte> l, int days)
        {
            var lElem = new List<long>();

            for (byte z = 0; z <= 8; z++)
             lElem.Add(l.Where(t => t == z).Count());
            
            for (long i = 1; i <= days; i++)
            {
                long zeroElem = lElem[0];
                lElem.RemoveAt(0);
                lElem.Add(zeroElem);
                lElem[6]+= zeroElem;
            }
             return lElem.Sum(t => t);
        }

    }
}
