using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public IEnumerable<KeyValuePair<int, int>> FindNakedSingles()
        {
            return FindNakedSingles(FindAllCandidates());
        }

        public IEnumerable<KeyValuePair<int, int>> FindNakedSingles(IEnumerable<KeyValuePair<int, IEnumerable<int>>> Possibilities)
        {
            return from item in Possibilities
                   where item.Value.Count() == 1
                   select new KeyValuePair<int, int>(item.Key, item.Value.First());
        }

        public IEnumerable<KeyValuePair<int, int>> FindHiddenSingles()
        {
            return FindHiddenSingles(FindAllCandidates());
        }

        public IEnumerable<KeyValuePair<int, int>> FindHiddenSingles(IEnumerable<KeyValuePair<int, IEnumerable<int>>> Possibilities)
        {
            Dictionary<int, int> results = new Dictionary<int, int>();

            for (int number = 1; number < 10; number++)
            {
                var locationsForThisNumber = from item in Possibilities
                                             where item.Value.Contains(number)
                                             select item.Key;

                for (int test = 0; test < 9; test++)
                {
                    var possible = from Index in locationsForThisNumber where Location.Zone(Index) == test select Index;
                    if (possible.Count() == 1) results[possible.First()] = number;
                    possible = from Index in locationsForThisNumber where Location.Row(Index) == test select Index;
                    if (possible.Count() == 1) results[possible.First()] = number;
                    possible = from Index in locationsForThisNumber where Location.Column(Index) == test select Index;
                    if (possible.Count() == 1) results[possible.First()] = number;
                }
            }

            return results;
        }

        public IEnumerable<KeyValuePair<int, int>> FindAllSingles()
        {
            return FindHiddenSingles().Union(FindNakedSingles());
        }

        public IEnumerable<KeyValuePair<int, int>> FindAllSingles(IEnumerable<KeyValuePair<int, IEnumerable<int>>> Possibilities)
        {
            return FindHiddenSingles(Possibilities).Union(FindNakedSingles(Possibilities));
        }
    }
}
