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
           // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Result = ", Day), DoTask1);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test);
           // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task2 Result = ", Day), DoTask2);
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

            for (int i=0;i<0;i++)
            {
                 NextStep(ref dic, instructions);
            }

            Dictionary<string, long> dd = new Dictionary<string, long>();

            foreach (var item in dic)
            {
                var ins = instructions.First(t => t.From == item.Key);
                string s = ins.To.ToString();

                if (dd.ContainsKey(s)) dd[s] = dd[s] + item.Value;
                else dd.Add(s, item.Value);


            }
            //foreach (var item in dic)
            //{
            //    string s1 = item.Key[0].ToString();
            //    string s2 = item.Key[1].ToString();

            //    long cout = item.Value;

            //    if (dd.ContainsKey(s1)) dd[s1] = dd[s1] + cout;
            //    else dd.Add(s1, cout);

            //    if (dd.ContainsKey(s2)) dd[s2] = dd[s2] + cout;
            //    else dd.Add(s2, cout);

            //}

            var grList = dd.Select(k => k.Value);
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
            foreach(var inst in instructions)
                s = str.Replace(inst.From, inst.ToStr);
            return s;
        }

        public static void TransfromV3(string str, ref ConcurrentDictionary<char, long> dic, int count, List<Instruction> instructions)
        {
            if (count == 40) return;
            Instruction inst = instructions.First(t => t.From == str);

            if (dic.ContainsKey(inst.To)) dic[inst.To] = dic.First(t => t.Key == inst.To).Value + 1;
            else dic.TryAdd(inst.To, 1);
            count++;

            TransfromV3(inst.ToStr.Substring(0, 2), ref dic, count, instructions);
            TransfromV3(inst.ToStr.Substring(1, 2), ref dic, count, instructions);

        }

        public static void Group(ref Dictionary<string,long> dic,List<char> str)
        {
            for (int i = 1; i < str.Count; i++)
            {
                string s = str[i - 1].ToString() + str[i].ToString();
                if (dic.ContainsKey(s))
                {
                    dic[s] = dic[s] + 1;
                }
                else
                {
                    dic.Add(s, 1);
                }

            }
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
           // return ndic;
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
