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
            const int SIZE = 100;

            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            Pixel[,] grid = new Pixel[SIZE, SIZE];
            Intcode.CPU cpu = new Intcode.CPU(input);

            int currentX = SIZE / 2;
            int currentY = SIZE / 2;
            grid[currentX, currentY].Color = 1;

            Direction currentDirection = Direction.UP;

            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                // Read current color
                var currentColor = grid[currentX, currentY].Color;

                // Input to Intcode
                cpu.Input = new long[1] { currentColor };
                cpu.Run();

                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    // Output is color
                    grid[currentX, currentY].Color = (int)cpu.Output;
                    grid[currentX, currentY].Painted = true;
                }

                cpu.Input = new long[0];
                cpu.Run();

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
                    }

                }
            }

            int painted = 0;
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    switch (grid[x, y].Color)
                    {
                        case 0:
                            Console.Write('.');
                            break;

                        case 1:
                            Console.Write('*');
                            break;

                        default:
                            break;
                    }

                    if (grid[x, y].Painted == true)
                    {
                        painted++;
                    }
                }

                Console.WriteLine("");
            }

            Console.WriteLine($"Painted {painted} tiles");
            Console.ReadKey();
        }
    }
}
