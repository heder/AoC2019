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
                    //Line2D lineOfSight = new Line2D(from.coord, to.coord);

                    var blockers = asteroids.Except(new List<Asteroid>() { from, to });
                    bool blockerExists = false;
                    foreach (var blocker in blockers)
                    {

                        //var tol = Angle.FromDegrees(0.5);

                        //Line2D lineToPotentialBlocker = new Line2D(from.coord, blocker.coord);
                        //blockerExists = lineOfSight.IsParallelTo(lineToPotentialBlocker) && lineOfSight.Length <= lineToPotentialBlocker.Length && lineOfSight.IntersectWith(lineToPotentialBlocker);
                        //blockerExists = lineOfSight.IntersectWith(lineToPotentialBlocker);
                        blockerExists = IsOnLine(from.coord, to.coord, blocker.coord);
                        if (blockerExists == true)
                        {
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



            static bool IsOnLine(PointF pt1, PointF pt2, PointF pt, double epsilon = 0.001)
            {
                if (pt.X - Math.Max(pt1.X, pt2.X) > epsilon ||
                    Math.Min(pt1.X, pt2.X) - pt.X > epsilon ||
                    pt.Y - Math.Max(pt1.Y, pt2.Y) > epsilon ||
                    Math.Min(pt1.Y, pt2.Y) - pt.Y > epsilon)
                    return false;

                if (Math.Abs(pt2.X - pt1.X) < epsilon)
                    return Math.Abs(pt1.X - pt.X) < epsilon || Math.Abs(pt2.X - pt.X) < epsilon;
                if (Math.Abs(pt2.Y - pt1.Y) < epsilon)
                    return Math.Abs(pt1.Y - pt.Y) < epsilon || Math.Abs(pt2.Y - pt.Y) < epsilon;

                double x = pt1.X + (pt.Y - pt1.Y) * (pt2.X - pt1.X) / (pt2.Y - pt1.Y);
                double y = pt1.Y + (pt.X - pt1.X) * (pt2.Y - pt1.Y) / (pt2.X - pt1.X);

                return Math.Abs(pt.X - x) < epsilon || Math.Abs(pt.Y - y) < epsilon;
            }

        }


    }
}
