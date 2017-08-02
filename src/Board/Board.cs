﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SudokuSharp
{
    /// <summary>
    /// The basic Sudoku class.
    /// It contains a grid of cells with values of 0-9; 0 corresponds to an empty cell, and the other digits the possible values.
    /// </summary>
    public partial class Board
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class, completely blank (ie every cell <see cref="Location"/> is empty).
        /// </summary>
        public Board() { }

        /// <summary>
        /// Copies an instance of the <see cref="Board"/> class.
        /// </summary>
        /// <param name="src">The source.</param>
        public Board(Board src)
        {
            Array.Copy(src.data, this.data, 81);
        }

        public Board(int Seed)
        {
            FillRandom(new Random(Seed));
        }

        public Board(Random stream)
        {
            FillRandom(stream);
        }
        #endregion

        /// <summary>
        /// Overrides array indexing (suare brackets []) for accessing locations in the Grid.
        /// Essentially, it's another way to access GetCell and PutCell.
        /// 
        /// You may use
        ///   <see cref="int"/> where = <see cref="Location"/>.Index(x,y);
        ///   <see cref="int"/> result = myGrid[where];
        ///   myGrid[where] = result;
        /// </summary>
        /// <value>
        /// The <see cref="int"/> representing the value of the cell (0 for empty, 1-9 for a value).
        /// </value>
        /// <param name="where">The index of the location to access.</param>
        /// <returns></returns>
        public int this[int where]
        {
            get { return data[where]; }
        }

        private int[] GetRow(int Row)
        {
            int[] result = new int[9];

            Array.Copy(data, Row * 9, result, 0, 9);

            return result;
        }
        private int[] GetColumn(int Column)
        {
            int[] result = new int[9];
            int idx = Column;
            for (int i = 0; i < 9; i++)
            {
                result[i] = data[idx];
                idx += 9;
            }
            return result;
        }
        private int[] GetZone(int Zone)
        {
            int[] result = new int[9];
            Array.Copy(data, ZoneIndices[Zone], result, 0, 3);
            Array.Copy(data, ZoneIndices[Zone] + 9, result, 3, 3);
            Array.Copy(data, ZoneIndices[Zone] + 18, result, 6, 3);

            return result;
        }

        /// <summary>
        /// Provides a pretty string representation of the Board instance.
        /// 3x3 blocks have one empty column and row between them, and empty cells are represented as '-'
        /// The resulting string is then 11x11 when printed on a terminal
        /// </summary>
        /// <returns>A <see cref="string"/> value.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var loc in Location.All)
            {
                sb.Append((this[loc] > 0) ? this[loc].ToString() : "-");
                if (Location.Column(loc) % 3 == 2) sb.Append(" ");
                if (Location.Column(loc) == 8) sb.Append("\n");
                if ((Location.Column(loc) == 8) && (Location.Row(loc) % 3 == 2)) sb.Append("\n");
            }
            return sb.ToString();
        }

        private int[] data = new int[81];
        private static int[] ZoneIndices = { 0, 3, 6, 27, 30, 33, 54, 57, 60 };
    }
}
