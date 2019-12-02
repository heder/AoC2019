using System;
using System.IO;

namespace Day2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt")[0].Split(',');

            int[] ram = new int[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                ram[i] = Convert.ToInt32(input[i]);
            }

            int[] originalRam = (int[])ram.Clone();

            for (int i = 0; i < 145; i++)
            {
                for (int j = 0; j < 145; j++)
                {
                    ram = (int[])originalRam.Clone();

                    ram[1] = i;
                    ram[2] = j;

                    int pc = 0;
                    while (ram[pc] != 99)
                    {
                        switch (ram[pc])
                        {
                            case 1:
                                ram[ram[pc + 3]] = ram[ram[pc + 1]] + ram[ram[pc + 2]];
                                break;

                            case 2:
                                ram[ram[pc + 3]] = ram[ram[pc + 1]] * ram[ram[pc + 2]];
                                break;

                            default:
                                break;
                        }

                        pc += 4;
                    }

                    if (ram[0] == 19690720)
                    {
                        Console.WriteLine($"{i},{j}");
                        Console.ReadKey();
                    }

                }

            }
        }
    }
}