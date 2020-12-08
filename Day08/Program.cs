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
            var (_, acc) = RunCode(input);
            Console.WriteLine($"Part 1: {acc}");

            // Part 2
            for (var i = 0; i < input.Length; i++)
            {
                var instruction = input[i].Split(' ')[0];
                if (instruction == "nop" || instruction == "jmp") {
                    var revisedInput = (string[])input.Clone();
                    if (instruction.StartsWith("nop")) {
                        revisedInput[i] = "jmp " + input[i].Split(' ')[1];
                    } else if (instruction.StartsWith("jmp")) {
                        revisedInput[i] = "nop +0";
                    }
                    bool hasLoop;
                    (hasLoop, acc) = RunCode(revisedInput);
                    if (!hasLoop) {
                        Console.WriteLine($"Part 2: {acc}");
                        break;
                    }
                }
            }
            Console.WriteLine("Done");
        }

        private static (string, int) ParseInstruction(string input)
        {
            var instruction = input.Split(' ')[0];
            var val = int.Parse(input.Split(' ')[1]);
            return (instruction, val);
        }

        private static (bool, int) RunCode(string[] input) {
            var acc = 0;
            var pointer = 0;
            var visited = new List<int>();
            while (!visited.Contains(pointer)) {
                if (pointer >= input.Length) {
                    return (false, acc);
                }
                visited.Add(pointer);
                var (instruction, value) = ParseInstruction(input[pointer]);
                switch (instruction) {
                    case "nop":
                        pointer++;
                        break;
                    case "acc":
                        acc += value;
                        pointer++;
                        break;
                    case "jmp":
                        pointer += value;
                        break;
                }
            }

            return (true, acc);
        }
    }
}
