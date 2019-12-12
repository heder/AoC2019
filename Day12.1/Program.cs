using System;
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

        public int Sum { get; set; }
    }




    class Program
    {
        static void Main(string[] args)
        {
            Moon[] moons = new Moon[4];
            Moon[] initialStates = new Moon[4];

            string[] input = File.ReadAllLines("in.txt"); //.Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            for (int i = 0; i < input.Length; i++)
            {
                var trimmed = input[i].Trim('<', '>');
                string[] parts = trimmed.Split(',');
                int x = System.Convert.ToInt32(parts[0].Split('=')[1]);
                int y = System.Convert.ToInt32(parts[1].Split('=')[1]);
                int z = System.Convert.ToInt32(parts[2].Split('=')[1]);

                moons[i] = new Moon() { Position = new Point3D() { x = x, y = y, z = z }, Velocity = new Vector3D() };
                initialStates[i] = new Moon() { Position = new Point3D() { x = x, y = y, z = z }, Velocity = new Vector3D() };
            }

            int initialchecksum = 0;
            foreach (var item in initialStates)
            {
                initialchecksum += item.Position.x + item.Position.y + item.Position.z;
            }



            List<Tuple<int, int>> moonPairs = new List<Tuple<int, int>>();

            for (int i = 0; i < moons.Length; i++)
            {
                for (int j = i; j < moons.Length; j++)
                {
                    moonPairs.Add(new Tuple<int, int>(i, j));
                }
            }

            Console.WriteLine($"After 0 steps");
            foreach (var item in moons)
            {
                Console.WriteLine($"Pos: {item.Position.ToString()}, Velocity: {item.Velocity.ToString()}");
            }

            int checksum = 0;
            for (long i = 0; i < 5000000000; i++)
            {
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
                for (int b = 0; b < moons.Length; b++)
                {
                    moons[b].Position.x += moons[b].Velocity.x;
                    moons[b].Position.y += moons[b].Velocity.y;
                    moons[b].Position.z += moons[b].Velocity.z;

                    checksum += moons[b].Position.x + moons[b].Position.y + moons[b].Position.z;
                }




                //Console.WriteLine($"After {i + 1} steps");
                //foreach (var item in moons)
                //{
                //    Console.WriteLine($"Pos: {item.Position.ToString()}, Velocity: {item.Velocity.ToString()}");
                //}

                // Check states only if sum of all moons is same

                if (checksum == initialchecksum)
                {

                    for (int x = 0; x < 4; x++)
                    {
                        if (moons[x].Position.x != initialStates[x].Position.x)
                        {
                            break;
                        }
                        if (moons[x].Position.y != initialStates[x].Position.y)
                        {
                            break;
                        }
                        if (moons[x].Position.z != initialStates[x].Position.z)
                        {
                            break;
                        }
                        if (moons[x].Velocity.x != initialStates[x].Velocity.x)
                        {
                            break;
                        }
                        if (moons[x].Velocity.y != initialStates[x].Velocity.y)
                        {
                            break;
                        }
                        if (moons[x].Velocity.z != initialStates[x].Velocity.z)
                        {
                            break;
                        }

                        if (x == 3)
                        {
                            // If we get here, the states are the same
                            Console.WriteLine($"State found after {i} iterations");
                            Console.ReadKey();
                        }
                    }
                }

                if (i % 1000000 == 0)
                {
                    Console.WriteLine(i);
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
