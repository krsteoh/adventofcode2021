using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9
{
    public static class Task9
    {
        const int day = 9;
        static string Day = string.Format("Day{0}", day);
        static string FilePath = string.Format(@"Day{0}\d{0}1.txt", day);
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
            int[,] matrix = CreateMatrix(l);
            var points = new List<Point>();
               FindMinPoints(matrix, points);
           
            List<int> counts = new List<int>();
            foreach (var point in points)
            {

                var pp = new List<Point>();
                 pp.Add(point);
                FindBasin(matrix, point, pp);
                counts.Add(pp.Count);
               
            }

            return counts.OrderByDescending(t=>t).Take(3).Aggregate((a,x)=>a*x).ToString();
        }

        public static string DoTask1(List<string> l)
        {
            int[,] matrix = CreateMatrix(l);
            var points = new List<Point>();
            FindMinPoints(matrix, points);
            return points.Select(t => t.value + 1).Sum().ToString();
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

        public static int[,] CreateMatrix(List<string> l)
        {
            int rowsNum = l.Count;
            int colsNum = l[0].Trim().Length;
            int[,] matrix = new int[rowsNum, colsNum];
            for (int i = 0; i < rowsNum; i++)
            {
                var cols = l[i].Trim().ToArray();

                for (int j = 0; j < colsNum; j++)
                 matrix[i, j] = int.Parse(cols[j].ToString());
                
            }
            return matrix;
        }

        public static void FindMinPoints(int[,] matrix, List<Point> points)
        {

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
         
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (points.Count > 0 && points.Any(t => t.y == i && t.x == j)) continue;
                    var cPoint = matrix[i, j];

                    int li = i;
                    int lj = j - 1;
                    if (lj >= 0 && cPoint >= matrix[li, lj]) continue;


                    int ri = i;
                    int rj = j + 1;
                    if (rj <= (cols - 1) && cPoint >= matrix[ri, rj]) continue;

                    int ui = i - 1;
                    int uj = j;
                    if (ui >= 0 && cPoint >= matrix[ui, uj]) continue;

                    int di = i + 1;
                    int dj = j;
                    if (di <= (rows - 1) && cPoint >= matrix[di, dj]) continue;

                    points.Add(new Point { y = i, x = j,value= cPoint });
                }
            }

           
        }
        public static void FindBasin(int[,] matrix,Point point, List<Point> points)
        {
           // DrawMatrix(matrix, points);
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            var list = new List<Point>();
            
            if(point.x - 1>=0) list.Add(new Point { x = point.x - 1, y = point.y, value = matrix[point.y,point.x-1]});
            if (point.x + 1 <= cols-1) list.Add(new Point { x = point.x + 1, y = point.y, value = matrix[point.y, point.x+1] });
            if (point.y - 1 >= 0) list.Add(new Point { x = point.x , y = point.y-1, value = matrix[point.y-1, point.x]});
            if (point.y + 1 <= rows - 1) list.Add(new Point { x = point.x, y = point.y + 1, value = matrix[point.y+1, point.x] });

            foreach(var item in list)
            {
                if (points.Any(t => t.x == item.x && t.y == item.y)) continue;
                int li = item.y;
                int lj = item.x - 1;
                if (lj >= 0 && (!points.Any(t => t.x == lj && t.y == li) && item.value > matrix[li, lj])) continue;
           

                int ri = item.y;
                int rj = item.x + 1;
                if (rj <= (cols - 1) && (!points.Any(t => t.x == rj && t.y == ri) && item.value > matrix[ri, rj])) continue;
            

                int ui = item.y - 1;
                int uj = item.x;
                if (ui >=0 && (!points.Any(t => t.x == uj && t.y == ui) && item.value > matrix[ui, uj])) continue;
              

                int di = item.y + 1;
                int dj = item.x;
                if (di <= (rows - 1) && (!points.Any(t => t.x == dj && t.y == di) && item.value > matrix[di, dj])) continue;
               

                if(item.value<9)
                {
                    points.Add(new Point { y = item.y, x = item.x, value = matrix[item.y, item.x] });
                    FindBasin(matrix, item, points);
                }
                
            }

        }

        public static void DrawMatrix(int[,] matrix,List<Point> points)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            

            for (int i = 0; i < rows; i++)
            {
                var pp = points.Where(t => t.y == i).Select(t => t).ToList();
                string str = "";
                for (int j = 0; j < cols; j++)
                {
                    Point? it = pp.FirstOrDefault(t => t.x == j);
                    if (it == null) str += "_";
                    else str += it.Value.value;


                }
                  
                Console.WriteLine(str);

            }
        }


        public struct Point
        {
            public int x { get; set; }
            public int y { get; set; }
            public int value { get; set; }
        }
    }
}