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
            var mask = new List<int> { 0, 10, 20, 30, 40, 50, 60, 70, 80, 4, 14, 24, 34, 44, 36, 46, 56, 66, 76, 15, 16, 25, 55, 64, 65 };
            var valuesToFill = from loc in mask
                               select (loc, 1);
            var maskedBoard = new SudokuSharp.Board().Put(valuesToFill);

            Console.Write("How many puzzles should be searched for? ");
            int n = int.Parse(Console.ReadLine());

            var start = DateTime.Now;
            var test = SudokuSharp.Board.CreatePuzzles(mask, n);
            var end = DateTime.Now;

            Console.WriteLine("Found {0} puzzles in {1} seconds, or {2} puzzles/second.", test.Count(), (end-start).TotalSeconds, test.Count()/(end-start).TotalSeconds);

            int withUnique = 0;
            Console.WriteLine("Checking them for unique solutions...");
            for (int i=0; i<test.Count(); i++)
            {
                if (i%25==0)
                {
                    Console.WriteLine("{0} puzzles with unique solutions found out of {1} tested.", withUnique, i);
                }
                if (test[i].ExistsUniqueSolution())
                {
                    Console.WriteLine(test[i]);
                    withUnique++;
                }
            }
        }
    }
}
