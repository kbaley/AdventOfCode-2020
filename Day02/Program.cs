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
            var passwords = input.Select(i => new PasswordPolicy(i));
            Console.WriteLine(passwords.Count(p => p.IsValid()));

            // Part 2
            Console.WriteLine(passwords.Count(p => p.IsValid2()));

            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }

    public class PasswordPolicy {

        public int Min { get; set; }
        public int Max { get; set; }
        public char Text { get; set; }
        public string Password { get; set; }

        public PasswordPolicy(string input) {
            var policy = input.Split(':', StringSplitOptions.TrimEntries)[0];
            Password = input.Split(':', StringSplitOptions.TrimEntries)[1];
            var minMax = policy.Split(' ')[0];
            Text = policy.Split(' ')[1][0];
            Min = int.Parse(minMax.Split('-')[0]);
            Max = int.Parse(minMax.Split('-')[1]);
        }

        public bool IsValid() {
            var count = Password.Count(c => c == Text);
            return count >= Min && count <= Max;
        }

        public bool IsValid2() {
            return Password[Min-1] == Text ^ Password[Max-1] == Text;
        }
    }
}
