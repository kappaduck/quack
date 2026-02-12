// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

public sealed class RectIntTests
{
    [Test]
    public async Task AreaShouldCalculateWidthTimesHeight()
    {
        RectInt rect = new(10, 20, 30, 40);

        await Assert.That(rect.Area).IsEqualTo(1200);
    }

    [Test]
    public async Task PositionShouldReturnXAndYAsVector2()
    {
        RectInt rect = new(10, 20, 30, 40);

        Vector2Int expectedPosition = new(10, 20);
        await Assert.That(rect.Position).IsEqualTo(expectedPosition);
    }

    [Test]
    public async Task UpdatingPositionShouldChangeXAndY()
    {
        RectInt rect = new(10, 20, 30, 40)
        {
            Position = new Vector2Int(50, 60)
        };

        await Assert.That(rect.X).IsEqualTo(50);
        await Assert.That(rect.Y).IsEqualTo(60);
    }

    [Test]
    public async Task SizeShouldReturnWidthAndHeightAsVector2()
    {
        RectInt rect = new(10, 20, 30, 40);

        SizeInt expectedSize = new(30, 40);
        await Assert.That(rect.Size).IsEqualTo(expectedSize);
    }

    [Test]
    public async Task SizeUpdatingShouldChangeWidthAndHeight()
    {
        RectInt rect = new(10, 20, 30, 40)
        {
            Size = new SizeInt(50, 60)
        };

        await Assert.That(rect.Width).IsEqualTo(50);
        await Assert.That(rect.Height).IsEqualTo(60);
    }

    [Test]
    public async Task RightShouldCalculateXAndWidth()
    {
        RectInt rect = new(10, 20, 30, 40);
        await Assert.That(rect.Right).IsEqualTo(40);
    }

    [Test]
    public async Task BottomShouldCalculateYAndHeight()
    {
        RectInt rect = new(10, 20, 30, 40);
        await Assert.That(rect.Bottom).IsEqualTo(60);
    }

    [Test]
    public async Task CenterShouldCalculateCorrectly()
    {
        RectInt rect = new(10, 20, 30, 40);

        Vector2Int expectedCenter = new(25, 40);
        await Assert.That(rect.Center).IsEqualTo(expectedCenter);
    }

    [Test]
    public async Task TopLeftShouldUseLeftAndTopCoordinates()
    {
        RectInt rect = new(10, 20, 30, 40);

        Vector2Int expectedTopLeft = new(10, 20);
        await Assert.That(rect.TopLeft).IsEqualTo(expectedTopLeft);
    }

    [Test]
    public async Task TopRightShouldUseRightAndTopCoordinates()
    {
        RectInt rect = new(10, 20, 30, 40);

        Vector2Int expectedTopRight = new(40, 20);
        await Assert.That(rect.TopRight).IsEqualTo(expectedTopRight);
    }

    [Test]
    public async Task BottomLeftShouldUseLeftAndBottomCoordinates()
    {
        RectInt rect = new(10, 20, 30, 40);

        Vector2Int expectedBottomLeft = new(10, 60);
        await Assert.That(rect.BottomLeft).IsEqualTo(expectedBottomLeft);
    }

