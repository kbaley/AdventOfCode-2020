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
                .ToArray();;
            var cardKey = input[0];
            var doorKey = input[1];
            var subjectNumber = 7;

            // Part 1
            long result = 1;
            var doorLoop = 1;
            while (result != doorKey) {
                result = Transform(result, 7);
                doorLoop++;
            }
            doorLoop--;
            result = 1;
            for (int i = 0; i < doorLoop; i++)
            {
                result = Transform(result, cardKey);
            }
            System.Console.WriteLine($"Part 1: {result}");

            // Part 2

            Console.WriteLine("Done");
        }

        static long Transform(long result, long subjectNumber) {
            return (result *= subjectNumber) % 20201227;
        }
    }
}
