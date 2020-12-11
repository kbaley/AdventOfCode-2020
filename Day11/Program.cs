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
            var input = GetGrid(fileName);

            // Part 1
            var (next, changed) = ApplyRules(input, GetNeighbours, 4);
            while (changed) {
                (next, changed) = ApplyRules(next, GetNeighbours, 4);
            }
            System.Console.WriteLine(next.Cast<char>().Count(n => n == '#'));

            // Part 2
            (next, changed) = ApplyRules(input, GetVisibleSeats, 5);
            while (changed) {
                (next, changed) = ApplyRules(next, GetVisibleSeats, 5);
            }
            System.Console.WriteLine(next.Cast<char>().Count(n => n == '#'));

            Console.WriteLine("Done");
        }

        static (char[,], bool) ApplyRules(char[,] input, 
            Func<char[,], int, int, IEnumerable<char>> getSeats, 
            int minOccupied) {
            var next = (char[,])input.Clone();
            var changed = false;
            for (var y = 0; y < input.GetLength(1); y++)
            {
                for (var x = 0; x < input.GetLength(0); x++)
                {
                    if (input[x,y] == '#') {
                        var neighbours = getSeats(input, x, y);
                        if (neighbours.Count(n => n == '#') >= minOccupied) {
                            next[x,y] = 'L';
                            changed = true;
                        } 
                    }
                    if (input[x,y] == 'L') {
                        var neighbours = getSeats(input, x, y);
                        if (neighbours.All(n => n != '#')) {
                            next[x,y] = '#';
                            changed = true;
                        }
                    }
                }   
            }

            return (next, changed);

        }

        static void WriteGrid(char[,] input) {
            for( var y = 0; y < input.GetLength(1); y++) {
                for (var x = 0; x < input.GetLength(0); x++) {
                    System.Console.Write(input[x,y]);
                }
                System.Console.WriteLine();
            }
            System.Console.WriteLine();
            System.Console.WriteLine();
        }

        static char[,] GetGrid(string filename) {

            var input = File.ReadAllLines(filename);
            var result = new char[input[0].Length, input.Length];
            for( var y = 0; y < input.Length; y++) {
                for (var x = 0; x < input[0].Length; x++)
                {
                    result[x,y] = input[y][x];    
                }
            }
            return result;
        }

        static IEnumerable<char> GetVisibleSeats(char[,] input, int x, int y) {
            var directions = new List<(int X, int Y)> {
                (-1, -1),
                (0, -1),
                (1, -1),
                (-1, 0),
                (1, 0),
                (-1, 1),
                (0, 1),
                (1, 1)
            };

            foreach (var (X, Y) in directions)
            {
                var x2 = x + X;
                var y2 = y + Y;

                while (x2 >= 0 && x2 <= input.GetUpperBound(0) && y2 >= 0 && y2 <= input.GetUpperBound(1)) {
                    if (input[x2, y2] != '.') {
                        yield return input[x2, y2];
                        break;
                    }
                    x2 += X;
                    y2 += Y;
                }   
            }
        }

        static IEnumerable<char> GetNeighbours(char[,] input, int x, int y) {
            var neighbours = new List<(int X, int Y)>
            {
                (x - 1, y - 1),
                (x, y - 1),
                (x + 1, y - 1),
                (x - 1, y),
                (x + 1, y),
                (x - 1, y + 1),
                (x, y + 1),
                (x + 1, y + 1)
            };
            foreach (var (X, Y) in neighbours)
            {
                if (X >= 0 && X <= input.GetUpperBound(0) && Y >= 0 && Y <= input.GetUpperBound(1)) {
                    yield return input[X, Y];
                }
            }
        }
    }
}
