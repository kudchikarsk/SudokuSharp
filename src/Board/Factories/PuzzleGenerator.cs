using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
    public partial class Board
    {
        public class PuzzleGenerator
        {
            public PuzzleGenerator(List<int> Clues)
            {
                clueLocations = Clues.OrderBy(x => x).ToArray();
                clueValues = new int[Clues.Count];
            }

            public PuzzleGenerator(bool[] Mask)
            {
                clueLocations = Mask.Where(x => x).Select((x, i) => i).OrderBy(x => x).ToArray();
                clueValues = new int[clueLocations.Length];
            }

            public Board First()
            {
            }

            public Board Next()
            {
            }

            int[] clueLocations;
            int[] clueValues;
        }
    }
}
