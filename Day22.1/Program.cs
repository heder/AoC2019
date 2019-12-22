using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22._1
{

    enum OpEnum
    {
        DEALINTONEWSTACK = 0,
        CUTN = 1,
        DEALWITHINCREMENT = 2
    }




    class Program
    {
        static int SIZE = 10007;
        static int[] deck = new int[SIZE];
        static List<(OpEnum, int)> actions = new List<(OpEnum, int)>();


        static void Main(string[] args)
        {
            string[] input = File.ReadAllLines("in.txt");

            for (int i = 0; i < input.Length; i++)
            {
                var split = input[i].Split(' ');

                if (split[0] == "deal" && split[1] == "with")
                {
                    actions.Add((OpEnum.DEALWITHINCREMENT, Convert.ToInt32(split[3])));
                }
                else if (split[0] == "deal" && split[1] == "into")
                {
                    actions.Add((OpEnum.DEALINTONEWSTACK, 0));
                }
                else if (split[0] == "cut")
                {
                    actions.Add((OpEnum.CUTN, Convert.ToInt32(split[1])));
                }
            }

            for (int i = 0; i < SIZE; i++)
            {
                deck[i] = i;
            }


            foreach (var item in actions)
            {
                switch (item.Item1)
                {
                    case OpEnum.DEALINTONEWSTACK:
                        NewStack();
                        break;
                    case OpEnum.CUTN:
                        CutN(item.Item2);
                        break;
                    case OpEnum.DEALWITHINCREMENT:
                        DealWithIncrementN(item.Item2);
                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < SIZE; i++)
            {
                if (deck[i] == 2019)
                {
                    Console.WriteLine(i);
                    break;
                }
            }


            Console.ReadKey();
        }




        static void CutN(int n)
        {
            if (n >= 0)
            {
                deck = deck.Skip(n).Concat(deck.Take(n)).ToArray();
            }
            else
            {
                deck = deck.Skip(deck.Length - Math.Abs(n)).Concat(deck.Take(deck.Length - Math.Abs(n))).ToArray();
            }
        }

        static void NewStack()
        {
            deck = deck.Reverse().ToArray();
        }


        static void DealWithIncrementN(int n)
        {
            int[] newDeck = new int[SIZE];
            int toPos = 0;

            for (int fromPos = 0; fromPos < SIZE; fromPos++)
            {
                newDeck[toPos] = deck[fromPos];
                toPos = GetNextN(toPos, SIZE, n);
            }

            newDeck.CopyTo(deck, 0);
        }

        static int GetNextN(int at, int size, int n)
        {
            if (at + n <= size)
            {
                return at + n;
            }
            else
            {
                return 0 + n - (size - at);
            }
        }

    }
}
