using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSharp
{
    class Puzzle
    {
        public static List<Board> CreateAll(List<int> Mask)
        {
            var found = new List<Board>();
            RecurseAll(found, new Board(), Mask, 0);
            return found;
        }

        private static void RecurseAll(List<Board> Results, Board scratch, List<int> Mask,int idx)
        {
            if (idx == Mask.Count)
            {
                if (scratch.ExistsUniqueSolution())
                    Results.Add(new Board(scratch));
            } else
            {
                foreach (var test in scratch.Find.Candidates(Mask[idx])) {
                    scratch[Mask[idx]] = test;
                    RecurseAll(Results, scratch, Mask, idx + 1);
                }
                scratch[Mask[idx]] = 0;
            }
        }
    }
}
