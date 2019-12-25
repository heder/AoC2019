using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day21._1
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            Intcode.CPU cpu = new Intcode.CPU(input);
            int output = 0;

            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                byte[] command;

                cpu.Run();

                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    output = (int)cpu.Output.Dequeue();
                }

                if (cpu.State == Intcode.CpuState.WAITING_FOR_INPUT)
                {
                    command = ASCIIEncoding.ASCII.GetBytes(Console.ReadLine() + '\n');

                    var i = command.Select(f => (long)f).ToArray();
                    foreach (var item in i)
                    {
                        cpu.Input.Enqueue(item);
                    }
                }

                if (output > 255)
                {
                    Console.ReadKey();
                }

                switch (output)
                {
                    case 10:
                        Console.WriteLine();
                        break;

                    default:
                        Console.Write(Encoding.ASCII.GetChars(new byte[] { (byte)output }));
                        break;
                }
            }
        }
    }
}
