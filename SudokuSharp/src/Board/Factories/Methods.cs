using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class PuzzleGenerator
        {
            public Board First()
            {
                if (StepDown(0))
                    return ToBoard();

                return null;
            }

            public Board Next()
            {
                if (StepUp(clueValues.Length - 1))
                    return ToBoard();

                return null;
            }

            private Board ToBoard()
            {
                var toPut = new List<(int loc, int val)>();
                for (int i = 0; i < clueLocations.Length; i++)
                    toPut.Add((clueLocations[i], clueValues[i]));

                return new Board().Put(toPut);
            }

            private bool StepUp(int idx)
            {
                if (idx < 0)
                    return false;

                var possible = Candidates(idx).Where(x => x > clueValues[idx]);

                foreach (var test in possible)
                {
                    clueValues[idx] = test;
                    if (StepDown(idx + 1))
                        return true;
                }
                clueValues[idx] = 0;
                return StepUp(idx - 1);
            }

            private bool StepDown(int idx)
            {
                if (idx == clueLocations.Length)
                    return true;

                var possible = Candidates(idx);

                foreach (var test in possible)
                {
                    clueValues[idx] = test;
                    if (StepDown(idx + 1))
                        return true;
                }
                clueValues[idx] = 0;
                return false;
            }


        }
    }
}
