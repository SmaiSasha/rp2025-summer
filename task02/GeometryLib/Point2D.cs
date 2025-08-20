using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib;

public readonly struct Point2D(double x, double y)
    : IEquatable<Point2D>
{
    // Максимальное отклонение, при котором координаты считаются равными.
    public const double Tolerance = 1e-10;

    // Количество знаков после запятой у максимального отклонения.
    public const int Precision = 10;

    /// <summary>
    ///  Координата по оси Ox.
    /// </summary>
    public double X { get; } = x;

    /// <summary>
    ///  Координата по оси Oy.
    /// </summary>
    public double Y { get; } = y;

    public static bool operator ==(Point2D left, Point2D right) => left.Equals(right);

    public static bool operator !=(Point2D left, Point2D right) => !(left == right);

    /// <summary>
    ///  Евклидово расстояние до другой точки.
    /// </summary>
    public double DistanceTo(Point2D other)
    {
        double dx = X - other.X;
        double dy = Y - other.Y;
        return Math.Sqrt((dx * dx) + (dy * dy));
    }

    public bool Equals(Point2D other)
    {
        return Math.Abs(X - other.X) < Tolerance
               && Math.Abs(Y - other.Y) < Tolerance;
    }

    public override bool Equals(object? obj)
    {
        return obj is Point2D other && Equals(other);
    }

    public override int GetHashCode()
    {
        return (X, Y).GetHashCode();
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}
