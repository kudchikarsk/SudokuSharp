using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public IEnumerable<(int loc, int val)> FindNakedSingles()
        {
            return FindNakedSingles(FindAllCandidates());
        }

        public IEnumerable<(int loc, int val)> FindNakedSingles(IEnumerable<KeyValuePair<int, IEnumerable<int>>> Possibilities)
        {
            return from item in Possibilities
                   where item.Value.Count() == 1
                   select (item.Key, item.Value.First());
        }

        public IEnumerable<(int loc, int val)> FindHiddenSingles()
        {
            return FindHiddenSingles(FindAllCandidates());
        }

        public IEnumerable<(int loc, int val)> FindHiddenSingles(IEnumerable<KeyValuePair<int, IEnumerable<int>>> Possibilities)
        {
            var candidates = new List<(int loc, int val)>();
            foreach (var cell in FindEmptyLocations())
            {
                foreach (var num in FindCandidates(cell))
                    candidates.Add((cell, num));
            }

            return from test in candidates
                              where !(
                                  from strike in candidates
                                  where strike.val == test.val
                                  where Location.Blocking[strike.loc].Contains(test.loc)
                                  select strike
                                  ).Any()
                              select test;
        }

        public IEnumerable<(int loc, int val)> FindAllSingles()
        {
            return FindHiddenSingles().Union(FindNakedSingles());
        }

        public IEnumerable<(int loc, int val)> FindAllSingles(IEnumerable<KeyValuePair<int, IEnumerable<int>>> Possibilities)
        {
            return FindHiddenSingles(Possibilities).Union(FindNakedSingles(Possibilities));
        }
    }
}
