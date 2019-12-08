using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = File.ReadAllLines("in.txt")[0];
            int[] input = inputString.ToString().ToCharArray().Select(f => (int)Char.GetNumericValue(f)).ToArray();

            const int STRIDE = 25;
            const int ROWS = 6;
            const int BPSIZE = STRIDE * ROWS;

            // Amiga-style bitplanes!!! Yeeey! :-)

            List<int[]> bitplanes = new List<int[]>();
            int noOfBitplanes = input.Length / BPSIZE;

            int pos = 0;
            for (int i = 0; i < noOfBitplanes; i++)
            {
                int[] bitplane = new int[BPSIZE];
                for (int j = 0; j < BPSIZE; j++)
                {
                    bitplane[j] = input[pos];
                    pos++;
                }

                bitplanes.Add(bitplane);
            }

            // Loop bitplanes.
            int[] image = new int[BPSIZE];
            for (int i = 0; i < BPSIZE; i++)
            {
                for (int j = 0; j < noOfBitplanes; j++)
                {
                    if (bitplanes[j][i] == 2)
                    {
                        // Transparent pixel.
                        image[i] = bitplanes[j][i];
                    }
                    else
                    {
                        // Non-transparent. Save color.
                        image[i] = bitplanes[j][i];
                        break;
                    }
                }
            }

            // Dump some ASCII-Art
            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < STRIDE; j++)
                {
                    switch (image[((i * STRIDE) + j)])
                    {
                        case 0:
                            Console.Write(" ");
                            break;

                        case 1:
                            Console.Write("o");
                            break;
                        case 2:
                            Console.Write(".");
                            break;

                        default:
                            break;
                    }
                }

                Console.Write(Environment.NewLine);
            }

            Console.ReadKey();
        }
    }
}
