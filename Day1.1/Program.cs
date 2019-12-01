using System;
using System.IO;
using System.Linq;

namespace Day1_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("in.txt").ToList();
            int totalFuel = lines.Sum(f => CalcFuel(Convert.ToInt32(f)));

            Console.WriteLine(totalFuel);
            Console.ReadLine();
        }

        static int CalcFuel(int mass)
        {
            return Convert.ToInt32(Math.Floor((decimal)mass / 3m) - 2);
        }
    }
}
