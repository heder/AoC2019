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

            int[] data = new int[input.Length - offset];

            // Throw away data before offset
            for (int i = 0; i < data.Length; ++i)
            {
                data[i] = input[offset + i];
            }

            // Multiply pattern is statically 1 in leftover array
            for (int p = 0; p < 100; p++)
            {
                for (int i = data.Length - 2; i >= 0; i--)
                {
                    data[i] = (data[i] + data[i + 1]) % 10;
                }
            }

            int[] result = new int[input.Length];
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
