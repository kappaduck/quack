#!/usr/bin/env dotnet

// Copyright (c) KappaDuck. All rights reserved.
// The source code is licensed under MIT License.

using System.Formats.Tar;
using System.IO.Compression;
using System.Security.Cryptography;

const string Tag = "v3.2.28";

const string Runtimes = $"https://github.com/kappaduck/quack.runtimes/releases/download/{Tag}/quack.sdl3.runtimes.tar.gz";
const string Checksums = $"https://github.com/kappaduck/quack.runtimes/releases/download/{Tag}/checksums.txt";

await DownloadAsync(Runtimes, "runtimes.tar.gz");
await DownloadAsync(Checksums, "checksums.txt");

VerifyChecksum("runtimes.tar.gz", "checksums.txt");
await Extract("runtimes.tar.gz", "SDL3");
Cleanup("runtimes.tar.gz", "checksums.txt");

async Task DownloadAsync(string url, string output)
{
    Console.WriteLine($"Downloading {url}");
    using HttpClient client = new();

    using HttpResponseMessage response = await client.GetAsync(url);
    response.EnsureSuccessStatusCode();

    await using FileStream fileStream = File.Create(output);
    await response.Content.CopyToAsync(fileStream);
}

void VerifyChecksum(string file, string checksumFile)
{
    string checksum = File.ReadAllText(checksumFile).Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
    byte[] checksumBytes = Convert.FromHexString(checksum);

    using SHA256 sha256 = SHA256.Create();
    using FileStream stream = File.OpenRead(file);

    byte[] actual = sha256.ComputeHash(stream);

    if (!CryptographicOperations.FixedTimeEquals(actual, checksumBytes))
    {
        Console.Error.WriteLine($"Checksum verification failed for {file}");
        Environment.Exit(1);
    }

    Console.WriteLine($"Checksum verification succeeded for {file}");
}

async Task Extract(string file, string output)
{
    Console.WriteLine($"Extracting {file} to {output}");

    await using FileStream fileStream = File.OpenRead(file);
    await using GZipStream gzipStream = new(fileStream, CompressionMode.Decompress);

    await TarFile.ExtractToDirectoryAsync(gzipStream, output, overwriteFiles: true);
}

void Cleanup(params Span<string> files)
{
    foreach (string file in files)
    {
        if (File.Exists(file))
        {
            Console.WriteLine($"Deleting {file}");
            File.Delete(file);
        }
    }
}
