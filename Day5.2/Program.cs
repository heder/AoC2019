using System;
using System.IO;

namespace Day5_2
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

            int pc = 0;
            int opcode;
            int p1mode;
            int p2mode;
            int p3mode;
            int p1val = 0;
            int p2val = 0;

            while (ram[pc] != 99)
            {
                string instruction = ram[pc].ToString().PadLeft(5, '0');

                opcode = Convert.ToInt32(instruction.Substring(3, 2));
                p1mode = Convert.ToInt32(instruction.Substring(2, 1));
                p2mode = Convert.ToInt32(instruction.Substring(1, 1));
                p3mode = Convert.ToInt32(instruction.Substring(0, 1));

                //if (opcode == 1 || opcode == 2 || opcode == 3 || opcode == 4)
                //{
                //    p1val = (p1mode == 0) ? ram[ram[pc + 1]] : ram[pc + 1];
                //}

                if (opcode == 1 || opcode == 2 || opcode == 5 || opcode == 6 || opcode == 7 || opcode == 8)
                {
                    p1val = (p1mode == 0) ? ram[ram[pc + 1]] : ram[pc + 1];
                    p2val = (p2mode == 0) ? ram[ram[pc + 2]] : ram[pc + 2];
                }

                if (opcode == 3 || opcode == 4)
                {
                    p1val = ram[pc + 1]; //(p1mode == 0) ? ram[ram[pc + 1]] : ram[pc + 1];
                }


                switch (opcode)
                {
                    case 1:
                        ram[ram[pc + 3]] = p1val + p2val;
                        pc += 4;
                        break;

                    case 2:
                        ram[ram[pc + 3]] = p1val * p2val;
                        pc += 4;
                        break;

                    case 3:
                        Console.Write($"Input at pc {pc}:");
                        int val = Convert.ToInt32(Console.ReadLine());
                        ram[p1val] = val;
                        pc += 2;
                        break;

                    case 4:
                        Console.Write($"Output at pc {pc}:{ram[p1val]}");
                        Console.WriteLine("");
                        pc += 2;
                        break;

                    case 5:
                        if (p1val != 0)
                        {
                            pc = p2val;
                        }
                        else
                        {
                            pc += 4;
                        }
                        break;

                    case 6:
                        if (p1val == 0)
                        {
                            pc = p2val;
                        }
                        else
                        {
                            pc += 4;
                        }
                        break;

                    case 7:
                        if (p1val < p2val)
                        {
                            ram[ram[pc + 3]] = 1;
                        }
                        else
                        {
                            ram[ram[pc + 3]] = 0;
                        }
                        pc += 4;
                        break;

                    case 8:
                        if (p1val == p2val)
                        {
                            ram[ram[pc + 3]] = 1;
                        }
                        else
                        {
                            ram[ram[pc + 3]] = 0;
                        }
                        pc += 4;
                        break;

                    default:
                        Console.WriteLine($"Invalid instruction {instruction} at pc {pc}");
                        Console.ReadKey();
                        break;
                }


            }

            //Console.WriteLine(ram[0]);
            Console.ReadKey();
        }
    }
}
