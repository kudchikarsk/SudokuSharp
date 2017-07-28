using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        public void CutQuad(Random Stream)
        {
            var Filled = Find.FilledLocations().ToList();

            if (Filled.Count > 0)
            {
                var loc = new int[4];
                loc[0] = Filled[Stream.Next(Filled.Count)];
                loc[1] = Location.FlipVertical(loc[0]);
                loc[2] = Location.FlipHorizontal(loc[0]);
                loc[3] = Location.FlipHorizontal(loc[1]);

                var test = new int[4];

                for (int i = 0; i < 4; i++)
                    test[i] = this[loc[i]];
                for (int i = 0; i < 4; i++)
                    this[loc[i]] = 0;

                if (!ExistsUniqueSolution())
                {
                    for (int i = 0; i < 4; i++)
                        this[loc[i]] = test[i];
                }
            }
        }

        public void CutPair(Random stream)
        {
            var Filled = Find.FilledLocations().ToList();

            if (Filled.Count > 0)
            {
                var loc = new int[2];
                loc[0] = Filled[stream.Next(Filled.Count)];
                loc[1] = (stream.Next(2) == 0) ? Location.FlipVertical(loc[0]) : Location.FlipHorizontal(loc[0]);

                var test = new int[2];

                test[0] = this[loc[0]];
                test[1] = this[loc[1]];
                this[loc[0]] = 0;
                this[loc[1]] = 0;

                if (!ExistsUniqueSolution())
                {
                    this[loc[0]] = test[0];
                    this[loc[1]] = test[1];
                }
            }
        }

        public void CutSingle(Random stream)
        {
            var Filled = Find.FilledLocations().ToList();

            if (Filled.Count>0)
            {
                var loc = Filled[stream.Next(Filled.Count)];
                int test = this[loc];
                this[loc] = 0;

                if (!ExistsUniqueSolution())
                    this[loc] = test;
            }
        }
    }
}
