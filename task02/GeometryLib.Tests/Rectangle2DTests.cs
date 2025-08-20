using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.Tests;

public class Rectangle2DTests
{
    [Fact]
    public void Cannot_create_rectangle_with_non_positive_width()
    {
        Assert.Throws<ArgumentException>(() =>
            new Rectangle2D(new Point2D(1, 5), new Point2D(1, 0)));

        Assert.Throws<ArgumentException>(() =>
            new Rectangle2D(new Point2D(2, 5), new Point2D(1, 0)));
    }

    [Fact]
    public void Cannot_create_rectangle_with_non_positive_height()
    {
        Assert.Throws<ArgumentException>(() =>
            new Rectangle2D(new Point2D(0, 1), new Point2D(5, 1)));

        Assert.Throws<ArgumentException>(() =>
            new Rectangle2D(new Point2D(0, 0), new Point2D(5, 1)));
    }

    [Theory]
    [MemberData(nameof(PropertiesTestData))]
    public void Can_calculate_properties(Rectangle2D rect, double width, double height, double area, double perimeter, double diagonal, Point2D center)
    {
        Assert.Equal(width, rect.Width, precision: Point2D.Precision);
        Assert.Equal(height, rect.Height, precision: Point2D.Precision);
        Assert.Equal(area, rect.Area, precision: Point2D.Precision);
        Assert.Equal(perimeter, rect.Perimeter, precision: Point2D.Precision);
        Assert.Equal(diagonal, rect.Diagonal, precision: Point2D.Precision);
        Assert.Equal(center, rect.Center);
    }

    public static TheoryData<Rectangle2D, double, double, double, double, double, Point2D> PropertiesTestData()
    {
        return new TheoryData<Rectangle2D, double, double, double, double, double, Point2D>
        {
            { new Rectangle2D(new Point2D(0, 10), new Point2D(10, 0)), 10, 10, 100, 40, Math.Sqrt(200), new Point2D(5, 5) },
        };
    }

    [Theory]
    [MemberData(nameof(ContainsTestData))]
    public void Can_check_point_containment(Rectangle2D rect, Point2D p, bool expected)
    {
        bool actual = rect.Contains(p);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<Rectangle2D, Point2D, bool> ContainsTestData()
    {
        Rectangle2D rect = new Rectangle2D(new Point2D(0, 10), new Point2D(10, 0));
        return new TheoryData<Rectangle2D, Point2D, bool>
        {
            { rect, new Point2D(5, 5), true },
            { rect, new Point2D(0, 5), true },
            { rect, new Point2D(10, 10), true },
            { rect, new Point2D(-1, 5), false },
            { rect, new Point2D(5, -0.1), false },
        };
    }

    [Theory]
    [MemberData(nameof(IntersectsTestData))]
    public void Can_check_intersection(Rectangle2D a, Rectangle2D b, bool expected)
    {
        bool actual = a.IntersectsWith(b);
        Assert.Equal(expected, actual);
    }

    public static TheoryData<Rectangle2D, Rectangle2D, bool> IntersectsTestData()
    {
        Rectangle2D r1 = new Rectangle2D(new Point2D(0, 10), new Point2D(10, 0));
        Rectangle2D r2 = new Rectangle2D(new Point2D(12, 8), new Point2D(20, 2));  // нет пересечения
        Rectangle2D r3 = new Rectangle2D(new Point2D(10, 8), new Point2D(18, 2));  // касаются
        Rectangle2D r4 = new Rectangle2D(new Point2D(5, 12), new Point2D(12, 5));  // пересекаются

        return new TheoryData<Rectangle2D, Rectangle2D, bool>
        {
            { r1, r2, false },
            { r1, r3, true },
            { r1, r4, true },
        };
    }

    [Theory]
    [MemberData(nameof(BoundingBoxTestData))]
    public void Can_get_bounding_box(IEnumerable<Point2D> points, Rectangle2D expected)
    {
        Rectangle2D actual = Rectangle2D.GetBoundingBox(points);
        Assert.Equal(expected.TopLeft, actual.TopLeft);
        Assert.Equal(expected.BottomRight, actual.BottomRight);
    }

    public static TheoryData<IEnumerable<Point2D>, Rectangle2D> BoundingBoxTestData()
    {
        return new TheoryData<IEnumerable<Point2D>, Rectangle2D>
        {
            {
                new List<Point2D>
                {
                    new Point2D(0, 0),
                    new Point2D(10, 10),
                    new Point2D(5, 3),
                },
                new Rectangle2D(new Point2D(0, 10), new Point2D(10, 0))
            },
            {
                new List<Point2D>
                {
                    new Point2D(-2, 4.5),
                    new Point2D(3, -1),
                },
                new Rectangle2D(new Point2D(-2, 4.5), new Point2D(3, -1))
            },
        };
    }

    [Fact]
    public void Cannot_get_bounding_box_for_empty_collection()
    {
        Assert.Throws<ArgumentException>(() =>
            Rectangle2D.GetBoundingBox(new List<Point2D>()));
    }
}
