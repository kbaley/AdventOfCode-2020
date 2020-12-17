using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC
{
    class ThreeD
    {
        public static List<Point3D> RunCycle(List<Point3D> active) {
            var survivors = GetSurvivors(active);
            var dead = GetDeadNeighbours(active);
            var newActive = GetNewActive(dead, active);
            return survivors.Union(newActive).ToList();
        }

        static List<Point3D> GetNewActive(List<Point3D> deadNeighbours, List<Point3D> active) {
            var newActive = new HashSet<Point3D>();
            foreach (var dead in deadNeighbours)
            {
                var liveNeighbourCount = GetLiveNeighbourCount(dead, active);
                if (liveNeighbourCount == 3) {
                    newActive.Add(dead);
                }
            }
            return newActive.ToList();
        }

        static List<Point3D> GetDeadNeighbours(List<Point3D> active) {
            var dead = new HashSet<Point3D>();
            foreach (var item in active)
            {
                foreach (var neighbour in GetNeighbours(item, active, false))
                {
                    dead.Add(neighbour);
                }
            }

            return dead.ToList();
        }

        static List<Point3D> GetSurvivors(List<Point3D> active) {
            var survivors = new List<Point3D>();
            foreach (var p in active)
            {
                var activeNeighbours = GetLiveNeighbourCount(p, active);
                if (activeNeighbours == 2 || activeNeighbours == 3) {
                    survivors.Add(p);
                } 
            }
            return survivors;
        }

        static List<Point3D> GetNeighbours(Point3D p, List<Point3D> active, bool isAlive = true) {
            var list = new List<Point3D>();
            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    for (int k = -1; k <= 1; k++)
                    {
                        if (i == 0 && j == 0 && k == 0) continue;
                        if (isAlive && active.Contains(new Point3D(p.x + i, p.y + j, p.z + k))) {
                            list.Add(new Point3D(p.x + i, p.y + j, p.z + k));
                        }
                        if (!isAlive && !active.Contains(new Point3D(p.x + i, p.y + j, p.z + k))) {
                            list.Add(new Point3D(p.x + i, p.y + j, p.z + k));
                        }
                    }    
                }    
            }
            return list;
        }

        static int GetLiveNeighbourCount(Point3D p, List<Point3D> active) {
            return GetNeighbours(p, active, true).Count;
        }
    }

    class Point3D {
        public int x;
        public int y;
        public int z;
        public Point3D(int x, int y, int z) {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override bool Equals(object obj)
        {
            return obj is Point3D d &&
                   x == d.x &&
                   y == d.y &&
                   z == d.z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public override string ToString()
        {
            return $"({x},{y},{z})";
        }
        public static bool operator ==(Point3D lhs, Point3D rhs)
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

        public static bool operator !=(Point3D lhs, Point3D rhs)
        {
            return !(lhs == rhs);
        }
    }
}
