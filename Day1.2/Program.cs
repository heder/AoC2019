using System;
using System.Collections.Generic;
using System.IO;

namespace Day1_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines("in.txt");
            List<int> input = new List<int>();
            int totalFuel = 0;

            foreach (var item in lines)
            {
                input.Add(Convert.ToInt32(item));
            }

            foreach (var mass in input)
            {
                int localFuel = CalcFuel(mass);
                int massAdded = localFuel;

                while (true)
                {
                    massAdded = CalcFuel(massAdded);

                    if (massAdded <= 0)
                    {
                        break;
                    }
                    else
                    {
                        localFuel += massAdded;
                    }
                }

                totalFuel += localFuel;
            }

            Console.WriteLine(totalFuel);
            Console.ReadLine();
        }

        static int CalcFuel(int mass)
        {
            return Convert.ToInt32(Math.Floor((decimal)mass / (decimal)3) - 2);
        }
    }
}
