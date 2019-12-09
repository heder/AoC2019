using System;
using System.IO;

namespace Day9._2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt")[0].Split(',');

            System.Int64[] ram = new System.Int64[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                ram[i] = Convert.ToInt64(input[i]);
            }

            var cpu = new Intcode.CPU(ram);

            System.Int64 inp = Convert.ToInt64(Console.ReadLine());

            Int64[] inArray = new Int64[1];
            inArray[0] = inp;
            cpu.Run(inArray);

            if (cpu.State == Intcode.CpuState.OUTPUT_READY)
            {
                var o = cpu.Output;
            }

            Console.ReadKey();
        }
    }
}
