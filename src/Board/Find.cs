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

        public List<int> FindCandidates(int Where)
        {
            if (this[Where] > 0)
                return new List<int>();

            bool[] present = new bool[10];
            foreach (var loc in Location.Blocking[Where])
                present[this[loc]] = true;

            List<int> result = new List<int>();
            for (int i = 1; i < 10; i++)
                if (!present[i])
                    result.Add(i);

            return result;
        }

        public Dictionary<int, List<int>> FindAllCandidates()
        {
            Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();

            foreach (var loc in FindEmptyLocations())
                result[loc] = FindCandidates(loc);

            return result;
        }
    }
}
