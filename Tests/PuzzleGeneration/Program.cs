using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            int numBatches = 10;
            int batchSize = 500_000;

            var mask = new List<int> { 0, 10, 20, 30, 40, 50, 60, 70, 80, 4, 14, 24, 34, 44, 36, 46, 56, 66, 76, 15, 16, 25, 55, 64, 65 };
            var valuesToFill = from loc in mask
                               select (loc, 1);
            var maskedBoard = new SudokuSharp.Board().Put(valuesToFill);

            var generator = new SudokuSharp.Board.MaskedGenerator(mask);
            var b = generator.First();

            int tGenerated = 0;
            TimeSpan tSpan = new TimeSpan();

            for (int i=0; i<numBatches;i++)
            {
                var start = DateTime.Now;
                for (int j = 0; j < batchSize; j++)
                    generator.Next();
                var end = DateTime.Now;

                var span = end - start;

                tGenerated += batchSize;
                tSpan += span;

                Console.WriteLine("Generated {0} puzzles in {1} seconds for {2:N} puzzles/second.", batchSize, span.TotalSeconds, batchSize / span.TotalSeconds);
            }

            Console.WriteLine("Generated {0} puzzles total in {1} seconds total for {2:N} puzzles/second.", tGenerated, tSpan.TotalSeconds, tGenerated / tSpan.TotalSeconds);
        }
    }
}
