using SegmentsIntersections.Algs;
using SegmentsIntersections.Structs;

namespace SegmentsIntersections.Tests;

public class SegmentTests
{
    private static IEnumerable<TestCaseData> OnSameLineTestDatas => SegmentTestCaseDataGenerator.GetOnSameLineTestCaseData();
    private static IEnumerable<TestCaseData> InSamePlaneTestCaseDatas => SegmentTestCaseDataGenerator.GetInSamePlaneTestCaseData();
   
    [Test]
    [TestCaseSource(nameof(OnSameLineTestDatas))]
    public void OnSameLineTest(Segment3D s1, Segment3D s2, bool expectedResult)
    {
        Assert.AreEqual(CalculationMethods.OnSameLine(s1, s2), expectedResult);
    }

    [Test]
    [TestCaseSource(nameof(InSamePlaneTestCaseDatas))]
    public void InSamePlaneTest(Segment3D s1, Segment3D s2, bool expectedResult)
    {
        Assert.AreEqual(CalculationMethods.InSamePlane(s1, s2), expectedResult);
    }
}