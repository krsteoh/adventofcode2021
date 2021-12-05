using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
{
    public static class Task3
    {
        const string Day = "Day3";
        const string FilePath = @"Day3\d31.txt";
        static List<string> testList = new List<string> { "00100", "1111", "10110", "10111", "10101", "01111", "00111", "11100", "10000", "11001", "00010", "01010" };

        public static void Start()
        {
            Console.WriteLine(Day+"-    Task1 Test Result = " + DoTask1(testList));
            Console.WriteLine(Day + "-  Task1 Result = " +  DoTask1());

            Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(testList));
            Console.WriteLine(Day + "-  Task2 Result = " + DoTask2());
        }

        public static int DoTask1(List<string> l)
        {
            if (l.Count == 0) return 0;

            int[] bits;
            bits = new int[l[0].Length];
          foreach(var item in l)
            {
                var arr = item.ToArray();
                for(int i=0; i<arr.Length;i++)
                {
                    if (arr[i]=='1') bits[i]++;
                }
            }

            string mcResult = "";
            string lcResult = "";
            int count = l.Count()/2;
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] > count) 
                {
                    mcResult += "1";
                    lcResult += "0";
                }
                else
                {
                    mcResult += "0";
                    lcResult += "1";
                }
            }
            return Convert.ToInt32(mcResult, 2) * Convert.ToInt32(lcResult, 2);
        }

        public static int DoTask1()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }

        public static int DoTask2(List<string> l)
        {
            if (l.Count == 0) return 0;

            var oxgList = new List<string>();
            var co2List = new List<string>();
            var oxgStr = "";
            var co2Str = "";

            var count = l.Count / 2;
            var nb1 = l.Select(t => t[0]).Where(t => t == '1').Count();
            if (nb1 >= count)
            {
                // 1 more than 0
                oxgStr += "1";
                co2Str += "0";
               
            }
            else
            {
                //0 more than 1
                oxgStr += "0";
                co2Str += "1";
            }

            oxgList = l.Where(t => t.StartsWith(oxgStr)).ToList();
            co2List = l.Where(t => t.StartsWith(co2Str)).ToList();

            while(oxgList.Count>1)
            {
                string startStr = oxgStr + "1";
                decimal c = (decimal)oxgList.Count / (decimal)2;
                var n= oxgList.Where(t => t.StartsWith(startStr)).Count();

                if (n>= c)oxgStr += "1";
                else oxgStr += "0";

                oxgList = oxgList.Where(t => t.StartsWith(oxgStr)).ToList();
            }
            while (co2List.Count > 1)
            {
                string startStr = co2Str + "0";
                decimal c = (decimal)co2List.Count / (decimal)2;
                var n = co2List.Where(t => t.StartsWith(startStr)).Count();

                if (n <= c) co2Str += "0";
                else co2Str += "1";

                co2List = co2List.Where(t => t.StartsWith(co2Str)).ToList();
            }
            return  Convert.ToInt32(oxgList.First().Trim(), 2) * Convert.ToInt32(co2List.First().Trim(), 2); ;
        }
        public static int DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }
    }
}
