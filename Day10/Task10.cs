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
        static string FilePath = string.Format(@"Day{0}\d{0}.txt", day);
        static string TestFilePath = string.Format(@"Day{0}\d{0}test.txt", day);

        public static void Start()
        {
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask1Test);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Result = ", Day), DoTask1);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task2 Result = ", Day), DoTask2);
        }
        public static string DoTask2(List<string> l)
        {
            var openArr = "<[{(".ToArray();
            List<long> results = new List<long>();
            foreach (var item in l)
            {
                bool isCorrupted = false;
                var q = new Stack<char>();
                var arr = item.Trim().ToArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (openArr.Contains(item[i])) { q.Push(item[i]); continue; }
                    var ii = q.Pop();
                    if (ii != Oposit(item[i]))
                    {
                        isCorrupted = true;
                        break;
                    }
                }

                if (isCorrupted) continue;

                long s = 0;
                while(q.Count>0)
                {
                    var it = q.Pop();
                    s *= 5;
                    s+= GetValueForComplete(Oposit(it));
                }
                results.Add(s);
            }
            int index = (results.Count / 2);
            results = results.OrderBy(t => t).ToList();
            return results[index].ToString();
        }

        public static string DoTask1(List<string> l)
        {
            var openArr = "<[{(".ToArray();
            long ret = 0;
            //List<string> results = new List<string>();
            foreach(var item in l)
            {
                var q = new Stack<char>();
                var arr = item.Trim().ToArray();
                for(int i=0;i<arr.Length;i++)
                {
                    if (openArr.Contains(item[i])) { q.Push(item[i]); continue; }
                    var ii = q.Pop();
                    if(ii!= Oposit(item[i]))
                    {
                       // results.Add(String.Format("{0}:{1}", Oposit(ii), item[i]));
                        ret += GetValue(item[i]);
                        break;
                    }
                }
            }
            return ret.ToString();
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

        private static char Oposit(char c)
        {
            if (c == '<' ) return '>';
            if (c == '[' ) return ']';
            if (c == '{' ) return '}';
            if (c == '(') return ')';
            if (c == '>') return '<';
            if (c == ']') return '[';
            if (c == '}') return '{';
            if (c == ')') return '(';

            return ' ';
        }

        private static int GetValueForComplete(char c)
        {
            if (c == '>') return 4;
            if (c == ']') return 2;
            if (c == '}') return 3;
            if (c == ')') return 1;

            return 0;
        }
        private static int GetValue(char c)
        {
          
            if (c == '>') return 25137;
            if (c == ']') return 57;
            if (c == '}') return 1197;
            if (c == ')') return 3;

            return 0;
        }
    }
}
