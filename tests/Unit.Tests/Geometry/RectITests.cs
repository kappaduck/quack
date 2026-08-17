// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class RectITests
{
    [Test]
    public async Task AreaShouldCalculateWidthAndHeight()
    {
        RectI rect = new(10, 20, 30, 40);
        await rect.Area.Should().BeEqualTo(1200);
    }

    [Test]
    public async Task PositionShouldReturnXAndYAsPoint()
    {
        RectI rect = new(10, 20, 30, 40);

        PointI position = rect.Position;

        await position.X.Should().BeEqualTo(10);
        await position.Y.Should().BeEqualTo(20);
    }

    [Test]
    public async Task UpdatingPositionShouldChangeXAndY()
    {
        RectI rect = new(10, 20, 30, 40)
        {
            Position = new PointI(50, 60)
        };

        await rect.X.Should().BeEqualTo(50);
        await rect.Y.Should().BeEqualTo(60);
    }

    [Test]
    public async Task SizeShouldReturnWidthAndHeightAsSize()
    {
        RectI rect = new(10, 20, 30, 40);

        Size size = rect.Size;

        await size.Width.Should().BeEqualTo(30);
        await size.Height.Should().BeEqualTo(40);
    }

    [Test]
    public async Task SizeUpdatingShouldChangeWidthAndHeight()
    {
        RectI rect = new(10, 20, 30, 40)
        {
            Size = new Size(50, 60)
        };

        await rect.Width.Should().BeEqualTo(50);
        await rect.Height.Should().BeEqualTo(60);
    }

    [Test]
    public async Task RightShouldCalculateXAndWidth()
    {
        RectI rect = new(10, 20, 30, 40);
        await rect.Right.Should().BeEqualTo(40);
    }

    [Test]
    public async Task BottomShouldCalculateYAndHeight()
    {
        RectI rect = new(10, 20, 30, 40);
        await rect.Bottom.Should().BeEqualTo(60);
    }

    [Test]
    public async Task CenterShouldCalculateCorrectly()
    {
        RectI rect = new(10, 20, 30, 40);

        Point center = rect.Center;

        await center.X.Should().BeEqualTo(25f);
        await center.Y.Should().BeEqualTo(40f);
    }

    [Test]
    public async Task TopLeftShouldUseLeftAndTopCoordinates()
    {
        RectI rect = new(10, 20, 30, 40);

        PointI topLeft = rect.TopLeft;

        await topLeft.X.Should().BeEqualTo(10);
        await topLeft.Y.Should().BeEqualTo(20);
    }

    [Test]
    public async Task TopRightShouldUseRightAndTopCoordinates()
    {
        RectI rect = new(10, 20, 30, 40);

        PointI topRight = rect.TopRight;

        await topRight.X.Should().BeEqualTo(40);
        await topRight.Y.Should().BeEqualTo(20);
    }

    [Test]
    public async Task BottomLeftShouldUseLeftAndBottomCoordinates()
    {
        RectI rect = new(10, 20, 30, 40);

        PointI bottomLeft = rect.BottomLeft;

        await bottomLeft.X.Should().BeEqualTo(10);
        await bottomLeft.Y.Should().BeEqualTo(60);
    }

    [Test]
    public async Task BottomRightShouldUseRightAndBottomCoordinates()
    {
        RectI rect = new(10, 20, 30, 40);

        PointI bottomRight = rect.BottomRight;

        await bottomRight.X.Should().BeEqualTo(40);
        await bottomRight.Y.Should().BeEqualTo(60);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        RectI rect = new(10, 20, 0, 15);
        await rect.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        RectI rect = new(10, 20, 15, 0);
        await rect.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthAndHeightIsZero()
    {
        RectI rect = new(10, 20, 0, 0);
        await rect.IsEmpty.Should().BeTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenWidthAndHeightAreNotZero()
    {
        RectI rect = new(10, 20, 15, 15);
        await rect.IsEmpty.Should().BeFalse();
    }

    [Test]
    public async Task ZeroShouldReturnRectIntWithAllComponentsSetToZero()
    {
        RectI zero = RectI.Zero;

        await zero.X.Should().BeZero();
        await zero.Y.Should().BeZero();
        await zero.Width.Should().BeZero();
        await zero.Height.Should().BeZero();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenPointIsInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI point = new(20, 20);

        await rect.Contains(point).Should().BeTrue();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenPointIsOnEdgeOfRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI point = new(10, 20);

        await rect.Contains(point).Should().BeTrue();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenPointIsOutsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI point = new(50, 50);

        await rect.Contains(point).Should().BeFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenRectIsEmpty()
    {
        RectI rect = new(10, 10, 0, 30);
        PointI point = new(10, 20);

        await rect.Contains(point).Should().BeFalse();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenRectIsFullyContainedWithinTheRect()
    {
        RectI left = new(0, 0, 30, 30);
        RectI right = new(0, 0, 30, 25);

        await left.Contains(right).Should().BeTrue();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenRectIsFullyContainedWithinTheEdgeOfTheRect()
    {
        RectI left = new(0, 0, 30, 30);
        RectI right = new(0, 0, 30, 30);

        await left.Contains(right).Should().BeTrue();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenLeftRectIsEmpty()
    {
        RectI left = new(0, 0, 0, 30);
        RectI right = new(0, 0, 30, 30);

        await left.Contains(right).Should().BeFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenRightRectIsEmpty()
    {
        RectI left = new(0, 0, 30, 30);
        RectI right = new(0, 0, 0, 30);

        await left.Contains(right).Should().BeFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenRectIsNotFullyContainedWithinTheRect()
    {
        RectI left = new(0, 0, 30, 30);
        RectI right = new(100, 0, 30, 30);

        await left.Contains(right).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenAllPointIsInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI[] points = [new(15, 15), new(20, 20), new(30, 30)];

        await rect.ContainsAll(points).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenNoPointsAreInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI[] points = [new(5, 5), new(50, 50), new(100, 100)];

        await rect.ContainsAll(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenRectIsEmpty()
    {
        RectI rect = new(10, 10, 0, 30);
        PointI[] points = [new(10, 20), new(15, 25)];

        await rect.ContainsAll(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenThereIsNoPoints()
    {
        RectI rect = new(10, 10, 30, 30);
        await rect.ContainsAll([]).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsArrayAndContainsAllPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI[] points = [new(10, 10), new(20, 20), new(30, 30)];

        await rect.ContainsAll(points.AsEnumerable()).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsListAndContainsAllPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        List<PointI> points = [new(10, 15), new(20, 20), new(30, 30)];

        await rect.ContainsAll(points).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsEnumerableAndContainsAllPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        IEnumerable<PointI> points = PointsAsEnumerable();

        await rect.ContainsAll(points).Should().BeTrue();

        static IEnumerable<PointI> PointsAsEnumerable()
        {
            yield return new PointI(10, 10);
            yield return new PointI(20, 20);
            yield return new PointI(30, 30);
        }
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsArrayAndContainsNoPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI[] points = [new(5, 5), new(50, 50), new(100, 100)];

        await rect.ContainsAll(points.AsEnumerable()).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsListAndContainsNoPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        List<PointI> points = [new(5, 5), new(50, 50), new(100, 100)];

        await rect.ContainsAll(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsEnumerableAndContainsNoPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        IEnumerable<PointI> points = PointsAsEnumerable();

        await rect.ContainsAll(points).Should().BeFalse();

        static IEnumerable<PointI> PointsAsEnumerable()
        {
            yield return new PointI(5, 5);
            yield return new PointI(50, 50);
            yield return new PointI(100, 100);
        }
    }

    [Test]
    public async Task ContainsAllEnumerableShouldReturnFalseWhenRectIsEmpty()
    {
        RectI rect = new(10, 10, 30, 0);
        PointI[] points = [];

        await rect.ContainsAll(points.AsEnumerable()).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenAnyPointIsInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI[] points = [new(5, 5), new(20, 20), new(50, 50)];

        await rect.ContainsAny(points).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenNoPointsAreInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI[] points = [new(5, 5), new(50, 50), new(100, 100)];

        await rect.ContainsAny(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenRectIsEmpty()
    {
        RectI rect = new(10, 10, 0, 30);
        PointI[] points = [new(10, 20), new(15, 25)];

        await rect.ContainsAny(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenThereIsNoPoints()
    {
        RectI rect = new(10, 10, 30, 30);
        await rect.ContainsAny([]).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsArrayAndContainsPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI[] points = [new(5, 5), new(20, 20), new(50, 50)];

        await rect.ContainsAny(points.AsEnumerable()).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsListAndContainsPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        List<PointI> points = [new(5, 5), new(20, 20), new(50, 50)];

        await rect.ContainsAny(points).Should().BeTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsEnumerableAndContainsPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        IEnumerable<PointI> points = PointsAsEnumerable();

        await rect.ContainsAny(points).Should().BeTrue();

        static IEnumerable<PointI> PointsAsEnumerable()
        {
            yield return new PointI(5, 5);
            yield return new PointI(20, 20);
            yield return new PointI(50, 50);
        }
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsArrayAndContainsNoPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI[] points = [new(5, 5), new(50, 50), new(100, 100)];

        await rect.ContainsAny(points.AsEnumerable()).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsListAndContainsNoPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        List<PointI> points = [new(5, 5), new(50, 50), new(100, 100)];

        await rect.ContainsAny(points).Should().BeFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsEnumerableAndContainsNoPointInsideRect()
    {
        RectI rect = new(10, 10, 30, 30);
        IEnumerable<PointI> points = PointsAsEnumerable();

        await rect.ContainsAny(points).Should().BeFalse();

        static IEnumerable<PointI> PointsAsEnumerable()
        {
            yield return new PointI(5, 5);
            yield return new PointI(50, 50);
            yield return new PointI(100, 100);
        }
    }

    [Test]
    public async Task ContainsAnyEnumerableShouldReturnFalseWhenRectIsEmpty()
    {
        RectI rect = new(10, 10, 30, 0);
        PointI[] points = [];

        await rect.ContainsAny(points.AsEnumerable()).Should().BeFalse();
    }

    [Test]
    public async Task DeconstructShouldAssignComponentsToOutParameters()
    {
        (int x, int y, int width, int height) = new RectI(10, 20, 30, 40);

        await x.Should().BeEqualTo(10);
        await y.Should().BeEqualTo(20);
        await width.Should().BeEqualTo(30);
        await height.Should().BeEqualTo(40);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsRectWhenAlreadyContainsThePoint()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI point = new(20, 20);

        rect.Encapsulate(point);
        await rect.Should().BeEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsRectToIncludePoint()
    {
        RectI rect = new(10, 10, 30, 30);
        PointI point = new(50, 50);

        rect.Encapsulate(point);

        RectI expectedRect = new(10, 10, 40, 40);
        await rect.Should().BeEqualTo(expectedRect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsRectWhenIsEmpty()
    {
        RectI rect = new(10, 10, 0, 0);
        PointI point = new(20, 20);

        rect.Encapsulate(point);

        RectI expectedRect = new(20, 20, 0, 0);
        await rect.Should().BeEqualTo(expectedRect);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsWhenTheyAreSameRects()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(10, 10, 30, 30);

        left.Encapsulate(right);
        await left.Should().BeEqualTo(left);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsWhenTheOtherRectIsEmpty()
    {
        RectI rect = new(10, 10, 30, 30);

        rect.Encapsulate(RectI.Zero);
        await rect.Should().BeEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsIntoOtherRectWhenCurrentIsEmpty()
    {
        RectI left = new(10, 10, 0, 0);
        RectI right = new(10, 10, 30, 30);

        left.Encapsulate(right);
        await left.Should().BeEqualTo(right);
    }

    [Test]
    public async Task EncapsulateShouldGrowsToIncludeOtherRect()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(5, 5, 40, 40);

        left.Encapsulate(right);

        RectI expectedRect = new(5, 5, 40, 40);
        await left.Should().BeEqualTo(expectedRect);
    }

    [Test]
    public async Task OverlapsShouldReturnTrueWhenRectsOverlap()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(20, 20, 30, 30);

        await left.Overlaps(right).Should().BeTrue();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenRectsDoNotOverlap()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(50, 50, 30, 30);

        await left.Overlaps(right).Should().BeFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenOtherRectIsEmpty()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(20, 20, 0, 0);

        await left.Overlaps(right).Should().BeFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenThisRectIsEmpty()
    {
        RectI left = new(10, 10, 0, 0);
        RectI right = new(10, 10, 30, 30);

        await left.Overlaps(right).Should().BeFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenRectsTouchAtEdgesAndTheyNotOverlappingInTheArea()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(40, 10, 30, 30);

        await left.Overlaps(right).Should().BeFalse();
    }

    [Test]
    public async Task IntersectShouldReturnCorrectIntersectionRect()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(20, 20, 30, 30);

        RectI expectedIntersection = new(20, 20, 20, 20);
        await RectI.Intersect(left, right).Should().BeEqualTo(expectedIntersection);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenNoOverlap()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(50, 50, 30, 30);

        await RectI.Intersect(left, right).Should().BeEqualTo(RectI.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenOneRectIsEmpty()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(20, 20, 0, 0);

        await RectI.Intersect(left, right).Should().BeEqualTo(RectI.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenBothRectsAreEmpty()
    {
        RectI left = new(10, 10, 0, 0);
        RectI right = new(20, 20, 0, 0);

        await RectI.Intersect(left, right).Should().BeEqualTo(RectI.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnCorrectIntersectionWhenRectsTouchAtEdges()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(40, 10, 30, 30);

        RectI expectedIntersection = new(40, 10, 0, 30);
        await RectI.Intersect(left, right).Should().BeEqualTo(expectedIntersection);
    }

    [Test]
    public async Task UnionShouldReturnCorrectUnionRect()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(20, 20, 30, 30);

        RectI expectedUnion = new(10, 10, 40, 40);
        await RectI.Union(left, right).Should().BeEqualTo(expectedUnion);
    }

    [Test]
    public async Task UnionShouldReturnRightRectWhenLeftRectIsEmpty()
    {
        RectI left = new(10, 10, 0, 0);
        RectI right = new(10, 10, 30, 30);

        await RectI.Union(left, right).Should().BeEqualTo(right);
    }

    [Test]
    public async Task UnionShouldReturnLeftRectWhenRightRectIsEmpty()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(20, 20, 0, 0);

        await RectI.Union(left, right).Should().BeEqualTo(left);
    }

    [Test]
    public async Task UnionShouldReturnEmptyRectWhenBothRectsAreEmpty()
    {
        RectI left = new(10, 10, 0, 0);
        RectI right = new(20, 20, 0, 0);

        await RectI.Union(left, right).Should().BeEqualTo(RectI.Zero);
    }

    [Test]
    public async Task EqualsShouldReturnTrueForIdenticalRects()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(10, 10, 30, 30);

        await left.Equals(right).Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForAnyTypeExceptRect()
    {
        RectI left = new(10, 10, 30, 30);
        Rect right = new(10, 10, 30, 30);

        await left.Equals(right).Should().BeFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenBothRectsAreEmpty()
    {
        RectI left = new(10, 10, 0, 0);
        RectI right = new(20, 20, 0, 0);

        await left.Equals(right).Should().BeTrue();
    }

    [Test]
    public async Task EqualsObjectShouldReturnTrueForIdenticalRects()
    {
        RectI left = new(10, 10, 30, 30);
        object right = new RectI(10, 10, 30, 30);

        await left.Equals(right).Should().BeTrue();
    }

    [Test]
    public async Task EqualsObjectShouldReturnFalseForAnyTypeExceptRectI()
    {
        RectI left = new(10, 10, 30, 30);
        object right = new Rect(10, 10, 30, 30);

        await left.Equals(right).Should().BeFalse();
    }

    [Test]
    public async Task ToStringShouldReturnCorrectFormat()
    {
        RectI rect = new(10, 20, 30, 40);
        await rect.ToString().Should().BeEqualTo("(10, 20, 30, 40)");
    }

    [Test]
    public async Task OperatorEqualityShouldReturnTrueForIdenticalRects()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(10, 10, 30, 30);

        bool result = left == right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task OperatorEqualityShouldReturnFalseForDifferentRects()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(20, 20, 30, 30);

        bool result = left == right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorInequalityShouldReturnFalseForIdenticalRects()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(10, 10, 30, 30);

        bool result = left != right;
        await result.Should().BeFalse();
    }

    [Test]
    public async Task OperatorInequalityShouldReturnTrueForDifferentRects()
    {
        RectI left = new(10, 10, 30, 30);
        RectI right = new(20, 20, 30, 30);

        bool result = left != right;
        await result.Should().BeTrue();
    }

    [Test]
    public async Task ToPointsShouldReturnAllPointsWithinRect()
    {
        RectI rect = new(10, 10, 2, 2);
        PointI[] points = rect.ToPoints();

        await points.Should().Contain(new PointI(10, 10));
        await points.Should().Contain(new PointI(11, 10));
        await points.Should().Contain(new PointI(10, 11));
        await points.Should().Contain(new PointI(11, 11));
    }

    [Test]
    public async Task ToPointsShouldReturnEmptyWhenRectIsEmpty()
    {
        RectI rect = new(10, 10, 0, 0);
        PointI[] points = rect.ToPoints();

        await points.Should().BeEmpty();
    }
}
