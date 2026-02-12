// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

public sealed class RectTests
{
    [Test]
    public async Task AreaShouldCalculateWidthTimesHeight()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        await Assert.That(rect.Area).IsEqualTo(1200f);
    }

    [Test]
    public async Task PositionShouldReturnXAndYAsVector2()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Vector2 expectedPosition = new(10f, 20f);
        await Assert.That(rect.Position).IsEqualTo(expectedPosition);
    }

    [Test]
    public async Task UpdatingPositionShouldChangeXAndY()
    {
        Rect rect = new(10f, 20f, 30f, 40f)
        {
            Position = new Vector2(50f, 60f)
        };

        await Assert.That(rect.X).IsEqualTo(50f);
        await Assert.That(rect.Y).IsEqualTo(60f);
    }

    [Test]
    public async Task SizeShouldReturnWidthAndHeightAsVector2()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Size expectedSize = new(30f, 40f);
        await Assert.That(rect.Size).IsEqualTo(expectedSize);
    }

    [Test]
    public async Task SizeUpdatingShouldChangeWidthAndHeight()
    {
        Rect rect = new(10f, 20f, 30f, 40f)
        {
            Size = new Size(50f, 60f)
        };

        await Assert.That(rect.Width).IsEqualTo(50f);
        await Assert.That(rect.Height).IsEqualTo(60f);
    }

    [Test]
    public async Task RightShouldCalculateXAndWidth()
    {
        Rect rect = new(10f, 20f, 30f, 40f);
        await Assert.That(rect.Right).IsEqualTo(40f);
    }

    [Test]
    public async Task BottomShouldCalculateYAndHeight()
    {
        Rect rect = new(10f, 20f, 30f, 40f);
        await Assert.That(rect.Bottom).IsEqualTo(60f);
    }

    [Test]
    public async Task CenterShouldCalculateCorrectly()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Vector2 expectedCenter = new(25f, 40f);
        await Assert.That(rect.Center).IsEqualTo(expectedCenter);
    }

    [Test]
    public async Task TopLeftShouldUseLeftAndTopCoordinates()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Vector2 expectedTopLeft = new(10f, 20f);
        await Assert.That(rect.TopLeft).IsEqualTo(expectedTopLeft);
    }

    [Test]
    public async Task TopRightShouldUseRightAndTopCoordinates()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Vector2 expectedTopRight = new(40f, 20f);
        await Assert.That(rect.TopRight).IsEqualTo(expectedTopRight);
    }

    [Test]
    public async Task BottomLeftShouldUseLeftAndBottomCoordinates()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Vector2 expectedBottomLeft = new(10f, 60f);
        await Assert.That(rect.BottomLeft).IsEqualTo(expectedBottomLeft);
    }

    [Test]
    public async Task BottomRightShouldUseRightAndBottomCoordinates()
    {
        Rect rect = new(10f, 20f, 30f, 40f);

        Vector2 expectedBottomRight = new(40f, 60f);
        await Assert.That(rect.BottomRight).IsEqualTo(expectedBottomRight);
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthIsZero()
    {
        Rect rect = new(10f, 20f, 0f, 15f);
        await Assert.That(rect.IsEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenHeightIsZero()
    {
        Rect rect = new(10f, 20f, 15f, 0f);
        await Assert.That(rect.IsEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnTrueWhenWidthAndHeightIsZero()
    {
        Rect rect = new(10f, 20f, 0f, 0f);
        await Assert.That(rect.IsEmpty).IsTrue();
    }

    [Test]
    public async Task IsEmptyShouldReturnFalseWhenWidthAndHeightAreNotZero()
    {
        Rect rect = new(10f, 20f, 15f, 15f);
        await Assert.That(rect.IsEmpty).IsFalse();
    }

    [Test]
    public async Task ZeroShouldReturnRectWithAllComponentsSetToZero()
    {
        Rect zeroRect = Rect.Zero;

        await Assert.That(zeroRect.X).IsEqualTo(0f);
        await Assert.That(zeroRect.Y).IsEqualTo(0f);
        await Assert.That(zeroRect.Width).IsEqualTo(0f);
        await Assert.That(zeroRect.Height).IsEqualTo(0f);
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenPointIsInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2 point = new(20f, 20f);

        await Assert.That(rect.Contains(point)).IsTrue();
    }

    [Test]
    public async Task ContainsShouldReturnTrueWhenPointIsOnEdgeOfRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2 point = new(10f, 20f);

        await Assert.That(rect.Contains(point)).IsTrue();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenPointIsOutsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2 point = new(50f, 50f);

        await Assert.That(rect.Contains(point)).IsFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenPointIsJustOutsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2 point = new(9.9f, 20f);

        await Assert.That(rect.Contains(point)).IsFalse();
    }

    [Test]
    public async Task ContainsShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 30f);
        Vector2 point = new(10f, 20f);

        await Assert.That(rect.Contains(point)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenAllPointIsInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2[] points = [new(15f, 15f), new(20f, 20f), new(30f, 30f)];

        await Assert.That(rect.ContainsAll(points)).IsTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenNoPointsAreInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2[] points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await Assert.That(rect.ContainsAll(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 30f);
        Vector2[] points = [new(10f, 20f), new(15f, 25f)];

        await Assert.That(rect.ContainsAll(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenThereIsNoPoints()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        ReadOnlySpan<Vector2> points = [];

        await Assert.That(rect.ContainsAll(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsArrayAndContainsAllPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2[] points = [new(10f, 10f), new(20f, 20f), new(30f, 30f)];

        await Assert.That(rect.ContainsAny(points.AsEnumerable())).IsTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsListAndContainsAllPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        List<Vector2> points = [new(10f, 15f), new(20f, 20f), new(30f, 30f)];

        await Assert.That(rect.ContainsAll(points)).IsTrue();
    }

    [Test]
    public async Task ContainsAllShouldReturnTrueWhenPointsIsEnumerableAndContainsAllPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        IEnumerable<Vector2> points = PointsAsEnumerable();

        await Assert.That(rect.ContainsAll(points)).IsTrue();

        static IEnumerable<Vector2> PointsAsEnumerable()
        {
            yield return new Vector2(10f, 10f);
            yield return new Vector2(20f, 20f);
            yield return new Vector2(30f, 30f);
        }
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsArrayAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2[] points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await Assert.That(rect.ContainsAll(points.AsEnumerable())).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsListAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        List<Vector2> points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await Assert.That(rect.ContainsAll(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAllShouldReturnFalseWhenPointsIsEnumerableAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        IEnumerable<Vector2> points = PointsAsEnumerable();

        await Assert.That(rect.ContainsAll(points)).IsFalse();

        static IEnumerable<Vector2> PointsAsEnumerable()
        {
            yield return new Vector2(5f, 5f);
            yield return new Vector2(50f, 50f);
            yield return new Vector2(100f, 100f);
        }
    }

    [Test]
    public async Task ContainsAllEnumerableShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 30f, 0f);
        Vector2[] points = [];

        await Assert.That(rect.ContainsAll(points.AsEnumerable())).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenAnyPointIsInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2[] points = [new(5f, 5f), new(20f, 20f), new(50f, 50f)];

        await Assert.That(rect.ContainsAny(points)).IsTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenNoPointsAreInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2[] points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await Assert.That(rect.ContainsAny(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 30f);
        Vector2[] points = [new(10f, 20f), new(15f, 25f)];

        await Assert.That(rect.ContainsAny(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenThereIsNoPoints()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        ReadOnlySpan<Vector2> points = [];

        await Assert.That(rect.ContainsAny(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsArrayAndContainsPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2[] points = [new(5f, 5f), new(20f, 20f), new(50f, 50f)];

        await Assert.That(rect.ContainsAny(points.AsEnumerable())).IsTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsListAndContainsPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        List<Vector2> points = [new(5f, 5f), new(20f, 20f), new(50f, 50f)];

        await Assert.That(rect.ContainsAny(points)).IsTrue();
    }

    [Test]
    public async Task ContainsAnyShouldReturnTrueWhenPointsIsEnumerableAndContainsPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        IEnumerable<Vector2> points = PointsAsEnumerable();

        await Assert.That(rect.ContainsAny(points)).IsTrue();

        static IEnumerable<Vector2> PointsAsEnumerable()
        {
            yield return new Vector2(5f, 5f);
            yield return new Vector2(20f, 20f);
            yield return new Vector2(50f, 50f);
        }
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsArrayAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2[] points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await Assert.That(rect.ContainsAny(points.AsEnumerable())).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsListAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        List<Vector2> points = [new(5f, 5f), new(50f, 50f), new(100f, 100f)];

        await Assert.That(rect.ContainsAny(points)).IsFalse();
    }

    [Test]
    public async Task ContainsAnyShouldReturnFalseWhenPointsIsEnumerableAndContainsNoPointInsideRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        IEnumerable<Vector2> points = PointsAsEnumerable();

        await Assert.That(rect.ContainsAny(points)).IsFalse();

        static IEnumerable<Vector2> PointsAsEnumerable()
        {
            yield return new Vector2(5f, 5f);
            yield return new Vector2(50f, 50f);
            yield return new Vector2(100f, 100f);
        }
    }

    [Test]
    public async Task ContainsAnyEnumerableShouldReturnFalseWhenRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 30f, 0f);
        Vector2[] points = [];

        await Assert.That(rect.ContainsAny(points.AsEnumerable())).IsFalse();
    }

    [Test]
    public async Task DeconstructShouldAssignComponentsToOutParameters()
    {
        (float x, float y, float width, float height) = new Rect(10f, 20f, 30f, 40f);

        await Assert.That(x).IsEqualTo(10f);
        await Assert.That(y).IsEqualTo(20f);
        await Assert.That(width).IsEqualTo(30f);
        await Assert.That(height).IsEqualTo(40f);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsRectWhenAlreadyContainsThePoint()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2 point = new(20f, 20f);

        rect.Encapsulate(point);
        await Assert.That(rect).IsEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsRectToIncludePoint()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Vector2 point = new(50f, 50f);

        rect.Encapsulate(point);

        Rect expectedRect = new(10f, 10f, 40f, 40f);
        await Assert.That(rect).IsEqualTo(expectedRect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsRectWhenIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 0f);
        Vector2 point = new(20f, 20f);

        rect.Encapsulate(point);
        Rect expectedRect = new(20f, 20f, 0f, 0f);
        await Assert.That(rect).IsEqualTo(expectedRect);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsWhenTheyAreSameRects()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Rect other = new(10f, 10f, 30f, 30f);

        rect.Encapsulate(other);
        await Assert.That(rect).IsEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldNotGrowsWhenTheOtherRectIsEmpty()
    {
        Rect rect = new(10f, 10f, 30f, 30f);

        rect.Encapsulate(Rect.Zero);
        await Assert.That(rect).IsEqualTo(rect);
    }

    [Test]
    public async Task EncapsulateShouldGrowsIntoOtherRectWhenCurrentIsEmpty()
    {
        Rect rect = new(10f, 10f, 0f, 0f);
        Rect other = new(20f, 20f, 30f, 30f);

        rect.Encapsulate(other);
        await Assert.That(rect).IsEqualTo(other);
    }

    [Test]
    public async Task EncapsulateShouldGrowsToIncludeOtherRect()
    {
        Rect rect = new(10f, 10f, 30f, 30f);
        Rect other = new(5f, 5f, 40f, 40f);

        rect.Encapsulate(other);

        Rect expectedRect = new(5f, 5f, 40f, 40f);
        await Assert.That(rect).IsEqualTo(expectedRect);
    }

    [Test]
    public async Task OverlapsShouldReturnTrueWhenRectsOverlap()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 30f, 30f);

        await Assert.That(rectA.Overlaps(rectB)).IsTrue();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenRectsDoNotOverlap()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(50f, 50f, 30f, 30f);

        await Assert.That(rectA.Overlaps(rectB)).IsFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenOtherRectIsEmpty()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 0f, 0f);

        await Assert.That(rectA.Overlaps(rectB)).IsFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenThisRectIsEmpty()
    {
        Rect rectA = new(10f, 10f, 0f, 0f);
        Rect rectB = new(20f, 20f, 30f, 30f);

        await Assert.That(rectA.Overlaps(rectB)).IsFalse();
    }

    [Test]
    public async Task OverlapsShouldReturnFalseWhenRectsTouchAtEdgesAndTheyNotOverlappingInTheArea()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(40f, 10f, 30f, 30f);

        await Assert.That(rectA.Overlaps(rectB)).IsFalse();
    }

    [Test]
    public async Task IntersectShouldReturnCorrectIntersectionRect()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 30f, 30f);

        Rect expectedIntersection = new(20f, 20f, 20f, 20f);
        await Assert.That(Rect.Intersect(rectA, rectB)).IsEqualTo(expectedIntersection);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenNoOverlap()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(50f, 50f, 30f, 30f);

        await Assert.That(Rect.Intersect(rectA, rectB)).IsEqualTo(Rect.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenOneRectIsEmpty()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 0f, 0f);

        await Assert.That(Rect.Intersect(rectA, rectB)).IsEqualTo(Rect.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnEmptyRectWhenBothRectsAreEmpty()
    {
        Rect rectA = new(10f, 10f, 0f, 0f);
        Rect rectB = new(20f, 20f, 0f, 0f);

        await Assert.That(Rect.Intersect(rectA, rectB)).IsEqualTo(Rect.Zero);
    }

    [Test]
    public async Task IntersectShouldReturnCorrectIntersectionWhenRectsTouchAtEdges()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(40f, 10f, 30f, 30f);

        Rect expectedIntersection = new(40f, 10f, 0f, 30f);
        await Assert.That(Rect.Intersect(rectA, rectB)).IsEqualTo(expectedIntersection);
    }

    [Test]
    public async Task UnionShouldReturnCorrectUnionRect()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 30f, 30f);

        Rect expectedUnion = new(10f, 10f, 40f, 40f);
        await Assert.That(Rect.Union(rectA, rectB)).IsEqualTo(expectedUnion);
    }

    [Test]
    public async Task UnionShouldReturnRightRectWhenLeftRectIsEmpty()
    {
        Rect rectA = new(10f, 10f, 0f, 0f);
        Rect rectB = new(20f, 20f, 30f, 30f);

        await Assert.That(Rect.Union(rectA, rectB)).IsEqualTo(rectB);
    }

    [Test]
    public async Task UnionShouldReturnLeftRectWhenRightRectIsEmpty()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 0f, 0f);

        await Assert.That(Rect.Union(rectA, rectB)).IsEqualTo(rectA);
    }

    [Test]
    public async Task UnionShouldReturnEmptyRectWhenBothRectsAreEmpty()
    {
        Rect rectA = new(10f, 10f, 0f, 0f);
        Rect rectB = new(20f, 20f, 0f, 0f);

        await Assert.That(Rect.Union(rectA, rectB)).IsEqualTo(Rect.Zero);
    }

    [Test]
    public async Task EqualsShouldReturnTrueForIdenticalRects()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(10f, 10f, 30f, 30f);

        await Assert.That(rectA.Equals(rectB)).IsTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalseForDifferentRects()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 30f, 30f);

        await Assert.That(rectA.Equals(rectB)).IsFalse();
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenBothRectsAreEmpty()
    {
        Rect rectA = new(10f, 10f, 0f, 0f);
        Rect rectB = new(20f, 20f, 0f, 0f);

        await Assert.That(rectA.Equals(rectB)).IsTrue();
    }

    [Test]
    public async Task EqualsObjectShouldReturnTrueForIdenticalRects()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        object rectB = new Rect(10f, 10f, 30f, 30f);

        await Assert.That(rectA.Equals(rectB)).IsTrue();
    }

    [Test]
    public async Task EqualsObjectShouldReturnFalseForDifferentRects()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        object rectB = new Rect(20f, 20f, 30f, 30f);

        await Assert.That(rectA.Equals(rectB)).IsFalse();
    }

    [Test]
    public async Task EqualsObjectShouldReturnFalseForNonRectObject()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        object nonRect = "NotARect";

        await Assert.That(rectA.Equals(nonRect)).IsFalse();
    }

    [Test]
    public async Task ToStringShouldReturnCorrectFormat()
    {
        Rect rect = new(10f, 20f, 30f, 40f);
        await Assert.That(rect.ToString()).IsEqualTo("(10, 20, 30, 40)");
    }

    [Test]
    public async Task OperatorEqualityShouldReturnTrueForIdenticalRects()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(10f, 10f, 30f, 30f);

        await Assert.That(rectA == rectB).IsTrue();
    }

    [Test]
    public async Task OperatorEqualityShouldReturnFalseForDifferentRects()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 30f, 30f);

        await Assert.That(rectA == rectB).IsFalse();
    }

    [Test]
    public async Task OperatorInequalityShouldReturnFalseForIdenticalRects()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(10f, 10f, 30f, 30f);

        await Assert.That(rectA != rectB).IsFalse();
    }

    [Test]
    public async Task OperatorInequalityShouldReturnTrueForDifferentRects()
    {
        Rect rectA = new(10f, 10f, 30f, 30f);
        Rect rectB = new(20f, 20f, 30f, 30f);

        await Assert.That(rectA != rectB).IsTrue();
    }
}
