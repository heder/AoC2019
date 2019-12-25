using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24._1
{

    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
    }


    class Program
    {
        static int XSIZE = 5;
        static int YSIZE = 5;

        static bool[,] grid = new bool[XSIZE, YSIZE];
        static bool[,] grid2 = new bool[XSIZE, YSIZE];

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt");

            HashSet<long> signatures = new HashSet<long>();

            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    grid[x, y] = (input[y][x] == '#') ? true : false;
                }
            };

            Visualize();

            while (true)
            {
                int factor = 1;
                int signature = 0; 

                for (int y = 0; y < YSIZE; y++)
                {
                    for (int x = 0; x < XSIZE; x++)
                    {
                        if (grid[x, y] == true)
                        { 
                            signature += factor;
                        }

                        var surrcnt = GetSurroundingBugs(x, y);

                        if (grid[x, y] == true && surrcnt != 1) grid2[x, y] = false;
                        else if (grid[x, y] == false && (surrcnt == 1 || surrcnt == 2)) grid2[x, y] = true;
                        else grid2[x, y] = grid[x, y];

                        factor *= 2;
                    }
                }

                Visualize();

                if (signatures.Contains(signature))
                {
                    Console.WriteLine(signature);
                    Console.ReadKey();
                }

                signatures.Add(signature);


                // Copy grid2 to grid1
                for (int x = 0; x < XSIZE; x++)
                {
                    for (int y = 0; y < YSIZE; y++)
                    {
                        grid[x, y] = grid2[x, y];
                    }
                }
            }
        }

        private static void Visualize()
        {
            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    switch (grid[x, y])
                    {
                        case false:
                            Console.Write('.');
                            break;
                        case true:
                            Console.Write('#');
                            break;
                        default:
                            break;
                    }
                }
                Console.WriteLine();
                
            };

            Console.WriteLine();

        }

        private static int GetSurroundingBugs(int x, int y)
        {
            List<bool> ret = new List<bool>();

            List<Coordinate> tocheck = new List<Coordinate>();
            tocheck.Add(new Coordinate() { X = x, Y = y - 1 });
            tocheck.Add(new Coordinate() { X = x - 1, Y = y });
            tocheck.Add(new Coordinate() { X = x + 1, Y = y });
            tocheck.Add(new Coordinate() { X = x, Y = y + 1 });

            var validcoords = tocheck.Where(f => f.X >= 0 && f.X <= XSIZE - 1 && f.Y >= 0 && f.Y <= YSIZE - 1);

            foreach (var item in validcoords)
            {
                if (grid[item.X, item.Y] == true) ret.Add(grid[item.X, item.Y]);
            }

            return ret.Count;
        }
    }
}