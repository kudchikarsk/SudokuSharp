using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public class MaskedGenerator
        {
            #region Constructors
            public MaskedGenerator(List<int> Clues)
            {
                clueLocations = Clues.OrderBy(x => x).ToArray();
                clueValues = new int[Clues.Count];

                SetupBlocking();
            }

            public MaskedGenerator(bool[] Mask)
            {
                clueLocations = Mask.Where(x => x).Select((x, i) => i).OrderBy(x => x).ToArray();
                clueValues = new int[clueLocations.Length];

                SetupBlocking();
            }
            #endregion

            #region Public Methods
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
            #endregion

            #region Private Methods
            private void SetupBlocking()
            {
                int num = clueLocations.Length;

                blocking = new bool[num, num];
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        blocking[i, j] = Location.IsBlockedBy(i, j);
                    }
                }
            }

            List<int> Candidates(int index)
            {
                var result = new List<int>();

                var digitsBlocking = new bool[10];
                var digitsUsed = new bool[10];

                for (int i = 0; i < index; i++)
                {
                    digitsUsed[clueValues[i]] = true;

                    if (blocking[index, i])
                    {
                        digitsBlocking[clueValues[i]] = true;
                    }
                }

                int firstUnused = 10;
                for (int i = 9; i > 0; i--)
                {
                    if (!digitsUsed[i])
                        firstUnused = i;
                }

                for (int i = 1; i < 10; i++)
                {
                    if (digitsUsed[i] && !digitsBlocking[i])
                        result.Add(i);
                }
                if (firstUnused < 10)
                    result.Add(firstUnused);

                return result;
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

            private Board ToBoard()
            {
                var toPut = new List<(int loc, int val)>();
                for (int i = 0; i < clueLocations.Length; i++)
                    toPut.Add((clueLocations[i], clueValues[i]));

                return new Board().Put(toPut);
            }
            #endregion

            #region Private Fields
            int[] clueLocations;
            int[] clueValues;

            bool[,] blocking;
            #endregion
        }
    }
}
