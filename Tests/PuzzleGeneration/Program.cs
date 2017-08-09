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
            var mask = new List<int> { 0, 10, 20, 30, 40, 50, 60, 70, 80, 4, 14, 24, 34, 44, 36, 46, 56, 66, 76 };
            var valuesToFill = from loc in mask
                               select (loc, 1);
            var maskedBoard = new SudokuSharp.Board().Put(valuesToFill);

            Console.WriteLine("Creating a puzzle to fill:");
            Console.WriteLine(maskedBoard);
            Console.WriteLine();

            var test = SudokuSharp.Board.CreatePuzzle(mask);

            Console.WriteLine("The generated puzzle:");
            Console.WriteLine(test);
            Console.WriteLine();

            Console.WriteLine("Is puzzle valid: {0}", test.IsValid);
            Console.WriteLine("Does a single solution exist: {0}", test.ExistsUniqueSolution());
            Console.WriteLine("Number of solutions: {0}", test.CountSolutions());

        }
    }
}
