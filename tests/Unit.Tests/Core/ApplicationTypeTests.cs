// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using KappaDuck.Quack.Core;

namespace Unit.Tests.Core;

public sealed class ApplicationTypeTests
{
    [Test]
    [Arguments(ApplicationType.Game, "Game")]
    [Arguments(ApplicationType.MediaPlayer, "MediaPlayer")]
    [Arguments(ApplicationType.Application, "Application")]
    [Arguments((ApplicationType)999, "Application")]
    public async Task NameShouldHaveTheExpectedValue(ApplicationType type, string expectedName)
    {
        string name = type.Name;
        await Assert.That(name).IsEqualTo(expectedName);
    }
}
