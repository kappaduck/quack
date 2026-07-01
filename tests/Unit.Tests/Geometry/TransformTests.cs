// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;

namespace Unit.Tests.Geometry;

internal sealed class TransformTests
{
    [Test]
    public async Task IdentityShouldLeaveAPointUnchanged()
    {
        PointF point = Transform.Identity.TransformPoint(new PointF(3f, -7f));

        await point.X.Should().BeEqualTo(3f);
        await point.Y.Should().BeEqualTo(-7f);
    }

    [Test]
    public async Task IdentityShouldBeNeutralWhenComposedWithAnotherTransform()
    {
        Transform transform = Transform.Create(new PointF(12f, 34f), Angle.FromDegrees(45f), new Vector2(2f, 2f));
        PointF direct = transform.TransformPoint(new PointF(1f, 1f));

        PointF point = (Transform.Identity * transform).TransformPoint(new PointF(1f, 1f));

        await point.X.Should().BeEqualTo(direct.X);
        await point.Y.Should().BeEqualTo(direct.Y);
    }

    [Test]
    public async Task TranslationShouldOffsetAPoint()
    {
        PointF point = Transform.Translation(10f, 20f).TransformPoint(new PointF(1f, 2f));

        await point.X.Should().BeEqualTo(11f);
        await point.Y.Should().BeEqualTo(22f);
    }

    [Test]
    public async Task ScalingShouldMultiplyEachAxis()
    {
        PointF point = Transform.Scaling(2, 3).TransformPoint(new PointF(4f, 5f));

        await point.X.Should().BeEqualTo(8f);
        await point.Y.Should().BeEqualTo(15f);
    }

