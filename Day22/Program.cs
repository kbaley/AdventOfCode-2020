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

            var i = 1;
            while (!string.IsNullOrWhiteSpace(input[i])) i++;
            var player1 = new Queue<long>(input.Skip(1).Take(i - 1).Select(long.Parse));
            var player2 = new Queue<long>(input.Skip(player1.Count + 3).Select(long.Parse));

            // Part 1
            while (player1.Any() && player2.Any()) {
                var (p1, p2) = (player1.Dequeue(), player2.Dequeue());
                if (p1 > p2) {
                    player1.Enqueue(p1);
                    player1.Enqueue(p2);
                } else {
                    player2.Enqueue(p2);
                    player2.Enqueue(p1);
                }
            }
            var winner = player1.Any() ? player1 : player2;
            var score = 0L;
            var multiplier = winner.Count;
            while (winner.Any()) {
                score += multiplier-- * winner.Dequeue();
            }
            System.Console.WriteLine($"Part 1: {score}");

            // Part 2
            player1 = new Queue<long>(input.Skip(1).Take(i - 1).Select(long.Parse));
            player2 = new Queue<long>(input.Skip(player1.Count + 3).Select(long.Parse));

            (_, winner) = PlayGame(player1, player2);
            score = 0L;
            multiplier = winner.Count;
            while (winner.Any()) {
                score += multiplier-- * winner.Dequeue();
            }
            System.Console.WriteLine($"Part 2: {score}");

            Console.WriteLine("Done");
        }

        static (bool p1Wins, Queue<long> winningHand) PlayGame(Queue<long> player1, Queue<long> player2) {
            var playedHands = new HashSet<string>();
            while (player1.Any() && player2.Any()) {
                var hand = string.Join(',', player1) + "::" + string.Join(',', player2);
                if (!playedHands.Add(hand)) return (true, player1);

                (player1, player2) = PlayRound(player1, player2);
            }

            return player1.Any() ? (true, player1) : (false, player2);
        }

        private static (Queue<long> player1, Queue<long> player2) PlayRound(Queue<long> player1, Queue<long> player2)
        {
            var (p1, p2) = (player1.Dequeue(), player2.Dequeue());
            bool p1Wins;
            if (player1.Count >= p1 && player2.Count >= p2) {
                // Subgame
                var p1NewHand = new Queue<long>(player1.Take((int)p1));
                var p2NewHand = new Queue<long>(player2.Take((int)p2));
                (p1Wins, _) = PlayGame(p1NewHand, p2NewHand);
            } else {
                p1Wins = p1 > p2;
            }
            if (p1Wins) {
                player1.Enqueue(p1);
                player1.Enqueue(p2);
            } else {
                player2.Enqueue(p2);
                player2.Enqueue(p1);
            }

            return (player1, player2);
        }
    }
}
