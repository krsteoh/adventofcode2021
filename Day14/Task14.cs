using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day14
{
    public static class Task14
    {
        const int day = 14;
        static string Day = string.Format("Day{0}", day);
        static string FilePath = string.Format(@"Day{0}\d{0}.txt", day);
        static string TestFilePath = string.Format(@"Day{0}\d{0}test.txt", day);

        public static void Start()
        {
           // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask1Test);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Result = ", Day), DoTask1);
            //GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task2 Result = ", Day), DoTask2);
        }
        public static string DoTask2(List<string> l)
        {
            var str = l[0].Trim().ToArray().ToList();
            var instructions = GetInstructions(l);
            Dictionary<string, long> dic = new Dictionary<string, long>(instructions.Select(t => new KeyValuePair<string, long>(t.From, 0)));

          for(int i=1;i<str.Count;i++)
            {
                string ss = str[i - 1].ToString() + str[i].ToString();
                dic[ss] = dic[ss] + 1;
            }

            for (int i=0;i<40;i++) NextStep(ref dic, instructions);
            
            Dictionary<string, long> dd = new Dictionary<string, long>();

            foreach (var item in dic)
            {
                string s1 = item.Key[0].ToString();
                long cout = item.Value;
                if (dd.ContainsKey(s1)) dd[s1] = dd[s1] + cout;
                else dd.Add(s1, cout);
            }
            string last = str[str.Count - 1].ToString();
            dd[last] = dd[last] + 1;
            var grList = dd.Select(k => k.Value);
            return (grList.Max() - grList.Min()).ToString();
        }

        public static string DoTask1(List<string> l)
        {
            var str = l[0].ToArray().ToList();
            var instructions = GetInstructions(l);
            for(int i=0;i<10;i++) Transfrom(ref str, instructions);
            
            var grList = str.GroupBy(t => t).Select(k => k.Count());
            return (grList.Max()-grList.Min()).ToString();
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

        public static void Transfrom(ref List<char> str, List<Instruction> instructions)
        {
            List<char> nStr = new List<char>();
            nStr.Add(str[0]);
            for (int i=1;i<str.Count;i++)
            {
                string f = str[i - 1].ToString() + str[i].ToString();
                if (!instructions.Any(t => t.From == f)) continue;
                var inst = instructions.First(t => t.From == f);
                nStr.Add(inst.To);
                nStr.Add(str[i]);
            }
            str= nStr;
        }
        public static string TransfromV2( string str, List<Instruction> instructions)
        {
            string s = "";
            foreach(var inst in instructions)s = str.Replace(inst.From, inst.ToStr);
            return s;
        }

        public static void NextStep( ref Dictionary<string, long> dic, List<Instruction> instructions)
        {
            Dictionary<string, long> ndic = new Dictionary<string, long>( instructions.Select(t=>new KeyValuePair<string,long>( t.From,0)));
            foreach (var item in dic)
             {
                if (item.Value == 0) continue;
                   var insr = instructions.First(t => t.From == item.Key);
                   string item1 = insr.ToStr.Substring(0, 2);
                   string item2= insr.ToStr.Substring(1, 2);
                ndic[item1] +=  item.Value;
                ndic[item2] +=  item.Value;
                
            }
            dic = ndic;
        }
      
        public static List<Instruction> GetInstructions(List<string> list)
        {
            List<Instruction> instr = new List<Instruction>();

            foreach(var item in list.Where(t=>t.Contains("->")).ToList())
            {
                Instruction i = new Instruction();
                var arr = item.Trim().Split("->");
                var arrFrom = arr[0].Trim().ToArray();
                i.From = arr[0].Trim();
                i.To= char.Parse(arr[1].Trim());
                i.ToStr = arrFrom[0].ToString() + arr[1].Trim().ToString() + arrFrom[1].ToString();
                instr.Add(i);
            }
            return instr;
        }
        public struct Instruction 
        { 
            public string From { get; set; }
            public char To { get; set; }
            public string ToStr { get; set; }
        }



    }
}
