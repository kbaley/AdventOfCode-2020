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
            var tiles = new List<Tile>();
            var i = 0;
            while (i < input.Length) {
                var number = int.Parse(input[i].Replace("Tile ", "").Replace(":", ""));
                i++;
                var start = i;
                for (; i < input.Length && !string.IsNullOrWhiteSpace(input[i]); i++);
                var data = input.Skip(start).Take(i - start);
                tiles.Add(new Tile(number, data.ToArray()));
                i++;
            }

            // Part 1
            var corners = GetCorners(tiles);
            long product = corners.Aggregate(1L, (x, y) => x * y);
            System.Console.WriteLine($"Part 1: {product}");

            // Part 2
            var topLeftCorner = corners[0];
            var tile = tiles.First(t => t.Number == topLeftCorner);
            var size = Math.Sqrt(tiles.Count);
            tile.X = 0;
            tile.Y = 0;
            for (int j = 0; j < tile.Edges().Length; j++)
            {
                var edge = tile.Edges()[j];
                if (tiles.Any(t => t.Number != tile.Number && t.Edges().Any(e => e == edge))) {
                    OrientFirstTile(tile, j);
                    break;
                }
            }
            System.Console.WriteLine(tile);
            System.Console.WriteLine();
            for (int y = 0; y < size; y++)
            {
                for (int x = 0; x < size; x++)
                {
                    if (x != 0 || y != 0) {
                        if (x == 0) {
                            var topTile = tiles.Single(t => t.X == 0 && t.Y == y - 1);
                            var bottomEdge = topTile.BottomEdge;
                            var match = tiles.Single(t => t.Number != topTile.Number && t.Edges().Any(e => e == bottomEdge));
                            OrientTile(topTile, match, 2);
                            match.FlipVertical();
                            match.RotateClockwise();
                            match.X = x;
                            match.Y = y;
                            System.Console.WriteLine(match);
                            System.Console.WriteLine();
                        } else {
                            var leftTile = tiles.Single(t => t.X == x - 1 && t.Y == y);
                            var rightEdge = leftTile.RightEdge;
                            var match = tiles.Single(t => t.Number != leftTile.Number && t.Edges().Any(e => e == rightEdge));
                            OrientTile(leftTile, match, 1);
                            match.X = x;
                            match.Y = y;
                            System.Console.WriteLine(match);
                            System.Console.WriteLine();
                        }
                    }
                }
            }
            var grid = BuildFinalGrid(tiles, size);
            var gridTile = new Tile(0, grid);
            var roughness = grid.Sum(g => g.Count(l => l == '#'));
            var seaMonster = new List<(int x, int y)> {
                (0,0),
                (-18,1),
                (-17,2),
                (-14,2),
                (-13,1),
                (-12,1),
                (-11, 2),
                (-8, 2),
                (-7, 1),
                (-6, 1),
                (-5, 2),
                (-2, 2),
                (-1, 1),
                (0, 1),
                (1, 1)
            };
            var numMonsters = 0;
            while (true) {
                numMonsters = GetSeaMonsters(gridTile.Data, seaMonster);
                if (numMonsters > 0) break;
                gridTile.RotateClockwise();
                numMonsters = GetSeaMonsters(gridTile.Data, seaMonster);
                if (numMonsters > 0) break;
                gridTile.RotateClockwise();
                numMonsters = GetSeaMonsters(gridTile.Data, seaMonster);
                if (numMonsters > 0) break;
                gridTile.RotateClockwise();
                numMonsters = GetSeaMonsters(gridTile.Data, seaMonster);
                if (numMonsters > 0) break;
                gridTile.FlipVertical();
                numMonsters = GetSeaMonsters(gridTile.Data, seaMonster);
                if (numMonsters > 0) break;
                gridTile.RotateClockwise();
                numMonsters = GetSeaMonsters(gridTile.Data, seaMonster);
                if (numMonsters > 0) break;
                gridTile.RotateClockwise();
                numMonsters = GetSeaMonsters(gridTile.Data, seaMonster);
                if (numMonsters > 0) break;
                gridTile.RotateClockwise();
                numMonsters = GetSeaMonsters(gridTile.Data, seaMonster);
                if (numMonsters > 0) break;
            }
            System.Console.WriteLine(roughness - (numMonsters * seaMonster.Count));
            Console.WriteLine("Done");
        }

        static int GetSeaMonsters(string[] grid, List<(int x, int y)> seaMonster) {
            var numMonsters = 0;
            for (int y = 0; y < grid.Length - 2; y++)
            {
                for (int x = 18; x < grid.Length; x++) {
                    if (seaMonster.All(m => grid[y + m.y][x + m.x] == '#')) {
                        numMonsters++;
                    }
                }    
            }
            return numMonsters;
        }

        static string[] BuildFinalGrid(List<Tile> tiles, double size) {
            var grid = new List<string>();
            foreach (var tile in tiles)
            {
                tile.Data = tile.Data.Skip(1).Take(tile.Data.Length - 2).ToArray(); 
                tile.Data = tile.Data.Select(d => d[1..^1]).ToArray();
            }
            for (int y = 0; y < size; y++)
            {
                var row = tiles.Where(t => t.Y == y).OrderBy(t => t.X).Select(r => r.Data);
                for (int i = 0; i < tiles.First().Data[0].Length; i++)
                {
                    grid.Add(string.Join("", row.Select(r => r[i])));
                }
            }
            return grid.ToArray();
        }
        static void OrientFirstTile(Tile topLeft, int matchingEdgeIndex) {

            switch (matchingEdgeIndex) {
                case 0:
                case 4:
                    topLeft.RotateClockwise(1);
                    break;
                case 1:
                case 5:
                    break;
                case 2:
                case 6:
                    topLeft.RotateClockwise(3);
                    break;
                case 3:
                case 7:
                    topLeft.RotateClockwise(2);
                    break;
            }
        }


        static void OrientTile(Tile topLeft, Tile match, int matchingEdgeIndex) {
            var matchingEdge = topLeft.Edges()[matchingEdgeIndex];
            matchingEdgeIndex = match.Edges().ToList().IndexOf(matchingEdge);
            switch (matchingEdgeIndex) {
                case 0:
                    match.FlipVertical();
                    match.RotateClockwise(1);
                    break;
                case 1:
                    match.RotateClockwise(2);
                    match.FlipVertical();
                    break;
                case 2:
                    match.RotateClockwise(1);
                    break;
                case 3:
                    break;
                case 4:
                    match.RotateClockwise(3);
                    break;
                case 5:
                    match.RotateClockwise(2);
                    break;
                case 6:
                    match.RotateClockwise();
                    match.FlipVertical();
                    break;
                case 7:
                    match.FlipVertical();
                    break;
            }
        }

        static List<long> GetCorners(List<Tile> tiles) {

            var corners = new List<long>();
            foreach (var tile in tiles)
            {
                var edges = tile.Edges();
                var matches = new List<int>();
                for (var j = 0; j < edges.Length; j++)
                {
                    var matchedTiles = tiles
                            .Where(t => t.Number != tile.Number && t.Edges().Any(e => e == edges[j]));
                    
                    matches.AddRange(matchedTiles.Select(t => t.Number));
                }
                if (matches.Distinct().Count() == 2) {
                    corners.Add(tile.Number);
                }
            }
            return corners;
        }

    }

    class Tile {
        public int Number { get; set; }
        public string[] Data { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string TopEdge {
            get {
                return Data[0];
            }
        }

        public string BottomEdge {
            get {
                return Data[^1];
            }
        }

        public string LeftEdge { 
            get {
                return new string(Data.Select(i => i[0]).ToArray());
            }
        }

        public string RightEdge {
            get {
                return new string(Data.Select(i => i[^1]).ToArray());
            }
        }

        public string[] Edges() {
            return new string[] {
                TopEdge,
                RightEdge,
                BottomEdge,
                LeftEdge,
                new string(TopEdge.Reverse().ToArray()),
                new string(RightEdge.Reverse().ToArray()),
                new string(BottomEdge.Reverse().ToArray()),
                new string(LeftEdge.Reverse().ToArray())
            };
        }

        public Tile(int number, string[] data) {
            Number = number;
            Data = data;
            X = -1;
            Y = -1;            
        }

        public override string ToString()
        {
            return string.Join('\n', Data);
        }

        public void RotateClockwise(int iterations) {
            for (int i = 0; i < iterations; i++)
            {
                RotateClockwise();                
            }
        }

        public void RotateClockwise() {
            var newData = new List<string>();
            for (int i = 0; i < Data.Length; i++)
            {
                newData.Add(new string(Data.Select(d => d[i]).Reverse().ToArray()));
            }
            Data = newData.ToArray();
        }

        public void FlipVertical() {
            Data = Data.Reverse().ToArray();
        }
    
        public bool IsPositioned() {
            return X > -1;
        }

        public int GetMatchingEdgeIndex(string edge, List<Tile> tiles) {

            var match = tiles.SingleOrDefault(t => t.Number != Number && t.Edges().Any(e => e == edge));
            if (match == null) return -1;
            return match.Edges().ToList().IndexOf(edge);
        }
    }
}
