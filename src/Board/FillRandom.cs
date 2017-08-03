using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
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
                    work.data[Index] = test;
                    data.InRow[test, Location.Row(Index)] = data.InColumn[test, Location.Column(Index)] = data.InZone[test, Location.Zone(Index)] = true;
                    if (RandomRecursion(work, data, Digits, Index + 1))
                        return true;
                    data.InRow[test, Location.Row(Index)] = data.InColumn[test, Location.Column(Index)] = data.InZone[test, Location.Zone(Index)] = false;
                }
            }

            work.data[Index] = 0;
            return false;
        }
    }
}
