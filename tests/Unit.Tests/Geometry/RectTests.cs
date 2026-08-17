// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class RectTests
{
    [Test]
    public async Task AreaShouldCalculateWidthAndHeight()
    {
        Rect rect = new(10f, 20f, 30f, 40f);
        await rect.Area.Should().BeEqualTo(1200f);
    }

    [Test]
    public async Task PositionShouldReturnXAndYAsPoint()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Point position = rect.Position;

        await position.X.Should().BeEqualTo(10f);
        await position.Y.Should().BeEqualTo(20f);
    }

    [Test]
    public async Task UpdatingPositionShouldChangeXAndY()
    {
        Rect rect = new(10f, 20f, 30f, 40f)
        {
            Position = new Point(50, 60)
        };

        await rect.X.Should().BeEqualTo(50f);
        await rect.Y.Should().BeEqualTo(60f);
    }

    [Test]
    public async Task SizeShouldReturnWidthAndHeightAsSize()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Size size = rect.Size;

        await size.Width.Should().BeEqualTo(30f);
        await size.Height.Should().BeEqualTo(40f);
    }

    [Test]
    public async Task SizeUpdatingShouldChangeWidthAndHeight()
    {
        Rect rect = new(10, 20, 30, 40)
        {
            Size = new Size(50, 60)
        };

        await rect.Width.Should().BeEqualTo(50f);
        await rect.Height.Should().BeEqualTo(60f);
    }

    [Test]
    public async Task RightShouldCalculateXAndWidth()
    {
        Rect rect = new(10f, 20f, 30f, 40f);
        await rect.Right.Should().BeEqualTo(40f);
    }

    [Test]
    public async Task BottomShouldCalculateYAndHeight()
    {
        Rect rect = new(10f, 20f, 30f, 40f);
        await rect.Bottom.Should().BeEqualTo(60f);
    }

    [Test]
    public async Task CenterShouldCalculateCorrectly()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Point center = rect.Center;

        await center.X.Should().BeEqualTo(25f);
        await center.Y.Should().BeEqualTo(40f);
    }

    [Test]
    public async Task TopLeftShouldUseLeftAndTopCoordinates()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Point topLeft = rect.TopLeft;

        await topLeft.X.Should().BeEqualTo(10f);
        await topLeft.Y.Should().BeEqualTo(20f);
    }

    [Test]
    public async Task TopRightShouldUseRightAndTopCoordinates()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Point topRight = rect.TopRight;

        await topRight.X.Should().BeEqualTo(40f);
        await topRight.Y.Should().BeEqualTo(20f);
    }

    [Test]
    public async Task BottomLeftShouldUseLeftAndBottomCoordinates()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Point bottomLeft = rect.BottomLeft;

        await bottomLeft.X.Should().BeEqualTo(10f);
        await bottomLeft.Y.Should().BeEqualTo(60f);
    }

    [Test]
    public async Task BottomRightShouldUseRightAndBottomCoordinates()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Point bottomRight = rect.BottomRight;

        await bottomRight.X.Should().BeEqualTo(40f);
        await bottomRight.Y.Should().BeEqualTo(60f);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        Rect rect = new(10f, 20f, 0f, 15f);
        await rect.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        Rect rect = new(10f, 20f, 15f, 0f);
        await rect.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthAndHeightIsZero()
    {
        Rect rect = new(10f, 20f, 0f, 0f);
        await rect.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenWidthAndHeightAreNotZero()
    {
        Rect rect = new(10f, 20f, 15f, 15f);
        await rect.IsEmpty.Should().BeFalse();
    }

    [Test]
    public async Task ZeroShouldReturnRectIntWithAllComponentsSetToZero()
    {
        Rect zero = Rect.Zero;

        await zero.X.Should().BeZero();
        await zero.Y.Should().BeZero();
        await zero.Width.Should().BeZero();
        await zero.Height.Should().BeZero();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenPointIsInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point point = new(20f, 20f);

        await rect.Contains(point).Should().BeTrue();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenPointIsOnEdgeOfRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point point = new(10f, 20f);

        await rect.Contains(point).Should().BeTrue();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenPointIsOutsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point point = new(50f, 50f);

        await rect.Contains(point).Should().BeFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 30f);
        Point point = new(10f, 20f);

        await rect.Contains(point).Should().BeFalse();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenRectIsFullyContainedWithinTheRect()
    {
        Rect left = new(0f, 0f, 30f, 30f);
        Rect right = new(0f, 0f, 30f, 25f);

        await left.Contains(right).Should().BeTrue();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenRectIsFullyContainedWithinTheEdgeOfTheRect()
    {
        Rect left = new(0f, 0f, 30f, 30f);
        Rect right = new(0f, 0f, 30f, 30f);

        await left.Contains(right).Should().BeTrue();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenLeftRectIsEmpty()
    {
        Rect left = new(0f, 0f, 0f, 30f);
        Rect right = new(0f, 0f, 30f, 30f);

        await left.Contains(right).Should().BeFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenRightRectIsEmpty()
    {
        Rect left = new(0f, 0f, 30f, 30f);
        Rect right = new(0f, 0f, 0f, 30f);

        await left.Contains(right).Should().BeFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenRectIsNotFullyContainedWithinTheRect()
    {
        Rect left = new(0f, 0f, 30f, 30f);
        Rect right = new(100f, 0f, 30f, 30f);

        await left.Contains(right).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenAllPointIsInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point[] points = [new(15f, 15f), new(20f, 20f), new(30f, 30f)];

        await rect.ContainsAll(points).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenNoPointsAreInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point[] points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await rect.ContainsAll(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 30f);
        Point[] points = [new(10f, 20f), new(15f, 25f)];

        await rect.ContainsAll(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenThereIsNoPoints()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        await rect.ContainsAll([]).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsArrayAndContainsAllPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point[] points = [new(10f, 10f), new(20f, 20f), new(30f, 30f)];

        await rect.ContainsAll(points.AsEnumerable()).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsListAndContainsAllPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        List<Point> points = [new(10f, 15f), new(20f, 20f), new(30f, 30f)];

        await rect.ContainsAll(points).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsEnumerableAndContainsAllPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        IEnumerable<Point> points = PointsAsEnumerable();

        await rect.ContainsAll(points).Should().BeTrue();

        static IEnumerable<Point> PointsAsEnumerable()
        {
            yield return new Point(10f, 10f);
            yield return new Point(20f, 20f);
            yield return new Point(30f, 30f);
        }
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsArrayAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point[] points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await rect.ContainsAll(points.AsEnumerable()).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsListAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        List<Point> points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await rect.ContainsAll(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsEnumerableAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        IEnumerable<Point> points = PointsAsEnumerable();

        await rect.ContainsAll(points).Should().BeFalse();

        static IEnumerable<Point> PointsAsEnumerable()
        {
            yield return new Point(5f, 5f);
            yield return new Point(50f, 50f);
            yield return new Point(100f, 100f);
        }
    }

    [Test]
    public async Task ContainsAllEnumerableShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 30f, 0f);
        Point[] points = [];

        await rect.ContainsAll(points.AsEnumerable()).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenAnyPointIsInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point[] points = [new(5f, 5f), new(20f, 20f), new(50f, 50f)];

        await rect.ContainsAny(points).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenNoPointsAreInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point[] points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await rect.ContainsAny(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 30f);
        Point[] points = [new(10f, 20f), new(15f, 25f)];

        await rect.ContainsAny(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenThereIsNoPoints()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        await rect.ContainsAny([]).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsArrayAndContainsPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point[] points = [new(5f, 5f), new(20f, 20f), new(50f, 50f)];

        await rect.ContainsAny(points.AsEnumerable()).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsListAndContainsPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        List<Point> points = [new(5f, 5f), new(20f, 20f), new(50f, 50f)];

        await rect.ContainsAny(points).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsEnumerableAndContainsPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        IEnumerable<Point> points = PointsAsEnumerable();

        await rect.ContainsAny(points).Should().BeTrue();

        static IEnumerable<Point> PointsAsEnumerable()
        {
            yield return new Point(5f, 5f);
            yield return new Point(20f, 20f);
            yield return new Point(50f, 50f);
        }
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsArrayAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point[] points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await rect.ContainsAny(points.AsEnumerable()).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsListAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        List<Point> points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await rect.ContainsAny(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsEnumerableAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        IEnumerable<Point> points = PointsAsEnumerable();

        await rect.ContainsAny(points).Should().BeFalse();

        static IEnumerable<Point> PointsAsEnumerable()
        {
            yield return new Point(5f, 5f);
            yield return new Point(50f, 50f);
            yield return new Point(100f, 100f);
        }
    }

    [Test]
    public async Task ContainsAnyEnumerableShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 30f, 0f);
        Point[] points = [];

        await rect.ContainsAny(points.AsEnumerable()).Should().BeFalse();
    }

    [Test]
    public async Task DeconstructShouldAssignComponentsToOutParameters()
    {
        (float x, float y, float width, float height) = new Rect(10, 20, 30, 40);

        await x.Should().BeEqualTo(10f);
        await y.Should().BeEqualTo(20f);
        await width.Should().BeEqualTo(30f);
        await height.Should().BeEqualTo(40f);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsRectWhenAlreadyContainsThePoint()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point point = new(20f, 20f);

        rect.Encapsulate(point);
        await rect.Should().BeEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsRectToIncludePoint()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Point point = new(50f, 50f);

        rect.Encapsulate(point);

        Rect expectedRect = new(10f, 10f, 40f, 40f);
        await rect.Should().BeEqualTo(expectedRect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsRectWhenIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 0f);
        Point point = new(20f, 20f);

        rect.Encapsulate(point);

        Rect expectedRect = new(20f, 20f, 0f, 0f);
        await rect.Should().BeEqualTo(expectedRect);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsWhenTheyAreSameRects()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(10f, 10f, 30f, 30f);

        left.Encapsulate(right);
        await left.Should().BeEqualTo(left);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsWhenTheOtherRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 30f, 30f);

        rect.Encapsulate(Rect.Zero);
        await rect.Should().BeEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsIntoOtherRectWhenCurrentIsEmpty()
    {
        Rect left = new(10f, 10f, 0f, 0f);
        Rect right = new(10f, 10f, 30f, 30f);

        left.Encapsulate(right);
        await left.Should().BeEqualTo(right);
    }

    [Test]
    public async Task EncapsulateShouldGrowsToIncludeOtherRect()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(5f, 5f, 40f, 40f);

        left.Encapsulate(right);

        Rect expectedRect = new(5f, 5f, 40f, 40f);
        await left.Should().BeEqualTo(expectedRect);
    }

    [Test]
    public async Task OverlapsShouldReturnTrueWhenRectsOverlap()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(20f, 20f, 30f, 30f);

        await left.Overlaps(right).Should().BeTrue();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenRectsDoNotOverlap()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(50f, 50f, 30f, 30f);

        await left.Overlaps(right).Should().BeFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenOtherRectIsEmpty()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(20f, 20f, 0f, 0f);

        await left.Overlaps(right).Should().BeFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenThisRectIsEmpty()
    {
        Rect left = new(10f, 10f, 0f, 0f);
        Rect right = new(10f, 10f, 30f, 30f);

        await left.Overlaps(right).Should().BeFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenRectsTouchAtEdgesAndTheyNotOverlappingInTheArea()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(40f, 10f, 30f, 30f);

        await left.Overlaps(right).Should().BeFalse();
    }

    [Test]
    public async Task IntersectShouldReturnCorrectIntersectionRect()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(20f, 20f, 30f, 30f);

        Rect expectedIntersection = new(20f, 20f, 20f, 20f);
        await Rect.Intersect(left, right).Should().BeEqualTo(expectedIntersection);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenNoOverlap()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(50f, 50f, 30f, 30f);

        await Rect.Intersect(left, right).Should().BeEqualTo(Rect.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenOneRectIsEmpty()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(20f, 20f, 0f, 0f);

        await Rect.Intersect(left, right).Should().BeEqualTo(Rect.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenBothRectsAreEmpty()
    {
        Rect left = new(10f, 10f, 0f, 0f);
        Rect right = new(20f, 20f, 0f, 0f);

        await Rect.Intersect(left, right).Should().BeEqualTo(Rect.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnCorrectIntersectionWhenRectsTouchAtEdges()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(40f, 10f, 30f, 30f);

        Rect expectedIntersection = new(40f, 10f, 0f, 30f);
        await Rect.Intersect(left, right).Should().BeEqualTo(expectedIntersection);
    }

    [Test]
    public async Task UnionShouldReturnCorrectUnionRect()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(20f, 20f, 30f, 30f);

        Rect expectedUnion = new(10f, 10f, 40f, 40f);
        await Rect.Union(left, right).Should().BeEqualTo(expectedUnion);
    }

    [Test]
    public async Task UnionShouldReturnRightRectWhenLeftRectIsEmpty()
    {
        Rect left = new(10f, 10f, 0f, 0f);
        Rect right = new(10f, 10f, 30f, 30f);

        await Rect.Union(left, right).Should().BeEqualTo(right);
    }

    [Test]
    public async Task UnionShouldReturnLeftRectWhenRightRectIsEmpty()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(20f, 20f, 0f, 0f);

        await Rect.Union(left, right).Should().BeEqualTo(left);
    }

    [Test]
    public async Task UnionShouldReturnEmptyRectWhenBothRectsAreEmpty()
    {
        Rect left = new(10f, 10f, 0f, 0f);
        Rect right = new(20f, 20f, 0f, 0f);

        await Rect.Union(left, right).Should().BeEqualTo(Rect.Zero);
    }

    [Test]
    public async Task EqualsShouldReturnTrueForIdenticalRects()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(10f, 10f, 30f, 30f);

        await left.Equals(right).Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForAnyTypeExceptRect()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        RectI right = new(10, 10, 30, 30);

        await left.Equals(right).Should().BeFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenBothRectsAreEmpty()
    {
        Rect left = new(10f, 10f, 0f, 0f);
        Rect right = new(20f, 20f, 0f, 0f);

        await left.Equals(right).Should().BeTrue();
    }

    [Test]
    public async Task EqualsObjectShouldReturnTrueForIdenticalRects()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        object right = new Rect(10f, 10f, 30f, 30f);

        await left.Equals(right).Should().BeTrue();
    }

    [Test]
    public async Task EqualsObjectShouldReturnFalseForAnyTypeExceptRect()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        object right = new RectI(10, 10, 30, 30);

        await left.Equals(right).Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnCorrectFormat()
    {
        Rect rect = new(10f, 20f, 30f, 40f);
        await rect.ToString().Should().BeEqualTo("(10, 20, 30, 40)");
    }

    [Test]
    public async Task OperatorEqualityShouldReturnTrueForIdenticalRects()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(10f, 10f, 30f, 30f);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualityShouldReturnFalseForDifferentRects()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(20f, 20f, 30f, 30f);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorInequalityShouldReturnFalseForIdenticalRects()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(10f, 10f, 30f, 30f);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorInequalityShouldReturnTrueForDifferentRects()
    {
        Rect left = new(10f, 10f, 30f, 30f);
        Rect right = new(20f, 20f, 30f, 30f);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task FloorShouldConvertRectByFlooringEachComponent()
    {
        Rect rect = new(7.64f, -7.6f, 7.64f, -7.6f);

        RectI result = rect.Floor();

        await result.X.Should().BeEqualTo(7);
        await result.Y.Should().BeEqualTo(-8);
        await result.Width.Should().BeEqualTo(7);
        await result.Height.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task RoundShouldConvertRectByRoundingEachComponent()
    {
        Rect rect = new(7.64f, -7.6f, 7.64f, -7.6f);

        RectI result = rect.Round();

        await result.X.Should().BeEqualTo(8);
        await result.Y.Should().BeEqualTo(-8);
        await result.Width.Should().BeEqualTo(8);
        await result.Height.Should().BeEqualTo(-8);
    }

    [Test]
    public async Task TruncateShouldConvertRectByTruncatingEachComponent()
    {
        Rect rect = new(7.64f, -7.6f, 7.64f, -7.6f);

        RectI result = rect.Truncate();

        await result.X.Should().BeEqualTo(7);
        await result.Y.Should().BeEqualTo(-7);
        await result.Width.Should().BeEqualTo(7);
        await result.Height.Should().BeEqualTo(-7);
    }
}
