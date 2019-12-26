using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Day25._1
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            Intcode.CPU cpu = new Intcode.CPU(input);

            int output = 0;
            bool trialmode = false;
            byte trial = 0;

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
                    if (trialmode == true)
                    {
                        string drop = "drop fuel cell\ndrop cake\ndrop pointer\ndrop boulder\ndrop mutex\ndrop antenna\ndrop tambourine\ndrop coin\n";
                        string take = "";

                        if (IsBitSet(trial, 0)) take += "take fuel cell\n";
                        if (IsBitSet(trial, 1)) take += "take cake\n";
                        if (IsBitSet(trial, 2)) take += "take pointer\n";
                        if (IsBitSet(trial, 3)) take += "take boulder\n";
                        if (IsBitSet(trial, 4)) take += "take mutex\n";
                        if (IsBitSet(trial, 5)) take += "take antenna\n";
                        if (IsBitSet(trial, 6)) take += "take tambourine\n";
                        if (IsBitSet(trial, 7)) take += "take coin\n";

                        string c = drop + take + "east\n";

                        command = ASCIIEncoding.ASCII.GetBytes(c);
                        Console.WriteLine(trial);
                        trial++;
                    }
                    else
                    {
                        command = ASCIIEncoding.ASCII.GetBytes(Console.ReadLine() + '\n');

                        if (command[0] == 97)
                        {
                            trialmode = true;
                        }
                    }

                    var i = command.Select(f => (long)f).ToArray();
                    foreach (var item in i)
                    {
                        cpu.Input.Enqueue(item);
                    }
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

        static bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }
    }
}



//west
//west
//west
//take coin
//east
//east
//east
//north
//north
//take mutex
//east
//take antenna
//west
//south
//east
//take cake
//east
//north
//take pointer
//south
//west
//west
//south
//east
//east
//take tambourine
//east
//take fuel cell
//east
//take boulder
//north
