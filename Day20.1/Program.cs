using System;
using System.Collections.Generic;
using System.IO;

namespace Day20._1
{
    public enum TileType
    {
        START_POS = 0,
        EMPTY = 1,
        WALL = 2,
        DESTINATION = 3,
        TELEPORTER = 4,
        UNKNOWN = 5
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
            Type = TileType.WALL;
            TeleporterIdentity = "";
        }

        public TileType Type { get; set; }
        public Direction DirectionBack { get; set; }
        public bool Visited { get; set; }
        public int DistanceFromOrigin { get; set; }

        public string TeleporterIdentity { get; set; }
        public Coordinate TeleportsTo { get; set; }

        public bool OxygenSpreadTo { get; set; }

        public bool OxygenSpreadedFrom { get; set; }

        public Coordinate pos { get; set; }
    }

    public struct Coordinate
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
        public static int XSIZE;
        public static int YSIZE;
        public static Tile[,] grid;
        public static Coordinate CurrentLocation;

        //public static Coordinate sensor;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt");

            XSIZE = input[0].Length - 4;
            YSIZE = input.Length - 4;

            grid = new Tile[XSIZE, YSIZE];

            for (int x = 0; x < XSIZE; x++)
            {
                for (int y = 0; y < YSIZE; y++)
                {
                    grid[x, y] = new Tile()
                    {
                        pos = new Coordinate() { X = x, Y = y }
                    };

                    if (input[y + 2][x + 2] == '#') grid[x, y].Type = TileType.WALL;
                    else if (input[y + 2][x + 2] == '.') grid[x, y].Type = TileType.EMPTY;
                }
            }

            // Hor
            grid[43, 0].TeleporterIdentity = "TJ";
            grid[51, 0].TeleporterIdentity = "MK";
            grid[63, 0].TeleporterIdentity = "SS";
            grid[71, 0].TeleporterIdentity = "RY";
            grid[77, 0].TeleporterIdentity = "HD";
            grid[83, 0].TeleporterIdentity = "FP";

            grid[41, 34].TeleporterIdentity = "GY";
            grid[53, 34].TeleporterIdentity = "RY";
            grid[59, 34].TeleporterIdentity = "HD";
            grid[67, 34].TeleporterIdentity = "JS";
            grid[73, 34].TeleporterIdentity = "NZ";
            grid[85, 34].TeleporterIdentity = "ES";

            grid[41, 88].TeleporterIdentity = "CK";
            grid[49, 88].TeleporterIdentity = "CD";
            grid[57, 88].TeleporterIdentity = "IJ";
            grid[67, 88].TeleporterIdentity = "OE";
            grid[75, 88].TeleporterIdentity = "NK";
            grid[83, 88].TeleporterIdentity = "YF";
            grid[91, 88].TeleporterIdentity = "TJ";

            grid[37, 122].TeleporterIdentity = "MG";
            grid[51, 122].TeleporterIdentity = "FK";
            grid[57, 122].TeleporterIdentity = "JS";
            grid[61, 122].TeleporterIdentity = "IJ";
            //grid[65, 122].TeleporterIdentity = "ZZ";
            grid[75, 122].TeleporterIdentity = "GY";
            grid[81, 122].TeleporterIdentity = "OK";
            grid[89, 122].TeleporterIdentity = "XA";

            // Vert
            grid[0, 43].TeleporterIdentity = "JW";
            grid[0, 51].TeleporterIdentity = "YF";
            //grid[0, 53].TeleporterIdentity = "AA";
            grid[0, 61].TeleporterIdentity = "NK";
            grid[0, 67].TeleporterIdentity = "NZ";
            grid[0, 75].TeleporterIdentity = "YL";
            grid[0, 85].TeleporterIdentity = "OE";

            grid[34, 41].TeleporterIdentity = "YL";
            grid[34, 47].TeleporterIdentity = "JW";
            grid[34, 57].TeleporterIdentity = "NS";
            grid[34, 67].TeleporterIdentity = "OK";
            grid[34, 75].TeleporterIdentity = "OP";
            grid[34, 83].TeleporterIdentity = "FK";

            grid[96, 41].TeleporterIdentity = "XA";
            grid[96, 45].TeleporterIdentity = "GU";
            grid[96, 55].TeleporterIdentity = "MG";
            grid[96, 61].TeleporterIdentity = "YU";
            grid[96, 63].TeleporterIdentity = "SS";
            grid[96, 67].TeleporterIdentity = "MK";
            grid[96, 77].TeleporterIdentity = "HK";
            grid[96, 79].TeleporterIdentity = "FP";


            grid[130, 37].TeleporterIdentity = "ES";
            grid[130, 47].TeleporterIdentity = "HK";
            grid[130, 51].TeleporterIdentity = "YU";
            grid[130, 57].TeleporterIdentity = "OP";
            grid[130, 59].TeleporterIdentity = "GU";
            grid[130, 67].TeleporterIdentity = "CK";
            grid[130, 73].TeleporterIdentity = "NS";
            grid[130, 79].TeleporterIdentity = "CD";


            for (int x = 0; x < XSIZE; x++)
            {
                for (int y = 0; y < YSIZE; y++)
                {
                    if (grid[x, y].TeleporterIdentity != "") grid[x, y].Type = TileType.TELEPORTER;
                }
            }


            for (int x = 0; x < XSIZE; x++)
            {
                for (int y = 0; y < YSIZE; y++)
                {
                    if (grid[x, y].Type == TileType.TELEPORTER)
                    {

                        // Find connected tile
                        for (int x1 = 0; x1 < XSIZE; x1++)
                        {
                            for (int y1 = 0; y1 < YSIZE; y1++)
                            {
                                if (x != x1 && y != y1 && grid[x1, y1].TeleporterIdentity == grid[x, y].TeleporterIdentity)
                                {
                                    grid[x, y].TeleportsTo = new Coordinate() { X = x1, Y = y1 };
                                }
                            }
                        }
                    }
                }
            }

            Visualise(true);

            var startLocation = GetCurrentTile();
            startLocation.Visited = true;
            startLocation.Type = TileType.START_POS;

            FillWithOxygen();
        }






        private static void FillWithOxygen()
        {
            // Flood fill with oxygen
            Coordinate startc = new Coordinate() { X = 0, Y = 53 };
            Coordinate destic = new Coordinate() { X = 65, Y = 122 };

            Tile start = grid[startc.X, startc.Y];
            Tile destination = grid[destic.X, destic.Y];

            start.OxygenSpreadTo = true;

            int spreadIterations = 0;
            while (true)
            {
                //List<Tile> next = new List<Tile>();

                List<Tile> filled = new List<Tile>();
                // Find filled tiles
                for (int i = 0; i < XSIZE; i++)
                {
                    for (int j = 0; j < YSIZE; j++)
                    {
                        if (grid[i, j].OxygenSpreadTo == true && grid[i, j].OxygenSpreadedFrom == false)
                        {
                            filled.Add(grid[i, j]);
                        }
                    }
                }

                foreach (var item in filled)
                {
                    item.OxygenSpreadedFrom = true;

                    // Spread 1 tile in all directions where tile is empty

                    var m = GetMovements(item);
                    foreach (var mmm in m)
                    {
                        mmm.OxygenSpreadTo = true;
                        mmm.OxygenSpreadedFrom = false;
                        mmm.DistanceFromOrigin = item.DistanceFromOrigin + 1;
                        mmm.Visited = true;
                    }
                }

                spreadIterations++;

                if (spreadIterations % 100 == 0)
                {
                    Visualise(true);
                }
            }
        }


        private static List<Tile> GetMovements(Tile t)
        {
            var ret = new List<Tile>();

            if (t.Type == TileType.TELEPORTER && grid[t.TeleportsTo.X, t.TeleportsTo.Y].Visited == false)
            {
                var tp = grid[t.TeleportsTo.X, t.TeleportsTo.Y];

                // Find destination of teleporter, if not already visited
                if (tp.Visited == false && tp.OxygenSpreadTo == false && tp.OxygenSpreadedFrom == false)
                {
                    ret.Add(grid[t.TeleportsTo.X, t.TeleportsTo.Y]);
                }
            }
            else
            {
                var n = GetTileInDirection(t, Direction.N);
                var s = GetTileInDirection(t, Direction.S);
                var w = GetTileInDirection(t, Direction.W);
                var e = GetTileInDirection(t, Direction.E);

                if (n != null && n.pos.X == 65 && n.pos.Y == 122 ||
                    s != null && s.pos.X == 65 && s.pos.Y == 122 ||
                    w != null && w.pos.X == 65 && w.pos.Y == 122 ||
                    e != null && e.pos.X == 65 && e.pos.Y == 122)
                {
                    Visualise(true);
                    var x = t;
                }


                if (n != null && n.Type != TileType.WALL && n.Visited == false && n.OxygenSpreadTo == false && n.OxygenSpreadedFrom == false)
                {
                    ret.Add(n);
                }
                if (s != null && s.Type != TileType.WALL && s.Visited == false && s.OxygenSpreadTo == false && s.OxygenSpreadedFrom == false)
                {
                    ret.Add(s);
                }
                if (w != null && w.Type != TileType.WALL && w.Visited == false && w.OxygenSpreadTo == false && w.OxygenSpreadedFrom == false)
                {
                    ret.Add(w);
                }
                if (e != null && e.Type != TileType.WALL && e.Visited == false && e.OxygenSpreadTo == false && e.OxygenSpreadedFrom == false)
                {
                    ret.Add(e);
                }
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

        private static Tile GetTileInDirection(Tile t, Direction dir)
        {
            var c = GetCoordinateByDirection(t, dir);
            if (c.X < 0 || c.Y < 0 || c.X > 130 || c.Y > 122)
                return null;
            else
                return grid[c.X, c.Y];
        }

        private static Coordinate GetCoordinateByDirection(Tile t, Direction d)
        {
            switch (d)
            {
                case Direction.N:
                    return new Coordinate() { X = t.pos.X, Y = t.pos.Y - 1 };

                case Direction.S:
                    return new Coordinate() { X = t.pos.X, Y = t.pos.Y + 1 };

                case Direction.W:
                    return new Coordinate() { X = t.pos.X - 1, Y = t.pos.Y };

                case Direction.E:
                    return new Coordinate() { X = t.pos.X + 1, Y = t.pos.Y };

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

            Console.Clear();
            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    if (x == CurrentLocation.X && y == CurrentLocation.Y)
                    {
                        Console.Write("D");
                    }
                    else
                    {
                        if (grid[x, y].Visited == true)
                            Console.Write("X");
                        else
                            switch (grid[x, y].Type)
                            {
                                case TileType.START_POS:
                                    Console.Write("S");
                                    break;

                                case TileType.EMPTY:
                                    Console.Write("·");
                                    break;

                                case TileType.WALL:
                                    Console.Write("█");
                                    break;

                                case TileType.DESTINATION:
                                    Console.Write("D");
                                    break;

                                case TileType.UNKNOWN:
                                    Console.Write(" ");
                                    break;

                                case TileType.TELEPORTER:
                                    Console.Write("T");
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
