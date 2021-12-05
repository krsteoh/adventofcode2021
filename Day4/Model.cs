using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
{
    public struct Element
    {
        public int Value { get; set; }
        public bool Selected { get; set; }
    }
    public struct Position
    {
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
