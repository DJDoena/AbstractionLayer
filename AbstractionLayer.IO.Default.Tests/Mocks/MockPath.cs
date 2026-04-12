using DoenaSoft.AbstractionLayer.IOServices;
using System;
using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IO.Default.Tests.Mocks;

internal class MockPath : IPath
{
    private readonly MockIOServices _ioServices;

    public MockPath(MockIOServices ioServices)
    {
        _ioServices = ioServices;
    }

    public IIOServices IOServices => _ioServices;

    public string GetFullPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path cannot be null or empty", nameof(path));
        }

        // Normalize path separators
        path = path.Replace('/', '\\');

        // If already absolute (contains : for drive letter or starts with \\)
        if (path.Contains(':') || path.StartsWith("\\\\"))
        {
            return SIO.Path.GetFullPath(path);
        }

        // Make it absolute by adding a dummy drive
        return SIO.Path.GetFullPath(SIO.Path.Combine("C:\\", path));
    }

    public string Combine(params string[] parts) => SIO.Path.Combine(parts);
    public string GetTempPath() => SIO.Path.GetTempPath();
    public IEnumerable<char> GetInvalidFileNameChars() => SIO.Path.GetInvalidFileNameChars();
    public IEnumerable<char> GetInvalidPathChars() => SIO.Path.GetInvalidPathChars();
    public string GetTempFileName() => SIO.Path.GetTempFileName();
    public string GetFileNameWithoutExtension(string path) => SIO.Path.GetFileNameWithoutExtension(path);
    public string GetExtension(string path) => SIO.Path.GetExtension(path);
    public string GetFolderName(string path) => SIO.Path.GetDirectoryName(path);
    public string GetFileName(string path) => SIO.Path.GetFileName(path);
    public string GetPathRoot(string path) => SIO.Path.GetPathRoot(path);
    public bool HasExtension(string path) => SIO.Path.HasExtension(path);
    public bool IsPathRooted(string path) => SIO.Path.IsPathRooted(path);
    public string ChangeExtension(string path, string extension) => SIO.Path.ChangeExtension(path, extension);
    public string GetRandomFileName() => SIO.Path.GetRandomFileName();
}