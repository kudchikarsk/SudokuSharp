using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public partial class _FindClass
        {
            /// <summary>
            /// Looks for Naked Singles. These are cells with only a single candidate
            /// </summary>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<int, int>> NakedSingles()
            {
                return NakedSingles(AllCandidates());
            }

            /// <summary>
            /// Looks for Naked Singles. These are cells with only a single candidate.
            /// This version is intended to be called by other reducing functions (such as LockedCandidates)
            /// </summary>
            /// <param name="Possibilities">A set of candidates</param>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<int, int>> NakedSingles(IEnumerable<KeyValuePair<int, List<int>>> Possibilities)
            {
                return from item in Possibilities
                       where item.Value.Count == 1
                       select new KeyValuePair<int, int>(item.Key, item.Value.First());
            }

            /// <summary>
            /// Looks for Hidden Singles. These are digits which may only be placed in one cell within a row, column, or zone
            /// </summary>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<int, int>> HiddenSingles()
            {
                return HiddenSingles(AllCandidates());
            }

            /// <summary>
            /// Looks for Hidden Singles. These are digits which may only be placed in one cell within a row, column, or zone
            /// This version is intended to be called by other reducing functions (such as LockedCandidates)
            /// </summary>
            /// <param name="Possibilities">A set of candidates</param>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<int, int>> HiddenSingles(IEnumerable<KeyValuePair<int, List<int>>> Possibilities)
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

            /// <summary>
            /// Returns a the results of both Naked Singles and Hidden Singles
            /// </summary>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<int,int>> AllSingles()
            {
                return HiddenSingles().Union(NakedSingles());
            }

            /// <summary>
            /// Returns a the results of both Naked Singles and Hidden Singles
            /// This version is intended to be called by other reducing functions (such as LockedCandidates)
            /// </summary>
            /// <param name="Possibilities">A set of candidates</param>
            /// <returns>A set of <see cref="KeyValuePair{Location, Int32}"/> items</returns>
            public IEnumerable<KeyValuePair<int, int>> AllSingles(IEnumerable<KeyValuePair<int, List<int>>> Possibilities)
            {
                return HiddenSingles(Possibilities).Union(NakedSingles(Possibilities));
            }
        }
    }
}
