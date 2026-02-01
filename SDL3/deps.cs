#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Formats.Tar;
using System.IO.Compression;
using System.Security.Cryptography;

const string Url = "https://github.com/kappaduck/quack.runtimes/releases/download/v3.4.0";

const string Runtimes = $"{Url}/quack.sdl3.runtimes.tar.gz";
const string Checksums = $"{Url}/checksums.txt";

await DownloadAsync(Runtimes, "runtimes.tar.gz");
await DownloadAsync(Checksums, "checksums.txt");

VerifyChecksums("runtimes.tar.gz", "checksums.txt");
await ExtractAsync("runtimes.tar.gz", "SDL3");

Cleanup("runtimes.tar.gz", "checksums.txt");

static async Task DownloadAsync(string url, string output)
{
    Console.WriteLine($"Downloading {url}...");
    using HttpClient client = new();

    using HttpResponseMessage response = await client.GetAsync(url);

    if (!response.IsSuccessStatusCode)
    {
        await Console.Error.WriteLineAsync($"Failed to download {url}: {response.StatusCode}");
        Environment.Exit(1);
    }

    await using FileStream stream = File.Create(output);
    await response.Content.CopyToAsync(stream);
}

static void VerifyChecksums(string runtime, string checksumFile)
{
    string checksum = File.ReadAllText(checksumFile).Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
    byte[] bytes = Convert.FromHexString(checksum);

    using SHA256 sha256 = SHA256.Create();
    using FileStream stream = File.OpenRead(runtime);

    byte[] actual = sha256.ComputeHash(stream);

    if (!CryptographicOperations.FixedTimeEquals(actual, bytes))
    {
        Console.Error.WriteLine("Checksum verification failed!");
        Environment.Exit(1);
    }

    Console.WriteLine("Checksum verification succeeded.");
}

static async Task ExtractAsync(string archive, string destination)
{
    Console.WriteLine($"Extracting {archive} to {destination}...");

    await using FileStream stream = File.OpenRead(archive);
    await using GZipStream gzipStream = new(stream, CompressionMode.Decompress);

    await TarFile.ExtractToDirectoryAsync(gzipStream, destination, overwriteFiles: true);
}

static void Cleanup(params ReadOnlySpan<string> files)
{
    foreach (string file in files)
    {
        Console.WriteLine($"Deleting {file}...");
        File.Delete(file);
    }
}
