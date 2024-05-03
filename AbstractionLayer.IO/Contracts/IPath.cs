using System.Collections.Generic;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate path concerns from an concrete implementation.
/// </summary>
public interface IPath
{
    /// <summary>
    /// Combines multiple path segments to each other, ensuring that they are properly concatenated.
    /// </summary>
    /// <param name="parts">the path parts</param>
    /// <returns>The combined path</returns>
    string Combine(params string[] parts);

    /// <summary>
    /// Returns the path of the temp folder.
    /// </summary>
    /// <returns>The path of the temp folder</returns>
    string GetTempPath();

    /// <summary>
    /// Returns all characters not allowed in a file name.
    /// </summary>
    /// <returns>All characters not allowed in a file name</returns>
    IEnumerable<char> GetInvalidFileNameChars();

    /// <summary>
    /// Returns all characters not allowed in a path.
    /// </summary>
    /// <returns>All characters not allowed in a path</returns>
    IEnumerable<char> GetInvalidPathChars();

    /// <summary>
    /// Returns the absolute path for the specified path string.
    /// </summary>
    /// <param name="path">the file or directory for which to obtain absolute path information</param>
    /// <returns>the fully qualified location of path, such as "C:\MyFile.txt"</returns>
    string GetFullPath(string path);

    /// <summary>
    /// Creates a uniquely named, zero-byte temporary file on disk and returns the full path of that file.
    /// </summary>
    /// <returns>the full path of the temporary file</returns>
    string GetTempFileName();

    /// <summary>
    /// Returns the file name of the specified path string without the extension.
    /// </summary>
    /// <param name="path">the path of the file</param>
    /// <returns>the string returned by System.IO.Path.GetFileName(System.String), minus the last period (.) and all characters following it</returns>
    string GetFileNameWithoutExtension(string path);

    /// <summary>
    /// Returns the extension of the specified path string.
    /// </summary>
    /// <param name="path">the path string from which to get the extension</param>
    /// <returns>
    /// The extension of the specified path (including the period "."), or null, or <see cref="string.Empty"/>.
    /// If path is null, it  returns null.
    /// If path does not have extension information, it returns <see cref="string.Empty"/>.
    /// </returns>
    string GetExtension(string path);
}