using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class PuzzleGenerator
        {
            public Board First()
            {
                var success = Recursion(0);
                if (success)
                    return ToBoard();

                return null;
            }

            public Board Next()
            {

            }

            private Board ToBoard()
            {
                var toPut = new List<(int loc, int val)>();
                for (int i = 0; i < clueLocations.Length; i++)
                    toPut.Add((clueLocations[i], clueValues[i]));

                return new Board().Put(toPut);
            }

            private bool Recursion(int idx)
            {
                if (idx == clueLocations.Length)
                    return true;

                var possible = Candidates(idx);

                foreach (var test in possible)
                {
                    clueValues[idx] = test;
                    if (Recursion(idx + 1))
                        return true;
                }
                clueValues[idx] = 0;
                return false;
            }
        }
    }
}
