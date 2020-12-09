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
                .Select(s => long.Parse(s))
                .ToArray();

            // Part 1
            var preamble = 25;
            var i = preamble;
            var check = long.MinValue;
            while (i < input.Length) {
                check = input[i];
                if (GetCombinationSums(input, i - preamble, preamble).All(s => s != check)) {
                    Console.WriteLine($"Part 1: {check}");
                    break;
                }
                i++;
            }

            // Part 2
            var start = 0;
            var end = 0;
            long sum = input[start];
            while (end < input.Length) {
                while (sum < check) {
                    sum += input[++end];
                }
                while (sum > check) {
                    sum -= input[start++];
                }
                if (sum == check) {
                    var range = input.Skip(start).Take(end - start);
                    Console.WriteLine($"Part 2: {range.Min() + range.Max()}");
                    break;
                }
            }
            Console.WriteLine("Done");
        }

        static IEnumerable<long> GetCombinationSums(long[] input, int start, int count) {
            for (var i = start; i < start + count - 1; i++)
            {
                for (var j = start + 1; j < start + count; j++) {
                    yield return input[i] + input[j];
                } 
            }
        }
    }
}
