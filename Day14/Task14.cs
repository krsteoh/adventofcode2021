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
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask1Test);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Result = ", Day), DoTask1);
           // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test);
           // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task2 Result = ", Day), DoTask2);
        }
        public static string DoTask2(List<string> l)
        {

            var str = l[0].Trim().ToArray().ToList();

            var dic = new ConcurrentDictionary<char, long>();

            dic = new ConcurrentDictionary<char, long>( str.GroupBy(t=>t).Select(t => new KeyValuePair<char, long>(t.Key, t.Count())));


            var instructions = GetInstructions(l);
            

            for (int i = 1; i < str.Count; i++)
            {
                var ss = str[i - 1].ToString() + str[i].ToString();
                
                TransfromV3(ss, ref dic,  0, instructions);
            }

           
            var grList = dic.Select(t=>t.Value).GroupBy(t => t).Select(k => k.LongCount());
            return (grList.Max() - grList.Min()).ToString();
        }

        public static string DoTask1(List<string> l)
        {
            var str = l[0].ToArray().ToList();
            var instructions = GetInstructions(l);
            for(int i=0;i<10;i++)
            {
                Transfrom(ref str, instructions);
            }
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
                var inst = instructions.First(t => t.From == f);
                nStr.Add(inst.To);
                nStr.Add(str[i]);

            }
            str= nStr;
        }
        public static string TransfromV2( string str, List<Instruction> instructions)
        {
            string s = "";
            foreach(var inst in instructions)
                s = str.Replace(inst.From, inst.ToStr);
            return s;
        }

        public static void TransfromV3(string str, ref ConcurrentDictionary<char, long> dic, int count, List<Instruction> instructions)
        {
            if (count == 40) return;
            var inst = instructions.First(t => t.From == str);

            if (dic.ContainsKey(inst.To)) dic[inst.To] = dic.First(t => t.Key == inst.To).Value + 1;
            else dic.TryAdd(inst.To, 1);
            count++;

            TransfromV3(inst.ToStr.Substring(0, 2), ref dic, count, instructions);
            TransfromV3(inst.ToStr.Substring(1, 2), ref dic, count, instructions);

        }
        //public static  string TransfromV3 ( ref string str, List<Instruction> instructions)
        //{
        //    //string s = "Go west Life is peaceful there";
        //    //s = Regex.Replace(s, @"\bwest\b", "something");

        //    foreach (var inst in instructions)
        //        str +=Regex.Replace(str, string.Format(@"{0}", inst.From), inst.ToStr);

        //    return str;
        //}

        //public static List<char> TransformV4(char[] str, List<Instruction> instructions)
        //{
        //    string f1 = new string(str);

        //    var in1 = instructions.First(t => t.From == f1);tra

        //}

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
