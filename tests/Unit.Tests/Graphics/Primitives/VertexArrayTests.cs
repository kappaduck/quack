// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Geometry;
using KappaDuck.Quack.Graphics.Drawing;
using KappaDuck.Quack.Graphics.Primitives;
using KappaDuck.Quack.Graphics.Rendering;
using KappaDuck.Quack.Video.Pixels;
using System.Diagnostics.CodeAnalysis;

namespace Unit.Tests.Graphics.Primitives;

internal sealed class VertexArrayTests
{
    private readonly CapturingTarget _target = new();

    [Test]
    public async Task ConstructorShouldStartEmpty()
    {
        VertexArray array = [];
        await array.Count.Should().BeEqualTo(0);
    }

    [Test]
    public async Task ConstructorWithNegativeCapacityShouldThrow()
    {
        await Assert.That(() => new VertexArray(PrimitiveType.Triangles, -1))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task PrimitiveTypeShouldDefaultToTriangles()
    {
        VertexArray array = [];
        await array.PrimitiveType.Should().BeEqualTo(PrimitiveType.Triangles);
    }

    [Test]
    public async Task TextureShouldBeNullByDefault()
    {
        VertexArray array = [];
        await array.Texture.Should().BeNull();
    }

    [Test]
    public async Task AddShouldIncreaseCount()
    {
        VertexArray array = [];
        array.Add(Vertex(0f, 0f));

        await array.Count.Should().BeEqualTo(1);
    }

    [Test]
    public async Task AddShouldStoreTheVertex()
    {
        VertexArray array = [];
        array.Add(Vertex(3f, 4f));

        await array[0].Position.Should().BeEqualTo(new PointF(3f, 4f));
    }

    [Test]
    public async Task AddRangeShouldAppendEveryVertex()
    {
        VertexArray array = [];
        array.AddRange(Vertex(0f, 0f), Vertex(1f, 1f));

        await array.Count.Should().BeEqualTo(2);
    }

    [Test]
    public async Task AddShouldGrowBeyondInitialCapacity()
    {
        VertexArray array = new(capacity: 1)
        {
            Vertex(0f, 0f),
            Vertex(1f, 1f),
            Vertex(2f, 2f)
        };

        await array.Count.Should().BeEqualTo(3);
        await array[2].Position.Should().BeEqualTo(new PointF(2f, 2f));
    }

    [Test]
    public async Task AddQuadShouldAddSixVertices()
    {
        VertexArray array = [];
        array.AddQuad(new Rect(5f, 10f, 20f, 30f), Colors.White);

        await array.Count.Should().BeEqualTo(6);
    }

    [Test]
    public async Task AddQuadShouldFormTwoTrianglesCoveringTheRectangle()
    {
        VertexArray array = [];
        array.AddQuad(new Rect(5f, 10f, 20f, 30f), Colors.White);

        await array[0].Position.Should().BeEqualTo(new PointF(5f, 10f));
        await array[1].Position.Should().BeEqualTo(new PointF(25f, 10f));
        await array[2].Position.Should().BeEqualTo(new PointF(25f, 40f));
        await array[3].Position.Should().BeEqualTo(new PointF(5f, 10f));
        await array[4].Position.Should().BeEqualTo(new PointF(25f, 40f));
        await array[5].Position.Should().BeEqualTo(new PointF(5f, 40f));
    }

    [Test]
    public async Task AddQuadShouldUseTheFullTextureByDefault()
    {
        VertexArray array = [];
        array.AddQuad(new Rect(0f, 0f, 10f, 10f), Colors.White);

        await array[0].TextureCoordinate.Should().BeEqualTo(new PointF(0f, 0f));
        await array[2].TextureCoordinate.Should().BeEqualTo(new PointF(1f, 1f));
    }

    [Test]
    public async Task AddQuadWithRegionShouldMapTextureCoordinates()
    {
        VertexArray array = [];
        array.AddQuad(new Rect(0f, 0f, 10f, 10f), new Rect(0.25f, 0.5f, 0.25f, 0.25f), Colors.White);

        await array[0].TextureCoordinate.Should().BeEqualTo(new PointF(0.25f, 0.5f));
        await array[2].TextureCoordinate.Should().BeEqualTo(new PointF(0.5f, 0.75f));
    }

    [Test]
    public async Task AddQuadShouldTintEveryVertex()
    {
        Color color = new(10, 20, 30, 40);

        VertexArray array = [];
        array.AddQuad(new Rect(0f, 0f, 10f, 10f), color);

        Vertex[] vertices = [.. array];
        ColorF expected = color.ToColorF();

        await vertices.Should().All(v => v.Color.Equals(expected));
    }

    [Test]
    public async Task IndexerShouldModifyVertexInPlace()
    {
        VertexArray array = [];
        array.Add(Vertex(0f, 0f));

        array[0].Position = new PointF(42f, 24f);

        await array[0].Position.Should().BeEqualTo(new PointF(42f, 24f));
    }

    [Test]
    [Arguments(-1)]
    [Arguments(3)]
    public async Task IndexerWithInvalidIndexShouldThrow(int index)
    {
        VertexArray array = [Vertex(0f, 0f), Vertex(1f, 1f), Vertex(2f, 2f)];

        await Assert.That(() => _ = array[index])
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ResizeShouldGrowWithDefaultVertices()
    {
        VertexArray array = [];
        array.Add(Vertex(1f, 1f));
        array.Resize(3);

        await array.Count.Should().BeEqualTo(3);
        await array[2].Position.Should().BeEqualTo(new PointF(0f, 0f));
    }

    [Test]
    public async Task ResizeAfterClearShouldExposeDefaultVertices()
    {
        VertexArray array = [Vertex(5f, 5f), Vertex(6f, 6f)];

        array.Clear();
        array.Resize(2);

        await array[0].Position.Should().BeEqualTo(new PointF(0f, 0f));
        await array[1].Position.Should().BeEqualTo(new PointF(0f, 0f));
    }

    [Test]
    public async Task ResizeShouldDropTrailingVertices()
    {
        VertexArray array = [Vertex(0f, 0f), Vertex(1f, 1f), Vertex(2f, 2f)];
        array.Resize(1);

        await array.Count.Should().BeEqualTo(1);
    }

    [Test]
    public async Task ResizeWithNegativeCountShouldThrow()
    {
        VertexArray array = [];

        await Assert.That(() => array.Resize(-1))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task ClearShouldResetCount()
    {
        VertexArray array = [Vertex(0f, 0f), Vertex(1f, 1f), Vertex(2f, 2f)];
        array.Clear();

        await array.Count.Should().BeEqualTo(0);
    }

    [Test]
    public async Task AsSpanShouldReturnTheCurrentVertices()
    {
        VertexArray array = [Vertex(0f, 0f), Vertex(1f, 1f)];

        await array.AsSpan().Length.Should().BeEqualTo(2);
    }

    [Test]
    public async Task CollectionExpressionShouldCreateTrianglesVertexArray()
    {
        VertexArray array = [Vertex(0f, 0f), Vertex(10f, 0f), Vertex(10f, 10f)];

        await array.Count.Should().BeEqualTo(3);
        await array.PrimitiveType.Should().BeEqualTo(PrimitiveType.Triangles);
    }

    [Test]
    public async Task CollectionExpressionWithPrimitiveTypeShouldSetIt()
    {
        VertexArray array = [with(PrimitiveType.TriangleFan), Vertex(0f, 0f), Vertex(10f, 0f), Vertex(10f, 10f)];

        await array.Count.Should().BeEqualTo(3);
        await array.PrimitiveType.Should().BeEqualTo(PrimitiveType.TriangleFan);
    }

    [Test]
    public async Task CollectionExpressionWithCapacityShouldCreateVertexArray()
    {
        VertexArray array = [with(PrimitiveType.Triangles, capacity: 10), Vertex(0f, 0f), Vertex(10f, 0f), Vertex(10f, 10f)];

        await array.Count.Should().BeEqualTo(3);
        await array.PrimitiveType.Should().BeEqualTo(PrimitiveType.Triangles);
    }

    [Test]
    public async Task CreateWithNegativeCapacityShouldThrow()
    {
        await Assert.That(() => VertexArray.Create(PrimitiveType.Triangles, -1, []))
                    .ThrowsExactly<ArgumentOutOfRangeException>();
    }

    [Test]
    public async Task DrawWithTrianglesShouldSubmitWithoutIndices()
    {
        VertexArray array = [Vertex(0f, 0f), Vertex(10f, 0f), Vertex(10f, 10f)];

        array.Draw(_target, RenderState.Default);

        await _target.Drawn.Should().BeTrue();
        await _target.Indices.Should().BeEmpty();
        await _target.Vertices.Length.Should().BeEqualTo(3);
    }

    [Test]
    public async Task DrawWithFewerThanThreeVerticesShouldNotDraw()
    {
        VertexArray array = [Vertex(0f, 0f), Vertex(10f, 0f)];

        array.Draw(_target, RenderState.Default);

        await _target.Drawn.Should().BeFalse();
    }

    [Test]
    public async Task DrawWithTriangleStripShouldGenerateStripIndices()
    {
        VertexArray array =
        [
            with(PrimitiveType.TriangleStrip),
            Vertex(0f, 0f), Vertex(10f, 0f), Vertex(10f, 10f), Vertex(0f, 10f)
        ];

        array.Draw(_target, RenderState.Default);

        await _target.Indices.Length.Should().BeEqualTo(6);
        await _target.Indices[3].Should().BeEqualTo(1);
    }

    [Test]
    public async Task DrawWithTriangleFanShouldGenerateFanIndices()
    {
        VertexArray array =
        [
            with(PrimitiveType.TriangleFan),
            Vertex(0f, 0f), Vertex(10f, 0f), Vertex(10f, 10f), Vertex(0f, 10f)
        ];

        array.Draw(_target, RenderState.Default);

        await _target.Indices.Length.Should().BeEqualTo(6);
        await _target.Indices[3].Should().BeEqualTo(0);
    }

    private static Vertex Vertex(float x, float y) => new(new PointF(x, y), Colors.White);

    private sealed class CapturingTarget : IRenderTarget
    {
        public bool Drawn { get; private set; }

        public Vertex[] Vertices { get; private set; } = [];

        public int[] Indices { get; private set; } = [];

        public RenderState State { get; private set; }

        public void Draw(IDrawable drawable) => drawable.Draw(this, RenderState.Default);

        public void Draw(IDrawable drawable, RenderState state) => drawable.Draw(this, state);

        public void Draw(ReadOnlySpan<Vertex> vertices, RenderState state)
        {
            Drawn = true;
            Vertices = vertices.ToArray();
            Indices = [];
            State = state;
        }

        [MemberNotNull(nameof(Indices))]
        public void Draw(ReadOnlySpan<Vertex> vertices, ReadOnlySpan<int> indices, RenderState state)
        {
            Drawn = true;
            Vertices = vertices.ToArray();
            Indices = indices.ToArray();
            State = state;
        }
    }
}
