using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryLib.Tests;

public class Point2DTests
{
    [Theory]
    [MemberData(nameof(DistanceTestData))]
    public void Can_calculate_distance(Point2D a, Point2D b, double expected)
    {
        double actual = a.DistanceTo(b);
        Assert.Equal(expected, actual, precision: Point2D.Precision);
    }

    public static TheoryData<Point2D, Point2D, double> DistanceTestData()
    {
        return new TheoryData<Point2D, Point2D, double>
        {
            { new Point2D(0, 0), new Point2D(3, 4), 5 },
            { new Point2D(-1, -1), new Point2D(-1, -1), 0 },
            { new Point2D(-2, 5), new Point2D(1, 1), 5 },
        };
    }
}