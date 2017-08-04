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
                foreach (var test in scratch.FindCandidates(Mask[idx])) {
                    RecurseAll(Results, scratch.Put(idx, test), Mask, idx + 1);
                }
            }
        }
    }
}
