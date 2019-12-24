using System;
using System.IO;
using System.Linq;

namespace Day23._1
{
    class Program
    {
        static void Main(string[] args)
        {
            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();

            int NODES = 50;

            bool[] booted = new bool[NODES];

            Intcode.CPU[] nodes = new Intcode.CPU[50];

            for (int i = 0; i < NODES; i++)
            {
                nodes[i] = new Intcode.CPU(input);
                nodes[i].Input.Enqueue(i);
            }

            long NATX = 0;
            long NATY = 0;
            long lastNatSentY = -1;

            while (true)
            {
                for (int i = 0; i < NODES; i++)
                {
                    //Console.WriteLine($"Running node {i}");

                    if (nodes[i].InputBuffer.Count == 0)
                    {
                        nodes[i].InputBuffer.Enqueue(-1);
                    }

                    while (nodes[i].InputBuffer.Count > 0)
                    {
                        if (nodes[i].InputBuffer.Peek() == -1)
                        {
                            nodes[i].Input.Enqueue(nodes[i].InputBuffer.Dequeue());
                        }
                        else
                        {
                            nodes[i].Input.Enqueue(nodes[i].InputBuffer.Dequeue());
                            nodes[i].Input.Enqueue(nodes[i].InputBuffer.Dequeue());
                        }

                        nodes[i].Run();

                        while (nodes[i].State != Intcode.CpuState.WAITING_FOR_INPUT)
                        {
                            if (nodes[i].State == Intcode.CpuState.OUTPUT_READY)
                            {
                                // Receive address
                                long address = nodes[i].Output.Dequeue();

                                nodes[i].Run();
                                long x = nodes[i].Output.Dequeue();

                                nodes[i].Run();
                                long y = nodes[i].Output.Dequeue();

                                Console.WriteLine($"Node {i} sending {address},{x},{y}");

                                if (address == 255)
                                {
                                    NATX = x;
                                    NATY = y;
                                }
                                else
                                {
                                    nodes[address].InputBuffer.Enqueue(x);
                                    nodes[address].InputBuffer.Enqueue(y);
                                }

                                nodes[i].Run();
                            }
                        }

                        bool isIdle = true;

                        // Check if network is idle
                        for (int z = 0; z < NODES; z++)
                        {
                            if (nodes[z].InputBuffer.Count > 0)
                            {
                                isIdle = false;
                                break;
                            }
                        }

                        if (isIdle == true)
                        {
                            if (NATY == lastNatSentY)
                            {
                                Console.WriteLine(NATY);
                                Console.ReadKey();
                            }

                            nodes[0].InputBuffer.Enqueue(NATX);
                            nodes[0].InputBuffer.Enqueue(NATY);

                            lastNatSentY = NATY;
                        }
                    }
                }
            }
        }
    }
}
