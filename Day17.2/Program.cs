using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day17._2
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            input[0] = 2;
            Intcode.CPU cpu = new Intcode.CPU(input);

            int output = 0;

            while (cpu.State != Intcode.CpuState.STOPPED)
            {
                cpu.Run();

                if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                {
                    output = (int)cpu.Output;
                }

                if (output > 255)
                {
                    Console.WriteLine(output);
                    Console.ReadKey();
                }

                switch (output)
                {
                    case 58:
                    case 63:
                        Console.Write(Encoding.ASCII.GetChars(new byte[] { (byte)output }));
                        var command = ASCIIEncoding.ASCII.GetBytes(Console.ReadLine() + '\n');

                        var i = command.Select(f => (long)f).ToArray();
                        cpu.Input = i;

                        //x = 0;
                        //y = 0;

                        break;


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
