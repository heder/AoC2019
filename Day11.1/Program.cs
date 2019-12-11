using System;
using System.IO;
using System.Linq;

namespace Day11._1
{

    enum Direction
    {
        UP = 0,
        LEFT = 1,
        DOWN = 2,
        RIGHT = 3
    }

    class Program
    {
        struct Pixel
        {
            public int Color { get; set; }
            public bool Painted { get; set; }
        }

        static void Main(string[] args)
        {

            string[] input = File.ReadAllLines("in.txt")[0].Split(',');

            long[] ram = new long[20000];
            for (int i = 0; i < input.Length; i++)
            {
                ram[i] = Convert.ToInt64(input[i]);
            }

            Pixel[,] grid = new Pixel[10000, 10000];
            Intcode.CPU cpu = new Intcode.CPU(ram);


            int currentX = 5000;
            int currentY = 5000;
            grid[currentX, currentY].Color = 1;

            Direction currentDirection = Direction.UP;

            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                // Read current color
                var currentColor = grid[currentX, currentY].Color;

                // Input to Intcode
                cpu.Run(new long[1] { currentColor });

                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    // Output is color
                    grid[currentX, currentY].Color = (int)cpu.Output;
                    grid[currentX, currentY].Painted = true;
                }

                cpu.Run(new long[0]);

                // Output is turn

                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    if (cpu.Output == 0)
                    {
                        // Turn Left
                        if (currentDirection == Direction.RIGHT)
                        {
                            currentDirection = Direction.UP;
                        }
                        else
                        {
                            currentDirection++;
                        }

                        // Move forward and paint
                    }
                    if (cpu.Output == 1)
                    {
                        if (currentDirection == Direction.UP)
                        {
                            currentDirection = Direction.RIGHT;
                        }
                        else
                        {
                            currentDirection--;
                        }
                    }

                    // Move
                    switch (currentDirection)
                    {
                        case Direction.UP:
                            currentX--;
                            break;
                        case Direction.LEFT:
                            currentY--;
                            break;
                        case Direction.DOWN:
                            currentX++;
                            break;
                        case Direction.RIGHT:
                            currentY++;
                            break;
                        default:
                            break;
                    }

                }
            }

            int painted = 0;
            for (int x = 0; x < 10000; x++)
            {
                for (int y = 0; y < 10000; y++)
                {
                    switch (grid[x,y].Color)
                    {
                        case 0:
                            //Console.Write('.');
                            break;

                        case 1:
                            //Console.Write('*');
                            break;

                        default:
                            break;
                    }

                    if (grid[x, y].Painted == true)
                    {
                        painted++;
                    }
                }

                //Console.WriteLine("");
            }

            Console.WriteLine("Painted {painted} tiles");
            Console.ReadKey();
        }
    }
}
