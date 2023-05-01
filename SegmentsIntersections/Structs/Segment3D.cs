namespace SegmentsIntersections.Structs;

public readonly struct Segment3D: IEquatable<Segment3D>
{
    public readonly Vector3D Start;
    public readonly Vector3D End;
    public readonly Vector3D Direction;
    public readonly double Length;

    public Segment3D(Vector3D start, Vector3D end)
    {
        Start = start;
        End = end;
        Direction = end - start;
        Length = Direction.Magnitude;
    }

    public static bool operator ==(Segment3D s1, Segment3D s2) => s1.Equals(s2);

    public static bool operator !=(Segment3D s1, Segment3D s2) => !(s1 == s2);

    public bool Equals(Segment3D other)
    {
        return Start == other.Start && End == other.End || Start == other.End && End == other.Start;
    }

    public override bool Equals(object? obj)
    {
        return obj is Segment3D other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }

    public override string ToString() => $"[{Start}, {End}]";
}