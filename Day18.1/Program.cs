using System;
using System.Collections.Generic;
using System.IO;

namespace Day18._1
{
    class Program
    {
        public enum TileType
        {
            START_POS = 0,
            EMPTY = 1,
            WALL = 2,
            KEY = 3,
            DOOR = 4
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
            }

            public TileType Type { get; set; }
            public Direction DirectionBack { get; set; }
            public bool Visited { get; set; }
            public int DistanceFromOrigin { get; set; }


            public char Key;
            public char Door;

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


        public static int XSIZE;
        public static int YSIZE;

        public static Tile[,] grid;
        public static Tile StartTile;

        //public static Coordinate CurrentLocation;
        static HashSet<char> keys = new HashSet<char>();

        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt");

            XSIZE = input[0].Length;
            YSIZE = input.Length;

            grid = new Tile[XSIZE, YSIZE];

            for (int x = 0; x < XSIZE; x++)
            {
                for (int y = 0; y < YSIZE; y++)
                {
                    grid[x, y] = new Tile()
                    {
                        pos = new Coordinate() { X = x, Y = y }
                    };

                    if (input[y][x] == '#') grid[x, y].Type = TileType.WALL;
                    if (input[y][x] == '.') grid[x, y].Type = TileType.EMPTY;

                    if (Char.IsLower((char)input[y][x]))
                    {
                        grid[x, y].Type = TileType.KEY;
                        grid[x, y].Key = (char)input[y][x];
                        keys.Add(grid[x, y].Key);
                    }

                    if (Char.IsUpper((char)input[y][x]))
                    {
                        grid[x, y].Type = TileType.DOOR;
                        grid[x, y].Door = (char)input[y][x];
                    }

                    if (input[y][x] == '@')
                    {
                        grid[x, y].Type = TileType.START_POS;
                        StartTile = grid[x, y];
                    }
                }
            }


            Visualise(true);

            var startLocation = StartTile;
            startLocation.Visited = true;
            startLocation.Type = TileType.START_POS;

            FillWithOxygen();



        }



        private static void FillWithOxygen()
        {
            // Flood fill with oxygen
            Coordinate startc = StartTile.pos;
            //Coordinate destic = new Coordinate() { X = 65, Y = 122 };

            Tile start = grid[startc.X, startc.Y];
            //Tile destination = grid[destic.X, destic.Y];

            start.SpreadTo = true;

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
                        if (grid[i, j].SpreadTo == true && grid[i, j].SpreadedFrom == false)
                        {
                            filled.Add(grid[i, j]);
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



                        if (mmm.Type == TileType.KEY)
                        {
                            for (int x = 0; x < XSIZE; x++)
                            {
                                for (int y = 0; y < YSIZE; y++)
                                {
                                    if (grid[x, y].Door.ToString() == mmm.Key.ToString().ToUpper())
                                    {
                                        grid[x, y].Type = TileType.EMPTY;
                                    }
                                }
                            }

                            keys.Remove(mmm.Key);

                            if (keys.Count == 0)
                            {
                                Console.ReadKey();
                            }

                            Visualise(true);
                        }

                    }
                }

                spreadIterations++;

                Visualise(true);

                //if (spreadIterations % 100 == 0)
                //{
                //    Visualise(true);
                //}
            }
        }


        private static List<Tile> GetMovements(Tile t)
        {
            var ret = new List<Tile>();

            //if (t.Type == TileType.TELEPORTER && grid[t.TeleportsTo.X, t.TeleportsTo.Y].Visited == false)
            //{
            //    var tp = grid[t.TeleportsTo.X, t.TeleportsTo.Y];

            //    // Find destination of teleporter, if not already visited
            //    if (tp.Visited == false && tp.OxygenSpreadTo == false && tp.OxygenSpreadedFrom == false)
            //    {
            //        ret.Add(grid[t.TeleportsTo.X, t.TeleportsTo.Y]);
            //    }
            //}
            //else

            var n = GetTileInDirection(t, Direction.N);
            var s = GetTileInDirection(t, Direction.S);
            var w = GetTileInDirection(t, Direction.W);
            var e = GetTileInDirection(t, Direction.E);





            if (n != null && (n.Type != TileType.WALL && n.Type != TileType.DOOR) && n.Visited == false && n.SpreadTo == false && n.SpreadedFrom == false)
            {
                ret.Add(n);
            }
            if (s != null && (s.Type != TileType.WALL && s.Type != TileType.DOOR) && s.Visited == false && s.SpreadTo == false && s.SpreadedFrom == false)
            {
                ret.Add(s);
            }
            if (w != null && (w.Type != TileType.WALL && w.Type != TileType.DOOR) && w.Visited == false && w.SpreadTo == false && w.SpreadedFrom == false)
            {
                ret.Add(w);
            }
            if (e != null && (e.Type != TileType.WALL && e.Type != TileType.DOOR) && e.Visited == false && e.SpreadTo == false && e.SpreadedFrom == false)
            {
                ret.Add(e);
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



        public static void Visualise(bool force)
        {

            Console.Clear();
            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
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

                            case TileType.KEY:
                                Console.Write(grid[x, y].Key);
                                break;

                            case TileType.DOOR:
                                Console.Write(grid[x, y].Door);
                                break;

                            default:
                                break;

                        }
                }

                Console.WriteLine();
            }
        }
    }
}

