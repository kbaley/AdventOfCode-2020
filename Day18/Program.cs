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
            long sum = 0;
            foreach (var item in input)
            {
                sum += EvaluateExpression(ParseExpression(item), DoMath);
            }
            System.Console.WriteLine("Sum: " + sum);

            // Part 2
            sum = 0;
            foreach (var item in input)
            {
                sum += EvaluateExpression(ParseExpression(item), DoOtherMath);
            }
            System.Console.WriteLine("Sum: " + sum);

            Console.WriteLine("Done");
        }

        static long EvaluateExpression(List<string> expression, Func<List<string>, long> mathFunc) {
            if (expression.All(e => !e.Contains('('))) {
                return mathFunc(expression);
            }
            for (int i = 0; i < expression.Count; i++)
            {
                if (expression[i].StartsWith("(")) {
                    expression[i] = EvaluateExpression(ParseExpression(expression[i][1..^1]), mathFunc).ToString();
                }
            }

            return mathFunc(expression);
        }

        static List<string> ParseExpression(string input) {
            var expression = new List<string>();
            input = Regex.Replace(input, @"\s+", "");
            var pieces = Regex.Replace(input, @"\s+", "").Select(c => c.ToString()).ToList();

            var i = 0;
            while (i < input.Length) {
                if ("0123456789+*".Contains(input[i])) {
                    expression.Add(input[i].ToString());
                    i++;
                } else if (input[i] == '(') {
                    var start = i++;
                    var count = 1;
                    while (count > 0) {
                        if (input[i] == '(') count++;
                        if (input[i] == ')') count--;
                        i++; 
                    }
                    expression.Add(input[start..i]);
                }
            }

            return expression;
        }

        static long DoOtherMath(IEnumerable<string> pieces) {
            var list = pieces.ToList();
            while (list.Contains("+")) {
                var plus = list.IndexOf("+");
                var sum = (long.Parse(list[plus - 1]) + long.Parse(list[plus + 1])).ToString();
                list[plus - 1] = sum;
                list.RemoveAt(plus);
                list.RemoveAt(plus);
            }
            var result = long.Parse(list.First());
            for (int i = 0; i < list.Count; i++)
            {
                switch (list.ElementAt(i)) {
                    case "*":
                        result *= long.Parse(list.ElementAt(i+1).ToString());
                        break;
                }
            }
            return result;
        }

        static long DoMath(IEnumerable<string> pieces) {
            var result = long.Parse(pieces.First());
            for (int i = 0; i < pieces.Count(); i++)
            {
                switch (pieces.ElementAt(i)) {
                    case "+":
                        result += long.Parse(pieces.ElementAt(i+1).ToString());
                        break;
                    case "*":
                        result *= long.Parse(pieces.ElementAt(i+1).ToString());
                        break;
                }
            }
            return result;
        }
    }
}
