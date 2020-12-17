using System;
using System.Collections.Generic;
using System.IO;

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
            var active = new List<Point3D>();
            PopulateActive(input, (i, j) => active.Add(new Point3D(i, j, 0)));
            for (var n = 0; n < 6; n++) {
                active = ThreeD.RunCycle(active);
            }
            System.Console.WriteLine(active.Count);

            // Part 2
            var activeX = new List<Point4D>();
            PopulateActive(input, (i, j) => activeX.Add(new Point4D(i, j, 0, 0)));
            for (var n = 0; n < 6; n++) {
                activeX = FourD.RunCycle(activeX);
            }
            System.Console.WriteLine(activeX.Count);

            Console.WriteLine("Done");
        }

        static void PopulateActive(string[] input, Action<int, int> action) {
            for (int i = 0; i < input.Length; i++)
            {
                for (var j = 0; j < input[i].Length; j++) {
                    if (input[i][j] == '#') {
                        action(i,j);
                    }
                }    
            }
        }
    }
}
