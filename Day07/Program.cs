using System;
using System.Collections.Generic;
using System.Globalization;
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
            var rules = File.ReadAllLines(fileName);
            var bagToFind = "shiny gold bag";

            // Part 1
            var results = new List<string>();
            var bagsContainingTarget = rules.Where(r => r.Contains(" " + bagToFind));
            while (bagsContainingTarget.Any()) {
                var bags = bagsContainingTarget.Select(b => b.Split("s contain ")[0]).ToList();
                results.AddRange(bags);
                results = results.Distinct().ToList();
                bagsContainingTarget = rules.Where(r => bags.Any(b => r.Contains(" " + b)));
            }
            Console.WriteLine(results.Count);

            // Part 2
            Console.WriteLine(GetBagCount(rules, "shiny gold") - 1);
            Console.WriteLine("Done");
        }

        static int GetBagCount(string[] rules, string bagColour) {
            var bagRule = rules.First(r => r.StartsWith(bagColour, true, CultureInfo.InvariantCulture));
            if (bagRule.IsEmpty()) return 1;

            var contents = bagRule.Contents().Colours();
            var sum = 0;
            // System.Console.WriteLine(bagRule);
            foreach (var item in contents)
            {
                var count = item.Split(' ')[0];
                var colour = item[(count.Length + 1)..];
                var add = int.Parse(count) * GetBagCount(rules, colour);
                sum += add;
            }
            return sum + 1;
        }
    }

    public static class BagExtensions {
        public static string Contents(this string rule) {
            return rule.Split( " contain " )[1];
        }

        public static bool IsEmpty(this string bag) {
            return bag.Contains("no other bags");
        }

        public static string[] Colours(this string contents) {
            return contents.Split(new[] {" bag, ", " bags, ", " bag.", " bags."}, StringSplitOptions.RemoveEmptyEntries);
        }
    }

}
