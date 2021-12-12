using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12
{
    public static class Task12
    {
        const int day = 12;
        static string Day = string.Format("Day{0}", day);
        static string FilePath = string.Format(@"Day{0}\d{0}.txt", day);
        static string TestFilePath = string.Format(@"Day{0}\d{0}test.txt", day);
        static string TestFilePath2 = string.Format(@"Day{0}\d{0}test2.txt", day);
        static string TestFilePath3 = string.Format(@"Day{0}\d{0}test3.txt", day);

        public static void Start()
        {
           //GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask1Test);
           // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test2 Result = ", Day), DoTask1Test2);
           // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test2 Result = ", Day), DoTask1Test3);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Result = ", Day), DoTask1);

           // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test);
          //GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test2);
          // GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task1 Test Result = ", Day), DoTask2Test3);
            GlobalFunctions.ConsolePrintTask(string.Format("{0}- Task2 Result = ", Day), DoTask2);
        }
        public static string DoTask2(List<string> l)
        {
            string start = "start";
            var dic = CreateStructure(l);
            List<List<string>> paths = new List<List<string>>();
            FindPath2(dic, start, new List<string>(), paths);
            PrintPaths(paths);
            return paths.Count.ToString();
        }

        public static string DoTask1(List<string> l)
        {
            string start = "start";
            var dic = CreateStructure(l);
            List<List<string>> paths = new List<List<string>>();
            FindPath(dic, start,new List<string>(), paths);
            PrintPaths(paths);
            return paths.Count.ToString();
        }
        public static string DoTask1Test()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, TestFilePath)).ToList());
        }
        public static string DoTask1Test2()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, TestFilePath2)).ToList());
        }
        public static string DoTask1Test3()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, TestFilePath3)).ToList());
        }
        public static string DoTask1()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }
        public static string DoTask2Test()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, TestFilePath)).ToList());
        }
       
        public static string DoTask2Test2()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, TestFilePath2)).ToList());
        }
        public static string DoTask2Test3()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, TestFilePath3)).ToList());
        }
        public static string DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }


        private static void FindPath(Dictionary<string, List<string>> dic, string node, List<string> path,List<List<string>> paths)
        {
            if (node == "end")
            {
                path.Add(node);
                paths.Add(path);
                return;
            }
            if (!dic.Keys.Contains(node)) return;
            var nNode = dic.First(t => t.Key == node);
            
            foreach (var item in nNode.Value)
            {  
                if(item!="start" && item != "end")
                {
                    var firstLett = item[0].ToString();
                    var firstlettNode = node[0].ToString();
                    if (firstLett != firstLett.ToUpper() && firstlettNode!= firstlettNode.ToUpper() && path.Any(t => t == item || t == node)) continue;
                    else if (firstLett != firstLett.ToUpper() && firstlettNode == firstlettNode.ToUpper() && path.Any(t => t == item)) continue;
                }

                if (node == "start" && path.Count > 0) return;

                var nPath = new List<string>(path);
                nPath.Add(node);
                FindPath(dic, item, nPath, paths);
            }
        }
        private static void FindPath2(Dictionary<string, List<string>> dic, string node, List<string> path, List<List<string>> paths)
        {
            if (node == "end")
            {
                path.Add(node);
                paths.Add(path);
                return;
            }
            if (!dic.Keys.Contains(node)) return;
            var nNode = dic.First(t => t.Key == node);

            foreach (var item in nNode.Value)
            {
                if (item != "start" && item != "end")
                {
                    var firstLett = item[0].ToString();
                    if (firstLett != firstLett.ToUpper() && !CheckIsValid(path,item,node)) continue;
                   
                }

                if (node == "start" && path.Count > 0) return;

                var nPath = new List<string>(path);
                nPath.Add(node);
                FindPath2(dic, item, nPath, paths);
            }
        }
        private static void PrintPaths(List<List<string>> paths)
        {
            foreach(var item in paths)
            {
                string str = item.Aggregate((x, y) => string.Format("{0},{1}",x,y));
                Console.WriteLine(str);
            }
        }

        private static Dictionary<string,List<string>> CreateStructure(List<string> list)
        {
            var dic = new Dictionary<string, List<string>>();

            foreach(var item in list)
            {
                var arr = item.Trim().Split('-');
                if (!dic.ContainsKey(arr[0]))
                {
                    var l = list.Where(t => t.StartsWith(arr[0] + "-")).Select(t => t.Split('-')[1]).ToList();
                    var l2= list.Where(t => t.EndsWith( "-"+ arr[0])).Select(t => t.Split('-')[0]).ToList();
                    l.AddRange(l2);
                    dic.Add(arr[0], l.Distinct().ToList());
                }
                if (!dic.ContainsKey(arr[1]))
                {
                    var l = list.Where(t => t.StartsWith(arr[1] + "-")).Select(t => t.Split('-')[1]).ToList();
                    var l2 = list.Where(t => t.EndsWith("-" + arr[1])).Select(t => t.Split('-')[0]).ToList();
                    l.AddRange(l2);
                    dic.Add(arr[1], l.Distinct().ToList());
                }

            }

            return dic;
        }

        private static bool CheckIsValid(List<string> list, string str1, string str2)
        {
            var l = new List<string>();
            foreach(var item in list)
            {
                if (item == "start" || item=="end") continue;
                string lett = item[0].ToString();
                if (lett != lett.ToUpper()) l.Add(item);
            }
            var s1 = str1[0].ToString();
            var s2 = str2[0].ToString();
            if (s1 != s1.ToUpper()) l.Add(str1);
            if (s2 != s2.ToUpper()) l.Add(str2);

            var bb = l.GroupBy(t => t).Select(p => new { val = p.Key, count = p.Count() }).ToList();
            var num = bb.Count(t => t.count > 1);
            if (num > 1 || bb.Any(t=>t.count>2)) return false;

            return true; 
        }
    }
}
