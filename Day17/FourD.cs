using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    class FourD
    {
        public static List<Point4D> RunCycle(List<Point4D> active) {
            var survivors = GetSurvivors(active);
            var dead = GetDeadNeighbours(active);
            var newActive = GetNewActive(dead, active);
            return survivors.Union(newActive).ToList();
        }

        static List<Point4D> GetNewActive(List<Point4D> deadNeighbours, List<Point4D> active) {
            var newActive = new HashSet<Point4D>();
            foreach (var dead in deadNeighbours)
            {
                var liveNeighbourCount = GetLiveNeighbourCount(dead, active);
                if (liveNeighbourCount == 3) {
                    newActive.Add(dead);
                }
            }
            return newActive.ToList();
        }

        static List<Point4D> GetDeadNeighbours(List<Point4D> active) {
            var dead = new HashSet<Point4D>();
            foreach (var item in active)
            {
                foreach (var neighbour in GetNeighbours(item, active, false))
                {
                    dead.Add(neighbour);
                }
            }

            return dead.ToList();
        }

        static List<Point4D> GetSurvivors(List<Point4D> active) {
            var survivors = new List<Point4D>();
            foreach (var p in active)
            {
                var activeNeighbours = GetLiveNeighbourCount(p, active);
                if (activeNeighbours == 2 || activeNeighbours == 3) {
                    survivors.Add(p);
                } 
            }
            return survivors;
        }

        static List<Point4D> GetNeighbours(Point4D p, List<Point4D> active, bool isAlive = true) {
            var list = new List<Point4D>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        for (int m = -1; m <= 1; m++) {
                            if (i == 0 && j == 0 && k == 0 && m == 0) continue;
                            if (isAlive && active.Contains(new Point4D(p.x + i, p.y + j, p.z + k, p.w + m))) {
                                list.Add(new Point4D(p.x + i, p.y + j, p.z + k, p.w + m));
                            }
                            if (!isAlive && !active.Contains(new Point4D(p.x + i, p.y + j, p.z + k, p.w + m))) {
                                list.Add(new Point4D(p.x + i, p.y + j, p.z + k, p.w + m));
                            }
                        }
                    }    
                }    
            }
            return list;
        }

        static int GetLiveNeighbourCount(Point4D p, List<Point4D> active) {
            return GetNeighbours(p, active, true).Count;
        }
    }

    class Point4D {
        public int x;
        public int y;
        public int z;
        public int w;
        public Point4D(int x, int y, int z, int w) {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override bool Equals(object obj)
        {
            return obj is Point4D d &&
                   x == d.x &&
                   y == d.y &&
                   z == d.z &&
                   w == d.w;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z, w);
        }

        public override string ToString()
        {
            return $"({x},{y},{z},{w})";
        }
        public static bool operator ==(Point4D lhs, Point4D rhs)
        {
            // Check for null.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }

                // Only the left side is null.
                return false;
            }
            // Equals handles the case of null on right side.
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Point4D lhs, Point4D rhs)
        {
            return !(lhs == rhs);
        }
    }
}
