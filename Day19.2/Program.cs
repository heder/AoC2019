using System;
using System.IO;
using System.Linq;

namespace Day19._2
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            const int SIZE = 50;

            int[,] grid = new int[SIZE, SIZE];
            int output = 0;

            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    Intcode.CPU cpu = new Intcode.CPU(input);
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

            int aggregate = 0;
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    if (grid[x, y] == 1) aggregate++;
                }
            }

            Console.WriteLine(aggregate);
            Console.ReadKey();
        }
    }
}
