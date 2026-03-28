using System.Collections.Generic;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate path concerns from an concrete implementation.
/// </summary>
public interface IPath : IIOServiceItem
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

    /// <summary>
    /// Returns the folder information for the specified path string.
    /// </summary>
    /// <param name="path">the path of a file or folder</param>
    /// <returns>folder information for path, or null if path denotes a root folder or is null</returns>
    string GetFolderName(string path);

    /// <summary>
    /// Returns the file name and extension of the specified path string.
    /// </summary>
    /// <param name="path">the path string from which to obtain the file name and extension</param>
    /// <returns>the characters after the last folder separator character in path</returns>
    string GetFileName(string path);

    /// <summary>
    /// Returns the root folder information of the specified path.
    /// </summary>
    /// <param name="path">the path from which to obtain root folder information</param>
    /// <returns>the root folder of path, such as "C:\", or null if path is null, or an empty string if path does not contain root folder information</returns>
    string GetPathRoot(string path);

    /// <summary>
    /// Determines whether a path includes a file name extension.
    /// </summary>
    /// <param name="path">the path to search for an extension</param>
    /// <returns>true if the characters that follow the last folder separator or volume separator in the path include a period (.) followed by one or more characters; otherwise, false</returns>
    bool HasExtension(string path);

    /// <summary>
    /// Gets whether the specified path string contains absolute or relative path information.
    /// </summary>
    /// <param name="path">the path to test</param>
    /// <returns>true if path contains an absolute path; otherwise, false</returns>
    bool IsPathRooted(string path);

    /// <summary>
    /// Changes the extension of a path string.
    /// </summary>
    /// <param name="path">the path information to modify. The path cannot contain any of the characters defined in GetInvalidPathChars()</param>
    /// <param name="extension">the new extension (with or without a leading period). Specify null to remove an existing extension from path</param>
    /// <returns>the modified path information</returns>
    string ChangeExtension(string path, string extension);

    /// <summary>
    /// Returns a random folder name or file name.
    /// </summary>
    /// <returns>a random folder name or file name</returns>
    string GetRandomFileName();
}