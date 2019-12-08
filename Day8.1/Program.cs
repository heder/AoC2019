using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day8._1
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = File.ReadAllLines("in.txt")[0];

            int[] input = inputString.ToString().ToCharArray().Select(f => (int)Char.GetNumericValue(f)).ToArray();

            const int BPSIZE = 150;
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

            int bpWithFewest0s = -1;
            int lowestSoFar = Int32.MaxValue;
            for (int i = 0; i < noOfBitplanes; i++)
            {
                int count = bitplanes[i].Where(f => f == 0).Count();

                if (count < lowestSoFar)
                {
                    lowestSoFar = count;
                    bpWithFewest0s = i;
                }
            }

            int numberOf1s = bitplanes[bpWithFewest0s].Where(f => f == 1).Count();
            int numberOf2s = bitplanes[bpWithFewest0s].Where(f => f == 2).Count();

            int result = numberOf1s * numberOf2s;

            Console.WriteLine(result);
            Console.ReadKey();
        }
    }
}
