// Copyright (c) KappaDuck.
// Licensed under the MIT license.

using KappaDuck.Quack.Events;

namespace Integration.Tests.Events;

[NotInParallel]
internal sealed class EventQueueTests
{
    [Before(Test)]
    public void FlushEvents()
    {
        EventQueue.Pump();
        EventQueue.Flush();
    }

    [Test]
    public async Task PeekShouldReturnZeroWhenDestinationIsEmpty()
    {
        Span<Event> events = [];

        int count = EventQueue.Peek(events);
        await count.Should().BeZero();
    }

    [Test]
    public async Task PeekShouldFillDestination()
    {
        Span<Event> events = new Event[3];

        EventQueue.Push(new QuitRequestedEvent());
        EventQueue.Push(new QuitRequestedEvent());

        int count = EventQueue.Peek(events);

        await count.Should().BeEqualTo(2);
    }

    [Test]
    public async Task PeekOfTypeShouldReturnZeroWhenDestinationIsEmpty()
    {
        Span<QuitRequestedEvent> events = [];

        int count = EventQueue.Peek(events);
        await count.Should().BeZero();
    }

    [Test]
    public async Task PeekOfTypeShouldFillDestination()
    {
        Span<QuitRequestedEvent> events = new QuitRequestedEvent[3];

        EventQueue.Push(new QuitRequestedEvent());
        EventQueue.Push(new QuitRequestedEvent());

        int count = EventQueue.Peek(events);

        await count.Should().BeEqualTo(2);
    }

    [Test]
    public async Task PushShouldReturnZeroWhenDestinationIsEmpty()
    {
        Span<Event> events = [];

        int count = EventQueue.Push(events);
        await count.Should().BeZero();
    }

    [Test]
    public async Task PushShouldPopulateTheQueue()
    {
        Span<Event> events = [new QuitRequestedEvent()];

        int count = EventQueue.Push(events);
        await count.Should().BeEqualTo(1);
    }

    [Test]
    public async Task RetrieveShouldReturnZeroWhenDestinationIsEmpty()
    {
        Span<Event> events = [];

        int count = EventQueue.Retrieve(events);
        await count.Should().BeZero();
    }

    [Test]
    public async Task RetrieveShouldPopulateTheQueue()
    {
        Span<Event> events = new Event[3];

        EventQueue.Push(new QuitRequestedEvent());
        EventQueue.Push(new QuitRequestedEvent());

        int count = EventQueue.Retrieve(events);
        await count.Should().BeEqualTo(2);
    }

    [Test]
    public async Task RetrieveOfTypeShouldReturnZeroWhenDestinationIsEmpty()
    {
        Span<QuitRequestedEvent> events = [];

        int count = EventQueue.Retrieve(events);
        await count.Should().BeZero();
    }

    [Test]
    public async Task RetrieveOfTypeShouldPopulateTheQueue()
    {
        Span<QuitRequestedEvent> events = new QuitRequestedEvent[3];

        EventQueue.Push(new QuitRequestedEvent());
        EventQueue.Push(new QuitRequestedEvent());

        int count = EventQueue.Retrieve(events);
        await count.Should().BeEqualTo(2);
    }
}
