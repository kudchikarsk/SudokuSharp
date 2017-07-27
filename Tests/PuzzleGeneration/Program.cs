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
            int Batches = 10;
            int BatchSize = 50;

            Console.WriteLine("Timing the creation of {0:N0} puzzles.", Batches*BatchSize);
            var rnd = new Random(0);
            var brd = SudokuSharp.Factory.Solution(rnd);

            TimeSpan elapsed;
            var start = DateTime.Now;
            for (int i = 0; i < Batches; i++)
            {
                var bStart = DateTime.Now;
                for (int j = 0; j < BatchSize; j++)
                {
                    SudokuSharp.Factory.Puzzle(brd, rnd, 10, 10, 10);
                }
                elapsed = DateTime.Now - bStart;
                Console.WriteLine("{0:N0} puzzles created in {1:0.00} seconds for {2:N0} puzzles per second.", BatchSize, elapsed.TotalSeconds, BatchSize / elapsed.TotalSeconds);
            }
            elapsed = DateTime.Now - start;
            Console.WriteLine("{0:N0} puzzles created in {1:0.00} seconds for {2:N0} puzzles per second.", BatchSize * Batches, elapsed.TotalSeconds, (BatchSize*Batches) / elapsed.TotalSeconds);
        }
    }
}
