using SegmentsIntersections.Algs;
using SegmentsIntersections.Structs;

namespace SegmentsIntersections.Tests;

public static class SegmentTestCaseDataGenerator
{
    public static IEnumerable<TestCaseData> GetOnSameLineTestCaseData()
    {
        foreach (var data in AllData)
        {
            foreach (var testData in GetAllCombination(data.P1, data.P2, data.Q1, data.Q2, data.OnSameLine,
                         data.InSamePlane, data.Intersection))
            {
                yield return new TestCaseData(testData.s1, testData.s2, testData.onSameLine);
            }
        }
    }

    public static IEnumerable<TestCaseData> GetInSamePlaneTestCaseData()
    {
        foreach (var data in AllData)
        {
            foreach (var testData in GetAllCombination(data.P1, data.P2, data.Q1, data.Q2, data.OnSameLine,
                         data.InSamePlane, data.Intersection))
            {
                yield return new TestCaseData(testData.s1, testData.s2, testData.inSamePlane);
            }
        }
    }

    public static IEnumerable<TestCaseData> GetAllSegments()
    {
        foreach (var data in AllData)
        {
            foreach (var testData in GetAllCombination(data.P1, data.P2, data.Q1, data.Q2, data.OnSameLine,
                         data.InSamePlane, data.Intersection))
            {
                yield return new TestCaseData(testData.s1, testData.s2, testData.intersection);
            }
        }
    }

    public static IEnumerable<TestCaseData> GetSegmentsOnDifferentLine()
    {
        foreach (var data in AllData)
        {
            foreach (var testData in GetAllCombination(data.P1, data.P2, data.Q1, data.Q2, data.OnSameLine,
                         data.InSamePlane, data.Intersection))
            {
                if (data.InSamePlane && !data.OnSameLine)
                {
                    yield return new TestCaseData(testData.s1, testData.s2, testData.intersection);
                }
            }
        }
    }

    public static IEnumerable<TestCaseData> GetSegmentsOnSameLine()
    {
        foreach (var data in AllData)
        {
            foreach (var testData in GetAllCombination(data.P1, data.P2, data.Q1, data.Q2, data.OnSameLine,
                         data.InSamePlane, data.Intersection))
            {
                if (data.OnSameLine)
                {
                    yield return new TestCaseData(testData.s1, testData.s2, testData.intersection);
                }
            }
        }
    }

    private static IEnumerable<SegmentData> AllData
    {
        get
        {
            foreach (var segmentData in NestedSegmentsData)
            {
                yield return segmentData;
            }

            foreach (var segmentData in PartiallyNestedSegmentsData)
            {
                yield return segmentData;
            }

            foreach (var segmentData in NonZeroOnSameLineSegmentsData)
            {
                yield return segmentData;
            }

            foreach (var segmentData in WithZeroLengthsSegments)
            {
                yield return segmentData;
            }

            foreach (var segmentData in NonZeroOnDifferentLinesSegments)
            {
                yield return segmentData;
            }
        }
    }

