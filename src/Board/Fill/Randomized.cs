using System;
using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FillClass
        {
            /// <summary>
            /// Attempts to fill the calling <see cref="Board"/> instance with numbers.
            /// The original instance remains unchanged
            /// </summary>
            /// <returns>Either a new instance of <see cref="Board"/> or, if unsuccessful, null</returns>
            public Board Randomized()
            {
                return Randomized(new Random());
            }

            /// <summary>
            /// Attempts to fill the calling <see cref="Board"/> instance with numbers.
            /// The original instance remains unchanged
            /// </summary>
            /// <param name="Seed">An integer Seed value for the random number generator</param>
            /// <returns>Either a new instance of <see cref="Board"/> or, if unsuccessful, null</returns>
            public Board Randomized(int Seed)
            {
                return Randomized(new Random(Seed));
            }

            /// <summary>
            /// Attempts to fill the calling <see cref="Board"/> instance with numbers.
            /// The original instance remains unchanged
            /// </summary>
            /// <param name="Stream">If you already have a <see cref="Random"/> stream, you may provide it here</param>
            /// <returns>Either a new instance of <see cref="Board"/> or, if unsuccessful, null</returns>
            public Board Randomized(Random Stream)
            {
                var result = new Board(_parent);
                ConstraintData data = new ConstraintData(result);

                var digits = new List<int>();
                for (int i = 1; i < 10; i++)
                    digits.Insert(Stream.Next(digits.Count), i);

                if (RandomRecursion(result, data, digits, 0))
                    return result;

                return null;
            }

            private static bool RandomRecursion(Board work, ConstraintData data, List<int> Digits, int Index)
            {
                if (Index == 81)
                    return true;

                if (work[Index] > 0)
                    return RandomRecursion(work, data, Digits, Index + 1);

                foreach (int test in Digits)
                {
                    if (!data.DigitInRow[test, Location.Row(Index)] && !data.DigitInColumn[test, Location.Column(Index)] && !data.DigitInZone[test, Location.Zone(Index)])
                    {
                        work[Index] = test;
                        data.DigitInRow[test, Location.Row(Index)] = data.DigitInColumn[test, Location.Column(Index)] = data.DigitInZone[test, Location.Zone(Index)] = true;
                        if (RandomRecursion(work, data, Digits, Index + 1))
                            return true;
                        data.DigitInRow[test, Location.Row(Index)] = data.DigitInColumn[test, Location.Column(Index)] = data.DigitInZone[test, Location.Zone(Index)] = false;
                    }
                }

                work[Index] = 0;
                return false;
            }
        }
    }
}
