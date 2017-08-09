using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        class Constraints
        {
            public Constraints()
            { }

            public Constraints(Board Src)
            {
                foreach (var loc in Location.All)
                {
                    InRow[Src[loc], Location.Row(loc)] =
                        InColumn[Src[loc], Location.Column(loc)] =
                        InZone[Src[loc], Location.Zone(loc)] = true;
                }
            }

            public Constraints(IEnumerable<(int loc, int val)> LocationsFilled)
            {
                foreach (var item in LocationsFilled)
                    InRow[item.val, Location.Row(item.loc)] =
                        InColumn[item.val, Location.Column(item.loc)] =
                        InZone[item.val, Location.Zone(item.loc)] = true;
            }

            public bool[] AtLocation(int where)
            {
                var result = new bool[10];
                for (int i = 0; i < 10; i++)
                    result[i] = InRow[i, Location.Row(where)] || InColumn[i, Location.Column(where)] || InZone[i, Location.Zone(where)];

                return result;
            }

            public bool[,] InRow = new bool[10, 9];
            public bool[,] InColumn = new bool[10, 9];
            public bool[,] InZone = new bool[10, 9];
        }
    }
}
