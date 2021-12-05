using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2
{
   public static class Task2
    {
        const string UP = "up";
        const string FORWARD = "forward";
        const string Day = "Day2";
        const string FilePath =@"Day2\d21.txt";
        static List<string> testList = new List<string> { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };
        public static void Start()
        {
            Console.WriteLine(Day + "-  Task1 Test Result = " + DoTask1(testList));
            Console.WriteLine(Day + "-  Task1 Result = " + DoTask1());

            Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(testList));
            Console.WriteLine(Day + "-  Task2 Result = " + DoTask2());
        }

        public static int DoTask1(List<string> l)
        {
            var xList = l.Where(t => t.StartsWith(FORWARD)).Select(t => int.Parse(t.Split(' ')[1])).ToList();
            var yList = l.Where(t => !t.StartsWith(FORWARD)).Select(t => t.StartsWith(UP)?(-1*int.Parse(t.Split(' ')[1])): int.Parse(t.Split(' ')[1])).ToList();
            return xList.Sum() * yList.Sum();
        }

        public static int DoTask1()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }

        public static int DoTask2(List<string> l)
        {
            int aim = 0;
            int y = 0;
            int x = 0;
            foreach(var item in l)
            {
                var itemList = item.Split(" ");

                if (itemList[0].Equals(FORWARD))
                {
                    int xPart = int.Parse(itemList[1]);
                    x += xPart;
                    y = y + (xPart * aim);
                }
                else if(itemList[0].Equals(UP)) aim -= int.Parse(itemList[1]);
                else aim += int.Parse(itemList[1]);

            }
            return x*y;
        }
        public static int DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }
    }
}
