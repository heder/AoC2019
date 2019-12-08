using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7._2
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt")[0].Split(',');

            int[] ram = new int[1000];
            for (int i = 0; i < input.Length; i++)
            {
                ram[i] = Convert.ToInt32(input[i]);
            }

            CPU[] processors = new CPU[5];

            int[] phaseArray = new int[5];

            List<string> permutations = new List<string>();
            GenerateHeapPermutations(5, new[] { '5', '6', '7', '8', '9' }, permutations);

            int highest = 0;
            int[] highestPhaseArray = new int[5];

            for (int i = 0; i < permutations.Count; i++)
            {
                phaseArray = permutations[i].ToString().ToCharArray().Select(f => (int)Char.GetNumericValue(f)).ToArray();

                processors[0] = new CPU(ram);
                processors[1] = new CPU(ram);
                processors[2] = new CPU(ram);
                processors[3] = new CPU(ram);
                processors[4] = new CPU(ram);

                int output = 0;
                bool running = true;

                int loopCounter = 0;
                while (running == true)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (loopCounter == 0)
                        {
                            int[] toCpu = new int[2];
                            toCpu[0] = phaseArray[j];
                            toCpu[1] = output;

                            processors[j].Run(toCpu);
                        }
                        else
                        {
                            int[] toCpu = new int[1];
                            toCpu[0] = output;

                            processors[j].Run(toCpu);
                        }

                        if (processors[j].State == CpuState.OUTPUT_READY)
                        {
                            output = processors[j].Output;
                        }

                        if (processors[j].State == CpuState.STOPPED && j == 4)
                        {
                            if (output > highest)
                            {
                                highest = output;
                                phaseArray.CopyTo(highestPhaseArray, 0);
                            }

                            running = false;
                        }
                    }

                    loopCounter++;
                }
            }


            Console.ReadKey();
        }



        public static void GenerateHeapPermutations(int n, char[] charArray, List<string> sList)
        {
            if (n == 1)
            {
                sList.Add(new string(charArray));
            }
            else
            {
                for (int i = 0; i < n - 1; i++)
                {
                    GenerateHeapPermutations(n - 1, charArray, sList);

                    int indexToSwapWithLast = (n % 2 == 0 ? i : 0);
                    var temp = charArray[indexToSwapWithLast];
                    charArray[indexToSwapWithLast] = charArray[n - 1];
                    charArray[n - 1] = temp;
                }

                GenerateHeapPermutations(n - 1, charArray, sList);
            }
        }
    }

    public enum CpuState
    {
        INIT = 0,
        RUNNING = 1,
        OUTPUT_READY = 2,
        STOPPED = 3
    }

    public class CPU
    {
        public int Output { get; set; }
        public CpuState State { get; set; }


        public CPU(int[] ram_in)
        {
            ram_in.CopyTo(ram, 0);
            this.pc = 0;
            this.State = CpuState.INIT;
        }

        private int[] ram = new int[1000];
        public int pc { get; set; }

        public void Run(int[] input)
        {
            this.State = CpuState.RUNNING;

            int opcode;
            int p1mode;
            int p2mode;
            int p3mode;
            int p1addr;
            int p2addr;
            int p3addr;

            int inputPos = 0;

            while (this.State == CpuState.RUNNING)
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
                        Console.WriteLine($"Input at pc {pc}:{input[inputPos]}");
                        int val = input[inputPos];
                        inputPos++;
                        ram[p1addr] = val;
                        pc += 2;
                        break;

                    case 4:
                        Console.WriteLine($"Output at pc {pc}:{ram[p1addr]}");
                        this.Output = ram[p1addr];
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
