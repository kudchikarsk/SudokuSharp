﻿namespace SudokuSharp
{
    public partial class Board
    {
        public Board FillSequential()
        {
            var work = new Board(this);

            Constraints data = new Constraints(work);

            if (SequentialRecursion(work, data, 0))
                return work;

            return null;
        }

        private static bool SequentialRecursion(Board work, Constraints data, int Index)
        {
            if (Index == 81)
                return true;

            if (work[Index] > 0)
                return SequentialRecursion(work, data, Index + 1);

            for (int i = 1; i < 10; i++)
            {
                if (!data.InRow[i, Location.Row(Index)] && !data.InColumn[i, Location.Column(Index)] && !data.InZone[i, Location.Zone(Index)])
                {
                    work.data[Index] = i;
                    data.InRow[i, Location.Row(Index)] = data.InColumn[i, Location.Column(Index)] = data.InZone[i, Location.Zone(Index)] = true;
                    if (SequentialRecursion(work, data, Index + 1))
                        return true;
                    data.InRow[i, Location.Row(Index)] = data.InColumn[i, Location.Column(Index)] = data.InZone[i, Location.Zone(Index)] = false;
                }
            }

            work.data[Index] = 0;
            return false;
        }
    }
}