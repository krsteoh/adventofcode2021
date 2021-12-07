using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
   public static class GlobalFunctions
    {
        public static void ConsolePrintTask(string str, Func<string> task)
            {
                var timer = new Stopwatch();

                timer.Start();
                var res = task();
                timer.Stop();
                Console.WriteLine(str + res + " Time taken " + timer.Elapsed.TotalMilliseconds + "ms");
            }

    }
}
