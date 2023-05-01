using SegmentsIntersections.Algs;
using SegmentsIntersections.Structs;

namespace SegmentsIntersections.Tests;

public class IntersectionsTests
{
    private static IEnumerable<TestCaseData> AllSegmentsTestDatas => SegmentTestCaseDataGenerator.GetAllSegments();
    private static IEnumerable<TestCaseData> SegmentsOnDifferentLineTestDatas => SegmentTestCaseDataGenerator.GetSegmentsOnDifferentLine();
    private static IEnumerable<TestCaseData> SegmentsOnSameLineTestDatas => SegmentTestCaseDataGenerator.GetSegmentsOnSameLine();

    [Test]
    [TestCaseSource(nameof(SegmentsOnSameLineTestDatas))]
    public void SegmentIntersectTest(Segment3D s1, Segment3D s2, CalculationMethods.IntersectionResult? expectedIntersection)
    {
        var actualIntersection = CalculationMethods.SegmentIntersect(s1, s2);
        Assert.AreEqual(actualIntersection, expectedIntersection);
    }

    [Test]
    [TestCaseSource(nameof(SegmentsOnDifferentLineTestDatas))]
    public void IntersectInPlaneTest(Segment3D s1, Segment3D s2, CalculationMethods.IntersectionResult? expectedIntersection)
    {
        var actualIntersection = CalculationMethods.IntersectInPlane(s1, s2);
        Assert.AreEqual(actualIntersection, expectedIntersection);
    }

    [Test]
    [TestCaseSource(nameof(AllSegmentsTestDatas))]
    public void IntersectTest(Segment3D s1, Segment3D s2, CalculationMethods.IntersectionResult? expectedIntersection)
    {
        var actualResult = CalculationMethods.Intersect(s1, s2);
        Assert.AreEqual(actualResult, expectedIntersection);
    }
}