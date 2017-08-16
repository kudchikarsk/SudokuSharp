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
            public PuzzleGenerator(List<int> Clues)
            {
                clueLocations = Clues.OrderBy(x => x).ToArray();
                clueValues = new int[Clues.Count];

                SetupBlocking();
            }

            public PuzzleGenerator(bool[] Mask)
            {
                clueLocations = Mask.Where(x => x).Select((x, i) => i).OrderBy(x => x).ToArray();
                clueValues = new int[clueLocations.Length];

                SetupBlocking();
            }

            private void SetupBlocking()
            {
                int num = clueLocations.Length;

                blocking = new bool[num, num];
                for (int i=0; i<num; i++)
                {
                    for (int j=0; j<num; j++)
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

                for (int i=0; i<index; i++)
                {
                    digitsUsed[clueValues[i]] = true;

                    if (blocking[index,i])
                    {
                        digitsBlocking[clueValues[i]] = true;
                    }
                }

                int firstUnused = 10;
                for (int i=9; i>0; i--)
                {
                    if (!digitsUsed[i])
                        firstUnused = i;
                }

                for (int i=1; i<10; i++)
                {
                    if (digitsUsed[i] && !digitsBlocking[i])
                        result.Add(i);
                }
                if (firstUnused < 10)
                    result.Add(firstUnused);

                return result;
            }

            int[] clueLocations;
            int[] clueValues;

            bool[,] blocking;
        }
    }
}
