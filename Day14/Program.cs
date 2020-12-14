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

            // Part 1
            var mask = new string('X', 36);
            var mem = new Dictionary<long, long>();
            foreach (var item in input)
            {
                if (item.StartsWith("mask")) {
                    mask = item.Replace("mask = ", "");
                } else {
                    var location = int.Parse(item[4..].Split(']')[0]);
                    var value = long.Parse(item.Split(" = ")[1]);
                    var binary = Convert.ToString(value, 2).PadLeft(36, '0').ToCharArray();
                    for (int i = 0; i < binary.Length; i++)
                    {
                        binary[i] = mask[i] == 'X' ? binary[i] : mask[i];
                    }
                    AddToMemory(mem, location, Convert.ToInt64(new string(binary), 2));
                }
            }
            System.Console.WriteLine(mem.Values.Sum());

            // Part 2
            mask = new string('X', 36);
            mem = new Dictionary<long, long>();
            foreach (var item in input)
            {
                var floats = new List<int>();
                if (item.StartsWith("mask")) {
                    mask = item.Replace("mask = ", "");
                } else {
                    var location = int.Parse(item[4..].Split(']')[0]);
                    var value = long.Parse(item.Split(" = ")[1]);
                    var binary = Convert.ToString(location, 2).PadLeft(36, '0').ToCharArray();
                    for (int i = 0; i < binary.Length; i++)
                    {
                        if (mask[i] == '1') {
                            binary[i] = '1';
                        } else if (mask[i] == 'X') {
                            floats.Add(35 - i);
                            binary[i] = '0';
                        }
                    };
                    var start = Convert.ToInt64(new string(binary), 2);
                    AddToMemory(mem, start, value);
                    foreach (var combo in GetCombinations(floats))
                    {
                        AddToMemory(mem, start + combo, value);
                    }
                }
            }
            System.Console.WriteLine(mem.Values.Sum());


            Console.WriteLine("Done");
        }

        private static void AddToMemory(Dictionary<long, long> mem, long key, long value)
        {
            if (mem.ContainsKey(key)) {
                mem[key] = value;
            } else {
                mem.Add(key, value);
            }
        }

        private static IEnumerable<long> GetCombinations(IEnumerable<int> floats)
        {
            var combos = new List<long>();
            if (!floats.Any()) return combos;
            var first = floats.First();
            combos.Add((long)Math.Pow(2, first));
            if (floats.Count() == 1) { 
                return combos;
            }
            var rest = GetCombinations(floats.Skip(1));
            combos.AddRange(rest);
            foreach (var item in rest)
            {
                combos.Add(item + (long)Math.Pow(2, first));
            }
            return combos;
        }
    }
}
