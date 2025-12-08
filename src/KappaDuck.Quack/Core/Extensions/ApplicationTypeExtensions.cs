using JetBrains.Annotations;

namespace KappaDuck.Quack.Core.Extensions;

/// <summary>
/// Provides extension methods for <see cref="ApplicationType"/>.
/// </summary>
[PublicAPI]
public static class ApplicationTypeExtensions
{
    extension(ApplicationType type)
    {
        /// <summary>
        /// Gets the human-readable name of the application type.
        /// </summary>
        /// <remarks>
        /// It returns "Unknown" for unrecognized application types.
        /// </remarks>
        public string Name => type switch
        {
            ApplicationType.Game => nameof(ApplicationType.Game),
            ApplicationType.MediaPlayer => nameof(ApplicationType.MediaPlayer),
            ApplicationType.Application => nameof(ApplicationType.Application),
            _ => "Unknown"
        };
    }
}
