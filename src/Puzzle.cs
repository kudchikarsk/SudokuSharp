using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
    public static class Puzzle
    {
        public enum ValidityReason
        {
            Valid,
            NotEnoughClues,
            DuplicateInRow,
            DuplicateInColumn,
            DuplicateInZone,
            DuplicateAtLocation
        }

        public static ValidityReason IsValid(IEnumerable<(int loc, int val)> clues)
        {
            if (clues.Count() < 17)
                return ValidityReason.NotEnoughClues;

            var filled = new bool[81];

            var r = new bool[9];
            var c = new bool[9];

            var dr = new bool[9, 10];
            var dc = new bool[9, 10];
            var dz = new bool[9, 10];

            foreach (var hint in clues)
            {
                if (filled[hint.loc])
                    return ValidityReason.DuplicateAtLocation;
                else
                    filled[hint.loc] = true;

                int row = Location.Row(hint.loc);
                int col = Location.Column(hint.loc);
                int zon = Location.Zone(hint.loc);

                r[row] = true;
                c[col] = true;

                if (dr[row, hint.val])
                    return ValidityReason.DuplicateInRow;
                else
                    dr[row, hint.val] = true;

                if (dc[col, hint.val])
                    return ValidityReason.DuplicateInColumn;
                else
                    dc[col, hint.val] = true;

                if (dz[zon, hint.val])
                    return ValidityReason.DuplicateInZone;
                else
                    dz[zon,hint.val] = true;
            }

            return ValidityReason.Valid;
        }

        public static List<Board> CreateAll(bool[] Mask)
        {
            var newMask = from loc in Location.All
                          where Mask[loc]
                          select loc;

            return CreateAll(newMask.ToList());
        }

        public static List<Board> CreateAll(List<int> Mask)
        {
            if (Mask.Count < 17)
                throw new ArgumentException("Not enough clues provided, a minimum of 17 is necessary.");

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
