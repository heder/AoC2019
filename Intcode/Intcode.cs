using System;

namespace Intcode
{

    public enum CpuState
    {
        INIT = 0,
        RUNNING = 1,
        OUTPUT_READY = 2,
        STOPPED = 3
    }

    public class CPU
    {
        public System.Int64 Output { get; set; }
        public CpuState State { get; set; }


        public CPU(System.Int64[] ram_in)
        {
            ram_in.CopyTo(ram, 0);
            this.pc = 0;
            this.State = CpuState.INIT;
        }

        private System.Int64[] ram = new System.Int64[20000];
        public System.Int64 pc { get; set; }

        public void Run(System.Int64[] input)
        {
            this.State = CpuState.RUNNING;

            System.Int64 opcode;
            System.Int64 p1mode;
            System.Int64 p2mode;
            System.Int64 p3mode;
            System.Int64 p1addr = 0;
            System.Int64 p2addr = 0;
            System.Int64 p3addr = 0;

            System.Int64 inputPos = 0;

            System.Int64 relativeBase = 0;

            while (this.State == CpuState.RUNNING)
            {
                string instruction = ram[pc].ToString().PadLeft(5, '0');

                opcode = Convert.ToInt64(instruction.Substring(3, 2));
                p1mode = Convert.ToInt64(instruction.Substring(2, 1));
                p2mode = Convert.ToInt64(instruction.Substring(1, 1));
                p3mode = Convert.ToInt64(instruction.Substring(0, 1));

                switch (p1mode)
                {
                    case 0:
                        p1addr = ram[pc + 1];
                        break;

                    case 1:
                        p1addr = pc + 1;
                        break;

                    case 2:
                        p1addr = relativeBase + ram[pc + 1];
                        break;
                }


                switch (p2mode)
                {
                    case 0:
                        p2addr = ram[pc + 2];
                        break;

                    case 1:
                        p2addr = pc + 2;
                        break;

                    case 2:
                        p2addr = relativeBase + ram[pc + 2];
                        break;
                }


                switch (p3mode)
                {
                    case 0:
                        p3addr = ram[pc + 3];
                        break;

                    case 1:
                        p3addr = pc + 3;
                        break;

                    case 2:
                        p3addr = relativeBase + ram[pc + 3];
                        break;
                }


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
                        Console.WriteLine($"Input at pc {pc}:{input[inputPos]}");
                        System.Int64 val = input[inputPos];
                        inputPos++;
                        ram[p1addr] = val;
                        pc += 2;
                        break;

                    case 4:
                        Console.WriteLine($"Output at pc {pc}:{ram[p1addr]}");
                        this.Output = ram[p1addr];
                        Console.WriteLine(this.Output);
                        this.State = CpuState.OUTPUT_READY;
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


                    case 9:
                        relativeBase += ram[p1addr];
                        pc += 2;
                        break;

                    case 99:
                        this.State = CpuState.STOPPED;
                        break;

                    default:
                        Console.WriteLine($"Invalid instruction {instruction} at pc {pc}");
                        Console.ReadKey();
                        break;
                }
            }

            return;
        }
    }
}
