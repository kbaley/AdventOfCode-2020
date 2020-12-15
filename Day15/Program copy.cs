// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;

// namespace AoC
// {
//     class Program
//     {
//         static void Main(string[] args)
//         {
//             var isSample = args.Length > 0 && args[0] == "-s";
//             var fileName = isSample ? "sample.txt" : "input.txt";
//             var input = "0,3,6"
//                 .Split(',')
//                 .Select(s => long.Parse(s))
//                 .ToArray();

//             // Part 1
//             var list = new List<Item>();
//             var dict = new Dictionary<long, long>();
//             for (var i = 0; i < input.Length - 1; i++)
//             {
//                 list.Add(new Item{Number = input[i], Index = i + 1});
//                 dict.Add(input[i], i + 1);
//             }
//             long index = input.Length;
//             var prev = new Item { Number = input[index - 1], Index = index};
//             while (index < 3000) {
//                 var last = list.LastOrDefault(i => i.Number == prev.Number);
//                 Item next;
//                 if (dict.ContainsKey(prev.Number)) {
//                     next = new Item { Number = index - last.Index, Index = index + 1};
//                     dict[prev.Number] = index + 1;
//                 } else {
//                     next = new Item { Number = 0}
//                 }
//                 list.Add(prev);
//                 if (last == null) {
//                     prev = new Item { Number = 0, Index = index + 1};
//                 } else {
//                     prev = new Item { Number = index - last.Index, Index = index + 1};
//                 }
//                 index++;
//                 System.Console.WriteLine($"{prev.Number}, {prev.Index}");
//                 if (index % 100000 == 0) {
//                     System.Console.WriteLine(index);
//                 }

//             }

//             // Part 2

//             Console.WriteLine("Done");
//         }
//     }

//     class Item {
//         public long Number { get; set; }
//         public long Index { get; set; }
//     }
// }
