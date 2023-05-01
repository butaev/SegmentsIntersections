using SegmentsIntersections.Algs;
using SegmentsIntersections.Structs;

namespace SegmentsIntersections.Tests;

public struct SegmentData
{
    public readonly string Name;
    public readonly Vector3D P1;
    public readonly Vector3D P2;
    public readonly Vector3D Q1;
    public readonly Vector3D Q2;
    public readonly bool OnSameLine;
    public readonly bool InSamePlane;
    public readonly CalculationMethods.IntersectionResult? Intersection;

    public SegmentData(string name, Vector3D p1, Vector3D p2, Vector3D q1, Vector3D q2, bool onSameLine, bool inSamePlane,
        CalculationMethods.IntersectionResult? intersection)
    {
        Name = name;
        P1 = p1;
        P2 = p2;
        Q1 = q1;
        Q2 = q2;
        OnSameLine = onSameLine;
        InSamePlane = inSamePlane;
        Intersection = intersection;
    }
}