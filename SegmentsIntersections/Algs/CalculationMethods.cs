using SegmentsIntersections.Structs;

namespace SegmentsIntersections.Algs;

public static class CalculationMethods
{
    public abstract class IntersectionResult : IEquatable<IntersectionResult>
    {
        public abstract bool Equals(IntersectionResult? other);
        public abstract override bool Equals(object? obj);
        public abstract override int GetHashCode();
        public abstract override string ToString();
    }

    public class PointIntersectionResult : IntersectionResult
    {
        private readonly Vector3D _point;

        public PointIntersectionResult(Vector3D point)
        {
            _point = point;
        }

        public override bool Equals(IntersectionResult? other)
        {
            if (other is not PointIntersectionResult otherResult)
            {
                return false;
            }

            return _point == otherResult._point;
        }

        public override bool Equals(object? obj)
        {
            return obj is PointIntersectionResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _point.GetHashCode();
        }

        public override string ToString()
        {
            return $"Intersection is the point: {_point}";
        }
    }

    public class SegmentIntersectionResult : IntersectionResult
    {
        private readonly Segment3D _segment;

        public SegmentIntersectionResult(Segment3D segment)
        {
            _segment = segment;
        }

        public override bool Equals(IntersectionResult? other)
        {
            if (other is not SegmentIntersectionResult otherResult)
            {
                return false;
            }

            return _segment == otherResult._segment;
        }

        public override bool Equals(object? obj)
        {
            return obj is SegmentIntersectionResult other && Equals(other);
        }

        public override int GetHashCode()
        {
            return _segment.GetHashCode();
        }

        public override string ToString()
        {
            return $"Intersection is the segment: {_segment}";
        }
    }

    /// <summary>Computes intersection of two segments.</summary>
    /// <returns>Intersection result or null if there is no intersection.</returns>
    public static IntersectionResult? Intersect(Segment3D segment1, Segment3D segment2)
    {
        if (segment1.Length <= Consts.Epsilon || segment2.Length <= Consts.Epsilon)
        {
            return IntersectSegmentWithPoint(segment1, segment2);
        }

        if (OnSameLine(segment1, segment2))
        {
            return SegmentIntersect(segment1, segment2);
        }

        return IntersectInPlane(segment1, segment2);
    }

    /// <summary>Computes intersection of two segments, one of which has zero length.</summary>
    /// <returns>PointIntersectionResult or null if there is no intersection.</returns>
    private static PointIntersectionResult? IntersectSegmentWithPoint(Segment3D segment1, Segment3D segment2)
    {
        if (segment1.Length <= Consts.Epsilon)
        {
            if (segment2.Length <= Consts.Epsilon)
            {
                return ApproximatelyEqual(segment1.Start, segment2.Start)
                    ? new PointIntersectionResult(segment1.Start)
                    : null;
            }

            return segment2.Contains(segment1.Start) ? new PointIntersectionResult(segment1.Start) : null;
        }

        if (segment1.Length <= Consts.Epsilon)
        {
            return ApproximatelyEqual(segment2.Start, segment1.Start)
                ? new PointIntersectionResult(segment2.Start)
                : null;
        }

        return segment1.Contains(segment2.Start) ? new PointIntersectionResult(segment2.Start) : null;
    }

    /// <summary>Computes intersection of two segments passing through same line.</summary>
    /// <returns>Intersection result or null if there is no intersection.</returns>
    public static IntersectionResult? SegmentIntersect(Segment3D segment1, Segment3D segment2)
    {
        return GetSegmentIntersect(segment1, segment2) ?? GetSegmentIntersect(segment2, segment1);
    }

