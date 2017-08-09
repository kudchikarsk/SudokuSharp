using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SudokuSharp
{
    /// <summary>
    /// Provides functions for generating games
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// First calls <see cref="Solution(int)"/> with the provided Seed, then calls Cut.Quad, .Pair, and .Single the specified number of times
        /// </summary>
        /// <param name="Seed">An integer seed for the <see cref="Random"/> number generator</param>
        /// <param name="QuadsToCut">The number of times to call Cut.Quad</param>
        /// <param name="PairsToCut">The number of times to call Cut.Pair</param>
        /// <param name="SinglesToCut">The number of times to call Cut.Single</param>
        /// <returns></returns>
        public static Board Puzzle(int Seed, int QuadsToCut, int PairsToCut, int SinglesToCut)
        {
            Random rnd = new Random(Seed);
            return Puzzle(new Board(rnd), rnd, QuadsToCut, PairsToCut, SinglesToCut);
        }

        /// <summary>
        /// Calls Cut.Quad, .Pair, and .Single the specified number of times on the provided <see cref="Board"/>
        /// </summary>
        /// <param name="Source">The <see cref="Board"/> to be modified</param>
        /// <param name="Stream">An existing <see cref="Random"/> number generator</param>
        /// <param name="QuadsToCut">The number of times to call Cut.Quad</param>
        /// <param name="PairsToCut">The number of times to call Cut.Pair</param>
        /// <param name="SinglesToCut">The number of times to call Cut.Single</param>
        /// <returns></returns>
        public static Board Puzzle(Board Source, Random Stream, int QuadsToCut, int PairsToCut, int SinglesToCut)
        {
            var work = new Board(Source);

            for (int i = 0; i < QuadsToCut; i++)
                work = work.CutQuad(Stream);
            for (int i = 0; i < PairsToCut; i++)
                work = work.CutPair(Stream);
            for (int i = 0; i < SinglesToCut; i++)
                work = work.CutSingle(Stream);

            return work;
        }
    }
}
