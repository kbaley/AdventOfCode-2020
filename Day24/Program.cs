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
            var black = new List<(double x, double y)>();

            // Part 1
            foreach (var item in input)
            {
                (double x, double y) pos = (0.0, 0.0);
                var i = 0;
                while (i < item.Length) {
                    switch (item[i]) {
                        case 'e':
                            pos.x += 1;
                            break;
                        case 'w':
                            pos.x -= 1;
                            break;
                        case 'n':
                            pos.y -= 1;
                            i++;
                            pos.x += item[i] == 'e' ? 0.5 : -0.5;
                            break;
                        case 's':
                            pos.y += 1;
                            i++;
                            pos.x += item[i] == 'e' ? 0.5 : -0.5;
                            break;
                    }
                    i++;
                } 
                if (black.Contains(pos)) {
                    black.Remove(pos);
                } else {
                    black.Add(pos);
                }
            }
            System.Console.WriteLine($"Part 1: {black.Count}");

            // Part 2
            for (int i = 0; i < 100; i++)
            {
                black = FlipTiles(black);
            }

            System.Console.WriteLine($"Part 2: {black.Count}");
            Console.WriteLine("Done");
        }

        private static List<(double x, double y)> FlipTiles(List<(double x, double y)> black)
        {
            var newBlack = new HashSet<(double x, double y)>();
            var allTiles = new List<(double x, double y)>();
            allTiles.AddRange(black);
            allTiles.AddRange(black.SelectMany(GetAdjacent));
            // var allTiles = new HashSet<(double x, double y)>(black).Concat(black.SelectMany(GetAdjacent));
            foreach (var item in allTiles)
            {
                var count = GetAdjacent(item).Count(t => black.Contains(t));
                if (black.Contains(item)) {
                    if (!(count > 2 || count == 0)) {
                        newBlack.Add(item);   
                    }
                } else {
                    if (count == 2) newBlack.Add(item);
                }
            }
            return newBlack.ToList();
        }

        private static IEnumerable<(double x, double y)> GetAdjacent((double x, double y) tile)
        {
            yield return (tile.x - 1, tile.y);
            yield return (tile.x + 1, tile.y);
            yield return (tile.x + 0.5, tile.y + 1);
            yield return (tile.x - 0.5, tile.y + 1);
            yield return (tile.x + 0.5, tile.y - 1);
            yield return (tile.x - 0.5, tile.y - 1);
        }
    }
}
