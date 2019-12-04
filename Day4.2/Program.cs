using System;
using System.Linq;

namespace Day4._2
{
    class Program
    {
        static void Main(string[] args)
        {
            int from = 165432;
            int to = 707912;

            int[] digits;

            bool adjacent;
            bool falling;
            int count = 0;

            for (int i = from; i <= to; i++)
            {
                digits = i.ToString().ToCharArray().Select(f => (int)Char.GetNumericValue(f)).ToArray();

                adjacent = false;
                falling = true;

                for (int j = 0; j < 5; j++)
                {
                    if (adjacent == false) // Single 2-group already found
                    {
                        if (digits[j] == digits[j + 1])
                        {
                            adjacent = true;
                        }

                        if (j < 4 && digits[j] == digits[j + 2])
                        {
                            adjacent = false;
                        }

                        if (j > 0 && digits[j] == digits[j - 1])
                        {
                            adjacent = false;
                        }
                    }

                    if (digits[j + 1] < digits[j])
                    {
                        falling = false;
                    }
                }

                if (adjacent == true && falling == true)
                {
                    count++;
                }
            }

            Console.WriteLine(count);
            Console.ReadKey();
        }
    }
}
