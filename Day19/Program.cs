using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
            var rules = new List<Rule>();
            var i = 0;
            for (; !string.IsNullOrWhiteSpace(input[i]); i++)
            {
                var line = input[i];    
                var ruleNumber = int.Parse(line.Split(':')[0]);
                var rule = line.Split(": ")[1].Replace("\"", "");
                var isTranslated = rule == "a" || rule == "b";
                var expression = "";
                var references = Array.Empty<int>();
                var orSplit = GetOrSplit(rule);
                if (isTranslated) {
                    expression = $"({rule})"; 
                } else {
                    references = rule
                        .Split(new[] { ' ', '|'}, StringSplitOptions.RemoveEmptyEntries)
                        .Select(r => int.Parse(r))
                        .ToArray();
                }
                rules.Add(new Rule { 
                    Number = ruleNumber, 
                    IsTranslated = isTranslated, 
                    RegEx = expression,
                    OrSplit = orSplit,
                    References = references });
            }
            while (rules.Any(r => !r.IsTranslated)) {
                var translated = rules.Where(r => r.IsTranslated).Select(r => r.Number);
                foreach (var rule in rules)
                {
                    if (!rule.IsTranslated && rule.References.All(r => translated.Contains(r))) {
                        ProcessRule(rule, rules);
                    }
                }
            }
            i++;    
            var regex = "^" + rules.First(r => r.Number == 0).RegEx + "$";
            var count = 0;
            while ( i < input.Length) {
                if (Regex.IsMatch(input[i], regex)) {
                    count++;
                };
                i++;
            }
            System.Console.WriteLine(count);

            // Part 2

            Console.WriteLine("Done");
        }

        private static int GetOrSplit(string rule)
        {
            if (!rule.Contains('|')) return -1;

            var pieces = rule.Split(" | ");
            return pieces[0].Split(' ').Length;
        }

        private static void ProcessRule(Rule rule, List<Rule> rules)
        {
            // System.Console.WriteLine($"{rule.Number}:{string.Join(',', rule.References)}:{rule.RegEx}:{rule.OrSplit}");
            if (rule.References.Length == 0) return;
            var rule1 = rules.First(r => r.Number == rule.References[0]);
            var rule2 = rule.References.Length < 2 ? null : rules.First(r => r.Number == rule.References[1]);
            var rule3 = rule.References.Length < 3 ? null : rules.First(r => r.Number == rule.References[2]);
            var rule4 = rule.References.Length < 4 ? null : rules.First(r => r.Number == rule.References[3]);
            var expr = $"{rule1.RegEx}";
            if (rule2 != null) {
                if (rule.OrSplit == 1) {
                    expr += "|";
                }
                expr += rule2.RegEx;
            }
            if (rule3 != null) {
                if (rule.OrSplit == 2) {
                    expr += "|";
                }
                expr += rule3.RegEx;
            }
            if (rule4 != null) {
                if (rule.OrSplit == 3) {
                    expr += "|";
                }
                expr += rule4.RegEx;
            }
            rule.RegEx = $"({expr})";
            // if (rule.Number == 42 || rule.Number == 31) {
                // System.Console.WriteLine($"{rule.Number}:{string.Join(',', rule.References)}:{rule.RegEx}");
            // }

            rule.IsTranslated = true;
        }
    }

    class Rule {
        public int Number { get; set; }
        public bool IsTranslated { get; set; }
        public string RegEx { get; set; }
        public int[] References { get; set; }
        public int OrSplit { get; set; }
    }
}
