using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
    public static class Puzzle
    {
        public static List<Board> CreateAll(bool[] Mask)
        {
            var newMask = from loc in Location.All
                          where Mask[loc]
                          select loc;

            return CreateAll(newMask.ToList());
        }

        public static List<Board> CreateAll(List<int> Mask)
        {
            // Check for column, row and digit representation
            var foundColumns = new bool[9];
            var foundRows = new bool[9];
            foreach (var i in Mask)
            {
                foundColumns[Location.Column(i)] = true;
                foundRows[Location.Row(i)] = true;
            }

            for (int outer = 0; outer < 3; outer++)
            {
                int fCol = 0;
                int fRow = 0;
                for (int inner = 0; inner < 3; inner++)
                {
                    if (foundColumns[outer * 3 + inner]) fCol++;
                    if (foundRows[outer * 3 + inner]) fRow++;
                }
                if (fCol < 2)
                    throw new InvalidOperationException("Two columns within the same triplet are empty and may be swapped. Puzzles with unique solutions do not exist.");
                if (fRow < 2)
                    throw new InvalidOperationException("Two rows within the same triplet are empty and may be swapped. Puzzles with unique solutions do not exist.");
            }

            var found = new List<Board>();
            RecurseAll(found, new Board(), Mask, 0);
            return found;
        }

        private static void RecurseAll(List<Board> Results, Board scratch, List<int> Mask, int idx)
        {
            if (idx == Mask.Count)
            {
                if (scratch.ExistsUniqueSolution())
                    Results.Add(new Board(scratch));
            }
            else
            {
                foreach (var test in scratch.FindCandidates(Mask[idx]))
                {
                    RecurseAll(Results, scratch.Put(idx, test), Mask, idx + 1);
                }
            }
        }
    }
}
