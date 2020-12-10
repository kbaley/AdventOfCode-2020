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
                joltDistribution[input[i] - input[i - 1] - 1] += 1;
                dist += (input[i] - input[i - 1]).ToString();
            }
            System.Console.WriteLine(joltDistribution[0] * joltDistribution[2]);

            // Part 2
            var oneDist = dist.Split('3', StringSplitOptions.RemoveEmptyEntries);
            long result = 1;
            var tribonaccis = GetTribonaccis(oneDist.Max(x => x.Length) + 3);
            foreach (var item in oneDist)
            {
                result *= tribonaccis[item.Length + 2];
            }
            System.Console.WriteLine(result);
            Console.WriteLine("Done");
        }
        static int[] GetTribonaccis(int length)
        {
            var tribonaccis = new int[length];
            tribonaccis[0] = 0;
            tribonaccis[1] = 0;
            tribonaccis[2] = 1;
            for (var i = 3; i < tribonaccis.Length; i++)
            {
                tribonaccis[i] = tribonaccis[i - 1] + tribonaccis[i - 2] + tribonaccis[i - 3];
            }
            return tribonaccis;
        }
    }
}