    private static IEnumerable<SegmentData> NestedSegmentsData
    {
        get
        {
            yield return new SegmentData("NestedSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(1, 1, 1), new Vector3D(2, 2, 2), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(1, 1, 1), new Vector3D(2, 2, 2))));

            yield return new SegmentData("NestedSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(1.2, 1.2, 1.2), new Vector3D(2, 2, 2), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(1.2, 1.2, 1.2), new Vector3D(2, 2, 2))));

            yield return new SegmentData("NestedSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(1, 1, 1), new Vector3D(1.2, 1.2, 1.2), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(1, 1, 1), new Vector3D(1.2, 1.2, 1.2))));

            yield return new SegmentData("NestedSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(1.2, 1.2, 1.2), new Vector3D(1.8, 1.8, 1.8), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(1.2, 1.2, 1.2), new Vector3D(1.8, 1.8, 1.8))));
        }
    }

    private static IEnumerable<SegmentData> PartiallyNestedSegmentsData
    {
        get
        {
            yield return new SegmentData("PartiallyNestedSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(1.2, 1.2, 1.2), new Vector3D(2.2, 2.2, 2.2), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(1.2, 1.2, 1.2), new Vector3D(2, 2, 2))));

            yield return new SegmentData("PartiallyNestedSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(-1.2, -1.2, -1.2), new Vector3D(1.2, 1.2, 1.2), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(1, 1, 1), new Vector3D(1.2, 1.2, 1.2))));

            yield return new SegmentData("PartiallyNestedSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(-1.2, -1.2, -1.2), new Vector3D(1, 1, 1), true, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(1, 1, 1)));

            yield return new SegmentData("PartiallyNestedSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(2, 2, 2), new Vector3D(2.2, 2.2, 2.2), true, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(2, 2, 2)));
        }
    }

    private static IEnumerable<SegmentData> NonZeroOnSameLineSegmentsData
    {
        get
        {
            yield return new SegmentData("NonZeroOnSameLineSegments", new Vector3D(1), new Vector3D(2), new Vector3D(3),
                new Vector3D(4), true, true, null);
            yield return new SegmentData("NonZeroOnSameLineSegments", new Vector3D(1), new Vector3D(2),
                new Vector3D(-1), new Vector3D(0), true, true, null);

            yield return new SegmentData("NonZeroOnSameLineSegments", new Vector3D(1, 0, 0), new Vector3D(3, 0, 0),
                new Vector3D(2, 0, 0), new Vector3D(4, 0, 0), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(2, 0, 0), new Vector3D(3, 0, 0))));

            yield return new SegmentData("NonZeroOnSameLineSegments", new Vector3D(0, 1, 0), new Vector3D(0, 3, 0),
                new Vector3D(0, 2, 0), new Vector3D(0, 4, 0), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(0, 2, 0), new Vector3D(0, 3, 0))));

            yield return new SegmentData("NonZeroOnSameLineSegments", new Vector3D(0, 0, 1), new Vector3D(0, 0, 3),
                new Vector3D(0, 0, 2), new Vector3D(0, 0, 4), true, true,
                new CalculationMethods.SegmentIntersectionResult(new Segment3D(new Vector3D(0, 0, 2), new Vector3D(0, 0, 3))));
        }
    }

    private static IEnumerable<SegmentData> WithZeroLengthsSegments
    {
        get
        {
            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1), new Vector3D(1),
                new Vector3D(1), new Vector3D(1), true, true, new CalculationMethods.PointIntersectionResult(new Vector3D(1)));

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1), new Vector3D(1),
                new Vector3D(2), new Vector3D(2), true, true, null);

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1), new Vector3D(1),
                new Vector3D(1, 1, 2), new Vector3D(1, 1, 2), true, true, null);

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1), new Vector3D(2),
                new Vector3D(1), new Vector3D(1), true, true, new CalculationMethods.PointIntersectionResult(new Vector3D(1)));

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1), new Vector3D(2),
                new Vector3D(2), new Vector3D(2), true, true, new CalculationMethods.PointIntersectionResult(new Vector3D(2)));

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(1.5), new Vector3D(1.5), true, true, new CalculationMethods.PointIntersectionResult(new Vector3D(1.5)));

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1), new Vector3D(2),
                new Vector3D(-1.2), new Vector3D(-1.2), true, true, null);

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1), new Vector3D(2),
                new Vector3D(2.2), new Vector3D(2.2), true, true, null);

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1, 1, 1), new Vector3D(2, 2, 2),
                new Vector3D(2, 1, 1), new Vector3D(2, 1, 1), false, true, null);

            yield return new SegmentData("WithZeroLengthsSegments", new Vector3D(1, 1, 1), new Vector3D(2, 1, 1),
                new Vector3D(2, 1, 2), new Vector3D(2, 1, 2), false, true, null);
        }
    }

    private static IEnumerable<SegmentData> NonZeroOnDifferentLinesSegments
    {
        get
        {
            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1), new Vector3D(2),
                new Vector3D(2, 1, 2), new Vector3D(2, 2, 2), false, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(2, 2, 2)));

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1), new Vector3D(2),
                new Vector3D(2, 1, 2), new Vector3D(1, 1, 1), false, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(1, 1, 1)));

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1, 1, 1),
                new Vector3D(2, 2, 2), new Vector3D(1, 2, 1), new Vector3D(2, 1, 2), false, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(1.5, 1.5, 1.5)));

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1, 1, 1),
                new Vector3D(2, 2, 2), new Vector3D(1, 1, 2), new Vector3D(2, 2, 1), false, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(1.5, 1.5, 1.5)));

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1, 1, 1),
                new Vector3D(2, 2, 1), new Vector3D(1.5, 1, 1), new Vector3D(1.5, 2, 1), false, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(1.5, 1.5, 1)));

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1, 1, 1),
                new Vector3D(2, 2, 2), new Vector3D(1, 1.5, 1.5), new Vector3D(2, 1.5, 1.5), false, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(1.5, 1.5, 1.5)));

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1, 1, 1),
                new Vector3D(2, 2, 2), new Vector3D(1, 2, 1), new Vector3D(2, 1, 1), false, false, null);

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1, 1, 1),
                new Vector3D(2, 1, 1), new Vector3D(2, 1, 2), new Vector3D(2, 2, 2), false, false, null);

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1, 1, 1),
                new Vector3D(2, 2, 2), new Vector3D(1.8, 1.8, 1), new Vector3D(1.8, 1.8, 2), false, true,
                new CalculationMethods.PointIntersectionResult(new Vector3D(1.8)));

            yield return new SegmentData("NonZeroOnDifferentLinesSegments", new Vector3D(1, 1, 1),
                new Vector3D(2, 1, 1), new Vector3D(1, 1, 2), new Vector3D(2, 1, 2), false, true, null);
        }
    }

    private static IEnumerable<(Segment3D s1, Segment3D s2, bool onSameLine, bool inSamePlane, CalculationMethods.IntersectionResult?
            intersection)>
        GetAllCombination(
            Vector3D p1, Vector3D p2, Vector3D q1, Vector3D q2, bool onSameLine, bool inSamePlane,
            CalculationMethods.IntersectionResult? intersection)
    {
        yield return (new Segment3D(p1, p2), new Segment3D(q1, q2), onSameLine, inSamePlane, intersection);
        if (p1 == p2 && p1 == q1 && p1 == q2) yield break;
        if (q1 != q2)
        {
            yield return (new Segment3D(p1, p2), new Segment3D(q2, q1), onSameLine, inSamePlane, intersection);
        }

        if (p1 != p2)
        {
            yield return (new Segment3D(p2, p1), new Segment3D(q1, q2), onSameLine, inSamePlane, intersection);
            if (q1 != q2)
                yield return (new Segment3D(p2, p1), new Segment3D(q2, q1), onSameLine, inSamePlane, intersection);
        }

        yield return (new Segment3D(q1, q2), new Segment3D(p1, p2), onSameLine, inSamePlane, intersection);
        if (p1 != p2)
        {
            yield return (new Segment3D(q1, q2), new Segment3D(p2, p1), onSameLine, inSamePlane, intersection);
        }

        if (q1 != q2)
        {
            yield return (new Segment3D(q2, q1), new Segment3D(p1, p2), onSameLine, inSamePlane, intersection);
            if (p1 != p2)
                yield return (new Segment3D(q2, q1), new Segment3D(p2, p1), onSameLine, inSamePlane, intersection);
        }
    }
}