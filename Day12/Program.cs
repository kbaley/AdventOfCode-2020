using System;
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
            var (x,y) = (0,0);
            var orientation = 0; // based on directions of {E, S, W, N}
            foreach (var item in input)
            {
                var (command, value) = (item[0], int.Parse(item.Substring(1)));
                switch (command) {
                    case 'N':
                        y += value;
                        break;
                    case 'S':
                        y -= value; 
                        break;
                    case 'E':
                        x += value;
                        break;
                    case 'W':
                        x -= value;
                        break;
                    case 'L':
                        command = 'R';
                        value = 360 - value;
                        goto case 'R';
                    case 'R':
                        orientation = (orientation + (value / 90)) % 4;
                        break;
                    case 'F':
                        switch (orientation) {
                            case 0:
                                x += value;
                                break;
                            case 1:
                                y -= value;
                                break;
                            case 2:
                                x -= value;
                                break;
                            case 3:
                                y += value;
                                break;
                        }
                        break;
                }
            }
            System.Console.WriteLine(Math.Abs(x) + Math.Abs(y));

            // Part 2
            (x, y) = (0,0);
            (int X, int Y) waypoint = (10, 1);
            foreach (var item in input)
            {
                var (command, value) = (item[0], int.Parse(item.Substring(1)));
                switch (command) {
                    case 'N':
                        waypoint.Y += value;
                        break;
                    case 'S':
                        waypoint.Y -= value;
                        break;
                    case 'E':
                        waypoint.X += value;
                        break;
                    case 'W':
                        waypoint.X -= value;
                        break;
                    case 'R':
                        command = 'L';
                        value = 360 - value;
                        goto case 'L';
                    case 'L':
                        for (var i = 0; i < value / 90; i++)
                        {
                            waypoint = (-waypoint.Y, waypoint.X);
                        }
                        break;
                    case 'F':
                        x += waypoint.X * value;
                        y += waypoint.Y * value;
                        break;
                }
            }
            System.Console.WriteLine(Math.Abs(x) + Math.Abs(y));
            Console.WriteLine("Done");
        }
    }
}
