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

            const int SIZE = 1000;

            int[,] grid = new int[SIZE, SIZE];
            int output = 0;
            Intcode.CPU cpu;

            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    Console.WriteLine($"{x},{y}");

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


            Bitmap bm = new Bitmap(SIZE, SIZE);


            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (grid[i,j] == 1)
                        bm.SetPixel(i, j, Color.White);
                    else
                        bm.SetPixel(i, j, Color.Black);
                }
            }


            bm.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);

            //// Find rightmost "1" pixel
            //for (int x = SIZE - 1; x > 0; x--)
            //{
            //    for (int y = 0; y < length; y++)
            //    {

            //    }
            //}



            // a     b
            // c     d

            // Virtual corners, top right
            coord a = new coord() { x = 0, y = SIZE - 100 - 1 };
            coord b = new coord() { x = 0, y = SIZE - 1 };
            coord c = new coord() { x = SIZE - 100 - 1, y = 99 };
            coord d = new coord() { x = SIZE - 1, y = 99 };

            // Move ship down until all coordinates are inside beam
            for (int i = 0; i < SIZE; i++)
            {
                a.y++;
                b.y++;
                c.y++;
                d.y++;

                if (grid[d.x, d.y] == 1)
                {
                    Console.Write($"d inside @ {d.x},{d.y}");
                }


                Console.WriteLine();
            }


            //Console.WriteLine(aggregate);
            Console.ReadKey();
        }
    }
}
