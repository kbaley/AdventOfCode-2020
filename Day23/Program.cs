using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    class Program
    {
        static void Main(string[] args)
        {
            var isSample = args.Length > 0 && args[0] == "-s";
            var fileName = isSample ? "sample.txt" : "input.txt";
            var input = "219748365";

            var list = new LinkedList<long>(input.Select(i => long.Parse(i.ToString())));
            var max = list.Max();
            var nodeList = BuildNodeList(list);

            // Part 1
            for (int i = 0; i < 100; i++)
            {
                Move(list, max, nodeList);
            }
            while (list.First() != 1) {
                var first = list.First;
                list.RemoveFirst();
                list.AddLast(first);
            }

            System.Console.WriteLine($"Part 1: {string.Join("", list.Skip(1))}");

            // Part 2
            list = new LinkedList<long>(input.Select(i => long.Parse(i.ToString())));
            for (var i = max+1; i <= 1_000_000; i++)
            {
                list.AddLast(i); 
            }
            max = list.Count;
            nodeList = BuildNodeList(list);
            for (var i = 0; i < 10_000_000; i++)
            {
                Move(list, max, nodeList);
            }
            var one = nodeList[1];
            System.Console.WriteLine($"Part 2: {one.Next.Value * one.Next.Next.Value}");

            Console.WriteLine("Done");
        }

        private static Dictionary<long, LinkedListNode<long>> BuildNodeList(LinkedList<long> list) {

            var max = list.Count;
            var nodeList = new Dictionary<long, LinkedListNode<long>>();
            var node = list.First;
            for (int i = 0; i < max; i++)
            {
                nodeList.Add(node.Value, node);
                node = node.Next;
            }
            return nodeList;
        }

        private static void Move(LinkedList<long> list, long max, Dictionary<long, LinkedListNode<long>> nodeList)
        {
            var head = list.First;
            var one = head.Next;
            var two = one.Next;
            var three = two.Next;
            list.Remove(one);
            list.Remove(two);
            list.Remove(three);
            var target = (head.Value - 2 + max) % max + 1;
            while (one.Value == target || two.Value == target || three.Value == target)
            {
                target = (target - 2 + max) % max + 1;
            }
            var targetNode = nodeList[target];
            list.AddAfter(targetNode, one);
            list.AddAfter(one, two);
            list.AddAfter(two, three);
            list.RemoveFirst();
            list.AddLast(head);
        }
    }
}
