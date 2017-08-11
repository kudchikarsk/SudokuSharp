using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        public static Board CreatePuzzle(List<int> LocationsToFill)
        {
            return PuzzleRecurse(LocationsToFill, new List<(int, int)>());
        }

        static Board PuzzleRecurse(IEnumerable<int> LocationsToFill, IEnumerable<(int loc, int val)> LocationsFilled)
        {
            if (LocationsToFill.Any())
            { // Cycle through possibilities
                var where = LocationsToFill.First();
                var cons = new Constraints(LocationsFilled);
                var blocking = cons.AtLocation(where);

                var usedNotBlocking = from test in Enumerable.Range(1, 9)
                                      where cons.InPuzzle[test]
                                      where !blocking[test]
                                      select test;

                var firstNotBlocking = (from test in Enumerable.Range(1, 9)
                                        where !cons.InPuzzle[test]
                                        select test).FirstOrDefault();

                var toTry = (firstNotBlocking > 0) ? usedNotBlocking.Concat(new[] { firstNotBlocking }) : usedNotBlocking;

                foreach (var test in toTry)
                {
                    var put = PuzzleRecurse(LocationsToFill.Skip(1), LocationsFilled.Concat(new[] { (where, test) }));
                    if (put != null)
                    {
                        if (put.IsValid)
                            return put;
                    }
                }
            }
            else
            { // We've filled all locations
                return new Board().Put(LocationsFilled);
            }

            return null;
        }

        public static List<Board> CreatePuzzles(List<int> LocationsToFill, int NumberOfPuzzles)
        {
            var result = new List<Board>();
            PuzzlesRecurse(result, NumberOfPuzzles, LocationsToFill, new List<(int, int)>());
            return result;
        }

        static void PuzzlesRecurse(List<Board> Result, int NumberOfPuzzles, IEnumerable<int> LocationsToFill, IEnumerable<(int loc, int val)> LocationsFilled)
        {
            if (LocationsToFill.Any())
            { // Cycle through possibilities
                var where = LocationsToFill.First();
                var cons = new Constraints(LocationsFilled);
                var blocking = cons.AtLocation(where);

                var usedNotBlocking = from test in Enumerable.Range(1, 9)
                                      where cons.InPuzzle[test]
                                      where !blocking[test]
                                      select test;

                var firstNotBlocking = (from test in Enumerable.Range(1, 9)
                                        where !cons.InPuzzle[test]
                                        select test).FirstOrDefault();

                var toTry = (firstNotBlocking > 0) ? usedNotBlocking.Concat(new[] { firstNotBlocking }) : usedNotBlocking;

                foreach (var test in toTry)
                {
                    if (Result.Count() < NumberOfPuzzles)
                        PuzzlesRecurse(Result, NumberOfPuzzles, LocationsToFill.Skip(1), LocationsFilled.Concat(new[] { (where, test) }));
                }
            }
            else
            { // We've filled all locations
                Result.Add(new Board().Put(LocationsFilled));
            }
        }

    }
}
