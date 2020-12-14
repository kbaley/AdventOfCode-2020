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
            var input = File.ReadAllLines(fileName);
            var start = int.Parse(input[0]);
            var ids = input[1]
                .Replace("x", "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.Parse(x))
                .ToList();

            // Part 1
            var min = int.MaxValue;
            var id = int.MaxValue;
            foreach (var item in ids)
            {
                if (item - (start % item) < min) {
                    id = item;
                    min = item - (start % item); 
                }    
            }
            System.Console.WriteLine(id * min);

            // Part 2
            var allIds = input[1].Split(',', StringSplitOptions.RemoveEmptyEntries);
            var busIndex = input[1].Split(',');
            var busses = new List<(int bus, int index)>();
            for (int i = 0; i < busIndex.Length; i++)
            {
                if (busIndex[i] != "x") {
                    busses.Add((int.Parse(busIndex[i]), i));
                }
            }
            long jumpBy = busses.ElementAt(0).bus;
            long time = jumpBy;
            for (int i = 1; i < busses.Count(); i++)
            {
                while ((time + busses[i].index) % busses[i].bus != 0) {
                    time += jumpBy;
                    // System.Console.WriteLine($"{jumpBy},{time},{i}");
                }
                jumpBy *= busses[i].bus;
            }
            System.Console.WriteLine(time);

            Console.WriteLine("Done");
        }
    }
}
