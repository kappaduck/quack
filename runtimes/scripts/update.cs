using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

const string envPath = "runtimes/utils/sdl.env";

Dictionary<string, string> repositories = new()
{
    ["SDL3"] = "SDL",
    ["SDL3_IMAGE"] = "SDL_image",
    ["SDL3_TTF"] = "SDL_ttf",
    ["SDL3_MIXER"] = "SDL_mixer"
};

if (!File.Exists(envPath))
{
    Console.Error.WriteLine($"Cannot find the SDL environment file at '{envPath}'");
    return 1;
}

using HttpClient http = CreateClient();

string[] lines = await File.ReadAllLinesAsync(envPath);
List<ReleaseUpdate> updates = [];

for (int i = 0; i < lines.Length; i++)
{
    string line = lines[i];
    int separator = line.IndexOf('=');

    if (separator <= 0 || line.TrimStart().StartsWith('#'))
        continue;

    string key = line[..separator].Trim();
    string value = line[(separator + 1)..].Trim();

    if (!repositories.TryGetValue(key, out string? repository))
        continue;

    if (!Version.TryParse(value, out Version? current))
    {
        Console.Error.WriteLine($"Skipping '{key}': '{value}' is not a valid version.");
        continue;
    }

    Version? latest = await GetLatestStableVersionAsync(repository);

    if (latest is null)
    {
        Console.WriteLine($"{key}: no stable release found on libsdl-org/{repository}.");
        continue;
    }

    if (latest <= current)
    {
        Console.WriteLine($"{key}: up to date ({current}).");
        continue;
    }

    Console.WriteLine($"{key}: {current} -> {latest}");

    lines[i] = $"{key}={latest}";
    updates.Add(new ReleaseUpdate(key, repository, current, latest));
}

if (updates.Count == 0)
{
    Console.WriteLine("All SDL libraries are up to date.");

    SetOutput("updated", "false");
    return 0;
}

await File.WriteAllTextAsync(envPath, string.Join('\n', lines) + '\n');
await File.WriteAllTextAsync("pr-body.md", BuildTable(updates, "Newer stable SDL releases are available. This updates the versions used to build the native runtimes in `runtimes/utils/sdl.env`.", withLinks: true));

SetOutput("updated", "true");
AppendStepSummary(BuildTable(updates, "### SDL updates", withLinks: false));

Console.WriteLine($"Updated {updates.Count} SDL {(updates.Count == 1 ? "library" : "libraries")} in '{envPath}'.");
return 0;

static HttpClient CreateClient()
{
    HttpClient http = new()
    {
        BaseAddress = new Uri("https://api.github.com/"),
        Timeout = TimeSpan.FromSeconds(30)
    };

    string? token = Environment.GetEnvironmentVariable("GITHUB_TOKEN");

    if (!string.IsNullOrEmpty(token))
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
    http.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("quack-sdl-updater", "1.0"));
    http.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2026-03-10");

    return http;
}

async Task<Version?> GetLatestStableVersionAsync(string repository)
{
    using HttpResponseMessage response = await http.GetAsync($"repos/libsdl-org/{repository}/releases?per_page=100");

    if (!response.IsSuccessStatusCode)
    {
        string reason = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"GitHub returned {(int)response.StatusCode} for libsdl-org/{repository}: {reason}");
    }

    Release[]? releases = await response.Content.ReadFromJsonAsync(GithubContext.Default.ReleaseArray);
    Version? latest = null;

    foreach (Release release in releases ?? [])
    {
        if (release.Draft || release.Prerelease)
            continue;

        if (!TryParseReleaseVersion(release.Tag, out Version? version))
            continue;

        if (latest is null || version > latest)
            latest = version;
    }

    return latest;
}

static bool TryParseReleaseVersion(string? tag, out Version? version)
{
    const string prefix = "release-";
    version = null;

    if (tag is null || !tag.StartsWith(prefix, StringComparison.Ordinal))
        return false;

    return Version.TryParse(tag[prefix.Length..], out version);
}

static string BuildTable(IEnumerable<ReleaseUpdate> updates, string heading, bool withLinks)
{
    StringBuilder builder = new();

    builder.AppendLine(heading);
    builder.AppendLine();
    builder.AppendLine("| Library | Current | Latest |");
    builder.AppendLine("| --- | --- | --- |");

    foreach (ReleaseUpdate update in updates)
    {
        string latest = withLinks
            ? $"[{update.Latest}](https://github.com/libsdl-org/{update.Repository}/releases/tag/release-{update.Latest})"
            : update.Latest.ToString();

        builder.AppendLine($"| `{update.Key}` | {update.Current} | {latest} |");
    }

    return builder.ToString();
}

static void SetOutput(string name, string value)
{
    string? file = Environment.GetEnvironmentVariable("GITHUB_OUTPUT");

    if (!string.IsNullOrEmpty(file))
        File.AppendAllText(file, $"{name}={value}\n");
}

static void AppendStepSummary(string content)
{
    string? file = Environment.GetEnvironmentVariable("GITHUB_STEP_SUMMARY");

    if (!string.IsNullOrEmpty(file))
        File.AppendAllText(file, content);
}

internal sealed record Release
{
    [JsonPropertyName("tag_name")]
    public string? Tag { get; init; }

    public bool Draft { get; init; }

    public bool Prerelease { get; init; }
}

internal sealed record ReleaseUpdate(string Key, string Repository, Version Current, Version Latest);


[JsonSerializable(typeof(Release[]))]
internal sealed partial class GithubContext : JsonSerializerContext
{
}
