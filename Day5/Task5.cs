using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5
{
    public  static class Task5
    {

        const string Day = "Day5";
        const string FilePath = @"Day5\d51.txt";
        static List<string> inputList = new List<string> { @"0,9 -> 5,9","8,0 -> 0,8", "9,4 -> 3,4", "2,2 -> 2,1", "7,0 -> 7,4", "6,4 -> 2,0", "0,9 -> 2,9", "3,4 -> 1,4" , "0,0 -> 8,8", "5,5 -> 8,2" };
       


        public static void Start()
        {
            Console.WriteLine(Day + "-  Task1 Test Result = " + DoTask1(inputList));
            Console.WriteLine(Day + "-  Task1 Result = " + DoTask1());

            Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(inputList));
            Console.WriteLine(Day + "-  Task2 Result = " + DoTask2());
        }



        public static int DoTask1(List<string> l)
        {
            return DoTask(l, false,false);
        }

        public static int DoTask2(List<string> l)
        {
            return DoTask(l, true,false);
        }
        public static int DoTask1()
        {
            return DoTask1(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }
        public static int DoTask2()
        {
            return DoTask2(System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath)).ToList());
        }

        public static int DoTask(List<string> l,bool calcDiagonal,bool print)
        {
            var testList = Transform(l);
            int maxX = testList.Select(t => (t.FromPoint.X > t.ToPoint.X) ? t.FromPoint.X : t.ToPoint.X).Max(t => t) + 1;
            int maxY = testList.Select(t => (t.FromPoint.Y > t.ToPoint.Y) ? t.FromPoint.Y : t.ToPoint.Y).Max(t => t) + 1;

            int[,] matrix = new int[maxY, maxX];

            int sum = 0;
            foreach (var item in testList)
            {
                if (item.FromPoint.X == item.ToPoint.X)
                {
                    int index = item.FromPoint.Y;
                    int step = 1;
                    if (item.ToPoint.Y < item.FromPoint.Y)
                    {
                        step = -1;
                        index++;
                    }
                    else index--;

                    do
                    {
                        index += step;
                        matrix[index, item.FromPoint.X]++;

                        if (matrix[index, item.FromPoint.X] == 2) sum++;

                    } while (index != item.ToPoint.Y);

                }
                else if (item.FromPoint.Y == item.ToPoint.Y)
                {
                    int index = item.FromPoint.X;
                    int step = 1;
                    if (item.ToPoint.X < item.FromPoint.X)
                    {
                        step = -1;
                        index++;
                    }
                    else index--;

                    do
                    {
                        index += step;
                        matrix[item.FromPoint.Y, index]++;

                        if (matrix[item.FromPoint.Y, index] == 2) sum++;

                    } while (index != item.ToPoint.X);

                }
                else if (calcDiagonal && IsDiagonal(item))
                {

                    int fromX = item.FromPoint.X;
                    int toX = item.ToPoint.X;
                    int fromY = item.FromPoint.Y;
                    int toY = item.ToPoint.Y;
                    
                    int i = fromY;
                    int stepY = 1;
                    if (toY < fromY)
                    {
                        stepY = -1;
                        i++;
                    }
                    else i--;

                    do
                    {
                        i += stepY;

                        int j = fromX;
                        int stepX = 1;
                        if (toX < fromX)
                        {
                            stepX = -1;
                            j++;
                        }
                        else j--;

                        do
                        {
                            j += stepX;
                            if (Math.Abs(j - fromX) == Math.Abs(i - fromY)) 
                            {
                                matrix[i, j]++;

                                if (matrix[i, j] == 2) sum++;
                            }
                              
                        } while (j != toX);

                        if (print) printMatrix(matrix);

                    } while (i != toY);
                    
                }
            }

            if(print)  printMatrix(matrix);

            return sum;
        }
        public static List<Line> Transform(List<string> l)
        {
            List<Line> list = new List<Line>();

            foreach (var item in l)
            {
                var arr = item.Split("->");

                var line = new Line();
                var pointsFromArr= arr[0].Split(',');
                var pointsToArr = arr[1].Split(',');

               var fromPoint = new Point();
               var toPoint = new Point();
                fromPoint.X = int.Parse(pointsFromArr[0].Trim());
                fromPoint.Y = int.Parse(pointsFromArr[1].Trim());

                toPoint.X = int.Parse(pointsToArr[0].Trim());
                toPoint.Y = int.Parse(pointsToArr[1].Trim());
                line.FromPoint = fromPoint;
                line.ToPoint = toPoint;
                list.Add(line);


            }
            return list;
        }

        public static bool IsDiagonal(Line line)
        {
            return (Math.Abs(line.FromPoint.X - line.ToPoint.X) == Math.Abs(line.FromPoint.Y - line.ToPoint.Y));
        }

        public static void printMatrix(int[,] matrix)
        {
            Console.WriteLine("\n");
            int rowSize = matrix.GetLength(0);
            int colSize = matrix.GetLength(1);
            for (int i = 0; i < rowSize; i++)
            {
                var row = "";
                for (int j = 0; j < colSize; j++)
                 row += matrix[i, j].ToString() + " ";
                
                Console.WriteLine(row);
            }
        }

        public struct Point
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        public struct Line
        {
            public Point FromPoint{ get; set; }
            public Point ToPoint { get; set; }
        }
    }
}
