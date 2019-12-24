using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Intcode
{
    public enum CpuState
    {
        INIT = 0,
        RUNNING = 1,
        OUTPUT_READY = 2,
        STOPPED = 3,
        WAITING_FOR_INPUT = 4
    }

    public class CPU
    {
        public Queue<long> InputBuffer = new Queue<long>();

        public Queue<long> Output = new Queue<long>();
        public CpuState State { get; set; }
        private long[] ram = new long[5000];
        public long pc { get; set; }
        private long relativeBase = 0;

        public Queue<long> Input = new Queue<long>();

        public CPU(long[] ram_in)
        {
            ram_in.CopyTo(ram, 0);
            pc = 0;
            State = CpuState.INIT;
        }


        public ParameterizedThreadStart Run()
        {
            Trace.WriteLine("Running");

            State = CpuState.RUNNING;

            long opcode;
            long p1mode;
            long p2mode;
            long p3mode;
            long p1addr = 0;
            long p2addr = 0;
            long p3addr = 0;

            while (State == CpuState.RUNNING)
            {
                opcode = ram[pc] % 100;
                p1mode = ram[pc] / 100 % 10;
                p2mode = ram[pc] / 1000 % 10;
                p3mode = ram[pc] / 10000 % 10;

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
                    case 1: // ADD
                        ram[p3addr] = ram[p1addr] + ram[p2addr];
                        pc += 4;
                        break;

                    case 2: // MUL
                        ram[p3addr] = ram[p1addr] * ram[p2addr];
                        pc += 4;
                        break;

                    case 3: // INP

                        //Console.WriteLine($"Input at pc {pc}");
                        if (Input.Count == 0)
                        {
                            State = CpuState.WAITING_FOR_INPUT;
                            break;
                        }


                        ram[p1addr] = Input.Dequeue(); // _input[inputPos];
                        pc += 2;
                        break;

                    case 4: // OUT
                        //Console.WriteLine($"Output at pc {pc}:{ram[p1addr]}");
                        Output.Enqueue(ram[p1addr]);
                        State = CpuState.OUTPUT_READY;
                        pc += 2;
                        break;

                    case 5: // JMP if not Z
                        if (ram[p1addr] != 0)
                        {
                            pc = ram[p2addr];
                        }
                        else
                        {
                            pc += 3;
                        }
                        break;

                    case 6: // JMP if Z
                        if (ram[p1addr] == 0)
                        {
                            pc = ram[p2addr];
                        }
                        else
                        {
                            pc += 3;
                        }
                        break;

                    case 7: // IFLESS
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

                    case 8: // IFEQ
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


                    case 9: // MOVEBASE
                        relativeBase += ram[p1addr];
                        pc += 2;
                        break;

                    case 99:
                        State = CpuState.STOPPED;
                        break;

                    default:
                        Console.WriteLine($"Invalid instruction {ram[pc]} at pc {pc}");
                        Console.ReadKey();
                        break;
                }
            }

            return null;
        }
    }
}
