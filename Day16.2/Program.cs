using System;
using System.IO;
using System.Linq;

namespace Day16._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int offset = Convert.ToInt32(File.ReadAllLines("in.txt")[0].Substring(0, 7));

            int[] input1 = File.ReadAllLines("in.txt")[0].ToCharArray().Select(f => (int)Char.GetNumericValue(f)).ToArray();

            int[] input = new int[input1.Length * 10000];
            for (int i = 0; i < 10000; i++)
            {
                input1.CopyTo(input, i * input1.Length);
            }







            int[] result = new int[input.Length];

            for (int i = 0; i < PHASES; i++)
            {
                Console.WriteLine($"Phase {i}");

                // Row in phase
                for (int j = 0; j < input.Length; j++)
                {
                    if (j % 1000 == 0)
                    {
                        Console.WriteLine($"Phase: {i} Phaserow {j}");
                    }

                    // Generate pattern
                    // Construct pattern
                    patternIterator = 0;
                    currentDigit = -1;

                    for (int p = 0; p < patternIterator + 1; p++)
                    {
                        if (patternIterator == input.Length + 1)
                        {
                            break;
                        }

                        // Take pattern digit p2 times
                        int d = GetNextPatternDigit();
                        for (int p2 = 0; p2 < i + 1; p2++)
                        {
                            if (patternIterator == input.Length + 1)
                            {
                                break;
                            }
                            pattern[patternIterator] = d;
                            patternIterator++;
                        }
                    }

                    //int aggregate = 0;
                    //for (int k = 0; k < input.Length; k++)
                    //{
                    //    // Multiplications
                    //    aggregate += input[k] * pattern[k + 1];

                    //}
                    //result[j] += Math.Abs(aggregate % 10);
                }

                result.CopyTo(input, 0);
            }


            for (int i = offset; i < 8; i++)
            {
                Console.Write(input[i]);
            }


            Console.ReadKey();
        }

        public static int[] basePattern = new int[4] { 0, 1, 0, -1 };
        public static int currentDigit = -1;

        public static int GetNextPatternDigit()
        {
            if (currentDigit == 3) currentDigit = 0; else currentDigit++;
            return basePattern[currentDigit];
        }
    }
}
