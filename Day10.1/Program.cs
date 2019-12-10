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
            public Point2D coord { get; set; }
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
                        asteroids.Add(new Asteroid() { coord = new Point2D(x, y), CanSee = 0 });
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
                    Line2D lineOfSight = new Line2D(from.coord, to.coord);

                    var blockers = asteroids.Except(new List<Asteroid>() { from, to });
                    bool blockerExists = false;
                    foreach (var blocker in blockers)
                    {

                        //var tol = Angle.FromDegrees(0.5);

                        Line2D lineToPotentialBlocker = new Line2D(from.coord, blocker.coord);
                        blockerExists = lineOfSight.IsParallelTo(lineToPotentialBlocker) && lineOfSight.Length <= lineToPotentialBlocker.Length;

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


        }


    }
}
