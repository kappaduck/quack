#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.IO.Compression;

string rootPath = GetRootPath();
string installPath = Path.Combine(rootPath, "SDL3");

await InstallSDLAsync().ConfigureAwait(false);

async Task InstallSDLAsync()
{
    const string version = "3.2.20";
    Uri download = new($"https://github.com/libsdl-org/SDL/releases/download/release-{version}/SDL3-{version}-win32-x64.zip");

    string zipFilePath = $"{Path.Combine(rootPath, $"SDL3-{version}")}.zip";

    Console.WriteLine($"Installing SDL3-{version}...");
    await DownloadAsync(download, zipFilePath).ConfigureAwait(false);

    Console.WriteLine("Extracting SDL3...");
    await ExtractFilesAsync(zipFilePath).ConfigureAwait(false);

    Console.WriteLine("Moving DLL files to installation...");
    MoveDllFiles(zipFilePath);
}

async static Task DownloadAsync(Uri uri, string filePath)
{
    using HttpClient client = new();

    Console.WriteLine($"Downloading {uri}...");
    Stream stream = await client.GetStreamAsync(uri).ConfigureAwait(false);

    FileStream fileStream = File.Create(filePath);

    await using (fileStream.ConfigureAwait(false))
    {
        await stream.CopyToAsync(fileStream).ConfigureAwait(false);
    }
}

async Task ExtractFilesAsync(string filePath)
{
    string directory = Path.Combine(rootPath, filePath[..filePath.LastIndexOf('.')]);

    await ZipFile.ExtractToDirectoryAsync(filePath, directory, overwriteFiles: true).ConfigureAwait(false);
    File.Delete(filePath);
}

void MoveDllFiles(string filePath)
{
    Directory.CreateDirectory(installPath);
    DirectoryInfo source = new(filePath[..filePath.LastIndexOf('.')]);

    foreach (FileInfo sourceFile in source.EnumerateFiles("*.dll", SearchOption.AllDirectories))
    {
        string destination = Path.Combine(installPath, sourceFile.Name);
        sourceFile.MoveTo(destination, overwrite: true);
    }

    source.Delete(recursive: true);
}

static string GetRootPath()
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
