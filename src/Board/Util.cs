using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        class Digits
        {
            public Digits()
            { }

            public Digits(Board Src)
            {
                foreach (var loc in Location.All)
                {
                    InRow[Src[loc], Location.Row(loc)] =
                        InColumn[Src[loc], Location.Column(loc)] =
                        InZone[Src[loc], Location.Zone(loc)] = true;
                }
            }

            public Digits(IEnumerable<(int loc, int val)> LocationsFilled)
            {
                foreach (var item in LocationsFilled)
                    InRow[item.val, Location.Row(item.loc)] =
                        InColumn[item.val, Location.Column(item.loc)] =
                        InZone[item.val, Location.Zone(item.loc)] = true;
            }

            IEnumerable<int> Possibilities(int loc)
            {
                int r = Location.Row(loc), c = Location.Column(loc), z = Location.Zone(loc);

                return from test in Enumerable.Range(1, 9)
                       where !InRow[test, r]
                       where !InColumn[test, c]
                       where !InZone[test, z]
                       select test;
            }

            public bool[,] InRow = new bool[10, 9];
            public bool[,] InColumn = new bool[10, 9];
            public bool[,] InZone = new bool[10, 9];
        }
    }
}
