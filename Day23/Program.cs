using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC
{
    class Program
    {
        static void Main(string[] args)
        {
            var isSample = args.Length > 0 && args[0] == "-s";
            var fileName = isSample ? "sample.txt" : "input.txt";
            var input = File.ReadAllLines(fileName)
                .Select(s => long.Parse(s));

            // Part 1

            // Part 2

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
}
