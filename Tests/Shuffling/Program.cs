using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shuffling
{
    class Program
    {
        static void Main(string[] args)
        {
            int Batches = 10;
            int BatchSize = 1_000_000;

            Console.WriteLine("Timing the shuffle of {0:N0} boards.", Batches * BatchSize);
            var rnd = new Random(0);

            TimeSpan elapsed;
            var start = DateTime.Now;
            for (int i = 0; i < Batches; i++)
            {
                var bStart = DateTime.Now;
                var src = SudokuSharp.Factory.Solution(rnd);
                for (int j = 0; j < BatchSize; j++)
                {
                    var shuffled = src.Shuffle(rnd);
                }
                elapsed = DateTime.Now - bStart;
                Console.WriteLine("{0:N0} boards shuffled in {1:0.00} seconds for {2:N0} boards per second.", BatchSize, elapsed.TotalSeconds, BatchSize / elapsed.TotalSeconds);
            }
            elapsed = DateTime.Now - start;
            Console.WriteLine("{0:N0} boards shuffled in {1:0.00} seconds for {2:N0} boards per second.", Batches * BatchSize, elapsed.TotalSeconds, (Batches * BatchSize) / elapsed.TotalSeconds);
        }
    }
}
