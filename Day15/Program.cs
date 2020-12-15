using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    class Program
    {
        static void Main(string[] args)
        {
            var isSample = args.Length > 0 && args[0] == "-s";
            var fileName = isSample ? "sample.txt" : "input.txt";
            var input = "2,1,10,11,0,6"
                .Split(',')
                .Select(s => long.Parse(s))
                .ToArray();

            // Part 1
            System.Console.WriteLine(RunGame(input, 2020));

            // Part 2
            System.Console.WriteLine(RunGame(input, 30000000));

            System.Console.WriteLine("Done");
        }

        static long RunGame(long[] input, long turns) {
            var dict = new Dictionary<long, long>();
            for (var i = 0; i < input.Length - 1; i++)
            {
                dict.Add(input[i], i + 1);
            }
            long index = input.Length;
            var prev = input[index - 1];
            while (index < turns) {
                if (dict.ContainsKey(prev)) {
                    var temp = dict[prev];
                    dict[prev] = index;
                    prev = index - temp;
                } else {
                    dict.Add(prev, index);
                    prev = 0;
                }
                index++;
            }

            return prev;
        }
    }
}
