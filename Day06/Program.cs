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
            var groups = File.ReadAllText(fileName)
                .Split("\n\n");

            // Part 1
            var consolidated = groups.Select(g => g.Replace("\n", ""));
            var sum = consolidated.Sum(g => g.Distinct().Count());
            Console.WriteLine($"Part one: {sum}");


            // Part 2
            sum = 0;
            foreach (var item in groups)
            {
                var items = item.Split("\n");
                var questions = item.Replace("\n", "").Distinct();
                sum += questions.Count(q => items.All(i => i.Contains(q)));
            }
            Console.WriteLine($"Part two: {sum}");
            Console.WriteLine("Done");
        }
    }
}
