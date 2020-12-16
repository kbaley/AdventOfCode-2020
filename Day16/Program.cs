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
            var i = 0;
            var fields = new Dictionary<string, List<int>>();
            var allValid = new List<int>();
            while (input[i].Trim() != "") {
                var fieldName = input[i].Split(':')[0];
                var rest = input[i].Split(':', StringSplitOptions.TrimEntries)[1].Split(" or ");
                fields.Add(fieldName, new List<int>());
                foreach (var item in rest)
                {
                    var bounds = item.Split('-').Select(i => int.Parse(i)).ToArray();    
                    fields[fieldName].AddRange(Enumerable.Range(bounds[0], bounds[1] - bounds[0] + 1));
                    allValid.AddRange(Enumerable.Range(bounds[0], bounds[1] - bounds[0] + 1).Where(i => !allValid.Contains(i)));
                }
                i++;
            }
            i += 2;
            // your ticket
            int[] myTicket = Array.Empty<int>();
            while (input[i].Trim() != "") {
                myTicket = input[i]
                    .Split(',')
                    .Select(i => int.Parse(i))
                    .ToArray();
                i++;
            }
            i += 2;
            // nearby tickets
            var errorRate = 0;
            var validTickets = new List<int[]>();
            while (i < input.Length) {
                var tickets = input[i]
                    .Split(',')
                    .Select(i => int.Parse(i))
                    .ToArray();
                if (tickets.All(t => allValid.Contains(t))) {
                    validTickets.Add(tickets);
                }
                errorRate += tickets.Where(i => !allValid.Contains(i)).Sum();
                i++;
            }
            System.Console.WriteLine($"Error rate: {errorRate}");

            // Part 2
            var fieldLocation = new Dictionary<string, List<int>>();
            var numFields = fields.Keys.Count;
            foreach (var field in fields.Keys)
            {
                fieldLocation.Add(field, Enumerable.Range(0, numFields).ToList());    
            }
            for (var j = 0; j < myTicket.Length; j++)
            {
                var nearby = validTickets.Select(t => t[j]).ToArray();
                var invalidFields = fields.Where(f => nearby.Any(n => !f.Value.Contains(n)));
                foreach (var f in invalidFields)
                {
                    fieldLocation[f.Key].Remove(j);
                }
            }
            while (fieldLocation.Values.Any(f => f.Count != 1)) {
                var ones = fieldLocation.Where(f => f.Value.Count == 1)
                    .SelectMany(f => f.Value);
                foreach (var item in ones)
                {
                    foreach (var f in fieldLocation.Where(t => t.Value.Count > 1 && t.Value.Contains(item)))
                    {
                        f.Value.Remove(item);
                    } 
                }
            }
            long result = 1;
            foreach (var item in fieldLocation.Where(f => f.Key.StartsWith("departure")))
            {
                result *= myTicket[item.Value[0]];
            }
            System.Console.WriteLine($"Part 2: {result}");
            Console.WriteLine("Done");
        }
    }
}