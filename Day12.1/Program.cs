using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
            //StringBuilder sb = new StringBuilder();

            int[] b;

            Moon[] moons = new Moon[4];
            Moon[] initialStates = new Moon[4];

            //ConcurrentDictionary<string, object> dict = new ConcurrentDictionary<string, object>();

            Dictionary<int[], object> dict = new Dictionary<int[], object>();

            

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



            List<Tuple<int, int>> moonPairs = new List<Tuple<int, int>>();

            for (int i = 0; i < moons.Length; i++)
            {
                for (int j = i + 1; j < moons.Length; j++)
                {
                    moonPairs.Add(new Tuple<int, int>(i, j));
                }
            }

            Console.WriteLine($"After 0 steps");
            foreach (var item in moons)
            {
                Console.WriteLine($"Pos: {item.Position.ToString()}, Velocity: {item.Velocity.ToString()}");
            }


            // Add init state
            //sb.Clear();
            int zzz = 0;
            b = new int[24];
            foreach (var item in initialStates)
            {
                //initialchecksum += item.Position.x + item.Position.y + item.Position.z + item.Velocity.x + item.Velocity.y + item.Velocity.z;
                b[zzz+0] = (int)item.Position.x;
                b[zzz+1] = (int)item.Position.y;
                b[zzz+2] = (int)item.Position.z;
                b[zzz+3] = (int)item.Velocity.x;
                b[zzz+4] = (int)item.Velocity.y;
                b[zzz+5] = (int)item.Velocity.z;

                zzz+=6;
            }

            dict.TryAdd(b, null);

            string s = "";


            //int checksum = 0;
            for (long i = 0; i < 5000000000; i++)
            {
                //checksum = 0;

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

                    //checksum += moons[b].Position.x + moons[b].Position.y + moons[b].Position.z + moons[b].Velocity.x + moons[b].Velocity.y + moons[b].Velocity.z;
                }

                // Calculate state
                zzz = 0;
                b = new int[24];
                foreach (var item in initialStates)
                {
                    //initialchecksum += item.Position.x + item.Position.y + item.Position.z + item.Velocity.x + item.Velocity.y + item.Velocity.z;
                    b[zzz + 0] = (int)item.Position.x;
                    b[zzz + 1] = (int)item.Position.y;
                    b[zzz + 2] = (int)item.Position.z;
                    b[zzz + 3] = (int)item.Velocity.x;
                    b[zzz + 4] = (int)item.Velocity.y;
                    b[zzz + 5] = (int)item.Velocity.z;

                    zzz+=6;
                }

                if (dict.ContainsKey(b) == true)
                {
                        Console.WriteLine($"duplicate found @ iteration {i}");
                        Console.ReadKey();
                }
                else
                {
                    dict.Add(b, null);
                }

                if (i % 100000 == 0)
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
