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
            int numberFound = 0;

            for (int i = from; i <= to; i++)
            {
                digits = i.ToString().ToCharArray().Select(f => (int)Char.GetNumericValue(f)).ToArray();

                adjacent = false;
                falling = true;

                for (int j = 0; j < 5; j++)
                {
                    //digits[0] = 1;
                    //digits[1] = 2;
                    //digits[2] = 3;
                    //digits[3] = 4;
                    //digits[4] = 4;
                    //digits[5] = 4;

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

                    //int groupsize = 0;

                    //// Check group size
                    //for (int k = j; k < 6; k++)
                    //{
                    //    if (digits[k] == digits[j])
                    //    {
                    //        groupsize++;
                    //    }
                    //}

                    //// Ugly left check
                    //if (j > 0 && groupsize == 2 && digits[j - 1] == digits[j])
                    //{
                    //    adjacent = false;
                    //}
                    //else if (groupsize > 2)
                    //{
                    //    adjacent = false;
                    //} 
                    //else
                    //{
                    //    adjacent = true;
                    //}
                    //}

                    if (digits[j + 1] < digits[j])
                    {
                        falling = false;
                    }
                }

                if (adjacent == true && falling == true)
                {
                    numberFound++;
                }
            }

            Console.WriteLine(numberFound);
            Console.ReadKey();
        }
    }
}
