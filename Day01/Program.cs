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
            Console.WriteLine(FindProductEqualing(input, 2020));

            // Part 2
            foreach (var item in input)
            {
                var product = FindProductEqualing(input, 2020 - item, item);
                if (product > 0) {
                    Console.WriteLine(item * product);
                    break;
                }
            }

            Console.WriteLine("Done");
            Console.ReadLine();
        }

        static long FindProductEqualing(IEnumerable<long> input, long sum, long numToIgnore = 0) {
            foreach (var item in input)
            {
                var answer = input.FirstOrDefault(i => (i != numToIgnore) && i + item == sum);
                if (answer > 0) {
                    return item * answer;
                }
            }

            return -1;
        }
    }
}
