using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        public Board Put(IEnumerable<(int loc, int val)> ToFill)
        {
            var result = new Board(this);
            foreach (var item in ToFill)
                result.data[item.loc] = item.val;

            return result;
        }
    }
}
