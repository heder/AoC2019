﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Day12._1
{
    class Point3D
    {
        public Point3D()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public override string ToString()
        {
            return $"x:{x}, y{y}, z{z}";
        }
    }

    class Vector3D
    {
        public Vector3D()
        {
            x = 0;
            y = 0;
            z = 0;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }

        public override string ToString()
        {
            return $"x:{x}, y{y}, z{z}";
        }
    }

    class Moon
    {
        public Point3D Position { get; set; }
        public Vector3D Velocity { get; set; }
    }


    class Program
    {

        static void Main(string[] args)
        {
            Moon[] moons = new Moon[4];
 
            string[] input = File.ReadAllLines("in.txt");

            for (int i = 0; i < input.Length; i++)
            {
                var trimmed = input[i].Trim('<', '>');
                string[] parts = trimmed.Split(',');
                int x = System.Convert.ToInt32(parts[0].Split('=')[1]);
                int y = System.Convert.ToInt32(parts[1].Split('=')[1]);
                int z = System.Convert.ToInt32(parts[2].Split('=')[1]);

                moons[i] = new Moon() { Position = new Point3D() { x = x, y = y, z = z }, Velocity = new Vector3D() };
            }

            List<(int, int)> moonPairs = new List<(int, int)>();
            for (int i = 0; i < moons.Length; i++)
            {
                for (int j = i + 1; j < moons.Length; j++)
                {
                    moonPairs.Add((i, j));
                }
            }

            for (long i = 0; i < 1500; i++)
            {
                Console.WriteLine($"After {i} steps");
                foreach (var item in moons)
                {
                    Console.WriteLine($"Pos: {item.Position.ToString()}, Velocity: {item.Velocity.ToString()}");
                }

                if (i == 1000)
                {
                    break;
                }

                // Find pairs to compare and adjust velocity
                foreach (var item in moonPairs)
                {
                    if (moons[item.Item1].Position.x < moons[item.Item2].Position.x)
                    {
                        moons[item.Item1].Velocity.x++;
                        moons[item.Item2].Velocity.x--;
                    }
                    else if (moons[item.Item1].Position.x > moons[item.Item2].Position.x)
                    {
                        moons[item.Item1].Velocity.x--;
                        moons[item.Item2].Velocity.x++;
                    }

                    if (moons[item.Item1].Position.y < moons[item.Item2].Position.y)
                    {
                        moons[item.Item1].Velocity.y++;
                        moons[item.Item2].Velocity.y--;
                    }
                    else if (moons[item.Item1].Position.y > moons[item.Item2].Position.y)
                    {
                        moons[item.Item1].Velocity.y--;
                        moons[item.Item2].Velocity.y++;
                    }

                    if (moons[item.Item1].Position.z < moons[item.Item2].Position.z)
                    {
                        moons[item.Item1].Velocity.z++;
                        moons[item.Item2].Velocity.z--;
                    }
                    else if (moons[item.Item1].Position.z > moons[item.Item2].Position.z)
                    {
                        moons[item.Item1].Velocity.z--;
                        moons[item.Item2].Velocity.z++;
                    }

                }

                // Use velocity to adjust position
                for (int bbb = 0; bbb < moons.Length; bbb++)
                {
                    moons[bbb].Position.x += moons[bbb].Velocity.x;
                    moons[bbb].Position.y += moons[bbb].Velocity.y;
                    moons[bbb].Position.z += moons[bbb].Velocity.z;
                }
            }

            int kinetic = 0;
            // Calc result
            foreach (var item in moons)
            {
                kinetic += (Math.Abs(item.Position.x) + Math.Abs(item.Position.y) + Math.Abs(item.Position.z)) * (Math.Abs(item.Velocity.x) + Math.Abs(item.Velocity.y) + Math.Abs(item.Velocity.z));
            }

            Console.WriteLine(kinetic);
            Console.ReadKey();
        }
    }
}
