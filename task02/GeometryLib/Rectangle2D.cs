using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib;
public sealed class Rectangle2D
{
    private readonly Point2D topLeft;
    private readonly Point2D bottomRight;

    public Rectangle2D(Point2D topLeft, Point2D bottomRight)
    {
        if (topLeft.X >= bottomRight.X)
        {
            throw new ArgumentException("Ширина прямоугольника должна быть положительной.");
        }

        if (topLeft.Y <= bottomRight.Y)
        {
            throw new ArgumentException("Высота прямоугольника должна быть положительной.");
        }

        this.topLeft = topLeft;
        this.bottomRight = bottomRight;
    }

    public Point2D TopLeft => topLeft;

    public Point2D BottomRight => bottomRight;

    public double Width => bottomRight.X - topLeft.X;

    public double Height => topLeft.Y - bottomRight.Y;

    public double Diagonal => Math.Sqrt((Width * Width) + (Height * Height));

    public Point2D Center => new Point2D(
        (topLeft.X + bottomRight.X) / 2.0,
        (topLeft.Y + bottomRight.Y) / 2.0);

    public double Area => Width * Height;

    public double Perimeter => 2.0 * (Width + Height);

    /// <summary>
    ///  Проверяет, лежит ли точка внутри прямоугольника или на его границе.
    /// </summary>
    public bool Contains(Point2D p)
    {
        return p.X >= topLeft.X && p.X <= bottomRight.X &&
               p.Y <= topLeft.Y && p.Y >= bottomRight.Y;
    }

    /// <summary>
    ///  Проверяет, пересекается ли данный прямоугольник с другим.
    /// </summary>
    public bool IntersectsWith(Rectangle2D other)
    {
        bool separated =
            bottomRight.X < other.topLeft.X ||
            other.bottomRight.X < topLeft.X ||
            topLeft.Y < other.bottomRight.Y ||
            other.topLeft.Y < bottomRight.Y;

        return !separated;
    }

    /// <summary>
    ///  Строит минимальный прямоугольник, содержащий все указанные точки.
    /// </summary>
    public static Rectangle2D GetBoundingBox(IEnumerable<Point2D> points)
    {
        if (points is null)
        {
            throw new ArgumentNullException(nameof(points));
        }

        bool hasAny = false;
        double minX = 0, maxX = 0, minY = 0, maxY = 0;

        foreach (Point2D p in points)
        {
            if (!hasAny)
            {
                minX = maxX = p.X;
                minY = maxY = p.Y;
                hasAny = true;
            }
            else
            {
                if (p.X < minX)
                {
                    minX = p.X;
                }

                if (p.X > maxX)
                {
                    maxX = p.X;
                }

                if (p.Y < minY)
                {
                    minY = p.Y;
                }

                if (p.Y > maxY)
                {
                    maxY = p.Y;
                }
            }
        }

        if (!hasAny)
        {
            throw new ArgumentException("Невозможно построить прямоугольник по пустому набору точек.");
        }

        return new Rectangle2D(new Point2D(minX, maxY), new Point2D(maxX, minY));
    }

    public override string ToString()
    {
        return $"[{topLeft} — {bottomRight}]";
    }
}
