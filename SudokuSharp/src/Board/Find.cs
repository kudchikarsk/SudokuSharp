using System.Collections.Generic;
using System.Linq;

namespace SudokuSharp
{
    public partial class Board
    {
        public IEnumerable<int> FindEmptyLocations()
        {
            return from loc in Location.All
                   where this[loc] == 0
                   select loc;
        }

        public IEnumerable<int> FindFilledLocations()
        {
            return from loc in Location.All
                   where this[loc] > 0
                   select loc;
        }

        public IEnumerable<int> FindCandidates(int Where)
        {
            if (this[Where] > 0)
                return new List<int>();

            bool[] missing = new bool[10] { true, true, true, true, true, true, true, true, true, true };
            foreach (var loc in Location.Blocking[Where])
                missing[this[loc]] = false;

            return from i in Enumerable.Range(1, 9)
                   where missing[i]
                   select i;
        }

        public Dictionary<int, IEnumerable<int>> FindAllCandidates()
        {
            var result = new Dictionary<int, IEnumerable<int>>();

            foreach (var loc in FindEmptyLocations())
                result[loc] = FindCandidates(loc);

            return result;
        }
    }
}
