using System;
using System.IO;

namespace IntcodeDISASM
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt")[0].Split(',');

            System.Int64[] ram = new System.Int64[input.Length + 3];

            for (int i = 0; i < input.Length; i++)
            {
                ram[i] = Convert.ToInt64(input[i]);
            }

            string disassembly = "";
            int pc = 0;
            while (pc < input.Length) //; pc += 0)
            {
                ram[pc] = Convert.ToInt64(input[pc]);

                System.Int64 opcode;
                System.Int64 p1mode;
                System.Int64 p2mode;
                System.Int64 p3mode;
                System.Int64 p1val;
                System.Int64 p2val;
                System.Int64 p3val;

                string p1mnemo = "";
                string p2mnemo = "";
                string p3mnemo = "";

                string instruction = ram[pc].ToString().PadLeft(5, '0');

                opcode = Convert.ToInt32(instruction.Substring(3, 2));
                p1mode = Convert.ToInt32(instruction.Substring(2, 1));
                p2mode = Convert.ToInt32(instruction.Substring(1, 1));
                p3mode = Convert.ToInt32(instruction.Substring(0, 1));

                System.Int64 p1code = ram[pc + 1];
                System.Int64 p2code = ram[pc + 2];
                System.Int64 p3code = ram[pc + 3];

                switch (p1mode)
                {
                    case 0:
                        // Positional
                        p1mnemo = "$" + p1code;
                        break;

                    case 1:
                        // Immediate
                        p1mnemo = "#" + p1code;
                        break;

                    case 2:
                        // Relative
                        p1mnemo = "%" + p1code;
                        break;
                }


                switch (p2mode)
                {
                    case 0:
                        // Positional
                        p2mnemo = "$" + p2code;
                        break;

                    case 1:
                        // Immediate
                        p2mnemo = "#" + p2code;
                        break;

                    case 2:
                        // Relative
                        p2mnemo = "%" + p2code;
                        break;
                }


                switch (p3mode)
                {
                    case 0:
                        // Positional
                        p3mnemo = "$" + p3code;
                        break;

                    case 1:
                        // Immediate
                        p3mnemo = "#" + p3code;
                        break;

                    case 2:
                        // Relative
                        p3mnemo = "%" + p3code;
                        break;
                }

                // ...

                disassembly += $" {pc:D4} : ";

                switch (opcode)
                {
                    case 1:
                        
                        disassembly += "ADD ";
                        disassembly += p1mnemo + ", ";
                        disassembly += p2mnemo + ", ";
                        disassembly += p3mnemo;
                        disassembly += Environment.NewLine;
                        pc += 4;
                        break;

                    case 2:
                        disassembly += "MUL ";
                        disassembly += p1mnemo + ", ";
                        disassembly += p2mnemo + ", ";
                        disassembly += p3mnemo;
                        disassembly += Environment.NewLine;
                        pc += 4;
                        break;

                    case 3:
                        disassembly += "INP ";
                        disassembly += p1mnemo;
                        disassembly += Environment.NewLine;
                        pc += 2;
                        break;

                    case 4:
                        disassembly += "OUT ";
                        disassembly += p1mnemo;
                        disassembly += Environment.NewLine;
                        pc += 2;
                        break;

                    case 5:
                        disassembly += "JMPNOTZ ";
                        disassembly += p1mnemo + ", ";
                        disassembly += p2mnemo;
                        disassembly += Environment.NewLine;
                        pc += 3;
                        break;

                    case 6:
                        disassembly += "JMPIFZ ";
                        disassembly += p1mnemo + ", ";
                        disassembly += p2mnemo;
                        disassembly += Environment.NewLine;
                        pc += 3;
                        break;

                    case 7:
                        disassembly += "IFLESS ";
                        disassembly += p1mnemo + ", ";
                        disassembly += p2mnemo + ", ";
                        disassembly += p3mnemo;
                        disassembly += Environment.NewLine;
                        pc += 4;
                        break;

                    case 8:
                        disassembly += "IFEQ ";
                        disassembly += p1mnemo + ", ";
                        disassembly += p2mnemo + ", ";
                        disassembly += p3mnemo;
                        disassembly += Environment.NewLine;
                        pc += 4;
                        break;


                    case 9:
                        disassembly += "CHBASE ";
                        disassembly += p1mnemo;
                        disassembly += Environment.NewLine;
                        pc += 2;
                        break;

                    case 99:
                        disassembly += "END";
                        disassembly += Environment.NewLine;
                        pc += 1;
                        break;

                    default:
                        Console.WriteLine($"Invalid instruction {instruction} at pc {pc}");
                        disassembly += "X";
                        disassembly += Environment.NewLine;
                        pc++;
                        break;
                }
            }

            Console.Write(disassembly);

        }
    }
}
