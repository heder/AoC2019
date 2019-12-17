using Day15._1;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15._1
{
    public enum TileType
    {
        START_POS = 0,
        EMPTY = 1,
        WALL = 2,
        O2_SENSOR = 3,
        UNKNOWN = 4
    }

    public enum Direction
    {
        N = 1,
        S = 2,
        W = 3,
        E = 4
    }

    public class Tile
    {
        public Tile()
        {
            Type = TileType.UNKNOWN;
        }

        public TileType Type { get; set; }

        public Direction DirectionBack { get; set; }
        public bool Visited { get; set; }

        public int DistanceFromOrigin { get; set; }

        public bool FilledWithOxygen { get; set; }

    }

    struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

    }

    
    
    


    class Program
    {
        public static int SIZE = 50;
        public static Tile[,] grid = new Tile[SIZE, SIZE];
        public static Coordinate CurrentLocation = new Coordinate() { X = SIZE / 2, Y = SIZE / 2 };
        public static Intcode.CPU cpu;
        public static int iterations = 0;

        public static Coordinate sensor;

        static void Main(string[] args)
        {
            for (int x = 0; x < SIZE; x++)
            {
                for (int y = 0; y < SIZE; y++)
                {
                    grid[x, y] = new Tile();
                }
            }

            long[] input = File.ReadAllLines("in.txt")[0].Split(',').Select(f => Convert.ToInt64(f)).ToArray();
            cpu = new Intcode.CPU(input);

            Visualise(true);

            var startLocation = GetCurrentTile();
            startLocation.Visited = true;
            startLocation.Type = TileType.START_POS;

            Explore();
        }



        public static void Explore()
        {
            while (true)
            {
                var toVisit = GetNotVisitedNeighBours();

                if (toVisit.Count == 0)
                {
                    while (toVisit.Count == 0)
                    {
                        cpu.Run(new long[] { Convert.ToInt64(GetCurrentTile().DirectionBack) });
                        if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                        {
                            var output = (int)cpu.Output; // Should always be 1
                            if (output != 1)
                            {
                                Console.WriteLine($"Error moving droid back");
                            }

                            CurrentLocation = GetCoordinateByDirection(GetCurrentTile().DirectionBack);
                        }

                        if (cpu.State == Intcode.CpuState.STOPPED)
                        {
                            Visualise(true);
                        }

                        toVisit = GetNotVisitedNeighBours();
                    }
                }

                var previousTile = GetCurrentTile();

                foreach (var direction in toVisit)
                {
                    int output = -1;
                    cpu.Run(new long[] { Convert.ToInt64(direction) });
                    if (cpu.State == Intcode.CpuState.OUTPUT_READY)
                    {
                        output = (int)cpu.Output;
                    }

                    if (output == 0) // Droid hit a wall. Tile in direction "item" is wall. Droid has not moved.
                    {
                        GetTileInDirection(direction).Type = TileType.WALL;
                        GetTileInDirection(direction).Visited = true;
                    }

                    if (output == 1) // Droid moved
                    {
                        GetTileInDirection(direction).Type = TileType.EMPTY;
                        GetTileInDirection(direction).Visited = true;
                        GetTileInDirection(direction).DirectionBack = GetOppositeDirection(direction);
                        CurrentLocation = GetCoordinateByDirection(direction);
                        GetCurrentTile().DistanceFromOrigin = previousTile.DistanceFromOrigin + 1;
                        break;
                    }

                    if (output == 2) // Droid found destination
                    {
                        GetTileInDirection(direction).Type = TileType.O2_SENSOR;
                        GetTileInDirection(direction).Visited = true;
                        GetTileInDirection(direction).DirectionBack = GetOppositeDirection(direction);
                        GetTileInDirection(direction).DistanceFromOrigin = previousTile.DistanceFromOrigin + 1;
                        CurrentLocation = GetCoordinateByDirection(direction);
                        GetCurrentTile().DistanceFromOrigin = previousTile.DistanceFromOrigin + 1;

                        sensor.X = CurrentLocation.X;
                        sensor.Y = CurrentLocation.Y;

                        break;
                        // Trace back to origin
                    }
                }

                iterations++;

                Visualise(false);
            }
        }

        private static List<Direction> GetNotVisitedNeighBours()
        {
            var ret = new List<Direction>();
            if (GetTileInDirection(Direction.N).Visited == false)
            {
                ret.Add(Direction.N);
            }
            if (GetTileInDirection(Direction.S).Visited == false)
            {
                ret.Add(Direction.S);
            }
            if (GetTileInDirection(Direction.W).Visited == false)
            {
                ret.Add(Direction.W);
            }
            if (GetTileInDirection(Direction.E).Visited == false)
            {
                ret.Add(Direction.E);
            }

            return ret;
        }

        private static Direction GetOppositeDirection(Direction dir)
        {
            if (dir == Direction.N) return Direction.S;
            if (dir == Direction.S) return Direction.N;
            if (dir == Direction.E) return Direction.W;
            if (dir == Direction.W) return Direction.E;

            return Direction.N;
        }

        private static Tile GetTileInDirection(Direction dir)
        {
            var c = GetCoordinateByDirection(dir);
            return grid[c.X, c.Y];
        }

        private static Coordinate GetCoordinateByDirection(Direction d)
        {
            switch (d)
            {
                case Direction.N:
                    return new Coordinate() { X = CurrentLocation.X - 1, Y = CurrentLocation.Y };

                case Direction.S:
                    return new Coordinate() { X = CurrentLocation.X + 1, Y = CurrentLocation.Y };

                case Direction.W:
                    return new Coordinate() { X = CurrentLocation.X, Y = CurrentLocation.Y - 1 };

                case Direction.E:
                    return new Coordinate() { X = CurrentLocation.X, Y = CurrentLocation.Y + 1 };

                default:
                    return new Coordinate();
            }
        }

        private static void MoveDroidInDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.N:
                    break;
                case Direction.S:
                    break;
                case Direction.W:
                    break;
                case Direction.E:
                    break;
                default:
                    break;
            }
        }


        private static Tile GetCurrentTile()
        {
            return grid[CurrentLocation.X, CurrentLocation.Y];
        }

        public static void Visualise(bool force)
        {

            if (iterations % 100 == 0 || force == true)
            {

                {
                    Console.Clear();
                    for (int i = 0; i < SIZE; i++)
                    {
                        for (int j = 0; j < SIZE; j++)
                        {
                            if (i == CurrentLocation.X && j == CurrentLocation.Y)
                            {
                                Console.Write("D");
                            }
                            else
                            {
                                switch (grid[i, j].Type)
                                {
                                    case TileType.START_POS:
                                        Console.Write("S");
                                        break;
                                    case TileType.EMPTY:
                                        Console.Write("·");
                                        //Console.Write(grid[i,j].DistanceFromOrigin);
                                        break;
                                    case TileType.WALL:
                                        Console.Write("█");
                                        break;
                                    case TileType.O2_SENSOR:
                                        Console.Write("Q");
                                        break;
                                    case TileType.UNKNOWN:
                                        Console.Write(" ");
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }

                        Console.WriteLine();

                    }
                }
            }
        }
    }
}
