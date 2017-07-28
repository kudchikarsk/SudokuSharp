using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        public Board FillSequential()
        {
            var work = new Board(this);

            Digits data = new Digits(work);

            if (SequentialRecursion(work, data, 0))
                return work;

            return null;
        }

        public Board FillRandom(Random Stream)
        {
            var result = new Board(this);
            Digits data = new Digits(result);

            var digits = new List<int>();
            for (int i = 1; i < 10; i++)
                digits.Insert(Stream.Next(digits.Count), i);

            if (RandomRecursion(result, data, digits, 0))
                return result;

            return null;
        }

        private static bool SequentialRecursion(Board work, Digits data, int Index)
        {
            if (Index == 81)
                return true;

            if (work[Index] > 0)
                return SequentialRecursion(work, data, Index + 1);

            for (int i = 1; i < 10; i++)
            {
                if (!data.InRow[i, Location.Row(Index)] && !data.InColumn[i, Location.Column(Index)] && !data.InZone[i, Location.Zone(Index)])
                {
                    work[Index] = i;
                    data.InRow[i, Location.Row(Index)] = data.InColumn[i, Location.Column(Index)] = data.InZone[i, Location.Zone(Index)] = true;
                    if (SequentialRecursion(work, data, Index + 1))
                        return true;
                    data.InRow[i, Location.Row(Index)] = data.InColumn[i, Location.Column(Index)] = data.InZone[i, Location.Zone(Index)] = false;
                }
            }

            work[Index] = 0;
            return false;
        }

        private static bool RandomRecursion(Board work, Digits data, List<int> Digits, int Index)
        {
            if (Index == 81)
                return true;

            if (work[Index] > 0)
                return RandomRecursion(work, data, Digits, Index + 1);

            foreach (int test in Digits)
            {
                if (!data.InRow[test, Location.Row(Index)] && !data.InColumn[test, Location.Column(Index)] && !data.InZone[test, Location.Zone(Index)])
                {
                    work[Index] = test;
                    data.InRow[test, Location.Row(Index)] = data.InColumn[test, Location.Column(Index)] = data.InZone[test, Location.Zone(Index)] = true;
                    if (RandomRecursion(work, data, Digits, Index + 1))
                        return true;
                    data.InRow[test, Location.Row(Index)] = data.InColumn[test, Location.Column(Index)] = data.InZone[test, Location.Zone(Index)] = false;
                }
            }

            work[Index] = 0;
            return false;
        }

        class Digits
        {
            public Digits(Board Src)
            {
                foreach (var loc in Location.All)
                {
                    InRow[Src[loc], Location.Row(loc)] =
                        InColumn[Src[loc], Location.Column(loc)] =
                        InZone[Src[loc], Location.Zone(loc)] = true;
                }
            }
            public bool[,] InRow = new bool[10, 9];
            public bool[,] InColumn = new bool[10, 9];
            public bool[,] InZone = new bool[10, 9];
        }
    }
}
