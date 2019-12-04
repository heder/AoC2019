using System;
using System.Linq;

namespace Day4._1
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
                    //var x = digits.GroupBy(f => f);

                    if (digits[j] == digits[j + 1])
                    {
                        adjacent = true;
                    }

                    if (digits[j + 1] < digits[j])
                    {
                        falling = false;
                        break;
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
