using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IPath"/> for <see cref="SIO.Path"/>.
/// </summary>
internal sealed class Path : IOServiceItem, IPath
{
    public Path(IIOServices ioServices)
        : base(ioServices)
    {
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

    /// <summary>
    /// Returns the folder information for the specified path string.
    /// </summary>
    /// <param name="path">the path of a file or folder</param>
    /// <returns>folder information for path, or null if path denotes a root folder or is null</returns>
    public string GetFolderName(string path)
        => SIO.Path.GetDirectoryName(path);

    /// <summary>
    /// Returns the file name and extension of the specified path string.
    /// </summary>
    /// <param name="path">the path string from which to obtain the file name and extension</param>
    /// <returns>the characters after the last directory separator character in path</returns>
    public string GetFileName(string path)
        => SIO.Path.GetFileName(path);

    /// <summary>
    /// Returns the root directory information of the specified path.
    /// </summary>
    /// <param name="path">the path from which to obtain root directory information</param>
    /// <returns>the root directory of path, such as "C:\", or null if path is null, or an empty string if path does not contain root directory information</returns>
    public string GetPathRoot(string path)
        => SIO.Path.GetPathRoot(path);

    /// <summary>
    /// Determines whether a path includes a file name extension.
    /// </summary>
    /// <param name="path">the path to search for an extension</param>
    /// <returns>true if the characters that follow the last directory separator or volume separator in the path include a period (.) followed by one or more characters; otherwise, false</returns>
    public bool HasExtension(string path)
        => SIO.Path.HasExtension(path);

    /// <summary>
    /// Gets whether the specified path string contains absolute or relative path information.
    /// </summary>
    /// <param name="path">the path to test</param>
    /// <returns>true if path contains an absolute path; otherwise, false</returns>
    public bool IsPathRooted(string path)
        => SIO.Path.IsPathRooted(path);

    /// <summary>
    /// Changes the extension of a path string.
    /// </summary>
    /// <param name="path">the path information to modify. The path cannot contain any of the characters defined in GetInvalidPathChars()</param>
    /// <param name="extension">the new extension (with or without a leading period). Specify null to remove an existing extension from path</param>
    /// <returns>the modified path information</returns>
    public string ChangeExtension(string path, string extension)
        => SIO.Path.ChangeExtension(path, extension);

    /// <summary>
    /// Returns a random folder name or file name.
    /// </summary>
    /// <returns>a random folder name or file name</returns>
    public string GetRandomFileName()
        => SIO.Path.GetRandomFileName();
}