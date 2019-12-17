using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day17._1
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            const int SIZE = 100;

            int[,] grid = new int[SIZE, SIZE];

            Intcode.CPU cpu = new Intcode.CPU(input);
            int output = 0;

            int x = 0;
            int y = 0;

            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                cpu.input = new long[] { 1 };
                cpu.Run();
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    output = (int)cpu.Output;
                }

                grid[x, y] = output;

                switch (output)
                {
                    case 10:
                        Console.WriteLine();
                        y++;
                        x = 0;
                        break;

                    default:
                        Console.Write(Encoding.ASCII.GetChars(new byte[] { (byte)output }));
                        x++;
                        break;
                }
            }

            int aggregate = 0;
            for (int i = 1; i < SIZE; i++)
            {
                for (int j = 1; j < SIZE; j++)
                {
                    // Check if "#" is surrounded by "#"
                    if (grid[i, j] == 35 && grid[i - 1, j] == 35 && grid[i + 1, j] == 35 && grid[i, j - 1] == 35 && grid[i, j + 1] == 35)
                    {
                        aggregate += i * j;
                    }
                }
            }

            Console.WriteLine(aggregate);
            Console.ReadKey();
        }
    }
}
