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
        public int Z { get; set; }
    }


    class Program
    {
        static int XSIZE = 5;
        static int YSIZE = 5;
        static int LEVELS = 100;

        static bool[,,] grid = new bool[XSIZE, YSIZE, LEVELS];
        static bool[,,] grid2 = new bool[XSIZE, YSIZE, LEVELS];

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt");

            HashSet<long> signatures = new HashSet<long>();

            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    grid[x, y, 50] = (input[y][x] == '#') ? true : false;
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
            for (int l = 0; l < LEVELS; l++)
            {
                bool any = false;
                for (int y = 0; y < YSIZE; y++)
                {
                    for (int x = 0; x < XSIZE; x++)
                    {
                        if (grid[x, y, l] == true)
                        {
                            any = true;
                            break;
                        }
                    }

                    if (any == true)
                        break;
                }


                if (any == true)
                {
                    Console.WriteLine(l);


                    for (int y = 0; y < YSIZE; y++)
                    {
                        for (int x = 0; x < XSIZE; x++)
                        {
                            switch (grid[x, y, l])
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
            }

        }

        private static int GetSurroundingBugs(int x, int y, int l)
        {
            List<bool> ret = new List<bool>();

            List<Coordinate> tocheck = new List<Coordinate>();


            // #....
            // .....
            // .....
            // .....
            // .....

            // Upper row, left corner
            if (x == 0 && y == 0)
            {
                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below

                tocheck.Add(new Coordinate() { X = 1, Y = 2, Z = l - 1 }); // Outer tile, left x = 1, y = 2
                tocheck.Add(new Coordinate() { X = 2, Y = 1, Z = l - 1 }); // Outer tile, above x = 2, y = 1
            }

            // .###.
            // .....
            // .....
            // .....
            // .....

            // Upper row, middle
            if (x > 0 && x < XSIZE - 1 && y == 0)
            {
                tocheck.Add(new Coordinate() { X = 2, Y = 1, Z = l - 1 }); // Outer tile, above x = 2, y = 1

                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below
                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left
            }

            // ....#
            // .....
            // .....
            // .....
            // .....

            // Upper row, right corner
            if (x == 0 && y == 0)
            {
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below
                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left

                tocheck.Add(new Coordinate() { X = 3, Y = 2, Z = l - 1 }); // Outer tile, right x = 1, y = 2
                tocheck.Add(new Coordinate() { X = 2, Y = 1, Z = l - 1 }); // Outer tile, above x = 2, y = 1
            }

            // .....
            // #....
            // #....
            // #....
            // .....

            // Row 2 3 4, left tile
            if ((y == 1 || y == 2 || y == 3) && x == 0)
            {
                tocheck.Add(new Coordinate() { X = 1, Y = 2, Z = l - 1 }); // Outer tile, left x = 1, y = 2

                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above
            }

            // .....
            // .#.#.
            // .....
            // .#.#.
            // .....

            // Row 2 and 4, tiles 2 and 4
            if ((y == 1 || y == 3) && (x == 1 || x == 3))
            {
                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above
                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left
            }

            // .....
            // ..#..
            // .....
            // .....
            // .....

            // Row 2, middle tile
            if (y == 1 && x == 2)
            {
                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above
                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left

                // Inner tile, below
                tocheck.Add(new Coordinate() { X = 0, Y = 0, Z = l + 1 }); 
                tocheck.Add(new Coordinate() { X = 1, Y = 0, Z = l + 1 }); 
                tocheck.Add(new Coordinate() { X = 2, Y = 0, Z = l + 1 }); 
                tocheck.Add(new Coordinate() { X = 3, Y = 0, Z = l + 1 }); 
                tocheck.Add(new Coordinate() { X = 4, Y = 0, Z = l + 1 }); 
            }

            // .....
            // ....#
            // ....#
            // ....#
            // .....

            // Row 2 3 4, right tile
            if ((y == 1 || y == 2 || y == 3) && x == 4)
            {
                tocheck.Add(new Coordinate() { X = 3, Y = 2, Z = l - 1 }); // Outer tile, right x = 1, y = 2

                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above
            }

            // .....
            // .....
            // .#...
            // .....
            // .....

            // Row 3, Tile 2
            if (y == 2  && x == 1)
            {
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above
                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left

                // Inner tile, right
                tocheck.Add(new Coordinate() { X = 0, Y = 0, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 0, Y = 1, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 0, Y = 2, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 0, Y = 3, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 0, Y = 4, Z = l + 1 });
            }

            // .....
            // .....
            // ...#.
            // .....
            // .....

            // Row 3, Tile 4
            if (y == 2 && x == 3)
            {
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above
                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right

                // Inner tile, right
                tocheck.Add(new Coordinate() { X = 4, Y = 0, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 4, Y = 1, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 4, Y = 2, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 4, Y = 3, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 4, Y = 4, Z = l + 1 });
            }

            // .....
            // .....
            // .....
            // ..#..
            // .....

            // Row 4, Tile 3
            if (y == 3 && x == 2)
            {
                tocheck.Add(new Coordinate() { X = x, Y = y + 1, Z = l }); // Same tile, below
                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left
                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right

                // Inner tile, right
                tocheck.Add(new Coordinate() { X = 0, Y = 4, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 1, Y = 4, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 2, Y = 4, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 3, Y = 4, Z = l + 1 });
                tocheck.Add(new Coordinate() { X = 4, Y = 4, Z = l + 1 });
            }


            // .....
            // .....
            // .....
            // .....
            // #....

            // Bottom row, left corner
            if (x == 0 && y == 4)
            {
                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above

                tocheck.Add(new Coordinate() { X = 2, Y = 3, Z = l - 1 }); // Outer tile, below 
                tocheck.Add(new Coordinate() { X = 1, Y = 2, Z = l - 1 }); // Outer tile, left 
            }


            // .....
            // .....
            // .....
            // .....
            // .###.

            // Bottom row, middle
            if (x > 0 && x < XSIZE - 1 && y == 4)
            {
                tocheck.Add(new Coordinate() { X = 2, Y = 3, Z = l - 1 }); // Outer tile, below 

                tocheck.Add(new Coordinate() { X = x + 1, Y = y, Z = l }); // Same tile, right
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above
                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left
            }

            // .....
            // .....
            // .....
            // .....
            // ....#


            // Bottom row, right corner
            if (x == 4 && y == 4)
            {
                tocheck.Add(new Coordinate() { X = x - 1, Y = y, Z = l }); // Same tile, left
                tocheck.Add(new Coordinate() { X = x, Y = y - 1, Z = l }); // Same tile, above

                tocheck.Add(new Coordinate() { X = 3, Y = 3, Z = l - 1 }); // Outer tile, below 
                tocheck.Add(new Coordinate() { X = 3, Y = 2, Z = l - 1 }); // Outer tile, right x = 1, y = 2
            }



            //var validcoords = tocheck.Where(f => f.X >= 0 && f.X <= XSIZE - 1 && f.Y >= 0 && f.Y <= YSIZE - 1);

            foreach (var item in validcoords)
            {
                if (grid[item.X, item.Y] == true) ret.Add(grid[item.X, item.Y]);
            }

            return ret.Count;
        }
    }
}