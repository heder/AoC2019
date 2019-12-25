using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24._2
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
        static int LEVELS = 500;

        static bool[,,] grid = new bool[XSIZE, YSIZE, LEVELS];
        static bool[,,] grid2 = new bool[XSIZE, YSIZE, LEVELS];

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt");

            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    grid[x, y, 250] = (input[y][x] == '#') ? true : false;
                }
            };

            Visualize();

            int iterations = 0;

            while (true)
            {
                int factor = 1;
                int signature = 0;

                for (int l = 1; l < LEVELS - 1; l++)
                {

                    for (int y = 0; y < YSIZE; y++)
                    {
                        for (int x = 0; x < XSIZE; x++)
                        {
                            if (!(x == 2 && y == 2)) // Skip middle tile
                            {
                                if (grid[x, y, l] == true)
                                {
                                    signature += factor;
                                }

                                var surrcnt = GetSurroundingBugs(x, y, l);

                                if (grid[x, y, l] == true && surrcnt != 1) grid2[x, y, l] = false;
                                else if (grid[x, y, l] == false && (surrcnt == 1 || surrcnt == 2)) grid2[x, y, l] = true;
                                else grid2[x, y, l] = grid[x, y, l];

                                factor *= 2;
                            }
                        }
                    }
                }

                iterations++;


                for (int l = 0; l < LEVELS; l++)
                {
                    for (int x = 0; x < XSIZE; x++)
                    {
                        for (int y = 0; y < YSIZE; y++)
                        {
                            grid[x, y, l] = grid2[x, y, l];
                        }
                    }
                }

                if (iterations == 200)
                {
                    int agg = 0;
                    for (int l = 0; l < LEVELS; l++)
                    {
                        for (int x = 0; x < XSIZE; x++)
                        {
                            for (int y = 0; y < YSIZE; y++)
                            {
                                if (grid[x, y, l] == true)
                                {
                                    agg++;
                                }
                            }
                        }
                    }

                    Console.WriteLine(agg);
                    Console.ReadKey();
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


        // Fulhack Deluxe!
        private static int GetSurroundingBugs(int x, int y, int l)
        {
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
            if (x == 4 && y == 0)
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
            if (y == 2 && x == 1)
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

                // Inner tile, left
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

                // Inner tile, above
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

                tocheck.Add(new Coordinate() { X = 2, Y = 3, Z = l - 1 }); // Outer tile, below 
                tocheck.Add(new Coordinate() { X = 3, Y = 2, Z = l - 1 }); // Outer tile, right x = 1, y = 2
            }

            return tocheck.Count(f => grid[f.X, f.Y, f.Z] == true);
        }
    }
}