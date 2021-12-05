using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
{
   public static class Task4
    {
        const string Day = "Day4";
        const string FilePath = @"Day4\d41.txt";
        static List<Element[,]> testList = new List<Element[,]> { };
        static List<int> testNumbers = new List<int> { 7, 4, 9, 5, 11, 17, 23, 2, 0, 14, 21, 24, 10, 16, 13, 6, 15, 25, 12, 22, 18, 20, 8, 19, 3, 26, 1 };


        public static void Start()
        {
            BuildTestList();

            Console.WriteLine(Day + "-  Task1 Test Result = " + DoTask1(testList, testNumbers));
            Console.WriteLine(Day + "-  Task1 Result = " + DoTask1());

            Console.WriteLine(Day + "-  Task2 Test Result = " + DoTask2(testList, testNumbers));
            Console.WriteLine(Day + "-  Task2 Result = " + DoTask2());
        }
     
        private static void BuildTestList()
        {
            testList.Add(Hellper.BuildMatrix(new List<string> {
                "22 13 17 11  0",
                "8  2 23  4 24",
                "21  9 14 16  7",
                "6 10  3 18  5",
                "1 12 20 15 19"
            }));
            testList.Add(Hellper.BuildMatrix(new List<string> {
                "3 15  0  2 22",
                "9 18 13 17  5",
                "19  8  7 25 23",
                "20 11 10 24  4",
                "14 21 16 12  6"
            }));
            testList.Add(Hellper.BuildMatrix(new List<string> {
                "14 21 17 24  4",
                "10 16 15  9 19",
                "18  8 23 26 20",
                "22 11 13  6  5",
                "2  0 12  3  7"
            }));
        }

        public static int DoTask1(List<Element[,]> l,List<int> numbers)
        {
            if (l.Count == 0) return 0;
          
            for (int n=0;n<numbers.Count;n++)
            {
                foreach (var matrix in l)
                {
                   var pos= Hellper.CheckNumber(matrix, numbers[n]);
                    if(pos.Row!=-1 && pos.Column!=-1)
                    {
                        if (Hellper.CheckWin(matrix, pos,n))
                        {
                            int sum = Hellper.CalculateWiningMatrixSum(matrix);
                            return numbers[n] * sum;
                        }
                    }
                }
            }
           
            return 0;
        }

        public static int DoTask2(List<Element[,]> l, List<int> numbers)
        {
           int lCount = l.Count;
            if (lCount == 0) return 0;
            List<int> winners = new List<int>();

            for (int n = 0; n < numbers.Count; n++)
            {
                for (int i=0;i< lCount; i++)
                {
                    if (winners.Contains(i)) continue; // skip winners
                    var matrix = l[i];
                    var pos = Hellper.CheckNumber(matrix, numbers[n]);
                    if (pos.Row != -1 && pos.Column != -1)
                    {
                        if (Hellper.CheckWin(matrix, pos, n))
                        {
                            int sum = Hellper.CalculateWiningMatrixSum(matrix);
                            winners.Add(i);

                            if (winners.Count == lCount) return numbers[n] * sum;
                        }
                    }
                }
               
            }


            return 0;
        }
        public static int DoTask1()
        {
            var arr = System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath));
            return DoTask1(ReadMatrix(arr), ReadNumbers(arr));
        }
        public static int DoTask2()
        {
            var arr = System.IO.File.ReadAllLines(string.Format("{0}{1}", Config.LocalPath, FilePath));
            return DoTask2(ReadMatrix(arr), ReadNumbers(arr));
        }



        private static List<int> ReadNumbers (string[] arr)
        {
            return Hellper.CleanText(arr[0]).Split(',').Select(t => int.Parse(t)).ToList();
        }
        private static List<Element[,]> ReadMatrix(string[] arr)
        {
            List<Element[,]> matrixList = new List<Element[,]>();
            List<string> currentMatrix = new List<string>();
            for (int i = 2; i < arr.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(arr[i]))
                {
                    if (currentMatrix.Count > 0) matrixList.Add(Hellper.BuildMatrix(currentMatrix));
                    currentMatrix = new List<string>();
                }
                else currentMatrix.Add(Hellper.CleanText(arr[i]));
            }
            return matrixList;
        }
    }
}
