using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        public Board Shuffle(Random stream)
        {
            var key = new List<int> { 1 };

            foreach (var n in Enumerable.Range(2,8))
                key.Insert(stream.Next(key.Count), n);
            key.Insert(0, 0);

            var result = new Board();
            foreach (var loc in Location.All)
                result[loc] = key[this[loc]];

            return result;
        }
    }
}
