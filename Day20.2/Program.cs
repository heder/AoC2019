using System;
using System.Collections.Generic;
using System.IO;

namespace Day20._2
{
    // DISCLAIMER: Ultra-hack deluxe solution. :)
    // **********************************************************
    // **********************************************************


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

    public enum TT
    {
        INNER = 0,
        OUTER = 1
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
        public TT TeleporterType { get; set; }
        public Coordinate TeleportsTo { get; set; }

        public bool SpreadTo { get; set; }

        public bool SpreadedFrom { get; set; }

        public Coordinate pos { get; set; }
    }

    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }

    class Program
    {
        public static int XSIZE;
        public static int YSIZE;
        public static int ZSIZE = 60;
        public static Tile[,,] grid;
        public static Coordinate CurrentLocation;

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt");

            XSIZE = input[0].Length - 4;
            YSIZE = input.Length - 4;

            grid = new Tile[XSIZE, YSIZE, ZSIZE];

            // Create mazes
            for (int z = 0; z < ZSIZE; z++)
            {
                // Baze maze :)
                for (int x = 0; x < XSIZE; x++)
                {
                    for (int y = 0; y < YSIZE; y++)
                    {
                        grid[x, y, z] = new Tile()
                        {
                            pos = new Coordinate() { X = x, Y = y, Z = z }
                        };

                        if (input[y + 2][x + 2] == '#') grid[x, y, z].Type = TileType.WALL;
                        else if (input[y + 2][x + 2] == '.') grid[x, y, z].Type = TileType.EMPTY;
                    }
                }

                if (z == 0)
                {
                    // Upper
                    grid[43, 0, 0].Type = TileType.WALL;                                //.TeleporterIdentity = "TJ";
                    grid[51, 0, 0].Type = TileType.WALL;                                 //.TeleporterIdentity = "MK";
                    grid[63, 0, 0].Type = TileType.WALL;                                 //.TeleporterIdentity = "SS";
                    grid[71, 0, 0].Type = TileType.WALL;                                 //.TeleporterIdentity = "RY";
                    grid[77, 0, 0].Type = TileType.WALL;                                 //.TeleporterIdentity = "HD";
                    grid[83, 0, 0].Type = TileType.WALL;                                 //.TeleporterIdentity = "FP";

                    // Inner
                    grid[41, 34, 0].TeleporterIdentity = "GY";
                    grid[53, 34, 0].TeleporterIdentity = "RY";
                    grid[59, 34, 0].TeleporterIdentity = "HD";
                    grid[67, 34, 0].TeleporterIdentity = "JS";
                    grid[73, 34, 0].TeleporterIdentity = "NZ";
                    grid[85, 34, 0].TeleporterIdentity = "ES";
                    
                    grid[41, 34, 0].TeleporterType = TT.INNER;
                    grid[53, 34, 0].TeleporterType = TT.INNER;
                    grid[59, 34, 0].TeleporterType = TT.INNER;
                    grid[67, 34, 0].TeleporterType = TT.INNER;
                    grid[73, 34, 0].TeleporterType = TT.INNER;
                    grid[85, 34, 0].TeleporterType = TT.INNER;

                    // Inner
                    grid[41, 88, 0].TeleporterIdentity = "CK";
                    grid[49, 88, 0].TeleporterIdentity = "CD";
                    grid[57, 88, 0].TeleporterIdentity = "IJ";
                    grid[67, 88, 0].TeleporterIdentity = "OE";
                    grid[75, 88, 0].TeleporterIdentity = "NK";
                    grid[83, 88, 0].TeleporterIdentity = "YF";
                    grid[91, 88, 0].TeleporterIdentity = "TJ";

                    grid[41, 88, 0].TeleporterType = TT.INNER;
                    grid[49, 88, 0].TeleporterType = TT.INNER;
                    grid[57, 88, 0].TeleporterType = TT.INNER;
                    grid[67, 88, 0].TeleporterType = TT.INNER;
                    grid[75, 88, 0].TeleporterType = TT.INNER;
                    grid[83, 88, 0].TeleporterType = TT.INNER;
                    grid[91, 88, 0].TeleporterType = TT.INNER;

                    // Bottom
                    grid[37, 122, 0].Type = TileType.WALL;                                                     //  .TeleporterIdentity = "MG";
                    grid[51, 122, 0].Type = TileType.WALL;                                                     //  .TeleporterIdentity = "FK";
                    grid[57, 122, 0].Type = TileType.WALL;                                                     //  .TeleporterIdentity = "JS";
                    grid[61, 122, 0].Type = TileType.WALL;                                                      //  .TeleporterIdentity = "IJ";
                                                                                                                //grid[65, 122].TeleporterIdentity = "ZZ";                        //
                                                                                                                //
                    grid[75, 122, 0].Type = TileType.WALL;                                                     //  .TeleporterIdentity = "GY";
                    grid[81, 122, 0].Type = TileType.WALL;                                                     //  .TeleporterIdentity = "OK";
                    grid[89, 122, 0].Type = TileType.WALL;                                                     //  .TeleporterIdentity = "XA";

                    // Left
                    grid[0, 43, 0].Type = TileType.WALL;
                    grid[0, 51, 0].Type = TileType.WALL;
                    //grid[0, 53].TeleporterIdentity = "AA";
                    grid[0, 61, 0].Type = TileType.WALL;
                    grid[0, 67, 0].Type = TileType.WALL;
                    grid[0, 75, 0].Type = TileType.WALL;
                    grid[0, 85, 0].Type = TileType.WALL;

                    // Left inner
                    grid[34, 41, 0].TeleporterIdentity = "YL";
                    grid[34, 47, 0].TeleporterIdentity = "JW";
                    grid[34, 57, 0].TeleporterIdentity = "NS";
                    grid[34, 67, 0].TeleporterIdentity = "OK";
                    grid[34, 75, 0].TeleporterIdentity = "OP";
                    grid[34, 83, 0].TeleporterIdentity = "FK";

                    grid[34, 41, 0].TeleporterType = TT.INNER;
                    grid[34, 47, 0].TeleporterType = TT.INNER;
                    grid[34, 57, 0].TeleporterType = TT.INNER;
                    grid[34, 67, 0].TeleporterType = TT.INNER;
                    grid[34, 75, 0].TeleporterType = TT.INNER;
                    grid[34, 83, 0].TeleporterType = TT.INNER;

                    // Right inner
                    grid[96, 41, 0].TeleporterIdentity = "XA";
                    grid[96, 45, 0].TeleporterIdentity = "GU";
                    grid[96, 55, 0].TeleporterIdentity = "MG";
                    grid[96, 61, 0].TeleporterIdentity = "YU";
                    grid[96, 63, 0].TeleporterIdentity = "SS";
                    grid[96, 67, 0].TeleporterIdentity = "MK";
                    grid[96, 77, 0].TeleporterIdentity = "HK";
                    grid[96, 79, 0].TeleporterIdentity = "FP";

                    grid[96, 41, 0].TeleporterType = TT.INNER;
                    grid[96, 45, 0].TeleporterType = TT.INNER;
                    grid[96, 55, 0].TeleporterType = TT.INNER;
                    grid[96, 61, 0].TeleporterType = TT.INNER;
                    grid[96, 63, 0].TeleporterType = TT.INNER;
                    grid[96, 67, 0].TeleporterType = TT.INNER;
                    grid[96, 77, 0].TeleporterType = TT.INNER;
                    grid[96, 79, 0].TeleporterType = TT.INNER;

                    // Right
                    grid[130, 37, 0].Type = TileType.WALL;
                    grid[130, 47, 0].Type = TileType.WALL;
                    grid[130, 51, 0].Type = TileType.WALL;
                    grid[130, 57, 0].Type = TileType.WALL;
                    grid[130, 59, 0].Type = TileType.WALL;
                    grid[130, 67, 0].Type = TileType.WALL;
                    grid[130, 73, 0].Type = TileType.WALL;
                    grid[130, 79, 0].Type = TileType.WALL;


                }

                if (z > 0)
                {
                    // Upper
                    grid[43, 0, z].TeleporterIdentity = "TJ";
                    grid[51, 0, z].TeleporterIdentity = "MK";
                    grid[63, 0, z].TeleporterIdentity = "SS";
                    grid[71, 0, z].TeleporterIdentity = "RY";
                    grid[77, 0, z].TeleporterIdentity = "HD";
                    grid[83, 0, z].TeleporterIdentity = "FP";

                    grid[43, 0, z].TeleporterType = TT.OUTER;
                    grid[51, 0, z].TeleporterType = TT.OUTER;
                    grid[63, 0, z].TeleporterType = TT.OUTER;
                    grid[71, 0, z].TeleporterType = TT.OUTER;
                    grid[77, 0, z].TeleporterType = TT.OUTER;
                    grid[83, 0, z].TeleporterType = TT.OUTER;

                    // Inner
                    grid[41, 34, z].TeleporterIdentity = "GY";
                    grid[53, 34, z].TeleporterIdentity = "RY";
                    grid[59, 34, z].TeleporterIdentity = "HD";
                    grid[67, 34, z].TeleporterIdentity = "JS";
                    grid[73, 34, z].TeleporterIdentity = "NZ";
                    grid[85, 34, z].TeleporterIdentity = "ES";

                    grid[41, 34, z].TeleporterType = TT.INNER;
                    grid[53, 34, z].TeleporterType = TT.INNER;
                    grid[59, 34, z].TeleporterType = TT.INNER;
                    grid[67, 34, z].TeleporterType = TT.INNER;
                    grid[73, 34, z].TeleporterType = TT.INNER;
                    grid[85, 34, z].TeleporterType = TT.INNER;

                    // Inner
                    grid[41, 88, z].TeleporterIdentity = "CK";
                    grid[49, 88, z].TeleporterIdentity = "CD";
                    grid[57, 88, z].TeleporterIdentity = "IJ";
                    grid[67, 88, z].TeleporterIdentity = "OE";
                    grid[75, 88, z].TeleporterIdentity = "NK";
                    grid[83, 88, z].TeleporterIdentity = "YF";
                    grid[91, 88, z].TeleporterIdentity = "TJ";

                    grid[41, 88, z].TeleporterType = TT.INNER;
                    grid[49, 88, z].TeleporterType = TT.INNER;
                    grid[57, 88, z].TeleporterType = TT.INNER;
                    grid[67, 88, z].TeleporterType = TT.INNER;
                    grid[75, 88, z].TeleporterType = TT.INNER;
                    grid[83, 88, z].TeleporterType = TT.INNER;
                    grid[91, 88, z].TeleporterType = TT.INNER;

                    // Bottom
                    grid[37, 122, z].TeleporterIdentity = "MG";
                    grid[51, 122, z].TeleporterIdentity = "FK";
                    grid[57, 122, z].TeleporterIdentity = "JS";
                    grid[61, 122, z].TeleporterIdentity = "IJ";
                    grid[65, 122, z].Type = TileType.WALL;            // .TeleporterIdentity = "ZZ";
                    grid[75, 122, z].TeleporterIdentity = "GY";
                    grid[81, 122, z].TeleporterIdentity = "OK";
                    grid[89, 122, z].TeleporterIdentity = "XA";

                    grid[37, 122, z].TeleporterType = TT.OUTER;
                    grid[51, 122, z].TeleporterType = TT.OUTER;
                    grid[57, 122, z].TeleporterType = TT.OUTER;
                    grid[61, 122, z].TeleporterType = TT.OUTER;
                    grid[75, 122, z].TeleporterType = TT.OUTER;
                    grid[81, 122, z].TeleporterType = TT.OUTER;
                    grid[89, 122, z].TeleporterType = TT.OUTER;

                    // Left
                    grid[0, 43, z].TeleporterIdentity = "JW";
                    grid[0, 51, z].TeleporterIdentity = "YF";
                    grid[0, 53, z].Type = TileType.WALL; //       .TeleporterIdentity = "AA";
                    grid[0, 61, z].TeleporterIdentity = "NK";
                    grid[0, 67, z].TeleporterIdentity = "NZ";
                    grid[0, 75, z].TeleporterIdentity = "YL";
                    grid[0, 85, z].TeleporterIdentity = "OE";

                    grid[0, 43, z].TeleporterType = TT.OUTER;
                    grid[0, 51, z].TeleporterType = TT.OUTER;
                    grid[0, 61, z].TeleporterType = TT.OUTER;
                    grid[0, 67, z].TeleporterType = TT.OUTER;
                    grid[0, 75, z].TeleporterType = TT.OUTER;
                    grid[0, 85, z].TeleporterType = TT.OUTER;

                    // Left inner
                    grid[34, 41, z].TeleporterIdentity = "YL";
                    grid[34, 47, z].TeleporterIdentity = "JW";
                    grid[34, 57, z].TeleporterIdentity = "NS";
                    grid[34, 67, z].TeleporterIdentity = "OK";
                    grid[34, 75, z].TeleporterIdentity = "OP";
                    grid[34, 83, z].TeleporterIdentity = "FK";

                    grid[34, 41, z].TeleporterType = TT.INNER;
                    grid[34, 47, z].TeleporterType = TT.INNER;
                    grid[34, 57, z].TeleporterType = TT.INNER;
                    grid[34, 67, z].TeleporterType = TT.INNER;
                    grid[34, 75, z].TeleporterType = TT.INNER;
                    grid[34, 83, z].TeleporterType = TT.INNER;

                    // Right inner
                    grid[96, 41, z].TeleporterIdentity = "XA";
                    grid[96, 45, z].TeleporterIdentity = "GU";
                    grid[96, 55, z].TeleporterIdentity = "MG";
                    grid[96, 61, z].TeleporterIdentity = "YU";
                    grid[96, 63, z].TeleporterIdentity = "SS";
                    grid[96, 67, z].TeleporterIdentity = "MK";
                    grid[96, 77, z].TeleporterIdentity = "HK";
                    grid[96, 79, z].TeleporterIdentity = "FP";

                    grid[96, 41, z].TeleporterType = TT.INNER;
                    grid[96, 45, z].TeleporterType = TT.INNER;
                    grid[96, 55, z].TeleporterType = TT.INNER;
                    grid[96, 61, z].TeleporterType = TT.INNER;
                    grid[96, 63, z].TeleporterType = TT.INNER;
                    grid[96, 67, z].TeleporterType = TT.INNER;
                    grid[96, 77, z].TeleporterType = TT.INNER;
                    grid[96, 79, z].TeleporterType = TT.INNER;

                    // Right
                    grid[130, 37, z].TeleporterIdentity = "ES";
                    grid[130, 47, z].TeleporterIdentity = "HK";
                    grid[130, 51, z].TeleporterIdentity = "YU";
                    grid[130, 57, z].TeleporterIdentity = "OP";
                    grid[130, 59, z].TeleporterIdentity = "GU";
                    grid[130, 67, z].TeleporterIdentity = "CK";
                    grid[130, 73, z].TeleporterIdentity = "NS";
                    grid[130, 79, z].TeleporterIdentity = "CD";

                    grid[130, 37, z].TeleporterType = TT.OUTER;
                    grid[130, 47, z].TeleporterType = TT.OUTER;
                    grid[130, 51, z].TeleporterType = TT.OUTER;
                    grid[130, 57, z].TeleporterType = TT.OUTER;
                    grid[130, 59, z].TeleporterType = TT.OUTER;
                    grid[130, 67, z].TeleporterType = TT.OUTER;
                    grid[130, 73, z].TeleporterType = TT.OUTER;
                    grid[130, 79, z].TeleporterType = TT.OUTER;
                }

                for (int x = 0; x < XSIZE; x++)
                {
                    for (int y = 0; y < YSIZE; y++)
                    {
                        if (grid[x, y, z].TeleporterIdentity != "") grid[x, y, z].Type = TileType.TELEPORTER;
                    }
                }
            }


            for (int z = 0; z < ZSIZE; z++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    for (int y = 0; y < YSIZE; y++)
                    {

                        if (grid[x, y, z].Type == TileType.TELEPORTER)
                        {

                            // Find connected tile
                            for (int x1 = 0; x1 < XSIZE; x1++)
                            {
                                for (int y1 = 0; y1 < YSIZE; y1++)
                                {
                                    // Connect Z-down
                                    if (z < ZSIZE - 1)
                                    {
                                        if (x != x1 && y != y1 && 
                                            grid[x1, y1, z + 1].TeleporterIdentity == grid[x, y, z].TeleporterIdentity && 
                                            grid[x, y, z].TeleporterType == TT.INNER 
                                            && grid[x1, y1, z + 1].TeleporterType == TT.OUTER)
                                        {
                                            grid[x, y, z].TeleportsTo = new Coordinate() { X = x1, Y = y1, Z = z + 1 };
                                        }
                                    }

                                    // Connect Z-up
                                    if (z > 0)
                                    {
                                        if (x != x1 && y != y1 && 
                                            grid[x1, y1, z - 1].TeleporterIdentity == grid[x, y, z].TeleporterIdentity &&
                                            grid[x, y, z].TeleporterType == TT.OUTER &&
                                            grid[x1, y1, z - 1].TeleporterType == TT.INNER)
                                        {
                                            grid[x, y, z].TeleportsTo = new Coordinate() { X = x1, Y = y1, Z = z - 1 };
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }


           // Visualise(true);

            var startLocation = GetCurrentTile();
            startLocation.Visited = true;
            startLocation.Type = TileType.START_POS;

            Fill();
        }

        private static void Fill()
        {
            // Flood fill BFS
            Coordinate startc = new Coordinate() { X = 0, Y = 53 };
            Coordinate destic = new Coordinate() { X = 65, Y = 122 };

            Tile start = grid[startc.X, startc.Y, 0];
            Tile destination = grid[destic.X, destic.Y, 0];

            start.SpreadTo = true;

            int spreadIterations = 0;
            while (true)
            {

                List<Tile> filled = new List<Tile>();
                // Find filled tiles
                for (int z = 0; z < ZSIZE; z++)
                {
                    for (int i = 0; i < XSIZE; i++)
                    {
                        for (int j = 0; j < YSIZE; j++)
                        {
                            if (grid[i, j, z].SpreadTo == true && grid[i, j, z].SpreadedFrom == false)
                            {
                                filled.Add(grid[i, j, z]);
                            }
                        }
                    }
                }

                foreach (var item in filled)
                {
                    item.SpreadedFrom = true;

                    // Spread 1 tile in all directions where tile is empty
                    var m = GetMovements(item);
                    foreach (var mmm in m)
                    {
                        mmm.SpreadTo = true;
                        mmm.SpreadedFrom = false;
                        mmm.DistanceFromOrigin = item.DistanceFromOrigin + 1;
                        mmm.Visited = true;

                        Console.WriteLine($"Level: {mmm.pos.Z}, Iteration: {spreadIterations}");
                    }
                }

                spreadIterations++;

                if (spreadIterations % 100 == 0)
                {
                  //  Visualise(true);
                }
            }
        }


        private static List<Tile> GetMovements(Tile t)
        {
            var ret = new List<Tile>();

            if (t.Type == TileType.TELEPORTER && grid[t.TeleportsTo.X, t.TeleportsTo.Y, t.TeleportsTo.Z].Visited == false)
            {
                var tp = grid[t.TeleportsTo.X, t.TeleportsTo.Y, t.TeleportsTo.Z];

                // Find destination of teleporter, if not already visited
                if (tp.Visited == false && tp.SpreadTo == false && tp.SpreadedFrom == false)
                {
                    ret.Add(grid[t.TeleportsTo.X, t.TeleportsTo.Y, t.TeleportsTo.Z]);
                }
            }
            else
            {
                var n = GetTileInDirection(t, Direction.N);
                var s = GetTileInDirection(t, Direction.S);
                var w = GetTileInDirection(t, Direction.W);
                var e = GetTileInDirection(t, Direction.E);

                if (n != null && n.pos.X == 65 && n.pos.Y == 122 && n.pos.Z == 0 ||
                    s != null && s.pos.X == 65 && s.pos.Y == 122 && s.pos.Z == 0 ||
                    w != null && w.pos.X == 65 && w.pos.Y == 122 && w.pos.Z == 0 ||
                    e != null && e.pos.X == 65 && e.pos.Y == 122 && e.pos.Z == 0)
                {
                    Visualise(true);
                    var x = t;
                    Console.WriteLine($"Result: {t.DistanceFromOrigin + 1}");
                }


                if (n != null && n.Type != TileType.WALL && n.Visited == false && n.SpreadTo == false && n.SpreadedFrom == false)
                {
                    ret.Add(n);
                }
                if (s != null && s.Type != TileType.WALL && s.Visited == false && s.SpreadTo == false && s.SpreadedFrom == false)
                {
                    ret.Add(s);
                }
                if (w != null && w.Type != TileType.WALL && w.Visited == false && w.SpreadTo == false && w.SpreadedFrom == false)
                {
                    ret.Add(w);
                }
                if (e != null && e.Type != TileType.WALL && e.Visited == false && e.SpreadTo == false && e.SpreadedFrom == false)
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
                return grid[c.X, c.Y, c.Z];
        }

        private static Coordinate GetCoordinateByDirection(Tile t, Direction d)
        {
            switch (d)
            {
                case Direction.N:
                    return new Coordinate() { X = t.pos.X, Y = t.pos.Y - 1, Z = t.pos.Z };

                case Direction.S:
                    return new Coordinate() { X = t.pos.X, Y = t.pos.Y + 1, Z = t.pos.Z };

                case Direction.W:
                    return new Coordinate() { X = t.pos.X - 1, Y = t.pos.Y, Z = t.pos.Z };

                case Direction.E:
                    return new Coordinate() { X = t.pos.X + 1, Y = t.pos.Y, Z = t.pos.Z };

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
            return grid[CurrentLocation.X, CurrentLocation.Y, CurrentLocation.Z];
        }

        public static void Visualise(bool force)
        {

            Console.Clear();

            for (int z = 0; z < ZSIZE; z++)
            {

                Console.WriteLine($"Level {z}");


                for (int y = 0; y < YSIZE; y++)
                {
                    for (int x = 0; x < XSIZE; x++)
                    {
                        if (x == CurrentLocation.X && y == CurrentLocation.Y && z == CurrentLocation.Z)
                        {
                            Console.Write("D");
                        }
                        else
                        {
                            if (grid[x, y, z].Visited == true)
                                Console.Write("X");
                            else
                                switch (grid[x, y, z].Type)
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
}
