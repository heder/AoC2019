using System;
using System.IO;
using System.Linq;

namespace Day13._1
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            const int SIZE = 100;

            int[,] grid = new int[SIZE, SIZE];

            Intcode.CPU cpu = new Intcode.CPU(input);

            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                int x = 0;
                int y = 0;
                int c = 0;

                cpu.Run(new long[] { });
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    x = (int)cpu.Output;
                }

                cpu.Run(new long[] { });
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    y = (int)cpu.Output;
                }

                cpu.Run(new long[] { });
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    c = (int)cpu.Output;
                }

                grid[x, y] = c;
            }

            int noOfBlockTiles = 0;
            // Dump some ASCII-Art
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    //switch (image[((i * STRIDE) + j)])
                    //{
                    //    case 0:
                    //        Console.Write(" ");
                    //        b.SetPixel(j, i, Color.Transparent);
                    //        break;

                    //    case 1:
                    //        Console.Write("o");
                    //        b.SetPixel(j, i, Color.Black);
                    //        break;
                    //    case 2:
                    //        Console.Write(".");
                    //        b.SetPixel(j, i, Color.White);
                    //        break;

                    //    default:
                    //        break;
                    //}
                    if (grid[j, i] == 2)
                    {
                        noOfBlockTiles++;
                    }

                }
            }

            Console.WriteLine(noOfBlockTiles);
            Console.ReadKey();
        }
    }
}
