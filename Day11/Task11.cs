using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11
{
    public static class Task11
    {
        const int day = 11;
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
            var matrix = CreateMatrix(l);
            int count = 0;
            bool  isSync = CheckIfSync(matrix);
            while (!isSync)
            {
                count++;
                Transform(ref matrix);
                CountFlashes(matrix);
                isSync = CheckIfSync(matrix);
            }

           // DrawMatrix(matrix);
            return count.ToString();
        }

        public static string DoTask1(List<string> l)
        {
            var matrix = CreateMatrix(l);
            long totalF = 0;
           // int[] checklist = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100 };
           for (int i=0; i<100;i++)
            {
               Transform(ref matrix);
                int x= CountFlashes(matrix); ;
                totalF += x;
                //if (checklist.Contains(i + 1))
                //{
                //    Console.WriteLine(" ");
                //    Console.WriteLine(totalF.ToString() + "  step=" + (i + 1).ToString());
                //    DrawMatrix(matrix);
                //    Console.WriteLine(" ");
                //}
            }
            return totalF.ToString();
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

        private static int[,] CreateMatrix(List<string> list)
        {
            int rows = list.Count;
            int cols = list[0].Trim().Length;
            int[,] matrix = new int[rows, cols];

            for(int i=0;i<rows;i++)
            {
                var c = list[i].Trim().ToArray().Select(t=>int.Parse( t.ToString())).ToArray();
                for (int j = 0; j < cols; j++) matrix[i, j] = c[j];
            }
            return matrix;
        }

        private static void Transform( ref int[,] matrix )
        {
            int rowN = matrix.GetLength(0);
            int colN = matrix.GetLength(1);
           
            for(int i=0;i<rowN;i++)
            {
                for (int j = 0; j < colN; j++)
                {
                    matrix[i, j] += 1;
                    if (matrix[i, j] ==10) Prop(ref matrix, new Point { x = j, y = i });
                }
            }
           
        }
        private static void Prop(ref int[,]  matrix, Point p)
        {
            int rowN = matrix.GetLength(0);
            int colN = matrix.GetLength(1);
            List<Point> points = GenerateListToCheck(matrix, p, rowN, colN);

            foreach(var point in points)
            {
              int val=  matrix[point.y, point.x];
                val++;
                matrix[point.y, point.x] = val;
                if (val ==10) Prop(ref matrix, point);
            }
        }
        private struct Point
        {
            public int x { get; set; }
            public int y { get; set; }
            public int value { get; set; }
        }

        private static bool CheckIfPointIsValid(Point p,int rowN, int colN)
        {
            if (p.x < 0 || p.y < 0) return false;
            if (p.x >= colN || p.y >= rowN) return false;
            return true;
        }
        

        private static List<Point> GenerateListToCheck(int[,] matrix, Point p, int rowN,int colN)
        {
            List<Point> points = new List<Point>();

            Point l = new Point { x = p.x - 1, y = p.y };
            if (CheckIfPointIsValid(l, rowN, colN))
            {
                l.value = matrix[l.y, l.x];
                if (l.value < 10) points.Add(l);
            }

            Point r = new Point { x = p.x + 1, y = p.y };
            if (CheckIfPointIsValid(r, rowN, colN))
            {
                r.value = matrix[r.y, r.x];
                if (r.value < 10) points.Add(r);
            }

            Point u = new Point { x = p.x, y = p.y - 1 };
            if (CheckIfPointIsValid(u, rowN, colN))
            {
                u.value = matrix[u.y, u.x];
                if (u.value < 10) points.Add(u);
            }

            Point d = new Point { x = p.x, y = p.y + 1 };
            if (CheckIfPointIsValid(d, rowN, colN))
            {
                d.value = matrix[d.y, d.x];
                if (d.value < 10) points.Add(d);
            }

            Point lu = new Point { x = p.x - 1, y = p.y - 1 };
            if (CheckIfPointIsValid(lu, rowN, colN))
            {
                lu.value = matrix[lu.y, lu.x];
                if (lu.value < 10) points.Add(lu);
            }

            Point ru = new Point { x = p.x + 1, y = p.y - 1 };
            if (CheckIfPointIsValid(ru, rowN, colN))
            {
                ru.value = matrix[ru.y, ru.x];
                if (ru.value < 10) points.Add(ru);
            }

            Point ld = new Point { x = p.x - 1, y = p.y + 1 };
            if (CheckIfPointIsValid(ld, rowN, colN))
            {
                ld.value = matrix[ld.y, ld.x];
                if (ld.value < 10) points.Add(ld);
            }

            Point rd = new Point { x = p.x + 1, y = p.y + 1 };
            if (CheckIfPointIsValid(rd, rowN, colN))
            {
                rd.value = matrix[rd.y, rd.x];
                if (rd.value < 10) points.Add(rd);
            }
            return points;
        }

       
        private static int CountFlashes(int[,] matrix)
        {
            int rowN = matrix.GetLength(0);
            int colN = matrix.GetLength(1);
            int refVal = matrix[0, 0];
            int count = 0;
            for (int i = 0; i < rowN; i++)
            {
                for (int j = 0; j < colN; j++)
                {
                    if (matrix[i, j] > 9)
                    {
                        matrix[i, j] = 0;
                        count++;
                    }
                }
            }
            return count;
        }

        private static bool CheckIfSync(int[,] matrix)
        {
            int rowN = matrix.GetLength(0);
            int colN = matrix.GetLength(1);
            int refVal = matrix[0, 0];
          
            for (int i = 0; i < rowN; i++)
            {
                for (int j = 0; j < colN; j++)
                    if (matrix[i, j] != refVal) return false;
            }
            return true;
        }
        private static void DrawMatrix(int[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                string str = "";
                for (int j = 0; j < cols; j++)
                 str += matrix[i,j];
                Console.WriteLine(str);
            }
        }
    }
}
