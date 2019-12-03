﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Day3_1
{
    class Program
    {

        struct pos
        {
            public bool wire1;
            public bool wire2;
        }

        struct coord
        {
            public int x;
            public int y;
        }

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt");

            string[] inputWire1 = input[0].Split(',');
            string[] inputWire2 = input[1].Split(',');

            pos[,] grid = new pos[10000, 10000];

            coord turtle = new coord();

            turtle.x = 5000;
            turtle.y = 5000;

            for (int i = 0; i < inputWire1.Length; i++)
            {
                string direction = inputWire1[i].Substring(1, 1);
                int distance = Convert.ToInt32(inputWire1[i].Substring(1));

                for (int j = 0; j < distance; j++)
                {
                    switch (direction)
                    {
                        case "R":
                            turtle.x++;
                            break;

                        case "L":
                            turtle.x--;
                            break;

                        case "U":
                            turtle.y++;
                            break;

                        case "D":
                            turtle.y--;
                            break;

                        default:
                            break;
                    }

                    grid[turtle.x, turtle.y].wire1 = true;
                }
            }

            for (int i = 0; i < inputWire2.Length; i++)
            {
                string direction = inputWire2[i].Substring(1, 1);
                int distance = Convert.ToInt32(inputWire1[i].Substring(1));

                for (int j = 0; j < distance; j++)
                {
                    switch (direction)
                    {
                        case "R":
                            turtle.x++;
                            break;

                        case "L":
                            turtle.x--;
                            break;

                        case "U":
                            turtle.y++;
                            break;

                        case "D":
                            turtle.y--;
                            break;

                        default:
                            break;
                    }

                    grid[turtle.x, turtle.y].wire2 = true;
                }
            }


            List<coord> intersections = new List<coord>();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 10000; j++)
                {
                    if (grid[i,j].wire1 == true && grid[i,j].wire2 == true)
                    {
                        intersections.Add(new coord() { x = i, y = j });
                    }
                }
            }

            List<int> distance = new List<int>();
            foreach (var item in intersections)
            {

            }


        }
    }
}
