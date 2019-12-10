using MathNet.Spatial.Euclidean;
using MathNet.Spatial.Units;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day10._1
{
    class Program
    {
        class Asteroid
        {
            public PointF coord { get; set; }
            public int CanSee { get; set; }
        }

        static void Main(string[] args)
        {

            string[] inputString = File.ReadAllLines("in.txt");

            List<Asteroid> asteroids = new List<Asteroid>();
            

            for (int y = 0; y < inputString.Length; y++)
            {
                for (int x = 0; x < inputString[y].Length; x++)
                {
                    if (inputString[y][x] == '#')
                    {
                        asteroids.Add(new Asteroid() { coord = new PointF(x, y), CanSee = 0 });
                    }
                }
            }


            List<Asteroid> blockingAsteroids = new List<Asteroid>();

            // Loop all asteroids
            foreach (var from in asteroids)
            {
                // Loop all other asteroids
                var others = asteroids.Except(new List<Asteroid>() { from });
                foreach (var to in others)
                {
                    var blockers = asteroids.Except(new List<Asteroid>() { from, to });

                    // Sort the blockers list according to distance, so that the blockingAsteroids list contains the closest ones.
                    blockers.OrderBy(f => new Line2D(new Point2D(from.coord.X, from.coord.Y), new Point2D(f.coord.X, f.coord.Y)).Length);

                    bool blockerExists = false;
                    foreach (var blocker in blockers)
                    {
                        blockerExists = IsOnLine(from.coord, to.coord, blocker.coord);
                        if (blockerExists == true)
                        {
                            blockingAsteroids.Add(blocker);
                            break;
                        }
                    }

                    if (blockerExists == false)
                    {
                        from.CanSee++;
                    }
                }
            }

            var max = asteroids.Max(f => f.CanSee);
            var res = asteroids.Where(g => g.CanSee == max);


            // Recall sorted blockers with angle.
            // Loop all asteroids
            var from = res.First();

            // Loop all other asteroids
            var others = asteroids.Except(new List<Asteroid>() { from });
            foreach (var to in others)
            {
                var blockers = asteroids.Except(new List<Asteroid>() { from, to });

                // Sort the blockers list according to distance, so that the blockingAsteroids list contains the closest ones.
                blockers.OrderBy(f => new Line2D(new Point2D(from.coord.X, from.coord.Y), new Point2D(f.coord.X, f.coord.Y)).Length);

                bool blockerExists = false;
                foreach (var blocker in blockers)
                {
                    blockerExists = IsOnLine(from.coord, to.coord, blocker.coord);
                    if (blockerExists == true)
                    {
                        blockingAsteroids.Add(blocker);
                        break;
                    }
                }

                if (blockerExists == false)
                {
                    from.CanSee++;
                }
            }
            

        }

        static bool IsOnLine(PointF start, PointF end, PointF pt, double epsilon = 0.001)
        {
            if (pt.X - Math.Max(start.X, end.X) > epsilon ||
                Math.Min(start.X, end.X) - pt.X > epsilon ||
                pt.Y - Math.Max(start.Y, end.Y) > epsilon ||
                Math.Min(start.Y, end.Y) - pt.Y > epsilon)
                return false;

            if (Math.Abs(end.X - start.X) < epsilon)
                return Math.Abs(start.X - pt.X) < epsilon || Math.Abs(end.X - pt.X) < epsilon;
            if (Math.Abs(end.Y - start.Y) < epsilon)
                return Math.Abs(start.Y - pt.Y) < epsilon || Math.Abs(end.Y - pt.Y) < epsilon;

            double x = start.X + (pt.Y - start.Y) * (end.X - start.X) / (end.Y - start.Y);
            double y = start.Y + (pt.X - start.X) * (end.Y - start.Y) / (end.X - start.X);

            return Math.Abs(pt.X - x) < epsilon || Math.Abs(pt.Y - y) < epsilon;
        }

    }
}
