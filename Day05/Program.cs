using System;
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
            var ids = input.Select(i => GetSeatId(i));
            Console.WriteLine($"Max seat ID: {ids.Max()}");

            // Part 2
            var nums = Enumerable.Range(ids.Min(), ids.Max() - ids.Min());
            var missingId = nums.First(n => !ids.Contains(n));
            Console.WriteLine($"My seat ID: {missingId}");

            Console.WriteLine("Done");
        }

        static int GetSeatId(string input) {
            var binary = input.Replace('B', '1').Replace('F', '0').Replace('R', '1').Replace('L', '0');
            return Convert.ToInt32(binary, 2);
        }
    }
}
