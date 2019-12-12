using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Day12._1
{
    class Point3D
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
    }

    class Vector3D
    {
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
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

            Console.WriteLine($"After 0 steps");
            foreach (var item in moons)
            {
                Console.WriteLine($"Pos: {item.Position.ToString()}, Velocity: {item.Velocity.ToString()}");
            }

            int xcycle = 0;
            int ycycle = 0;
            int zcycle = 0;

            HashSet<(int, int, int, int, int, int, int, int)> states = new HashSet<(int, int, int, int, int, int, int, int)>();

            for (int i = 0; i < 100000000; i++)
            {
                if (states.Contains((moons[0].Position.x, moons[1].Position.x, moons[2].Position.x, moons[3].Position.x, moons[0].Velocity.x, moons[1].Velocity.x, moons[2].Velocity.x, moons[3].Velocity.x)) == false)
                {
                    states.Add((moons[0].Position.x, moons[1].Position.x, moons[2].Position.x, moons[3].Position.x, moons[0].Velocity.x, moons[1].Velocity.x, moons[2].Velocity.x, moons[3].Velocity.x));
                }
                else
                {
                    xcycle = i;
                    break;
                }

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
                }

                for (int bbb = 0; bbb < moons.Length; bbb++)
                {
                    moons[bbb].Position.x += moons[bbb].Velocity.x;
                }
            }

            states.Clear();
            for (int i = 0; i < 100000000; i++)
            {
                if (states.Contains((moons[0].Position.y, moons[1].Position.y, moons[2].Position.y, moons[3].Position.y, moons[0].Velocity.y, moons[1].Velocity.y, moons[2].Velocity.y, moons[3].Velocity.y)) == false)
                {
                    states.Add((moons[0].Position.y, moons[1].Position.y, moons[2].Position.y, moons[3].Position.y, moons[0].Velocity.y, moons[1].Velocity.y, moons[2].Velocity.y, moons[3].Velocity.y));
                }
                else
                {
                    ycycle = i;
                    break;
                }

                foreach (var item in moonPairs)
                {
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
                }

                for (int bbb = 0; bbb < moons.Length; bbb++)
                {
                    moons[bbb].Position.y += moons[bbb].Velocity.y;
                }
            }

            states.Clear();
            for (int i = 0; i < 100000000; i++)
            {
                if (states.Contains((moons[0].Position.z, moons[1].Position.z, moons[2].Position.z, moons[3].Position.z, moons[0].Velocity.z, moons[1].Velocity.z, moons[2].Velocity.z, moons[3].Velocity.z)) == false)
                {
                    states.Add((moons[0].Position.z, moons[1].Position.z, moons[2].Position.z, moons[3].Position.z, moons[0].Velocity.z, moons[1].Velocity.z, moons[2].Velocity.z, moons[3].Velocity.z));
                }
                else
                {
                    zcycle = i;
                    break;
                }

                foreach (var item in moonPairs)
                {
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

                for (int bbb = 0; bbb < moons.Length; bbb++)
                {
                    moons[bbb].Position.z += moons[bbb].Velocity.z;
                }
            }

            var cycle = Euclid.LeastCommonMultiple(new long[3] { xcycle , ycycle , zcycle});
            Console.WriteLine($"cycle: {cycle}");
            Console.ReadKey();
        }
    }
}
