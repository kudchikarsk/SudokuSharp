﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SudokuSharp
{
    /// <summary>
    /// The Location class is a reference to a specific cell on a Sudoku board.
    /// It is internally represented as an integer for performance, but contains many useful methods
    /// </summary>
    public class Location
    {
        public static int Index(int Column, int Row) { return (9 * Row) + Column; }

        public static int Row(int Index) { return (Index / 9); }

        public static int Column(int Index) { return (Index % 9); }

        public static int Zone(int Index) { return Row(Index) - (Row(Index) % 3) + (Column(Index) / 3); }

        public static int FlipHorizontal(int Origin) { return Index(8 - Column(Origin), Row(Origin)); }

        public static int FlipVertical(int Origin) { return Index(Column(Origin), 8 - Row(Origin)); }

        public bool IsBlockedBy(int First, int Second)
        {
            return (Row(First) == Row(Second)) || (Column(First) == Column(Second)) || (Zone(First) == Zone(Second));
        }

        public static ReadOnlyCollection<int> All = new ReadOnlyCollection<int>(new int[81]
        {
            00, 1, 2, 3, 4, 5, 6, 7, 8,
            09,10,11,12,13,14,15,16,17,
            18,19,20,21,22,23,24,25,26,
            27,28,29,30,31,32,33,34,35,
            36,37,38,39,40,41,42,43,44,
            45,46,47,48,49,50,51,52,53,
            54,55,56,57,58,59,60,61,62,
            63,64,65,66,67,68,69,70,71,
            72,73,74,75,76,77,78,79,80
        });

        public static ReadOnlyCollection<ReadOnlyCollection<int>> Blocking = new ReadOnlyCollection<ReadOnlyCollection<int>>(
            new ReadOnlyCollection<int>[] {
    new ReadOnlyCollection<int>(new int[] {1,2,3,4,5,6,7,8,9,10,11,18,19,20,27,36,45,54,63,72}),
    new ReadOnlyCollection<int>(new int[] {0,2,3,4,5,6,7,8,9,10,11,18,19,20,28,37,46,55,64,73}),
    new ReadOnlyCollection<int>(new int[] {0,1,3,4,5,6,7,8,9,10,11,18,19,20,29,38,47,56,65,74}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,4,5,6,7,8,12,13,14,21,22,23,30,39,48,57,66,75}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,3,5,6,7,8,12,13,14,21,22,23,31,40,49,58,67,76}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,3,4,6,7,8,12,13,14,21,22,23,32,41,50,59,68,77}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,3,4,5,7,8,15,16,17,24,25,26,33,42,51,60,69,78}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,3,4,5,6,8,15,16,17,24,25,26,34,43,52,61,70,79}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,3,4,5,6,7,15,16,17,24,25,26,35,44,53,62,71,80}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,10,11,12,13,14,15,16,17,18,19,20,27,36,45,54,63,72}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,9,11,12,13,14,15,16,17,18,19,20,28,37,46,55,64,73}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,9,10,12,13,14,15,16,17,18,19,20,29,38,47,56,65,74}),
    new ReadOnlyCollection<int>(new int[] {3,4,5,9,10,11,13,14,15,16,17,21,22,23,30,39,48,57,66,75}),
    new ReadOnlyCollection<int>(new int[] {3,4,5,9,10,11,12,14,15,16,17,21,22,23,31,40,49,58,67,76}),
    new ReadOnlyCollection<int>(new int[] {3,4,5,9,10,11,12,13,15,16,17,21,22,23,32,41,50,59,68,77}),
    new ReadOnlyCollection<int>(new int[] {6,7,8,9,10,11,12,13,14,16,17,24,25,26,33,42,51,60,69,78}),
    new ReadOnlyCollection<int>(new int[] {6,7,8,9,10,11,12,13,14,15,17,24,25,26,34,43,52,61,70,79}),
    new ReadOnlyCollection<int>(new int[] {6,7,8,9,10,11,12,13,14,15,16,24,25,26,35,44,53,62,71,80}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,9,10,11,19,20,21,22,23,24,25,26,27,36,45,54,63,72}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,9,10,11,18,20,21,22,23,24,25,26,28,37,46,55,64,73}),
    new ReadOnlyCollection<int>(new int[] {0,1,2,9,10,11,18,19,21,22,23,24,25,26,29,38,47,56,65,74}),
    new ReadOnlyCollection<int>(new int[] {3,4,5,12,13,14,18,19,20,22,23,24,25,26,30,39,48,57,66,75}),
    new ReadOnlyCollection<int>(new int[] {3,4,5,12,13,14,18,19,20,21,23,24,25,26,31,40,49,58,67,76}),
    new ReadOnlyCollection<int>(new int[] {3,4,5,12,13,14,18,19,20,21,22,24,25,26,32,41,50,59,68,77}),
    new ReadOnlyCollection<int>(new int[] {6,7,8,15,16,17,18,19,20,21,22,23,25,26,33,42,51,60,69,78}),
    new ReadOnlyCollection<int>(new int[] {6,7,8,15,16,17,18,19,20,21,22,23,24,26,34,43,52,61,70,79}),
    new ReadOnlyCollection<int>(new int[] {6,7,8,15,16,17,18,19,20,21,22,23,24,25,35,44,53,62,71,80}),
    new ReadOnlyCollection<int>(new int[] {0,9,18,28,29,30,31,32,33,34,35,36,37,38,45,46,47,54,63,72}),
    new ReadOnlyCollection<int>(new int[] {1,10,19,27,29,30,31,32,33,34,35,36,37,38,45,46,47,55,64,73}),
    new ReadOnlyCollection<int>(new int[] {2,11,20,27,28,30,31,32,33,34,35,36,37,38,45,46,47,56,65,74}),
    new ReadOnlyCollection<int>(new int[] {3,12,21,27,28,29,31,32,33,34,35,39,40,41,48,49,50,57,66,75}),
    new ReadOnlyCollection<int>(new int[] {4,13,22,27,28,29,30,32,33,34,35,39,40,41,48,49,50,58,67,76}),
    new ReadOnlyCollection<int>(new int[] {5,14,23,27,28,29,30,31,33,34,35,39,40,41,48,49,50,59,68,77}),
    new ReadOnlyCollection<int>(new int[] {6,15,24,27,28,29,30,31,32,34,35,42,43,44,51,52,53,60,69,78}),
    new ReadOnlyCollection<int>(new int[] {7,16,25,27,28,29,30,31,32,33,35,42,43,44,51,52,53,61,70,79}),
    new ReadOnlyCollection<int>(new int[] {8,17,26,27,28,29,30,31,32,33,34,42,43,44,51,52,53,62,71,80}),
    new ReadOnlyCollection<int>(new int[] {0,9,18,27,28,29,37,38,39,40,41,42,43,44,45,46,47,54,63,72}),
    new ReadOnlyCollection<int>(new int[] {1,10,19,27,28,29,36,38,39,40,41,42,43,44,45,46,47,55,64,73}),
    new ReadOnlyCollection<int>(new int[] {2,11,20,27,28,29,36,37,39,40,41,42,43,44,45,46,47,56,65,74}),
    new ReadOnlyCollection<int>(new int[] {3,12,21,30,31,32,36,37,38,40,41,42,43,44,48,49,50,57,66,75}),
    new ReadOnlyCollection<int>(new int[] {4,13,22,30,31,32,36,37,38,39,41,42,43,44,48,49,50,58,67,76}),
    new ReadOnlyCollection<int>(new int[] {5,14,23,30,31,32,36,37,38,39,40,42,43,44,48,49,50,59,68,77}),
    new ReadOnlyCollection<int>(new int[] {6,15,24,33,34,35,36,37,38,39,40,41,43,44,51,52,53,60,69,78}),
    new ReadOnlyCollection<int>(new int[] {7,16,25,33,34,35,36,37,38,39,40,41,42,44,51,52,53,61,70,79}),
    new ReadOnlyCollection<int>(new int[] {8,17,26,33,34,35,36,37,38,39,40,41,42,43,51,52,53,62,71,80}),
    new ReadOnlyCollection<int>(new int[] {0,9,18,27,28,29,36,37,38,46,47,48,49,50,51,52,53,54,63,72}),
    new ReadOnlyCollection<int>(new int[] {1,10,19,27,28,29,36,37,38,45,47,48,49,50,51,52,53,55,64,73}),
    new ReadOnlyCollection<int>(new int[] {2,11,20,27,28,29,36,37,38,45,46,48,49,50,51,52,53,56,65,74}),
    new ReadOnlyCollection<int>(new int[] {3,12,21,30,31,32,39,40,41,45,46,47,49,50,51,52,53,57,66,75}),
    new ReadOnlyCollection<int>(new int[] {4,13,22,30,31,32,39,40,41,45,46,47,48,50,51,52,53,58,67,76}),
    new ReadOnlyCollection<int>(new int[] {5,14,23,30,31,32,39,40,41,45,46,47,48,49,51,52,53,59,68,77}),
    new ReadOnlyCollection<int>(new int[] {6,15,24,33,34,35,42,43,44,45,46,47,48,49,50,52,53,60,69,78}),
    new ReadOnlyCollection<int>(new int[] {7,16,25,33,34,35,42,43,44,45,46,47,48,49,50,51,53,61,70,79}),
    new ReadOnlyCollection<int>(new int[] {8,17,26,33,34,35,42,43,44,45,46,47,48,49,50,51,52,62,71,80}),
    new ReadOnlyCollection<int>(new int[] {0,9,18,27,36,45,55,56,57,58,59,60,61,62,63,64,65,72,73,74}),
    new ReadOnlyCollection<int>(new int[] {1,10,19,28,37,46,54,56,57,58,59,60,61,62,63,64,65,72,73,74}),
    new ReadOnlyCollection<int>(new int[] {2,11,20,29,38,47,54,55,57,58,59,60,61,62,63,64,65,72,73,74}),
    new ReadOnlyCollection<int>(new int[] {3,12,21,30,39,48,54,55,56,58,59,60,61,62,66,67,68,75,76,77}),
    new ReadOnlyCollection<int>(new int[] {4,13,22,31,40,49,54,55,56,57,59,60,61,62,66,67,68,75,76,77}),
    new ReadOnlyCollection<int>(new int[] {5,14,23,32,41,50,54,55,56,57,58,60,61,62,66,67,68,75,76,77}),
    new ReadOnlyCollection<int>(new int[] {6,15,24,33,42,51,54,55,56,57,58,59,61,62,69,70,71,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {7,16,25,34,43,52,54,55,56,57,58,59,60,62,69,70,71,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {8,17,26,35,44,53,54,55,56,57,58,59,60,61,69,70,71,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {0,9,18,27,36,45,54,55,56,64,65,66,67,68,69,70,71,72,73,74}),
    new ReadOnlyCollection<int>(new int[] {1,10,19,28,37,46,54,55,56,63,65,66,67,68,69,70,71,72,73,74}),
    new ReadOnlyCollection<int>(new int[] {2,11,20,29,38,47,54,55,56,63,64,66,67,68,69,70,71,72,73,74}),
    new ReadOnlyCollection<int>(new int[] {3,12,21,30,39,48,57,58,59,63,64,65,67,68,69,70,71,75,76,77}),
    new ReadOnlyCollection<int>(new int[] {4,13,22,31,40,49,57,58,59,63,64,65,66,68,69,70,71,75,76,77}),
    new ReadOnlyCollection<int>(new int[] {5,14,23,32,41,50,57,58,59,63,64,65,66,67,69,70,71,75,76,77}),
    new ReadOnlyCollection<int>(new int[] {6,15,24,33,42,51,60,61,62,63,64,65,66,67,68,70,71,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {7,16,25,34,43,52,60,61,62,63,64,65,66,67,68,69,71,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {8,17,26,35,44,53,60,61,62,63,64,65,66,67,68,69,70,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {0,9,18,27,36,45,54,55,56,63,64,65,73,74,75,76,77,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {1,10,19,28,37,46,54,55,56,63,64,65,72,74,75,76,77,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {2,11,20,29,38,47,54,55,56,63,64,65,72,73,75,76,77,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {3,12,21,30,39,48,57,58,59,66,67,68,72,73,74,76,77,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {4,13,22,31,40,49,57,58,59,66,67,68,72,73,74,75,77,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {5,14,23,32,41,50,57,58,59,66,67,68,72,73,74,75,76,78,79,80}),
    new ReadOnlyCollection<int>(new int[] {6,15,24,33,42,51,60,61,62,69,70,71,72,73,74,75,76,77,79,80}),
    new ReadOnlyCollection<int>(new int[] {7,16,25,34,43,52,60,61,62,69,70,71,72,73,74,75,76,77,78,80}),
    new ReadOnlyCollection<int>(new int[] {8,17,26,35,44,53,60,61,62,69,70,71,72,73,74,75,76,77,78,79})
    }
            );
    }
}
