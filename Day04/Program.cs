using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AoC
{
    class Program
    {
        static Dictionary<string, Func<string, bool>> Rules = new Dictionary<string, Func<string, bool>>();
        static string[] requiredFields = new[] { "byr:", "iyr:", "eyr:", "hgt:", "hcl:", "ecl:", "pid:"};

        static void Main(string[] args)
        {
            var isSample = args.Length > 0 && args[0] == "-s";
            var fileName = isSample ? "sample.txt" : "input.txt";
            var passports = File.ReadAllText(fileName)
                .Split("\n\n")
                .Select(p => p.Replace('\n', ' '))
                .ToList();

            // Part 1
            Console.WriteLine($"Valid: " + passports.Count(p => IsValidPart1(p)));

            // Part 2
            Rules.Add("byr", byr);
            Rules.Add("iyr", iyr);
            Rules.Add("eyr", eyr);
            Rules.Add("hgt", hgt);
            Rules.Add("hcl", hcl);
            Rules.Add("ecl", ecl);
            Rules.Add("pid", pid);
            Rules.Add("cid", cid);
            Console.WriteLine($"Valid: " + passports.Count(p => IsValidPart2(p)));
            Console.WriteLine("Done");
        }

        static bool IsValidPart1(string passport) {
            return requiredFields.All(f => passport.Contains(f));
        }

        static bool IsValidPart2(string passport) {
            if (!IsValidPart1(passport)) return false;
            var fields = passport.Split(new[] {' ', ':'}, StringSplitOptions.RemoveEmptyEntries);
            for( var i = 0; i < fields.Length; i += 2) {
                var (field, value) = (fields[i], fields[i+1]);
                if (!Rules[fields[i]](fields[i+1])) {
                    // Console.WriteLine($"Failed: {field} for {passport}");
                    return false;
                }
            }
            return true;
        }

        static Func<int, int, string, bool> yearRule = (start, end, f) => 
            int.TryParse(f, out int year) && year >= start && year <= end; 

        static Func<string, bool> byr = f => yearRule(1920, 2002, f);
        static Func<string, bool> iyr = f => yearRule(2010, 2020, f);
        static Func<string, bool> eyr = f => yearRule(2020, 2030, f);

        static Func<string, bool> hgt = f => {
            var unit = f.Substring(f.Length - 2);
            if (int.TryParse(f.Substring(0, f.Length - 2), out int measure)) {
                if (unit == "cm") {
                    return measure >= 150 && measure <= 193;
                } else if (unit == "in") {
                    return measure >= 59 && measure <= 76;
                }
            }
            return false;
        };

        static Func<string, bool> hcl = f => Regex.Match(f, "^[#][0-9a-f]{6}$").Success;
        static Func<string, bool> ecl = f => new[] {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}.Contains(f);
        static Func<string, bool> pid = f => Regex.Match(f, "^[0-9]{9}$").Success;
        static Func<string, bool> cid => f => true;
    }

}
