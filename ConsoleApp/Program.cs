using SegmentsIntersections.Algs;
using SegmentsIntersections.Structs;

namespace ConsoleApp;

internal static class Program
{
    private static void Main()
    {
        var v1 = new Vector3D(2, 3, 4);
        var v2 = new Vector3D(4, 7, 10);
        var v3 = new Vector3D(0, 3, 6);
        var v4 = new Vector3D(6, 7, 8);
        var v5 = new Vector3D(1, 1, 1);
        var v6 = new Vector3D(5, 9, 13);
        var s12 = new Segment3D(v1, v2);
        var s34 = new Segment3D(v3, v4);
        var s52 = new Segment3D(v5, v2);
        var s16 = new Segment3D(v1, v6);
        var s15 = new Segment3D(v1, v5);

        PrintIntersectionResult(s12, s34);
        PrintIntersectionResult(s52, s16);
        PrintIntersectionResult(s15, s34);
    }

    private static void PrintIntersectionResult(Segment3D s1, Segment3D s2)
    {
        var intersection = CalculationMethods.Intersect(s1, s2);
        if (intersection != null)
        {
            Console.WriteLine(intersection);
        }
        else
        {
            Console.WriteLine("There is no intersection");
        }
    }
}