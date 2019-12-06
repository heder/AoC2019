using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day6_1
{
    class Program
    {
        class CelestialObject
        {
            public string Name { get; set; }
            public CelestialObject Orbits { get; set; }
            public CelestialObject OrbitedBy { get; set; }
        }

        

        static void Main(string[] args)
        {
            List<CelestialObject> objectList = new List<CelestialObject>();

            string[] input = File.ReadAllLines("in.txt"); //[0].Split(',');

            foreach (var item in input)
            {
                var x = item.Split(')');

                var a = x[0];
                var b = x[1];

                var aExists = objectList.FirstOrDefault(f => f.Name == a);
                var bExists = objectList.FirstOrDefault(f => f.Name == b);

                if (aExists == null)
                {
                    aExists = new CelestialObject() { Name = a };
                    objectList.Add(aExists);
                }

                if (bExists == null)
                {
                    objectList.Add(new CelestialObject() { Name = b, Orbits = aExists });
                }
                else
                {
                    bExists.Orbits = aExists;
                }

                aExists.OrbitedBy = bExists;


            }

            int totalOrbits = 0;
            foreach (var item in objectList)
            {
                var start = item;

                while (start.Orbits != null)
                {
                    totalOrbits++;
                    start = start.Orbits;
                }

            }

            Console.WriteLine($"Total orbits: {totalOrbits}");
            Console.ReadKey();

        }
    }
}