    private static IntersectionResult? GetSegmentIntersect(Segment3D segment1, Segment3D segment2)
    {
        if (segment1.Contains(segment2.Start))
        {
            if (segment1.Contains(segment2.End) && !ApproximatelyEqual(segment2.Start, segment2.End))
                return new SegmentIntersectionResult(segment2);
            if (segment2.Contains(segment1.Start) && !ApproximatelyEqual(segment2.Start, segment1.Start))
                return new SegmentIntersectionResult(new Segment3D(segment2.Start, segment1.Start));
            if (segment2.Contains(segment1.End) && !ApproximatelyEqual(segment2.Start, segment1.End))
                return new SegmentIntersectionResult(new Segment3D(segment2.Start, segment1.End));
            return new PointIntersectionResult(segment2.Start);
        }

        if (segment1.Contains(segment2.End))
        {
            if (segment1.Contains(segment2.Start) && !ApproximatelyEqual(segment2.End, segment2.Start))
                return new SegmentIntersectionResult(segment2);
            if (segment2.Contains(segment1.Start) && !ApproximatelyEqual(segment2.End, segment1.Start))
                return new SegmentIntersectionResult(new Segment3D(segment2.End, segment1.Start));
            if (segment2.Contains(segment1.End) && !ApproximatelyEqual(segment2.End, segment1.End))
                return new SegmentIntersectionResult(new Segment3D(segment2.End, segment1.End));
            return new PointIntersectionResult(segment2.End);
        }

        return null;
    }

    /// <summary>Computes intersection of segments in plane.</summary>
    /// <returns>PointIntersectionResult if there is an intersection, otherwise returns null.</returns>
    public static PointIntersectionResult? IntersectInPlane(Segment3D segment1, Segment3D segment2)
    {
        if (!InSamePlane(segment1, segment2))
        {
            return null;
        }

        var x1 = segment1.Start.X;
        var x2 = segment1.End.X;
        var x3 = segment2.Start.X;
        var x4 = segment2.End.X;
        var y1 = segment1.Start.Y;
        var y2 = segment1.End.Y;
        var y3 = segment2.Start.Y;
        var y4 = segment2.End.Y;
        var z1 = segment1.Start.Z;
        var z2 = segment1.End.Z;
        var z3 = segment2.Start.Z;
        var z4 = segment2.End.Z;
        var (s, t) = (-1d, -1d);

        if (Math.Abs(x2 - x1) > Consts.Epsilon)
        {
            (t, s) = GetParams(x1, x2, x3, x4, y1, y2, y3, y4);
            if (!Valid(t, s))
            {
                (t, s) = GetParams(x1, x2, x3, x4, z1, z2, z3, z4);
            }
        }

        if (Math.Abs(y2 - y1) > Consts.Epsilon && !Valid(t, s))
        {
            (t, s) = GetParams(y1, y2, y3, y4, x1, x2, x3, x4);
            if (!Valid(t, s))
            {
                (t, s) = GetParams(y1, y2, y3, y4, z1, z2, z3, z4);
            }
        }

        if (Math.Abs(z2 - z1) > Consts.Epsilon && !Valid(t, s))
        {
            (t, s) = GetParams(z1, z2, z3, z4, x1, x2, x3, x4);
            if (!Valid(t, s))
            {
                (t, s) = GetParams(z1, z2, z3, z4, y1, y2, y3, y4);
            }
        }

        PointIntersectionResult? result = null;
        if (t is <= 1 and >= 0 && s is <= 1 and >= 0 && Valid(t, s))
        {
            result = new PointIntersectionResult(new Vector3D(x1 + (x2 - x1) * t, y1 + (y2 - y1) * t,
                z1 + (z2 - z1) * t));
        }

        return result;

        bool Valid(double tt, double ss)
        {
            var c1 = Math.Abs(x1 + (x2 - x1) * tt - (x3 + (x4 - x3) * ss));
            var c2 = Math.Abs(y1 + (y2 - y1) * tt - (y3 + (y4 - y3) * ss));
            var c3 = Math.Abs(z1 + (z2 - z1) * tt - (z3 + (z4 - z3) * ss));
            return c1 is not double.NaN && c2 is not double.NaN && c3 is not double.NaN && c1 <= Consts.Epsilon
                   && c2 <= Consts.Epsilon && c3 <= Consts.Epsilon;
        }
    }

