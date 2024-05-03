using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IPath"/> for <see cref="SIO.Path"/>.
/// </summary>
internal sealed class Path : IPath
{
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
}