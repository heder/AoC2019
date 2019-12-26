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

// Part 1
// (!A || !B || !C) && D
// "Om det finns ett hål i antingen A, B, eller C och D är safe -> hoppa"

// NOT A T  - Hål i A -> Hoppa
// NOT B J  - Hål i B -> Temp
// OR T J   - Hål i antingen T eller J -> Hoppa
// NOT C T  - Hål i C -> Temp
// OR T J   - Hål i T (C) eller J (A or B) -> Hoppa
// AND D J  - D måste alltid vara safe

// Part 2
// (!A || !B || !C) && D && (E || H)
// "Hoppa om det finns ett hål i A, B eller C, men endast om vi kan landa på D samt att vi tar oss vidare genom att gå eller hoppa.
// NOT A T - Hål i A -> J
// NOT B J - Hål i B -> T
// OR T J - Hål i antingen T eller J
// NOT C T - Hål i C -> T
// OR T J - Hål i antingen T eller J
// AND D J - Vi kan landa på D
// NOT H T - Ladda Hs inversstatus för direkthopp
// NOT T T - Invertera H status i T (kolla om säker)
// OR E T - Ladda E (gå vidare)
// AND T J - Hål i antingen A,B,C + D säker samt at vi tar oss vidare.

