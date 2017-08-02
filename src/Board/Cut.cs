using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    public partial class Board
    {
        public Board CutQuad(Random Stream)
        {
            var work = new Board(this);

            var Filled = work.FindFilledLocations().ToList();

            if (Filled.Count > 0)
            {
                var loc = new int[4];
                loc[0] = Filled[Stream.Next(Filled.Count)];
                loc[1] = Location.FlipVertical(loc[0]);
                loc[2] = Location.FlipHorizontal(loc[0]);
                loc[3] = Location.FlipHorizontal(loc[1]);

                for (int i = 0; i < 4; i++)
                    work.data[loc[i]] = 0;

                if (work.ExistsUniqueSolution())
                    return work;
            }

            return new Board(this);
        }

        public Board CutPair(Random stream)
        {
            var work = new Board(this);
            var Filled = work.FindFilledLocations().ToList();

            if (Filled.Count > 0)
            {
                var loc = new int[2];
                loc[0] = Filled[stream.Next(Filled.Count)];
                loc[1] = (stream.Next(2) == 0) ? Location.FlipVertical(loc[0]) : Location.FlipHorizontal(loc[0]);

                work.data[loc[0]] = 0;
                work.data[loc[1]] = 0;

                if (ExistsUniqueSolution())
                    return work;
            }

            return new Board(this);
        }

        public Board CutSingle(Random stream)
        {
            var work = new Board(this);
            var Filled = FindFilledLocations().ToList();

            if (Filled.Count>0)
            {
                var loc = Filled[stream.Next(Filled.Count)];
                work.data[loc] = 0;

                if (ExistsUniqueSolution())
                    return work;
            }

            return new Board(this);
        }
    }
}
