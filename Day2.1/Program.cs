using System;
using System.IO;

namespace Day2.1
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

            ram[1] = 12;
            ram[2] = 2;

            int pc = 0;

            while(ram[pc] != 99)
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

            Console.WriteLine(ram[0]);
            Console.ReadKey();
        }
    }
}