    [Test]
    public async Task RotationShouldTurnClockwiseOnScreen()
    {
        PointF point = Transform.Rotation(Angle.FromDegrees(90f)).TransformPoint(new PointF(1f, 0f));

        await point.X.Should().BeEqualTo(0f);
        await point.Y.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task RotationShouldKeepTheRotationCenterFixed()
    {
        PointF center = new(5f, 5f);
        PointF point = Transform.Rotation(Angle.FromDegrees(37f), center).TransformPoint(center);

        await point.X.Should().BeEqualTo(5f);
        await point.Y.Should().BeEqualTo(5f);
    }

    [Test]
    public async Task RotationShouldSwingAPointAroundTheGivenCenter()
    {
        PointF center = new(2f, 2f);
        PointF point = Transform.Rotation(Angle.FromDegrees(90f), center).TransformPoint(new PointF(3f, 2f));

        await point.X.Should().BeEqualTo(2f);
        await point.Y.Should().BeEqualTo(3f);
    }

    [Test]
    [Arguments(0f, 1f, 0f)]
    [Arguments(90f, 0f, 1f)]
    [Arguments(180f, -1f, 0f)]
    [Arguments(270f, 0f, -1f)]
    public async Task RotationShouldMapTheUnitXAxisToTheExpectedDirection(float degrees, float expectedX, float expectedY)
    {
        PointF point = Transform.Rotation(Angle.FromDegrees(degrees)).TransformPoint(new PointF(1f, 0f));

        await point.X.Should().BeEqualTo(expectedX);
        await point.Y.Should().BeEqualTo(expectedY);
    }

    [Test]
    public async Task OperatorMultiplicationShouldApplyTheRightOperandFirst()
    {
        Transform transform = Transform.Rotation(Angle.FromDegrees(90f)) * Transform.Translation(100f, 0f);
        PointF point = transform.TransformPoint(new PointF(1f, 0f));

        await point.X.Should().BeEqualTo(0f);
        await point.Y.Should().BeEqualTo(101f);
    }

    [Test]
    public async Task OperatorMultiplicationShouldNotBeCommutative()
    {
        Transform left = Transform.Rotation(Angle.FromDegrees(90f)) * Transform.Translation(100f, 0f);
        Transform right = Transform.Translation(100f, 0f) * Transform.Rotation(Angle.FromDegrees(90f));

        PointF leftPoint = left.TransformPoint(new PointF(1f, 0f));
        PointF rightPoint = right.TransformPoint(new PointF(1f, 0f));

        await leftPoint.X.Should().BeEqualTo(0f);
        await leftPoint.Y.Should().BeEqualTo(101f);

        await rightPoint.X.Should().BeEqualTo(100f);
        await rightPoint.Y.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task CreateShouldPlaceTheOriginExactlyAtPosition()
    {
        Transform transform = Transform.Create(new PointF(100f, 50f), Angle.FromDegrees(30f), new Vector2(2f, 2f), new PointF(8f, 8f));

        PointF point = transform.TransformPoint(new PointF(8f, 8f));

        await point.X.Should().BeEqualTo(100f);
        await point.Y.Should().BeEqualTo(50f);
    }

    [Test]
    public async Task CreateShouldScaleThenTranslateWhenThereIsNoRotation()
    {
        Transform transform = Transform.Create(new PointF(10f, 10f), Angle.Zero, new Vector2(3f, 4f));

        PointF point = transform.TransformPoint(new PointF(2f, 1f));

        await point.X.Should().BeEqualTo(16f);
        await point.Y.Should().BeEqualTo(14f);
    }

    [Test]
    public async Task InvertedShouldUndoTheTransform()
    {
        Transform transform = Transform.Create(new PointF(40f, -15f), Angle.FromDegrees(63f), new Vector2(1.5f, 0.5f));

        PointF start = new(7f, 9f);
        PointF point = transform.Inverted.TransformPoint(transform.TransformPoint(start));

        await point.X.Should().BeCloseTo(7f, 1e-3f);
        await point.Y.Should().BeCloseTo(9f, 1e-3f);
    }

    [Test]
    public async Task InvertedShouldReturnIdentityWhenTheTransformIsNotInvertible()
    {
        Transform transform = Transform.Scaling(0f, 1f);
        Transform inverted = transform.Inverted;

        await inverted.Should().BeEqualTo(Transform.Identity);
    }

    [Test]
    public async Task TransformRectShouldShiftTheRectangleUnderTranslation()
    {
        Rect rect = Transform.Translation(5f, 5f).TransformRect(new Rect(0f, 0f, 10f, 20f));

        await rect.X.Should().BeEqualTo(5f);
        await rect.Y.Should().BeEqualTo(5f);
        await rect.Width.Should().BeEqualTo(10f);
        await rect.Height.Should().BeEqualTo(20f);
    }

    [Test]
    public async Task TransformRectShouldGrowTheBoundsUnderRotation()
    {
        Rect rect = Transform.Rotation(Angle.FromDegrees(45f)).TransformRect(new Rect(0f, 0f, 10f, 10f));

        float diag = 10 * MathF.Sqrt(2f);

        await rect.Width.Should().BeCloseTo(diag, 1e-3f);
        await rect.Height.Should().BeCloseTo(diag, 1e-3f);
    }

    [Test]
    public async Task TryDecomposeShouldRecoverTranslationRotationAndScale()
    {
        Transform transform = Transform.Create(new PointF(120f, -40f), Angle.FromDegrees(33f), new Vector2(2f, 3f));

        bool result = transform.TryDecompose(out PointF translation, out Angle rotation, out Vector2 scale);

        await result.Should().BeTrue();

        await translation.X.Should().BeEqualTo(120f);
        await translation.Y.Should().BeEqualTo(-40f);
        await rotation.Degrees.Should().BeCloseTo(33f, 1e-3f);
        await scale.X.Should().BeEqualTo(2f);
        await scale.Y.Should().BeEqualTo(3f);
    }

    [Test]
    public async Task TryDecomposeShouldRoundTripBackToTheSameMapping()
    {
        Transform original = Transform.Create(new PointF(5f, 6f), Angle.FromDegrees(80f), new Vector2(1.25f, 2.5f));

        original.TryDecompose(out PointF translation, out Angle rotation, out Vector2 scale);

        Transform rebuilt = Transform.Create(translation, rotation, scale);

        PointF left = original.TransformPoint(new PointF(3f, 4f));
        PointF right = rebuilt.TransformPoint(new PointF(3f, 4f));

        await right.X.Should().BeEqualTo(left.X);
        await right.Y.Should().BeEqualTo(left.Y);
    }

    [Test]
    public async Task TryDecomposeShouldReportReflectionAsNegativeScale()
    {
        bool result = Transform.Scaling(-2f, 3f).TryDecompose(out _, out _, out Vector2 scale);

        await result.Should().BeTrue();
        await scale.Y.Should().BeLessThan(0f);
    }

    [Test]
    public async Task TryDecomposeShouldFailWhenTheTransformShears()
    {
        Transform sheared = Transform.Scaling(1f, 5f) * Transform.Rotation(Angle.FromDegrees(30f));
        bool result = sheared.TryDecompose(out _, out Angle rotation, out Vector2 scale);

        await result.Should().BeFalse();
        await rotation.Should().BeEqualTo(Angle.Zero);
        await scale.Should().BeEqualTo(Vector2.One);
    }

    [Test]
    public async Task EqualsShouldReturnTrueWhenAreEquals()
    {
        Transform left = Transform.Translation(3f, 4f);
        Transform right = Transform.Translation(3f, 4f);

        bool result = left == right;

        await result.Should().BeTrue();
    }

    [Test]
    public async Task EqualsShouldReturnFalserueWhenAreNotEquals()
    {
        Transform left = Transform.Translation(3f, 4f);
        Transform right = Transform.Translation(3f, 5f);

        bool result = left == right;

        await result.Should().BeFalse();
    }

    [Test]
    public async Task NotEqualsShouldReturnTrueWhenAreNotEquals()
    {
        Transform left = Transform.Translation(3f, 4f);
        Transform right = Transform.Translation(3f, 5f);

        bool result = left != right;

        await result.Should().BeTrue();
    }

    [Test]
    public async Task NotEqualsShouldReturnFalseWhenAreEquals()
    {
        Transform left = Transform.Translation(3f, 4f);
        Transform right = Transform.Translation(3f, 4f);

        bool result = left != right;

        await result.Should().BeFalse();
    }
}
