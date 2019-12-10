using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day10_2
{
    class Program
    {
        class Asteroid
        {
            public Asteroid()
            {
                CanSeeAsteroids = new List<Asteroid>();
            }

            public PointF coord { get; set; }
            public int CanSee { get; set; }

            public List<Asteroid> CanSeeAsteroids { get; set; }

            public double Angle { get; set; }
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


            // Loop all asteroids
            foreach (var from in asteroids)
            {
                // Loop all other asteroids
                var others = asteroids.Except(new List<Asteroid>() { from });
                foreach (var to in others)
                {
                    var potentialBlockers = asteroids.Except(new List<Asteroid>() { from, to });

                    bool blockerExists = false;
                    foreach (var checkIfBlocks in potentialBlockers)
                    {
                        blockerExists = IsOnLine(from.coord, to.coord, checkIfBlocks.coord);
                        if (blockerExists == true)
                        {
                            break;
                        }
                    }

                    if (blockerExists == false)
                    {
                        from.CanSee++;
                        from.CanSeeAsteroids.Add(to);
                    }
                }
            }

            var max = asteroids.Max(f => f.CanSee);
            var res = asteroids.First(g => g.CanSee == max);

            // Calculate angle on all that can be seen.
            foreach (var item in res.CanSeeAsteroids)
            {
                float deltaX = item.coord.X - res.coord.X;
                float deltaY = item.coord.Y - res.coord.Y;

                if (deltaY < 0 && deltaX >= 0)
                {
                    item.Angle = Math.Atan2(Math.Abs(deltaX), Math.Abs(deltaY)) * 180d / Math.PI;
                }
                else if (deltaY >= 0 && deltaX > 0)
                {
                    item.Angle = Math.Atan2(Math.Abs(deltaY), Math.Abs(deltaX)) * 180d / Math.PI;
                    item.Angle += 90;
                }
                else if (deltaY > 0 && deltaX <= 0)
                {
                    item.Angle = Math.Atan2(Math.Abs(deltaX), Math.Abs(deltaY)) * 180d / Math.PI;
                    item.Angle += 180;
                }
                else if (deltaY <= 0 && deltaX <= 0)
                {
                    item.Angle = Math.Atan2(Math.Abs(deltaY), Math.Abs(deltaX)) * 180d / Math.PI;
                    item.Angle += 270;
                }
            }

            // Sort 

            var xxx = res.CanSeeAsteroids.OrderBy(f => f.Angle);

            var yyy = xxx.ElementAt(199);

            var code = (yyy.coord.X * 100) + yyy.coord.Y;
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
