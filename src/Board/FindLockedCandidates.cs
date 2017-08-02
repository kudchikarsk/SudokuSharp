using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        public IEnumerable<KeyValuePair<int, int>> FindLockedCandidates()
        {
            var possible = FindAllCandidates();

            for (int number = 1; number < 9; number++)
            {
                var numberLocations = from item in possible
                                      where item.Value.Contains(number)
                                      select item.Key;

                for (int zone = 0; zone < 9; zone++)
                {
                    var numberLocationsInThisZone = from loc in numberLocations
                                                    where Location.Zone(loc) == zone
                                                    select loc;

                    if (numberLocationsInThisZone.Count() > 0)
                    {
                        int lockedRow = Location.Row(numberLocationsInThisZone.First());
                        int lockedColumn = Location.Column(numberLocationsInThisZone.First());

                        foreach (var loc in numberLocationsInThisZone)
                        {
                            if (Location.Row(loc) != lockedRow) lockedRow = -1;
                            if (Location.Column(loc) != lockedColumn) lockedColumn = -1;
                        }
                        if (lockedRow != -1)
                        {
                            foreach (var loc in
                                (from idx in numberLocations
                                 where Location.Zone(idx) != zone
                                 where Location.Row(idx) == lockedRow
                                 select idx))
                                possible[loc].Remove(number);
                        }
                        if (lockedColumn != -1)
                        {
                            foreach (var loc in
                                (from idx in numberLocations
                                 where Location.Zone(idx) != zone
                                 where Location.Column(idx) == lockedColumn
                                 select idx))
                                possible[loc].Remove(number);
                        }
                    }
                }
            }

            return FindAllSingles(possible);
        }
    }
}
