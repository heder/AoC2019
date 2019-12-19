using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Day13._1
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            const int XSIZE = 50;
            const int YSIZE = 50;

            int[,] grid = new int[XSIZE, YSIZE];

            input[0] = 2; // Free to play
            Intcode.CPU cpu = new Intcode.CPU(input);

            int score = 0;
            int ballPos = 0;
            int paddlePos = 0;

            int iterations = 0;
            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                int x = 0;
                int y = 0;
                int c = 0;


                int ii = 0;

                //if (iterations > 1000)
                //{
                //    var joy = Console.ReadKey();


                //    if (joy.KeyChar == 'a')
                //    {
                //        ii = -1;
                //    }
                //    if (joy.KeyChar == 'd')
                //    {
                //        ii = 1;
                //    }
                //    if (joy.KeyChar == ' ')
                //    {
                //        ii = 0;
                //    }
                //}

                if (paddlePos > ballPos)
                {
                    ii = -1;
                }
                if (paddlePos < ballPos)
                {
                    ii = 1;
                }
                if (paddlePos == ballPos)
                {
                    ii = 0;
                }


                cpu.Input = new long[1] { (long)ii };
                cpu.Run();
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    x = (int)cpu.Output;
                }

                cpu.Input = new long[1] { (long)ii };
                cpu.Run();
                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    y = (int)cpu.Output;
                }

                cpu.Input = new long[1] { (long)ii };
                cpu.Run();
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

                    if (c == 4)
                    {
                        ballPos = x;
                    }

                    if (c == 3)
                    {
                        paddlePos = x;
                    }
                }



                //if (iterations > 1000)
                //{
                //    Console.Clear();
                //    for (int i = 0; i < YSIZE; i++)
                //    {
                //        for (int j = 0; j < XSIZE; j++)
                //        {
                //            //Console.Write(grid[i, j]);

                //            switch (grid[i, j])
                //            {
                //                case 0:
                //                    Console.Write(" ");
                //                    //b.SetPixel(j, i, Color.White);
                //                    break;

                //                case 1:
                //                    Console.Write("I");
                //                    //b.SetPixel(j, i, Color.White);
                //                    break;
                //                case 2:
                //                    Console.Write("X");
                //                    //b.SetPixel(j, i, Color.White);
                //                    break;
                //                case 3:
                //                    Console.Write("H");
                //                    //b.SetPixel(j, i, Color.White);
                //                    break;
                //                case 4:
                //                    Console.Write("o");
                //                    //b.SetPixel(j, i, Color.White);
                //                    break;

                //                default:
                //                    break;
                //            }


                //        }
                //        Console.WriteLine();
                        
                //    }

                    Console.WriteLine(score);
                //}

                iterations++;

            }


            Console.ReadKey();
        }
    }
}
