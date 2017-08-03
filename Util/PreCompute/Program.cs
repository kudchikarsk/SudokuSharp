using System;

namespace PreCompute
{
    class Program
    {
        static void WriteMethod(string MethodName, int[] Data)
        {
            Console.WriteLine("public static int {0}(int Index) {{", MethodName);
            Console.WriteLine("switch(Index) {");
            for (int i = 0; i < 81; i++)
                Console.WriteLine("case {0}: return {1};", i, Data[i]);
            Console.WriteLine("}");
            Console.WriteLine("throw new ArgumentOutOfRangeException(\"Index\", Index, \"Value must be between 0 and 80 inclusive\");");
            Console.WriteLine("}");
        }

        public static int Index(int Column, int Row)
            => Row * 9 + Column;

        static void Main(string[] args)
        {
            Console.WriteLine("Pre-computing Location data structures.");

            var z = new int[81];
            var c = new int[81];
            var r = new int[81];
            var fv = new int[81];
            var fh = new int[81];

            for (int i=0; i<81; i++)
            {
                r[i] = i / 9;
                c[i] = i % 9;
                z[i] = r[i] - (r[i] % 3) + c[i]/3;
                fv[i] = Index(8 - c[i], r[i]);
                fh[i] = Index(c[i], 8 - r[i]);
            }

            Console.Write("\n\n\n");
            WriteMethod("Zone", z);
            Console.Write("\n\n\n");
            WriteMethod("Column", c);
            Console.Write("\n\n\n");
            WriteMethod("Row", r);
            Console.Write("\n\n\n");
            WriteMethod("FlipHorizontal", fh);
            Console.Write("\n\n\n");
            WriteMethod("FlipVertical", fv);
        }
    }
}