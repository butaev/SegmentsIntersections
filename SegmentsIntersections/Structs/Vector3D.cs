namespace SegmentsIntersections.Structs;

public readonly struct Vector3D: IEquatable<Vector3D>
{
    public readonly double X;
    public readonly double Y;
    public readonly double Z;
    public double Magnitude { get; }

    public Vector3D(double value = 0)
    {
        X = value;
        Y = value;
        Z = value;
        Magnitude = Math.Sqrt(3 * value * value);
    }

    public Vector3D(double x, double y, double z)
    {
        X = x;
        Y = y;
        Z = z;
        Magnitude = Math.Sqrt(x * x + y * y + z * z);
    }

    public static double Dot(Vector3D v1, Vector3D v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;

    public static Vector3D Normalize(Vector3D v) => v.Magnitude > 0 ? v / v.Magnitude : default;

    public static Vector3D Cross(Vector3D vector1, Vector3D vector2)
    {
        return new Vector3D(
            vector1.Y * vector2.Z - vector1.Z * vector2.Y,
            vector1.Z * vector2.X - vector1.X * vector2.Z,
            vector1.X * vector2.Y - vector1.Y * vector2.X
        );
    }

    public static Vector3D operator -(Vector3D v) => -1 * v;
    public static Vector3D operator +(Vector3D v1, Vector3D v2) => new(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
    public static Vector3D operator -(Vector3D v1, Vector3D v2) => v1 + -v2;
    public static Vector3D operator *(double x, Vector3D v) => new(x * v.X, x * v.Y, x * v.Z);
    public static Vector3D operator *(Vector3D v, double x) => x * v;
    public static Vector3D operator /(Vector3D v, double x) => 1 / x * v;
  
    public static bool operator ==(Vector3D v1, Vector3D v2) => v1.Equals(v2);
    public static bool operator !=(Vector3D v1, Vector3D v2) => !(v1 == v2);

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override bool Equals(object? obj)
    {
        return obj is Vector3D other && Equals(other);
    }

    public bool Equals(Vector3D other)
    {
        return X == other.X && Y == other.Y && Z == other.Z;
    }

    public override string ToString() => $"({X}, {Y}, {Z})";
}