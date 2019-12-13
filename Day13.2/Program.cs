﻿using System;
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

            input[0] = 2; // Free to play
            Intcode.CPU cpu = new Intcode.CPU(input);

            int score = 0;

            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                int x = 0;
                int y = 0;
                int c = 0;

                var joy = Console.ReadKey();
                int ii = 0;

                if (joy.KeyChar == 'a')
                {
                    ii = -1;
                }
                if (joy.KeyChar == 'd')
                {
                    ii = 1;
                }
                if (joy.KeyChar == ' ')
                {
                    ii = 0;
                }

                cpu.Run(new long[1] { (long)ii });
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    x = (int)cpu.Output;
                }

                cpu.Run(new long[1] { (long)ii });
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    y = (int)cpu.Output;
                }

                cpu.Run(new long[1] { (long)ii });
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    c = (int)cpu.Output;
                }

                if (x == -1 && y == 0)
                {
                    score = c;
                }
                else
                {
                    grid[x, y] = c;
                }


                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        Console.WriteLine(grid[i, j]);

                        //switch (grid[i,j])
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


                    }
                }

                Console.WriteLine(score);
            }


            Console.ReadKey();
        }
    }
}
