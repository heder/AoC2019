using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day19._2
{
    struct coord
    {
        public int x;
        public int y;
    }

    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            const int SIZE = 1500;

            int[,] grid = new int[SIZE, SIZE];
            int output = 0;
            Intcode.CPU cpu;

            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    cpu = new Intcode.CPU(input);
                    cpu.Input = new long[] { x, y };
                    cpu.Run();

                    if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                    {
                        output = (int)cpu.Output;
                        if (output == 1)
                        {
                            grid[x, y] = 1;
                        }
                    }
                }
            }

            int xmaxTractorBeam = 0;

            // Find rightmost tractor beam position
            for (int x = SIZE - 1; x > 0; x--)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (grid[x, y] == 1)
                    {
                        Console.WriteLine($"First tractor beam grid is at xpos {x}");
                        xmaxTractorBeam = x;
                        break;
                    }
                }

                if (xmaxTractorBeam > 0) break;
            }

            xmaxTractorBeam -= 100;

            int lowx = 10000;
            int lowy = 10000;

            for (int xx = xmaxTractorBeam - 100; xx >= 0; xx--)
            {
                coord a = new coord() { x = xx, y = 0 };
                coord b = new coord() { x = xx + 99, y = 0 };
                coord c = new coord() { x = xx, y = 99 };
                coord d = new coord() { x = xx + 99, y = 99 };

                // Move ship down until all coordinates are inside beam
                for (int i = 0; i < SIZE - 100; i++)
                {
                    a.y++;
                    b.y++;
                    c.y++;
                    d.y++;

                    if (grid[a.x, a.y] == 1 && grid[b.x, b.y] == 1 && grid[c.x, c.y] == 1 && grid[d.x, d.y] == 1)
                    {
                        //Console.WriteLine($"Ship fits @ {a.x},{a.y}");
                        if (a.x < lowx) { lowx = a.x; lowy = a.y; }
                    }
                }
            }

            Bitmap bm = new Bitmap(SIZE, SIZE);
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (grid[i, j] == 1)
                        bm.SetPixel(i, j, Color.White);
                    else
                        bm.SetPixel(i, j, Color.Black);
                }
            }

            bm.SetPixel(lowx, lowy, Color.Red);
            bm.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine($"{lowx},{lowy}");
            Console.ReadKey();
        }
    }
}
