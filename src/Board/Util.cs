using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        class Digits
        {
            public Digits(Board Src)
            {
                foreach (var loc in Location.All)
                {
                    InRow[Src[loc], Location.Row[loc]] =
                        InColumn[Src[loc], Location.Column[loc]] =
                        InZone[Src[loc], Location.Zone[loc]] = true;
                }
            }
            public bool[,] InRow = new bool[10, 9];
            public bool[,] InColumn = new bool[10, 9];
            public bool[,] InZone = new bool[10, 9];
        }
    }
}
