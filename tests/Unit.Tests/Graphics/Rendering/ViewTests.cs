// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Rendering;

namespace Unit.Tests.Graphics.Rendering;

internal sealed class ViewTests
{
    [Test]
    public async Task ConstructorWithSizeShouldDefaultCenterToOrigin()
    {
        View view = new(new SizeF(200f, 100f));

        await view.Center.X.Should().BeEqualTo(0f);
        await view.Center.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task ConstructorWithSizeShouldSetSize()
    {
        View view = new(new SizeF(200f, 100f));

        await view.Size.Width.Should().BeEqualTo(200f);
        await view.Size.Height.Should().BeEqualTo(100f);
    }

    [Test]
    public async Task ConstructorWithCenterAndSizeShouldSetBoth()
    {
        View view = new(new Point(50f, 75f), new SizeF(200f, 100f));

        await view.Center.X.Should().BeEqualTo(50f);
        await view.Center.Y.Should().BeEqualTo(75f);
        await view.Size.Width.Should().BeEqualTo(200f);
        await view.Size.Height.Should().BeEqualTo(100f);
    }

    [Test]
    public async Task ConstructorWithRectShouldMatchTheRectsCenterAndSize()
    {
        Rect rect = new(100f, 200f, 400f, 300f);
        View view = new(rect);

        await view.Center.X.Should().BeEqualTo(300f);
        await view.Center.Y.Should().BeEqualTo(350f);
        await view.Size.Width.Should().BeEqualTo(400f);
        await view.Size.Height.Should().BeEqualTo(300f);
    }

    [Test]
    public async Task ConstructorShouldDefaultViewportToTheFullTarget()
    {
        View view = new(new SizeF(200f, 100f));

        await view.Viewport.X.Should().BeEqualTo(0f);
        await view.Viewport.Y.Should().BeEqualTo(0f);
        await view.Viewport.Width.Should().BeEqualTo(1f);
        await view.Viewport.Height.Should().BeEqualTo(1f);
    }

    [Test]
    public async Task ConstructorShouldDefaultRotationToZero()
    {
        View view = new(new SizeF(200f, 100f));

        await view.Rotation.Degrees.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task ConstructorShouldDefaultBoundsToNull()
    {
        View view = new(new SizeF(200f, 100f));

        await view.Bounds.HasValue.Should().BeFalse();
    }

    [Test]
    [Arguments(0f, 100f)]
    [Arguments(-10f, 100f)]
    [Arguments(100f, 0f)]
    [Arguments(100f, -10f)]
    public async Task ConstructorWithNonPositiveSizeShouldThrow(float width, float height)
    {
        await Assert.That(() => new View(new SizeF(width, height)))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task SizeSetterShouldUpdateTheSize()
    {
        View view = new(new SizeF(200f, 100f))
        {
            Size = new SizeF(400f, 300f)
        };

        await view.Size.Width.Should().BeEqualTo(400f);
        await view.Size.Height.Should().BeEqualTo(300f);
    }

    [Test]
    [Arguments(0f, 100f)]
    [Arguments(100f, 0f)]
    public async Task SizeSetterWithNonPositiveValueShouldThrow(float width, float height)
    {
        View view = new(new SizeF(200f, 100f));

        await Assert.That(() => view.Size = new SizeF(width, height))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task MoveShouldOffsetCenterByTheGivenVector()
    {
        View view = new(new Point(10f, 20f), new SizeF(200f, 100f));
        view.Move(new Vector2(5f, -5f));

        await view.Center.X.Should().BeEqualTo(15f);
        await view.Center.Y.Should().BeEqualTo(15f);
    }

    [Test]
    public async Task RotateShouldAddToCurrentRotation()
    {
        View view = new(new SizeF(200f, 100f))
        {
            Rotation = Angle.FromDegrees(30f)
        };

        view.Rotate(Angle.FromDegrees(15f));

        await view.Rotation.Degrees.Should().BeCloseTo(45f, 1e-3f);
    }

    [Test]
    public async Task ZoomShouldScaleSizeByFactor()
    {
        View view = new(new SizeF(200f, 100f));
        view.Zoom(2f);

        await view.Size.Width.Should().BeEqualTo(400f);
        await view.Size.Height.Should().BeEqualTo(200f);
    }

    [Test]
    [Arguments(0f)]
    [Arguments(-1f)]
    public async Task ZoomWithNonPositiveFactorShouldThrow(float factor)
    {
        View view = new(new SizeF(200f, 100f));

        await Assert.That(() => view.Zoom(factor))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ResetShouldSetCenterAndSizeFromTheRectAndClearRotation()
    {
        View view = new(new SizeF(200f, 100f))
        {
            Rotation = Angle.FromDegrees(90f)
        };

        view.Reset(new Rect(0f, 0f, 800f, 600f));

        await view.Center.X.Should().BeEqualTo(400f);
        await view.Center.Y.Should().BeEqualTo(300f);
        await view.Size.Width.Should().BeEqualTo(800f);
        await view.Size.Height.Should().BeEqualTo(600f);
        await view.Rotation.Degrees.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task MoveTowardsShouldMoveExactlyMaxDistanceWhenTargetIsFarther()
    {
        View view = new(new Point(0f, 0f), new SizeF(200f, 100f));
        view.MoveTowards(new Point(100f, 0f), maxDistance: 30f);

        await view.Center.X.Should().BeEqualTo(30f);
        await view.Center.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task MoveTowardsShouldSnapToTargetWhenWithinMaxDistance()
    {
        View view = new(new Point(0f, 0f), new SizeF(200f, 100f));
        view.MoveTowards(new Point(10f, 0f), maxDistance: 30f);

        await view.Center.X.Should().BeEqualTo(10f);
        await view.Center.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task MoveTowardsShouldSnapToTargetWhenExactlyAtMaxDistance()
    {
        View view = new(new Point(0f, 0f), new SizeF(200f, 100f));
        view.MoveTowards(new Point(30f, 0f), maxDistance: 30f);

        await view.Center.X.Should().BeEqualTo(30f);
        await view.Center.Y.Should().BeEqualTo(0f);
    }

    [Test]
    public async Task MoveTowardsShouldLeaveCenterUnchangedWhenAlreadyAtTarget()
    {
        View view = new(new Point(50f, 50f), new SizeF(200f, 100f));
        view.MoveTowards(new Point(50f, 50f), maxDistance: 30f);

        await view.Center.X.Should().BeEqualTo(50f);
        await view.Center.Y.Should().BeEqualTo(50f);
    }

    [Test]
    public async Task ComputeViewportShouldReturnTheFullTargetByDefault()
    {
        View view = new(new SizeF(200f, 100f));
        RectI pixels = view.ComputeViewport(new SizeI(1920, 1080));

        await pixels.X.Should().BeEqualTo(0);
        await pixels.Y.Should().BeEqualTo(0);
        await pixels.Width.Should().BeEqualTo(1920);
        await pixels.Height.Should().BeEqualTo(1080);
    }

    [Test]
    public async Task ComputeViewportShouldScaleByTheNormalizedRect()
    {
        View view = new(new SizeF(200f, 100f))
        {
            Viewport = new Rect(0.5f, 0f, 0.5f, 1f)
        };

        RectI pixels = view.ComputeViewport(new SizeI(1920, 1080));

        await pixels.X.Should().BeEqualTo(960);
        await pixels.Y.Should().BeEqualTo(0);
        await pixels.Width.Should().BeEqualTo(960);
        await pixels.Height.Should().BeEqualTo(1080);
    }

    [Test]
    public async Task ComputeViewportShouldClampToAtLeastOnePixelWhenTheNormalizedRectRoundsToZero()
    {
        View view = new(new SizeF(200f, 100f))
        {
            Viewport = new Rect(0f, 0f, 0.0001f, 0.0001f)
        };

        RectI pixels = view.ComputeViewport(new SizeI(100, 100));

        await pixels.Width.Should().BeEqualTo(1);
        await pixels.Height.Should().BeEqualTo(1);
    }

    [Test]
    public async Task GetTransformShouldMapCenterToTheMiddleOfTheViewport()
    {
        View view = new(new Point(500f, 300f), new SizeF(200f, 100f));
        Transform transform = view.GetTransform(new SizeI(200, 100));

        Point result = transform.TransformPoint(new Point(500f, 300f));

        await result.X.Should().BeEqualTo(100f);
        await result.Y.Should().BeEqualTo(50f);
    }

    [Test]
    public async Task GetTransformShouldMapTheSceneEdgeToTheViewportEdgeWhenSizeMatchesTheViewport()
    {
        View view = new(new Point(0f, 0f), new SizeF(200f, 100f));
        Transform transform = view.GetTransform(new SizeI(200, 100));

        Point result = transform.TransformPoint(new Point(100f, 50f));

        await result.X.Should().BeEqualTo(200f);
        await result.Y.Should().BeEqualTo(100f);
    }

    [Test]
    public async Task GetTransformShouldScaleWhenSizeDiffersFromTheViewport()
    {
        View view = new(new Point(0f, 0f), new SizeF(400f, 400f));
        Transform transform = view.GetTransform(new SizeI(200, 200));

        Point result = transform.TransformPoint(new Point(200f, 0f));

        await result.X.Should().BeEqualTo(200f);
        await result.Y.Should().BeEqualTo(100f);
    }

    [Test]
    public async Task GetTransformShouldRotateTheSceneOppositeToTheViewsRotation()
    {
        View view = new(new Point(0f, 0f), new SizeF(200f, 200f))
        {
            Rotation = Angle.FromDegrees(90f)
        };

        Transform transform = view.GetTransform(new SizeI(200, 200));
        Point result = transform.TransformPoint(new Point(100f, 0f));

        await result.X.Should().BeCloseTo(100f, 1e-3f);
        await result.Y.Should().BeCloseTo(0f, 1e-3f);
    }

    [Test]
    public async Task GetTransformShouldClampTheEffectiveCenterToBoundsWithoutMutatingCenter()
    {
        View view = new(new Point(5000f, 5000f), new SizeF(200f, 100f))
        {
            Bounds = new Rect(0f, 0f, 1000f, 600f)
        };

        Transform transform = view.GetTransform(new SizeI(200, 100));
        Point viewportCenter = transform.TransformPoint(new Point(900f, 550f));

        await viewportCenter.X.Should().BeEqualTo(100f);
        await viewportCenter.Y.Should().BeEqualTo(50f);

        await view.Center.X.Should().BeEqualTo(5000f);
        await view.Center.Y.Should().BeEqualTo(5000f);
    }

    [Test]
    public async Task GetTransformShouldCenterOnBoundsWhenTheViewIsLargerThanBoundsOnBothAxes()
    {
        View view = new(new Point(9999f, 9999f), new SizeF(500f, 500f))
        {
            Bounds = new Rect(0f, 0f, 100f, 100f)
        };

        Transform transform = view.GetTransform(new SizeI(500, 500));
        Point viewportCenter = transform.TransformPoint(new Point(50f, 50f));

        await viewportCenter.X.Should().BeEqualTo(250f);
        await viewportCenter.Y.Should().BeEqualTo(250f);
    }
}