    private static (double t, double s) GetParams(double x1, double x2, double x3, double x4, double y1, double y2,
        double y3, double y4)
    {
        var s = ((y1 - y3) * (x2 - x1) + (y2 - y1) * (x3 - x1)) / ((y4 - y3) * (x2 - x1) - (y2 - y1) * (x4 - x3));
        var t = (x3 + (x4 - x3) * s - x1) / (x2 - x1);
        return (t, s);
    }

    /// <summary>Determines if the segments are on the same plane</summary>
    public static bool InSamePlane(Segment3D segment1, Segment3D segment2)
    {
        var v1 = new Vector3D(segment1.End.X - segment1.Start.X, segment2.Start.X - segment1.Start.X,
            segment2.End.X - segment1.Start.X);
        var v2 = new Vector3D(segment1.End.Y - segment1.Start.Y, segment2.Start.Y - segment1.Start.Y,
            segment2.End.Y - segment1.Start.Y);
        var v3 = new Vector3D(segment1.End.Z - segment1.Start.Z, segment2.Start.Z - segment1.Start.Z,
            segment2.End.Z - segment1.Start.Z);
        return Math.Abs(Determinant(v1, v2, v3)) <= Consts.Epsilon;
    }

    /// <summary>Computes determinant of 3x3 matrix [v1, v2, v3].</summary>
    public static double Determinant(Vector3D v1, Vector3D v2, Vector3D v3)
    {
        return v1.X * v2.Y * v3.Z + v2.X * v3.Y * v1.Z + v1.Y * v2.Z * v3.X - v3.X * v2.Y * v1.Z - v1.Y * v2.X * v3.Z -
               v1.X * v3.Y * v2.Z;
    }

    /// <summary>Determines if the segments are on the same line.</summary>
    public static bool OnSameLine(Segment3D segment1, Segment3D segment2)
    {
        return VectorIsOnSegmentsLine(segment1, segment2.Start) && VectorIsOnSegmentsLine(segment1, segment2.End) &&
               VectorIsOnSegmentsLine(segment2, segment1.Start) && VectorIsOnSegmentsLine(segment2, segment1.End);
    }

    /// <summary>Determines whether a line passing through a segment contains a given point.</summary>
    public static bool VectorIsOnSegmentsLine(Segment3D segment, Vector3D vector)
    {
        var cross1 = Vector3D.Cross(segment.Start, segment.Direction);
        var cross2 = Vector3D.Cross(vector, segment.Direction);
        return ApproximatelyEqual(cross1, cross2);
    }

    /// <summary>Calculates whether the segment contains the point.</summary>                                                     
    /// <returns>true if contains, false otherwise.</returns>                                                          
    private static bool Contains(this Segment3D segment, Vector3D vector)
    {
        if (!VectorIsOnSegmentsLine(segment, vector))
        {
            return false;
        }

        if (ApproximatelyEqual(segment.Start, vector) || ApproximatelyEqual(segment.End, vector))
        {
            return true;
        }

        var isZero1 = Math.Abs(segment.End.X - segment.Start.X) <= Consts.Epsilon;
        var isZero2 = Math.Abs(segment.End.Y - segment.Start.Y) <= Consts.Epsilon;
        var isZero3 = Math.Abs(segment.End.Z - segment.Start.Z) <= Consts.Epsilon;
        var condition1 = !isZero1 && (vector.X - segment.Start.X) / (segment.End.X - segment.Start.X) is >= 0 and <= 1;
        var condition2 = !isZero2 && (vector.Y - segment.Start.Y) / (segment.End.Y - segment.Start.Y) is >= 0 and <= 1;
        var condition3 = !isZero3 && (vector.Z - segment.Start.Z) / (segment.End.Z - segment.Start.Z) is >= 0 and <= 1;
        return condition1 || condition2 || condition3;
    }

    private static bool ApproximatelyEqual(Vector3D v1, Vector3D v2)
    {
        return (v1 - v2).Magnitude <= Consts.Epsilon;
    }
}