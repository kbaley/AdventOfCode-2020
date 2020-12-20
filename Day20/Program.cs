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
            var tiles = new List<Tile>();
            var i = 0;
            while (i < input.Length) {
                var number = int.Parse(input[i].Replace("Tile ", "").Replace(":", ""));
                i++;
                var start = i;
                for (; i < input.Length && !string.IsNullOrWhiteSpace(input[i]); i++);
                var data = input.Skip(start).Take(i - start);
                tiles.Add(new Tile(number, data.ToArray()));
                i++;
            }

            // Part 1
            var corners = new List<long>();
            foreach (var tile in tiles)
            {
                var edges = tile.Edges();
                var matches = new List<int>();
                for (var j = 0; j < edges.Length; j++)
                {
                    matches.AddRange(
                        tiles
                            .Where(t => t.Number != tile.Number && t.Edges().Any(e => e == edges[j]))
                            .Select(t => t.Number));
                }
                if (matches.Distinct().Count() == 2) {
                    corners.Add(tile.Number);
                }
            }
            long product = corners.Aggregate(1L, (x, y) => x * y);
            System.Console.WriteLine($"Part 1: {product}");

            // Part 2

            Console.WriteLine("Done");
        }


    }

    class Tile {
        public int Number { get; set; }
        public string[] Data { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string TopEdge {
            get {
                return Data[0];
            }
        }

        public string BottomEdge {
            get {
                return Data[^1];
            }
        }

        public string LeftEdge { 
            get {
                return new string(Data.Select(i => i[0]).ToArray());
            }
        }

        public string RightEdge {
            get {
                return new string(Data.Select(i => i[^1]).ToArray());
            }
        }

        public string[] Edges() {
            return new string[] {
                TopEdge,
                LeftEdge,
                BottomEdge,
                RightEdge,
                new string(TopEdge.Reverse().ToArray()),
                new string(LeftEdge.Reverse().ToArray()),
                new string(BottomEdge.Reverse().ToArray()),
                new string(RightEdge.Reverse().ToArray())
            };
        }

        public Tile(int number, string[] data) {
            Number = number;
            Data = data;
            X = -1;
            Y = -1;            
        }

        public override string ToString()
        {
            return string.Join('\n', Data);
        }
    }
}
