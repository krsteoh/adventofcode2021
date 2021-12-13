using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day13
{
    public static class Task13
    {
        const int day = 13;
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
            var points = GetPoints(l);
            var instructions = GetInstructions(l);
            var matrix = CreateMatrix(points);

            foreach (var instruction in instructions)
            {

                if (instruction.x == 0) matrix = OverlapY(matrix, instruction.y);
                 else matrix = OverlapX(matrix, instruction.x);
             
             }
            DrawMatrix(matrix);
            return CountDots(matrix).ToString();
        }

        public static string DoTask1(List<string> l)
        {
            var points = GetPoints(l);
            var instructions = GetInstructions(l);
            var matrix = CreateMatrix(points);
            var instruction = instructions[0];
           // foreach(var instruction in instructions)
            //{

            if (instruction.x == 0) matrix = OverlapY(matrix, instruction.y);
            else matrix = OverlapX(matrix, instruction.x);
             // DrawMatrix(matrix);
           // }


            return CountDots(matrix).ToString();
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

        private static bool[,] OverlapY(bool[,] matrix, int y)
        {
            int rNOrig = matrix.GetLength(0);
            int cNOrig= matrix.GetLength(1);
            int rN = y;
            int cN= cNOrig ;
            bool[,] newMatrix = new bool[rN, cN];

            for (int i = 0; i < rN; i++)
            {
                for (int j = 0; j < cN; j++)
                {
                    int yy = 2 * y - i;
                    if (yy < rNOrig ) newMatrix[i, j] = matrix[i, j] | matrix[yy, j];
                    else newMatrix[i, j] = matrix[i, j];
                }
            }

            return newMatrix;

        }
        private static bool[,] OverlapX(bool[,] matrix, int x)
        {
            int rNOrig = matrix.GetLength(0);
            int cNOrig = matrix.GetLength(1);
            int rN = rNOrig;
            int cN = x;
            bool[,] newMatrix = new bool[rN, cN];

            for (int i = 0; i < rN; i++)
            {
                for (int j = 0; j < cN; j++)
                {
                    int xx = 2 * x - j;
                    if (xx < cNOrig) newMatrix[i, j] = matrix[i, j] | matrix[i, xx];
                    else newMatrix[i, j] = matrix[i, j];
                }
            }

            return newMatrix;

        }
        private static long CountDots(bool[,] matrix)
        {
            int rNOrig = matrix.GetLength(0);
            int cNOrig = matrix.GetLength(1);

            long count = 0;

            for (int i = 0; i < rNOrig; i++)
            {
                for (int j = 0; j < cNOrig; j++)
                    if (matrix[i, j]) count++;
            }

            return count;

        }

        public static List<Point> GetPoints(List<string> list)
        { 
            var points = new List<Point>();
            
            foreach(var item in list)
            {
                if (string.IsNullOrWhiteSpace(item)) return points;
                var arr = item.Trim().Split(',');
                points.Add(new Point {y=int.Parse(arr[1]), x = int.Parse(arr[0]) });
               
            }
            return points;
        }
        public static List<Instruction> GetInstructions(List<string> list)
        {
            var instructions = new List<Instruction>();

            foreach (var item in list.Where(t=>t.StartsWith("fold")).ToList())
            {
                var arr = item.Trim().Split(' ');
                var arr2 = arr[2].Split('=');
                int _x = 0;
                int _y = 0;
                if (arr2[0] == "x") _x = int.Parse(arr2[1]);
                else _y= int.Parse(arr2[1]);
                instructions.Add(new Instruction {x=_x,y=_y });


            }
            return instructions;
        }

        public static bool[,] CreateMatrix(List<Point> points)
        {
            int rN = points.Select(t => t.y).Max()+1;
            int cN = points.Select(t => t.x).Max()+1;
            bool[,] matrix = new bool[rN, cN];

            for (int i = 0; i < rN; i++)
            {
                for (int j = 0; j < cN; j++)
                {
                    bool c = false;
                    if (points.Any(t => t.y == i && t.x == j)) c = true;

                    matrix[i, j] = c;
                }
            }
            return matrix;
        }
        public static void DrawMatrix(bool[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            Console.WriteLine();
            for (int i = 0; i < rows; i++)
            {
                string str = "";
                for (int j = 0; j < cols; j++)
                 str += (matrix[i,j]?"#":".");
                    
                Console.WriteLine(str);

            }
            Console.WriteLine();
        }
        public struct Point
        {
            public int x { get; set; }
            public int y { get; set; }
        }

        public struct Instruction
        {
            public int y { get; set; }
            public int x { get; set; }
         
        }

        


    }
}
