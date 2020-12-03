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
            var input = File.ReadAllLines(fileName);

            // Part 1
            var trees = CountTrees(input, 3, 1);
            Console.WriteLine($"Trees: {trees}");

            // Part 2
            trees *= CountTrees(input, 1, 1);
            trees *= CountTrees(input, 5, 1);
            trees *= CountTrees(input, 7, 1);
            trees *= CountTrees(input, 1, 2);
            Console.WriteLine($"Trees: {trees}");

            Console.WriteLine("Done");
        }

        static long CountTrees(string[] input, int right, int down) {
            var trees = 0;
            var x = right;
            for (var y = down; y < input.Length; y += down) {
                trees += input[y % input.Length][x % input[0].Length] == '#' ? 1 : 0;
                x += right;
            }
            return trees;
        }
    }
}