    [Test]
    public async Task BottomRightShouldUseRightAndBottomCoordinates()
    {
        RectInt rect = new(10, 20, 30, 40);

        Vector2Int expectedBottomRight = new(40, 60);
        await Assert.That(rect.BottomRight).IsEqualTo(expectedBottomRight);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        RectInt rect = new(10, 20, 0, 15);
        await Assert.That(rect.IsEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        RectInt rect = new(10, 20, 15, 0);
        await Assert.That(rect.IsEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthAndHeightIsZero()
    {
        RectInt rect = new(10, 20, 0, 0);
        await Assert.That(rect.IsEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenWidthAndHeightAreNotZero()
    {
        RectInt rect = new(10, 20, 15, 15);
        await Assert.That(rect.IsEmpty).IsFalse();
    }

    [Test]
    public async Task ZeroShouldReturnRectIntWithAllComponentsSetToZero()
    {
        RectInt zeroRect = RectInt.Zero;

        await Assert.That(zeroRect.X).IsEqualTo(0);
        await Assert.That(zeroRect.Y).IsEqualTo(0);
        await Assert.That(zeroRect.Width).IsEqualTo(0);
        await Assert.That(zeroRect.Height).IsEqualTo(0);
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenPointIsInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int point = new(20, 20);

        await Assert.That(rect.Contains(point)).IsTrue();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenPointIsOnEdgeOfRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int point = new(10, 20);

        await Assert.That(rect.Contains(point)).IsTrue();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenPointIsOutsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int point = new(50, 50);

        await Assert.That(rect.Contains(point)).IsFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenRectIsEmpty()
    {
        RectInt rect = new(10, 10, 0, 30);
        Vector2Int point = new(10, 20);

        await Assert.That(rect.Contains(point)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenAllPointIsInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int[] points = [new(15, 15), new(20, 20), new(30, 30)];

        await Assert.That(rect.ContainsAll(points)).IsTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenNoPointsAreInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int[] points = [new(5, 5), new(50, 50), new(100, 100)];

        await Assert.That(rect.ContainsAll(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenRectIsEmpty()
    {
        RectInt rect = new(10, 10, 0, 30);
        Vector2Int[] points = [new(10, 20), new(15, 25)];

        await Assert.That(rect.ContainsAll(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenThereIsNoPoints()
    {
        RectInt rect = new(10, 10, 30, 30);
        ReadOnlySpan<Vector2Int> points = [];

        await Assert.That(rect.ContainsAll(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsArrayAndContainsAllPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int[] points = [new(10, 10), new(20, 20), new(30, 30)];

        await Assert.That(rect.ContainsAny(points.AsEnumerable())).IsTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsListAndContainsAllPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        List<Vector2Int> points = [new(10, 15), new(20, 20), new(30, 30)];

        await Assert.That(rect.ContainsAll(points)).IsTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsEnumerableAndContainsAllPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        IEnumerable<Vector2Int> points = PointsAsEnumerable();

        await Assert.That(rect.ContainsAll(points)).IsTrue();

        static IEnumerable<Vector2Int> PointsAsEnumerable()
        {
            yield return new Vector2Int(10, 10);
            yield return new Vector2Int(20, 20);
            yield return new Vector2Int(30, 30);
        }
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsArrayAndContainsNoPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int[] points = [new(5, 5), new(50, 50), new(100, 100)];

        await Assert.That(rect.ContainsAll(points.AsEnumerable())).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsListAndContainsNoPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        List<Vector2Int> points = [new(5, 5), new(50, 50), new(100, 100)];

        await Assert.That(rect.ContainsAll(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsEnumerableAndContainsNoPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        IEnumerable<Vector2Int> points = PointsAsEnumerable();

        await Assert.That(rect.ContainsAll(points)).IsFalse();

        static IEnumerable<Vector2Int> PointsAsEnumerable()
        {
            yield return new Vector2Int(5, 5);
            yield return new Vector2Int(50, 50);
            yield return new Vector2Int(100, 100);
        }
    }

    [Test]
    public async Task ContainsAllEnumerableShouldReturnFalseWhenRectIsEmpty()
    {
        RectInt rect = new(10, 10, 30, 0);
        Vector2Int[] points = [];

        await Assert.That(rect.ContainsAll(points.AsEnumerable())).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenAnyPointIsInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int[] points = [new(5, 5), new(20, 20), new(50, 50)];

        await Assert.That(rect.ContainsAny(points)).IsTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenNoPointsAreInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int[] points = [new(5, 5), new(50, 50), new(100, 100)];

        await Assert.That(rect.ContainsAny(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenRectIsEmpty()
    {
        RectInt rect = new(10, 10, 0, 30);
        Vector2Int[] points = [new(10, 20), new(15, 25)];

        await Assert.That(rect.ContainsAny(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenThereIsNoPoints()
    {
        RectInt rect = new(10, 10, 30, 30);
        ReadOnlySpan<Vector2Int> points = [];

        await Assert.That(rect.ContainsAny(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsArrayAndContainsPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int[] points = [new(5, 5), new(20, 20), new(50, 50)];

        await Assert.That(rect.ContainsAny(points.AsEnumerable())).IsTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsListAndContainsPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        List<Vector2Int> points = [new(5, 5), new(20, 20), new(50, 50)];

        await Assert.That(rect.ContainsAny(points)).IsTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsEnumerableAndContainsPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        IEnumerable<Vector2Int> points = PointsAsEnumerable();

        await Assert.That(rect.ContainsAny(points)).IsTrue();

        static IEnumerable<Vector2Int> PointsAsEnumerable()
        {
            yield return new Vector2Int(5, 5);
            yield return new Vector2Int(20, 20);
            yield return new Vector2Int(50, 50);
        }
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsArrayAndContainsNoPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int[] points = [new(5, 5), new(50, 50), new(100, 100)];

        await Assert.That(rect.ContainsAny(points.AsEnumerable())).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsListAndContainsNoPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        List<Vector2Int> points = [new(5, 5), new(50, 50), new(100, 100)];

        await Assert.That(rect.ContainsAny(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsEnumerableAndContainsNoPointInsideRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        IEnumerable<Vector2Int> points = PointsAsEnumerable();

        await Assert.That(rect.ContainsAny(points)).IsFalse();

        static IEnumerable<Vector2Int> PointsAsEnumerable()
        {
            yield return new Vector2Int(5, 5);
            yield return new Vector2Int(50, 50);
            yield return new Vector2Int(100, 100);
        }
    }

    [Test]
    public async Task ContainsAnyEnumerableShouldReturnFalseWhenRectIsEmpty()
    {
        RectInt rect = new(10, 10, 30, 0);
        Vector2Int[] points = [];

        await Assert.That(rect.ContainsAny(points.AsEnumerable())).IsFalse();
    }

    [Test]
    public async Task DeconstructShouldAssignComponentsToOutParameters()
    {
        (int x, int y, int width, int height) = new RectInt(10, 20, 30, 40);

        await Assert.That(x).IsEqualTo(10);
        await Assert.That(y).IsEqualTo(20);
        await Assert.That(width).IsEqualTo(30);
        await Assert.That(height).IsEqualTo(40);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsRectWhenAlreadyContainsThePoint()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int point = new(20, 20);

        rect.Encapsulate(point);
        await Assert.That(rect).IsEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsRectToIncludePoint()
    {
        RectInt rect = new(10, 10, 30, 30);
        Vector2Int point = new(50, 50);

        rect.Encapsulate(point);

        RectInt expectedRect = new(10, 10, 40, 40);
        await Assert.That(rect).IsEqualTo(expectedRect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsRectWhenIsEmpty()
    {
        RectInt rect = new(10, 10, 0, 0);
        Vector2Int point = new(20, 20);

        rect.Encapsulate(point);
        RectInt expectedRect = new(20, 20, 0, 0);
        await Assert.That(rect).IsEqualTo(expectedRect);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsWhenTheyAreSameRects()
    {
        RectInt rect = new(10, 10, 30, 30);
        RectInt other = new(10, 10, 30, 30);

        rect.Encapsulate(other);
        await Assert.That(rect).IsEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsWhenTheOtherRectIsEmpty()
    {
        RectInt rect = new(10, 10, 30, 30);

        rect.Encapsulate(RectInt.Zero);
        await Assert.That(rect).IsEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsIntoOtherRectWhenCurrentIsEmpty()
    {
        RectInt rect = new(10, 10, 0, 0);
        RectInt other = new(10, 10, 30, 30);

        rect.Encapsulate(other);
        await Assert.That(rect).IsEqualTo(other);
    }

    [Test]
    public async Task EncapsulateShouldGrowsToIncludeOtherRect()
    {
        RectInt rect = new(10, 10, 30, 30);
        RectInt other = new(5, 5, 40, 40);

        rect.Encapsulate(other);

        RectInt expectedRect = new(5, 5, 40, 40);
        await Assert.That(rect).IsEqualTo(expectedRect);
    }

    [Test]
    public async Task OverlapsShouldReturnTrueWhenRectsOverlap()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(20, 20, 30, 30);

        await Assert.That(rectA.Overlaps(rectB)).IsTrue();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenRectsDoNotOverlap()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(50, 50, 30, 30);

        await Assert.That(rectA.Overlaps(rectB)).IsFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenOtherRectIsEmpty()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(20, 20, 0, 0);

        await Assert.That(rectA.Overlaps(rectB)).IsFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenThisRectIsEmpty()
    {
        RectInt rectA = new(10, 10, 0, 0);
        RectInt rectB = new(10, 10, 30, 30);

        await Assert.That(rectA.Overlaps(rectB)).IsFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenRectsTouchAtEdgesAndTheyNotOverlappingInTheArea()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(40, 10, 30, 30);

        await Assert.That(rectA.Overlaps(rectB)).IsFalse();
    }

    [Test]
    public async Task IntersectShouldReturnCorrectIntersectionRect()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(20, 20, 30, 30);

        RectInt expectedIntersection = new(20, 20, 20, 20);
        await Assert.That(RectInt.Intersect(rectA, rectB)).IsEqualTo(expectedIntersection);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenNoOverlap()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(50, 50, 30, 30);

        await Assert.That(RectInt.Intersect(rectA, rectB)).IsEqualTo(RectInt.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenOneRectIsEmpty()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(20, 20, 0, 0);

        await Assert.That(RectInt.Intersect(rectA, rectB)).IsEqualTo(RectInt.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenBothRectsAreEmpty()
    {
        RectInt rectA = new(10, 10, 0, 0);
        RectInt rectB = new(20, 20, 0, 0);

        await Assert.That(RectInt.Intersect(rectA, rectB)).IsEqualTo(RectInt.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnCorrectIntersectionWhenRectsTouchAtEdges()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(40, 10, 30, 30);

        RectInt expectedIntersection = new(40, 10, 0, 30);
        await Assert.That(RectInt.Intersect(rectA, rectB)).IsEqualTo(expectedIntersection);
    }

    [Test]
    public async Task UnionShouldReturnCorrectUnionRect()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(20, 20, 30, 30);

        RectInt expectedUnion = new(10, 10, 40, 40);
        await Assert.That(RectInt.Union(rectA, rectB)).IsEqualTo(expectedUnion);
    }

    [Test]
    public async Task UnionShouldReturnRightRectWhenLeftRectIsEmpty()
    {
        RectInt rectA = new(10, 10, 0, 0);
        RectInt rectB = new(10, 10, 30, 30);

        await Assert.That(RectInt.Union(rectA, rectB)).IsEqualTo(rectB);
    }

    [Test]
    public async Task UnionShouldReturnLeftRectWhenRightRectIsEmpty()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(20, 20, 0, 0);

        await Assert.That(RectInt.Union(rectA, rectB)).IsEqualTo(rectA);
    }

    [Test]
    public async Task UnionShouldReturnEmptyRectWhenBothRectsAreEmpty()
    {
        RectInt rectA = new(10, 10, 0, 0);
        RectInt rectB = new(20, 20, 0, 0);

        await Assert.That(RectInt.Union(rectA, rectB)).IsEqualTo(RectInt.Zero);
    }

    [Test]
    public async Task EqualsShouldReturnTrueForIdenticalRects()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(10, 10, 30, 30);

        await Assert.That(rectA.Equals(rectB)).IsTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForDifferentRects()
    {
        RectInt rectA = new(10, 10, 30, 30);
        Rect rectB = new(10, 10, 30, 30);

        await Assert.That(rectA.Equals(rectB)).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenBothRectsAreEmpty()
    {
        RectInt rectA = new(10, 10, 0, 0);
        RectInt rectB = new(20, 20, 0, 0);

        await Assert.That(rectA.Equals(rectB)).IsTrue();
    }

    [Test]
    public async Task EqualsObjectShouldReturnTrueForIdenticalRects()
    {
        RectInt rectA = new(10, 10, 30, 30);
        object rectB = new RectInt(10, 10, 30, 30);

        await Assert.That(rectA.Equals(rectB)).IsTrue();
    }

    [Test]
    public async Task EqualsObjectShouldReturnFalseForDifferentRects()
    {
        RectInt rectA = new(10, 10, 30, 30);
        object rectB = new Rect(10, 10, 30, 30);

        await Assert.That(rectA.Equals(rectB)).IsFalse();
    }

    [Test]
    public async Task EqualsObjectShouldReturnFalseForNonRectObject()
    {
        RectInt rectA = new(10, 10, 30, 30);
        object nonRect = "NotARect";

        await Assert.That(rectA.Equals(nonRect)).IsFalse();
    }

    [Test]
    public async Task ToStringShouldReturnCorrectFormat()
    {
        RectInt rect = new(10, 20, 30, 40);
        await Assert.That(rect.ToString()).IsEqualTo("(10, 20, 30, 40)");
    }

    [Test]
    public async Task OperatorEqualityShouldReturnTrueForIdenticalRects()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(10, 10, 30, 30);

        await Assert.That(rectA == rectB).IsTrue();
    }

    [Test]
    public async Task OperatorEqualityShouldReturnFalseForDifferentRects()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(20, 20, 30, 30);

        await Assert.That(rectA == rectB).IsFalse();
    }

    [Test]
    public async Task OperatorInequalityShouldReturnFalseForIdenticalRects()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(10, 10, 30, 30);

        await Assert.That(rectA != rectB).IsFalse();
    }

    [Test]
    public async Task OperatorInequalityShouldReturnTrueForDifferentRects()
    {
        RectInt rectA = new(10, 10, 30, 30);
        RectInt rectB = new(20, 20, 30, 30);

        await Assert.That(rectA != rectB).IsTrue();
    }

    [Test]
    public async Task ToPointsShouldReturnAllPointsWithinRect()
    {
        RectInt rect = new(10, 10, 2, 2);
        Vector2Int[] points = rect.ToPoints();

        await Assert.That(points).Contains(new Vector2Int(10, 10));
        await Assert.That(points).Contains(new Vector2Int(11, 10));
        await Assert.That(points).Contains(new Vector2Int(10, 11));
        await Assert.That(points).Contains(new Vector2Int(11, 11));
    }

    [Test]
    public async Task ToPointsShouldReturnEmptyWhenRectIsEmpty()
    {
        RectInt rect = new(10, 10, 0, 0);
        Vector2Int[] points = rect.ToPoints();

        await Assert.That(points).IsEmpty();
    }
}
