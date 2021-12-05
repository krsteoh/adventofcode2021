using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
{
    public static class Hellper
    {
        public static string CleanText(string str)
        {
            str = str.Replace("   ", " ");
            return str.Replace("  ", " ").Trim();
        }
        public static Position CheckNumber(Element[,] matrix, int num)
        {
            var pos = new Position() { Row = -1, Column = -1 };
            int rowSize = matrix.GetLength(0);
            int colSize = matrix.GetLength(1);
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                {
                    if (matrix[i, j].Value == num)
                    {
                        matrix[i, j].Selected = true;
                        pos.Row = i;
                        pos.Column = j;
                        return pos;
                    }
                }
            }
            return pos;
        }

        public static bool CheckWinCol(Element[,] matrix, int col, int index)
        {
            int rowSize = matrix.GetLength(0);
            if (index < rowSize) return false;
            for (int i = 0; i < rowSize; i++)
                if (!matrix[i, col].Selected) return false;

            return true;
        }
        public static bool CheckWinRow(Element[,] matrix, int row, int index)
        {
            int colSize = matrix.GetLength(1);
            if (index < colSize) return false;
            for (int i = 0; i < colSize; i++)
                if (!matrix[row, i].Selected) return false;

            return true;
        }

        public static bool CheckWin(Element[,] matrix, Position pos, int index)
        {

            return CheckWinRow(matrix, pos.Row, index) || CheckWinCol(matrix, pos.Column, index);

        }
        public static int CalculateWiningMatrixSum(Element[,] matrix)
        {
            int rowSize = matrix.GetLength(0);
            int colSize = matrix.GetLength(1);
            int sum = 0;
            for (int i = 0; i < rowSize; i++)
            {
                for (int j = 0; j < colSize; j++)
                    if (!matrix[i, j].Selected) sum += matrix[i, j].Value;

            }
            return sum;
        }

        public static Element[,] BuildMatrix(List<string> l)
        {
            var lenght = l.Count;
            var elem = new Element[lenght, lenght];
            int j = 0;
            foreach (var it in l)
            {
                var arr = Hellper.CleanText(it).Split(" ");
                for (int i = 0; i < arr.Length; i++)
                {
                    elem[j, i] = new Element { Value = int.Parse(arr[i]), Selected = false };

                }
                j++;
            }

            return elem;
        }
    }
}


