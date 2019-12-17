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

            input[0] = 2; // Free to play
            Intcode.CPU cpu = new Intcode.CPU(input);

            int score = 0;
            int ballPos = 0;
            int paddlePos = 0;

            int iterations = 0;
            int output = 0;

            int x = 0;
            int y = 0;


            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                cpu.Run(new long[] { 1 });
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

            Console.ReadKey();

            int aggregate = 0;
            for (int i = 1; i < SIZE; i++)
            {
                for (int j = 1; j < SIZE; j++)
                {
                    // Check if "#" is surronded by "#"
                    if (grid[i, j] == 35 && grid[i - 1, j] == 35 && grid[i + 1, j] == 35 && grid[i, j - 1] == 35 && grid[i, j + 1] == 35)
                    {
                        aggregate += i * j;
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
