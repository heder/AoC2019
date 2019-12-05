using System;
using System.IO;

namespace Day5_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt")[0].Split(',');

            int[] ram = new int[20000];
            for (int i = 0; i < input.Length; i++)
            {
                ram[i] = Convert.ToInt32(input[i]);
            }

            int pc = 0;
            int opcode;
            int p1mode;
            int p2mode;
            int p3mode;
            int p1addr;
            int p2addr;
            int p3addr;

            while (ram[pc] != 99)
            {
                string instruction = ram[pc].ToString().PadLeft(5, '0');

                opcode = Convert.ToInt32(instruction.Substring(3, 2));
                p1mode = Convert.ToInt32(instruction.Substring(2, 1));
                p2mode = Convert.ToInt32(instruction.Substring(1, 1));
                p3mode = Convert.ToInt32(instruction.Substring(0, 1));

                p1addr = (p1mode == 0) ? ram[pc + 1] : pc + 1;
                p2addr = (p2mode == 0) ? ram[pc + 2] : pc + 2;
                p3addr = (p3mode == 0) ? ram[pc + 3] : pc + 3;

                switch (opcode)
                {
                    case 1:
                        ram[p3addr] = ram[p1addr] + ram[p2addr];
                        pc += 4;
                        break;

                    case 2:
                        ram[p3addr] = ram[p1addr] * ram[p2addr];
                        pc += 4;
                        break;

                    case 3:
                        Console.Write($"Input at pc {pc}:");
                        int val = Convert.ToInt32(Console.ReadLine());
                        ram[p1addr] = val;
                        pc += 2;
                        break;

                    case 4:
                        Console.Write($"Output at pc {pc}:{ram[p1addr]}");
                        Console.WriteLine("");
                        pc += 2;
                        break;

                    case 5:
                        if (ram[p1addr] != 0)
                        {
                            pc = ram[p2addr];
                        }
                        else
                        {
                            pc += 3;
                        }
                        break;

                    case 6:
                        if (ram[p1addr] == 0)
                        {
                            pc = ram[p2addr];
                        }
                        else
                        {
                            pc += 3;
                        }
                        break;

                    case 7:
                        if (ram[p1addr] < ram[p2addr])
                        {
                            ram[p3addr] = 1;
                        }
                        else
                        {
                            ram[p3addr] = 0;
                        }
                        pc += 4;
                        break;

                    case 8:
                        if (ram[p1addr] == ram[p2addr])
                        {
                            ram[p3addr] = 1;
                        }
                        else
                        {
                            ram[p3addr] = 0;
                        }
                        pc += 4;
                        break;

                    default:
                        Console.WriteLine($"Invalid instruction {instruction} at pc {pc}");
                        Console.ReadKey();
                        break;
                }
            }

            Console.ReadKey();
        }
    }
}
