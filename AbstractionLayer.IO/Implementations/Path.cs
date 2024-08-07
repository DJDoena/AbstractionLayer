﻿using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IPath"/> for <see cref="SIO.Path"/>.
/// </summary>
internal sealed class Path : IPath
{
    /// <summary>
    /// The master interface.
    /// </summary>
    public IIOServices IOServices { get; }

    public Path(IIOServices ioServices)
    {
        this.IOServices = ioServices;
    }

    /// <summary>
    /// Combines multiple path segments to each other, ensuring that they are properly concatenated.
    /// </summary>
    /// <param name="parts">the path parts</param>
    /// <returns>The combined path</returns>
    public string Combine(params string[] parts)
        => SIO.Path.Combine(parts);

    /// <summary>
    /// Returns the path of the temp folder.
    /// </summary>
    /// <returns>The path of the temp folder</returns>
    public string GetTempPath()
        => SIO.Path.GetTempPath();

    /// <summary>
    /// Returns all characters not allowed in a file name.
    /// </summary>
    /// <returns>All characters not allowed in a file name</returns>
    public IEnumerable<char> GetInvalidFileNameChars()
        => SIO.Path.GetInvalidFileNameChars();

    /// <summary>
    /// Returns all characters not allowed in a path.
    /// </summary>
    /// <returns>All characters not allowed in a path</returns>
    public IEnumerable<char> GetInvalidPathChars()
        => SIO.Path.GetInvalidPathChars();

    /// <summary>
    /// Returns the absolute path for the specified path string.
    /// </summary>
    /// <param name="path">the file or directory for which to obtain absolute path information</param>
    /// <returns>the fully qualified location of path, such as "C:\MyFile.txt"</returns>
    public string GetFullPath(string path)
        => SIO.Path.GetFullPath(path);

    /// <summary>
    /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
    /// </summary>
    /// <returns>the full path of the temporary file</returns>
    public string GetTempFileName()
        => SIO.Path.GetTempFileName();

    /// <summary>
    /// Returns the file name of the specified path string without the extension.
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the string returned by System.IO.Path.GetFileName(System.String), minus the last period (.) and all characters following it</returns>
    public string GetFileNameWithoutExtension(string path)
        => SIO.Path.GetFileNameWithoutExtension(path);

    /// <summary>
    /// Returns the extension of the specified path string.
    /// </summary>
    /// <param name="path">the path string from which to get the extension</param>
    /// <returns>
    /// The extension of the specified path (including the period "."), or null, or <see cref="string.Empty"/>.
    /// If path is null, it  returns null.
    /// If path does not have extension information, it returns <see cref="string.Empty"/>.
    /// </returns>
    public string GetExtension(string path)
        => SIO.Path.GetExtension(path);
}