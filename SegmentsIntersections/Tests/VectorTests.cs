using SegmentsIntersections.Algs;
using SegmentsIntersections.Structs;

namespace SegmentsIntersections.Tests;

public class VectorTests
{
    public static IEnumerable<TestCaseData> DeterminantTestCaseDatas
    {
        get
        {
            yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(4, 5, 6), new Vector3D(7, 8, 9), 0);
            yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(-4, 5, 6), new Vector3D(7, 8, -9), -282);
            yield return new TestCaseData(new Vector3D(1, 2, 3), new Vector3D(5, 7, 11), new Vector3D(13, 17, 19), 24);
        }
    }

    [Test]
    [TestCaseSource(nameof(DeterminantTestCaseDatas))]
    public void DeterminantTest(Vector3D v1, Vector3D v2, Vector3D v3, double expectedResult)
    {
        var actualResult = CalculationMethods.Determinant(v1, v2, v3);

        Assert.That(Math.Abs(actualResult - expectedResult) < double.Epsilon);
    }

    public static IEnumerable<TestCaseData> VectorIsOnSegmentsLine
    {
        get
        {
            yield return new TestCaseData(new Vector3D(1, 1, 1), new Vector3D(2, 2, 2), new Vector3D(1, 1, 1), true);
            yield return new TestCaseData(new Vector3D(1, 1, 1), new Vector3D(2, 2, 2), new Vector3D(2, 2, 2), true);
            yield return new TestCaseData(new Vector3D(1, 1, 1), new Vector3D(2, 2, 2), new Vector3D(1.5, 1.5, 1.5),
                true);
            yield return new TestCaseData(new Vector3D(1, 1, 1), new Vector3D(2, 2, 2), new Vector3D(-10, -10, -10),
                true);
        }
    }

    public static IEnumerable<TestCaseData> VectorIsOubOfSegmentsLine
    {
        get
        {
            yield return new TestCaseData(new Vector3D(1, 1, 1), new Vector3D(2, 2, 2), new Vector3D(1, 2, 1), false);
            yield return new TestCaseData(new Vector3D(1, 1, 1), new Vector3D(2, 2, 2), new Vector3D(2, 1, 2), false);
        }
    }

    [Test]
    [TestCaseSource(nameof(VectorIsOnSegmentsLine))]
    [TestCaseSource(nameof(VectorIsOubOfSegmentsLine))]
    public void VectorIsOnSegmentsLineTest(Vector3D v0, Vector3D v1, Vector3D v2, bool expectedResult)
    {
        var actualResult = CalculationMethods.VectorIsOnSegmentsLine(new Segment3D(v1, v2), v0);
        Assert.AreEqual(actualResult, expectedResult);
    }
}