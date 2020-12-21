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
            var allergens = new Dictionary<string, List<string>>();
            var allIngredients = new List<string>();
            foreach (var item in input)
            {
                var ingredients = item.Split(" (")[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (var ingredient in ingredients)
                {
                    allIngredients.Add(ingredient); 
                }
                var allergenList = item.Split("contains ")[1][0..^1].Split(", ", StringSplitOptions.RemoveEmptyEntries);
                foreach (var allergen in allergenList)
                {
                    if (!allergens.ContainsKey(allergen)) {
                        allergens.Add(allergen, ingredients.ToList());
                    } else {
                        allergens[allergen].RemoveAll(i => !ingredients.Contains(i));
                    }
                }
            }
            var unsafeIngredients = allergens.Values.SelectMany(i => i).Distinct();
            var safeIngredients = allIngredients.Where(i => !unsafeIngredients.Contains(i));
            System.Console.WriteLine($"Part 1: {safeIngredients.Count()}");

            // Part 2
            while (allergens.Values.Any(i => i.Count > 1)) {
                var ones = allergens.Where(a => a.Value.Count == 1);
                foreach (var item in ones)
                {
                    var ingredient = item.Value[0];
                    var pruneList = allergens.Where(a => a.Key != item.Key && a.Value.Contains(ingredient));
                    foreach (var prune in pruneList)
                    {
                        prune.Value.Remove(ingredient);
                    }
                }
            }
            var alphaList = allergens.OrderBy(a => a.Key).ToList();
            System.Console.WriteLine(string.Join(',', alphaList.SelectMany(a => a.Value)));

            Console.WriteLine("Done");
        }
    }
}
