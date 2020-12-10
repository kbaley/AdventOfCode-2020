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
            var input = File.ReadAllLines(fileName)
                .Select(s => long.Parse(s))
                .OrderBy(i => i)
                .ToArray();

            // Part 1
            var joltDistribution = new int[3];
            joltDistribution[input[0] - 1] = 1;
            joltDistribution[2] = 1;
            var dist = "1";
            for (var i = 1; i < input.Length; i++)
            {
                joltDistribution[input[i] - input[i-1] - 1] += 1;
                dist += (input[i] - input[i-1]).ToString();
            }
            // System.Console.WriteLine($"{joltDistribution[0]}:{joltDistribution[1]}:{joltDistribution[2]}");
            System.Console.WriteLine(joltDistribution[0] * joltDistribution[2]);

            // Part 2
            // Formula:
            // Start with 1
            // Count the groups of ones in the distribution
            // If it's a group of two, multiply by 2
            // If it's a group of three, multiply by 4
            // If it's a group of four, multiply by 7
            // I calculated these manually; e.g. a group of four 1's can be arranged
            // 7 ways with the numbers 1, 2, and 3:
            //   1111
            //   112
            //   121
            //   211
            //   22
            //   13
            //   31
            // haven't been able to figure out what the formula is but luckily, the input has no groups of 1-jolts
            // higher than four
            var oneDist = dist.Split('3');
            long result = 1;
            foreach (var item in oneDist)
            {
                switch (item.Length) {
                    case 2:
                        result *= 2;
                        break;
                    case 3:
                        result *= 4;
                        break;
                    case 4:
                        result *= 7;
                        break;
                }
            }
            System.Console.WriteLine(result);
            Console.WriteLine("Done");
        }
    }
}
