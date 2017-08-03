using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace SudokuSharp
{
    /// <summary>
    /// The Location class is a reference to a specific cell on a Sudoku board.
    /// It is internally represented as an integer for performance, but contains many useful methods
    /// </summary>
    public static class Location
    {
        public static int Index(int Column, int Row) { return (9 * Row) + Column; }

        public static bool IsBlockedBy(int First, int Second)
        {
            return Blocking[First].Contains(Second);
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

        public static int Zone(int Index)
        {
            switch (Index)
            {
                case 0: return 0;
                case 1: return 0;
                case 2: return 0;
                case 3: return 1;
                case 4: return 1;
                case 5: return 1;
                case 6: return 2;
                case 7: return 2;
                case 8: return 2;
                case 9: return 0;
                case 10: return 0;
                case 11: return 0;
                case 12: return 1;
                case 13: return 1;
                case 14: return 1;
                case 15: return 2;
                case 16: return 2;
                case 17: return 2;
                case 18: return 0;
                case 19: return 0;
                case 20: return 0;
                case 21: return 1;
                case 22: return 1;
                case 23: return 1;
                case 24: return 2;
                case 25: return 2;
                case 26: return 2;
                case 27: return 3;
                case 28: return 3;
                case 29: return 3;
                case 30: return 4;
                case 31: return 4;
                case 32: return 4;
                case 33: return 5;
                case 34: return 5;
                case 35: return 5;
                case 36: return 3;
                case 37: return 3;
                case 38: return 3;
                case 39: return 4;
                case 40: return 4;
                case 41: return 4;
                case 42: return 5;
                case 43: return 5;
                case 44: return 5;
                case 45: return 3;
                case 46: return 3;
                case 47: return 3;
                case 48: return 4;
                case 49: return 4;
                case 50: return 4;
                case 51: return 5;
                case 52: return 5;
                case 53: return 5;
                case 54: return 6;
                case 55: return 6;
                case 56: return 6;
                case 57: return 7;
                case 58: return 7;
                case 59: return 7;
                case 60: return 8;
                case 61: return 8;
                case 62: return 8;
                case 63: return 6;
                case 64: return 6;
                case 65: return 6;
                case 66: return 7;
                case 67: return 7;
                case 68: return 7;
                case 69: return 8;
                case 70: return 8;
                case 71: return 8;
                case 72: return 6;
                case 73: return 6;
                case 74: return 6;
                case 75: return 7;
                case 76: return 7;
                case 77: return 7;
                case 78: return 8;
                case 79: return 8;
                case 80: return 8;
            }
            throw new ArgumentOutOfRangeException("Index", Index, "Value must be between 0 and 80 inclusive");
        }



        public static int Column(int Index)
        {
            switch (Index)
            {
                case 0: return 0;
                case 1: return 1;
                case 2: return 2;
                case 3: return 3;
                case 4: return 4;
                case 5: return 5;
                case 6: return 6;
                case 7: return 7;
                case 8: return 8;
                case 9: return 0;
                case 10: return 1;
                case 11: return 2;
                case 12: return 3;
                case 13: return 4;
                case 14: return 5;
                case 15: return 6;
                case 16: return 7;
                case 17: return 8;
                case 18: return 0;
                case 19: return 1;
                case 20: return 2;
                case 21: return 3;
                case 22: return 4;
                case 23: return 5;
                case 24: return 6;
                case 25: return 7;
                case 26: return 8;
                case 27: return 0;
                case 28: return 1;
                case 29: return 2;
                case 30: return 3;
                case 31: return 4;
                case 32: return 5;
                case 33: return 6;
                case 34: return 7;
                case 35: return 8;
                case 36: return 0;
                case 37: return 1;
                case 38: return 2;
                case 39: return 3;
                case 40: return 4;
                case 41: return 5;
                case 42: return 6;
                case 43: return 7;
                case 44: return 8;
                case 45: return 0;
                case 46: return 1;
                case 47: return 2;
                case 48: return 3;
                case 49: return 4;
                case 50: return 5;
                case 51: return 6;
                case 52: return 7;
                case 53: return 8;
                case 54: return 0;
                case 55: return 1;
                case 56: return 2;
                case 57: return 3;
                case 58: return 4;
                case 59: return 5;
                case 60: return 6;
                case 61: return 7;
                case 62: return 8;
                case 63: return 0;
                case 64: return 1;
                case 65: return 2;
                case 66: return 3;
                case 67: return 4;
                case 68: return 5;
                case 69: return 6;
                case 70: return 7;
                case 71: return 8;
                case 72: return 0;
                case 73: return 1;
                case 74: return 2;
                case 75: return 3;
                case 76: return 4;
                case 77: return 5;
                case 78: return 6;
                case 79: return 7;
                case 80: return 8;
            }
            throw new ArgumentOutOfRangeException("Index", Index, "Value must be between 0 and 80 inclusive");
        }



        public static int Row(int Index)
        {
            switch (Index)
            {
                case 0: return 0;
                case 1: return 0;
                case 2: return 0;
                case 3: return 0;
                case 4: return 0;
                case 5: return 0;
                case 6: return 0;
                case 7: return 0;
                case 8: return 0;
                case 9: return 1;
                case 10: return 1;
                case 11: return 1;
                case 12: return 1;
                case 13: return 1;
                case 14: return 1;
                case 15: return 1;
                case 16: return 1;
                case 17: return 1;
                case 18: return 2;
                case 19: return 2;
                case 20: return 2;
                case 21: return 2;
                case 22: return 2;
                case 23: return 2;
                case 24: return 2;
                case 25: return 2;
                case 26: return 2;
                case 27: return 3;
                case 28: return 3;
                case 29: return 3;
                case 30: return 3;
                case 31: return 3;
                case 32: return 3;
                case 33: return 3;
                case 34: return 3;
                case 35: return 3;
                case 36: return 4;
                case 37: return 4;
                case 38: return 4;
                case 39: return 4;
                case 40: return 4;
                case 41: return 4;
                case 42: return 4;
                case 43: return 4;
                case 44: return 4;
                case 45: return 5;
                case 46: return 5;
                case 47: return 5;
                case 48: return 5;
                case 49: return 5;
                case 50: return 5;
                case 51: return 5;
                case 52: return 5;
                case 53: return 5;
                case 54: return 6;
                case 55: return 6;
                case 56: return 6;
                case 57: return 6;
                case 58: return 6;
                case 59: return 6;
                case 60: return 6;
                case 61: return 6;
                case 62: return 6;
                case 63: return 7;
                case 64: return 7;
                case 65: return 7;
                case 66: return 7;
                case 67: return 7;
                case 68: return 7;
                case 69: return 7;
                case 70: return 7;
                case 71: return 7;
                case 72: return 8;
                case 73: return 8;
                case 74: return 8;
                case 75: return 8;
                case 76: return 8;
                case 77: return 8;
                case 78: return 8;
                case 79: return 8;
                case 80: return 8;
            }
            throw new ArgumentOutOfRangeException("Index", Index, "Value must be between 0 and 80 inclusive");
        }



        public static int FlipHorizontal(int Index)
        {
            switch (Index)
            {
                case 0: return 72;
                case 1: return 73;
                case 2: return 74;
                case 3: return 75;
                case 4: return 76;
                case 5: return 77;
                case 6: return 78;
                case 7: return 79;
                case 8: return 80;
                case 9: return 63;
                case 10: return 64;
                case 11: return 65;
                case 12: return 66;
                case 13: return 67;
                case 14: return 68;
                case 15: return 69;
                case 16: return 70;
                case 17: return 71;
                case 18: return 54;
                case 19: return 55;
                case 20: return 56;
                case 21: return 57;
                case 22: return 58;
                case 23: return 59;
                case 24: return 60;
                case 25: return 61;
                case 26: return 62;
                case 27: return 45;
                case 28: return 46;
                case 29: return 47;
                case 30: return 48;
                case 31: return 49;
                case 32: return 50;
                case 33: return 51;
                case 34: return 52;
                case 35: return 53;
                case 36: return 36;
                case 37: return 37;
                case 38: return 38;
                case 39: return 39;
                case 40: return 40;
                case 41: return 41;
                case 42: return 42;
                case 43: return 43;
                case 44: return 44;
                case 45: return 27;
                case 46: return 28;
                case 47: return 29;
                case 48: return 30;
                case 49: return 31;
                case 50: return 32;
                case 51: return 33;
                case 52: return 34;
                case 53: return 35;
                case 54: return 18;
                case 55: return 19;
                case 56: return 20;
                case 57: return 21;
                case 58: return 22;
                case 59: return 23;
                case 60: return 24;
                case 61: return 25;
                case 62: return 26;
                case 63: return 9;
                case 64: return 10;
                case 65: return 11;
                case 66: return 12;
                case 67: return 13;
                case 68: return 14;
                case 69: return 15;
                case 70: return 16;
                case 71: return 17;
                case 72: return 0;
                case 73: return 1;
                case 74: return 2;
                case 75: return 3;
                case 76: return 4;
                case 77: return 5;
                case 78: return 6;
                case 79: return 7;
                case 80: return 8;
            }
            throw new ArgumentOutOfRangeException("Index", Index, "Value must be between 0 and 80 inclusive");
        }



        public static int FlipVertical(int Index)
        {
            switch (Index)
            {
                case 0: return 8;
                case 1: return 7;
                case 2: return 6;
                case 3: return 5;
                case 4: return 4;
                case 5: return 3;
                case 6: return 2;
                case 7: return 1;
                case 8: return 0;
                case 9: return 17;
                case 10: return 16;
                case 11: return 15;
                case 12: return 14;
                case 13: return 13;
                case 14: return 12;
                case 15: return 11;
                case 16: return 10;
                case 17: return 9;
                case 18: return 26;
                case 19: return 25;
                case 20: return 24;
                case 21: return 23;
                case 22: return 22;
                case 23: return 21;
                case 24: return 20;
                case 25: return 19;
                case 26: return 18;
                case 27: return 35;
                case 28: return 34;
                case 29: return 33;
                case 30: return 32;
                case 31: return 31;
                case 32: return 30;
                case 33: return 29;
                case 34: return 28;
                case 35: return 27;
                case 36: return 44;
                case 37: return 43;
                case 38: return 42;
                case 39: return 41;
                case 40: return 40;
                case 41: return 39;
                case 42: return 38;
                case 43: return 37;
                case 44: return 36;
                case 45: return 53;
                case 46: return 52;
                case 47: return 51;
                case 48: return 50;
                case 49: return 49;
                case 50: return 48;
                case 51: return 47;
                case 52: return 46;
                case 53: return 45;
                case 54: return 62;
                case 55: return 61;
                case 56: return 60;
                case 57: return 59;
                case 58: return 58;
                case 59: return 57;
                case 60: return 56;
                case 61: return 55;
                case 62: return 54;
                case 63: return 71;
                case 64: return 70;
                case 65: return 69;
                case 66: return 68;
                case 67: return 67;
                case 68: return 66;
                case 69: return 65;
                case 70: return 64;
                case 71: return 63;
                case 72: return 80;
                case 73: return 79;
                case 74: return 78;
                case 75: return 77;
                case 76: return 76;
                case 77: return 75;
                case 78: return 74;
                case 79: return 73;
                case 80: return 72;
            }
            throw new ArgumentOutOfRangeException("Index", Index, "Value must be between 0 and 80 inclusive");
        }
    }
}
