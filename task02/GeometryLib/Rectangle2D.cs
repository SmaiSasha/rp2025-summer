using System;
using System.Collections.Generic;

namespace GeometryLib;

public sealed class Rectangle2D
{
    private readonly Point2D topLeft;
    private readonly Point2D bottomRight;

    /// <summary>
    /// Initializes a new instance of the <see cref="Rectangle2D"/> class.
    /// </summary>
    /// <param name="topLeft">Координаты верхнего левого угла прямоугольника.</param>
    /// <param name="bottomRight">Координаты нижнего правого угла прямоугольника.</param>
    /// <exception cref="ArgumentException">
    ///  Выбрасывается, если ширина или высота прямоугольника неположительны.
    /// </exception>
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

    /// <summary>
    ///  Возвращает верхний левый угол прямоугольника.
    /// </summary>
    public Point2D TopLeft => topLeft;

    /// <summary>
    ///  Возвращает нижний правый угол прямоугольника.
    /// </summary>
    public Point2D BottomRight => bottomRight;

    /// <summary>
    ///  Возвращает ширину прямоугольника.
    /// </summary>
    public double Width => bottomRight.X - topLeft.X;

    /// <summary>
    ///  Возвращает высоту прямоугольника.
    /// </summary>
    public double Height => topLeft.Y - bottomRight.Y;

    /// <summary>
    ///  Возвращает длину диагонали прямоугольника.
    /// </summary>
    public double Diagonal => Math.Sqrt((Width * Width) + (Height * Height));

    /// <summary>
    ///  Возвращает координаты центра прямоугольника.
    /// </summary>
    public Point2D Center => new Point2D(
        (topLeft.X + bottomRight.X) / 2.0,
        (topLeft.Y + bottomRight.Y) / 2.0);

    /// <summary>
    ///  Возвращает площадь прямоугольника.
    /// </summary>
    public double Area => Width * Height;

    /// <summary>
    ///  Возвращает периметр прямоугольника.
    /// </summary>
    public double Perimeter => 2.0 * (Width + Height);

    /// <summary>
    ///  Проверяет, лежит ли точка внутри прямоугольника или на его границе.
    /// </summary>
    /// <param name="p">Проверяемая точка.</param>
    /// <returns><c>true</c>, если точка находится внутри прямоугольника или на его границе; иначе <c>false</c>.</returns>
    public bool Contains(Point2D p)
    {
        return p.X >= topLeft.X && p.X <= bottomRight.X &&
               p.Y <= topLeft.Y && p.Y >= bottomRight.Y;
    }

    /// <summary>
    ///  Проверяет, пересекается ли данный прямоугольник с другим.
    /// </summary>
    /// <param name="other">Другой прямоугольник.</param>
    /// <returns><c>true</c>, если прямоугольники имеют общую область или касаются; иначе <c>false</c>.</returns>
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
    /// <param name="points">Набор точек.</param>
    /// <returns>Минимальный прямоугольник, охватывающий все точки.</returns>
    /// <exception cref="ArgumentNullException">Выбрасывается, если <paramref name="points"/> равен <c>null</c>.</exception>
    /// <exception cref="ArgumentException">Выбрасывается, если набор точек пустой.</exception>
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

    /// <summary>
    ///  Возвращает строковое представление текущего прямоугольника в формате [topLeft — bottomRight].
    /// </summary>
    public override string ToString()
    {
        return $"[{topLeft} — {bottomRight}]";
    }
}
