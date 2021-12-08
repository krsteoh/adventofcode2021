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
        const string TestFilePath = @"Day8\d8test.txt";
    
        public static void Start()
        {
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask1Test);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Result = ", Day), DoTask1);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task2 Result = ", Day), DoTask2);
        }

        public static string DoTask2(List<string> l)
        {
            List<int> numbers = new List<int>();
            foreach (var listStr in l)
            {
                var arr = listStr.Split('|');
                List<string> sig = CleanText(arr[0]).Split(" ").ToList();
                List<string> dig = CleanText(arr[1]).Split(" ").ToList();

                List<Elem> list = sig.Select(t => new Elem { Value = new string(t.Trim().ToCharArray().OrderBy(t=>t).ToArray()), Count = t.Trim().Length }).ToList();
                List<Elem> listDig = dig.Select(t => new Elem { Value = new string(t.Trim().ToCharArray().OrderBy(t => t).ToArray()), Count = t.Trim().Length }).ToList();

                Dictionary<int, string> dic = new Dictionary<int, string>();
                dic.Add(1, list.Where(t => t.Count == 2).Select(t => t.Value).First());
                dic.Add(7, list.Where(t => t.Count == 3).Select(t => t.Value).First());
                dic.Add(8, list.Where(t => t.Count == 7).Select(t => t.Value).First());
                dic.Add(4, list.Where(t => t.Count == 4).Select(t => t.Value).First());

                var elem1 = dic.Where(t => t.Key == 1).First();
                var elem4 = dic.Where(t => t.Key == 4).First();
                var elem7 = dic.Where(t => t.Key == 7).First();
                foreach (var item in list.Where(t=>t.Count==6).ToList())
                {
                    if (!Contains(item.Value,elem1.Value))
                    {
                        dic.Add(6, item.Value);
                        continue;
                    }
                    if (!Contains(item.Value,elem4.Value)) dic.Add(0, item.Value);
                    else dic.Add(9, item.Value);
                    
                }
                foreach (var item in list.Where(t => t.Count == 5).ToList())
                {
                    if (Contains(item.Value,elem1.Value) && Contains(item.Value,elem7.Value) && !Contains(item.Value,elem4.Value))
                    {
                        dic.Add(3, item.Value);
                        continue;
                    }
                    var elem6 = dic.Where(t => t.Key == 6).First();
                    if (Contains(elem6.Value,item.Value)) dic.Add(5, item.Value);
                    else dic.Add(2, item.Value);
                }
                var ss = "";
                foreach (var d in listDig)
                {
                    if (d.Count == 7) ss+="8";
                    else if (d.Count == 4) ss += "4"; 
                    else if (d.Count == 2) ss += "1"; 
                    else if (d.Count == 3) ss += "7";
                    else ss+=dic.First(c => c.Value == d.Value).Key.ToString();
                }
                numbers.Add(int.Parse(ss));
            }
            return numbers.Sum().ToString();
        }

        public static string DoTask1(List<string> l)
        {
            int total = 0;
            foreach (var listStr in l)
            {
                List<string> dig = CleanText(listStr.Split('|')[1]).Split(" ").ToList();
                foreach (var item in dig)
                {
                    var numDig = item.Trim().Length;
                    if (numDig == 7 || numDig == 4 || numDig == 2 || numDig == 3) total++;
                }
            }
            return total.ToString();
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
        public static string CleanText(string str)
        {
            str = str.Replace("   ", " ");
            return str.Replace("  ", " ").Trim();
        }

        public static bool Contains(string text,string word)
        {
            var s1 = text.ToArray();
            var s2 = word.ToArray();

            foreach(var c in s2)
             if (!s1.Any(t => t == c)) return false;
            
            return true;
        }

        public struct Elem
        {
            public int Count { get; set; }
            public string Value { get; set; }
        }
    }
}
