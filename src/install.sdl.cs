#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.IO.Compression;

await new Installer("SDL3", "3.2.20").InstallAsync("SDL").ConfigureAwait(false);
await new Installer("SDL3_image", "3.2.4").InstallAsync("SDL_image").ConfigureAwait(false);

file sealed class Installer
{
    private readonly string _libraryPath;
    private readonly string _library;
    private readonly string _version;
    private readonly string _rootPath;
    private readonly string _installPath;

    public Installer(string library, string version)
    {
        _rootPath = GetRootPath();
        _installPath = Path.Combine(_rootPath, "SDL3", OperatingSystem.IsWindows() ? "win32" : "linux");

        _library = $"{library}-{version}";
        _version = version;

        _libraryPath = $"{Path.Combine(_rootPath, $"{library}-{version}")}.zip";
    }

    internal async Task InstallAsync(string repository)
    {
        await DownloadAsync(repository).ConfigureAwait(false);
        await ExtractFilesAsync().ConfigureAwait(false);
        MoveLibs();

        Console.WriteLine($"{_library} installation complete.\n");
    }

    private async Task DownloadAsync(string repository)
    {
        Uri download = new($"https://github.com/libsdl-org/{repository}/releases/download/release-{_version}/{_library}-win32-x64.zip");

        Console.WriteLine($"Downloading {_library}...");

        using HttpClient client = new();
        Stream stream = await client.GetStreamAsync(download).ConfigureAwait(false);

        FileStream fileStream = File.Create(_libraryPath);

        await using (fileStream.ConfigureAwait(false))
        {
            await stream.CopyToAsync(fileStream).ConfigureAwait(false);
        }
    }

    private async Task ExtractFilesAsync()
    {
        Console.WriteLine($"Extracting {_library}...");
        string directory = Path.Combine(_rootPath, _libraryPath[.._libraryPath.LastIndexOf('.')]);

        await ZipFile.ExtractToDirectoryAsync(_libraryPath, directory, overwriteFiles: true).ConfigureAwait(false);
        File.Delete(_libraryPath);
    }

    private void MoveLibs()
    {
        Console.WriteLine($"Copying {_library} to installation folder...");

        Directory.CreateDirectory(_installPath);
        DirectoryInfo source = new(_libraryPath[.._libraryPath.LastIndexOf('.')]);

        foreach (FileInfo sourceFile in source.EnumerateFiles("*.dll", SearchOption.AllDirectories))
        {
            string destination = Path.Combine(_installPath, sourceFile.Name);
            sourceFile.MoveTo(destination, overwrite: true);
        }

        source.Delete(recursive: true);
    }

    private static string GetRootPath()
    {
        const string rootFile = ".git";
        DirectoryInfo? directory = new(Directory.GetCurrentDirectory());

        while (directory is not null)
        {
            string path = Path.Combine(directory.FullName, rootFile);

            if (Directory.Exists(path))
                return directory.FullName;

            directory = directory.Parent;
        }

        throw new DirectoryNotFoundException("Root directory not found.");
    }

}
