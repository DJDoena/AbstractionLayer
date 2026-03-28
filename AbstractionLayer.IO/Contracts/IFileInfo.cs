using System;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate FileInfo concerns from an concrete implementation.
/// </summary>
public interface IFileInfo : IIOServiceItem, IEquatable<IFileInfo>
{
    /// <summary>
    /// Returns the file name including the extension but without the path.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Returns the file extension including the leading '.'.
    /// </summary>
    string Extension { get; }

    /// <summary>
    /// Returns the full file name including path and extension.
    /// </summary>
    string FullName { get; }

    /// <summary>
    /// Returns the folder that contains the file.
    /// </summary>
    IFolderInfo Folder { get; }

    /// <summary>
    /// Returns the path without the file name.
    /// </summary>
    string FolderName { get; }

    /// <summary>
    /// Returns the file name without path and extension.
    /// </summary>
    string NameWithoutExtension { get; }

    /// <summary>
    /// Returns whether the file exists.
    /// </summary>
    bool Exists { get; }

    /// <summary>
    /// Returns the size in bytes.
    /// </summary>
    long Length { get; }

    /// <summary>
    /// Gets or sets the attributes for the current file.
    /// </summary>
    SIO.FileAttributes Attributes { get; set; }

    /// <summary>
    /// Gets a value indicating whether the file is read-only.
    /// </summary>
    bool IsReadOnly { get; set; }

    /// <summary>
    /// Gets or sets the time when the current file was last written to.
    /// </summary>
    DateTime LastWriteTime { get; set; }

    /// <summary>
    /// Gets or sets the time (UTC) when the current file was last written to.
    /// </summary>
    DateTime LastWriteTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the current file.
    /// </summary>
    DateTime CreationTime { get; set; }

    /// <summary>
    /// Gets or sets the creation time (UTC) of the current file.
    /// </summary>
    DateTime CreationTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the time the current file was last accessed.
    /// </summary>
    DateTime LastAccessTime { get; set; }

    /// <summary>
    /// Gets or sets the time (UTC) the current file was last accessed.
    /// </summary>
    DateTime LastAccessTimeUtc { get; set; }

    /// <summary>
    /// Moves a specified file to a new location, providing the option to specify a new file name.
    /// </summary>
    /// <param name="targetFileName">the path to move the file to, which can specify a different file name</param>
    void MoveTo(string targetFileName);

    /// <summary>
    /// Copies an existing file to a new file, disallowing the overwriting of an existing file.
    /// </summary>
    /// <param name="destFileName">The name of the new file to copy to.</param>
    IFileInfo CopyTo(string destFileName);

    /// <summary>
    /// Copies an existing file to a new file, allowing the overwriting of an existing file.
    /// </summary>
    /// <param name="destFileName">The name of the new file to copy to.</param>
    /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
    IFileInfo CopyTo(string destFileName, bool overwrite);

    /// <summary>
    /// Deletes the file.
    /// </summary>
    void Delete();

    /// <summary>
    /// Creates a StreamWriter that writes a new text file.
    /// </summary>
    SIO.StreamWriter CreateText();

    /// <summary>
    /// Creates a write-only FileStream.
    /// </summary>
    SIO.Stream Create();

    /// <summary>
    /// Opens a file in the specified mode.
    /// </summary>
    SIO.Stream Open(SIO.FileMode mode);

    /// <summary>
    /// Opens a file in the specified mode with read, write, or read/write access.
    /// </summary>
    SIO.Stream Open(SIO.FileMode mode, SIO.FileAccess access);

    /// <summary>
    /// Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.
    /// </summary>
    SIO.Stream Open(SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share);

    /// <summary>
    /// Creates a read-only FileStream.
    /// </summary>
    SIO.Stream OpenRead();

    /// <summary>
    /// Creates a StreamReader with UTF8 encoding that reads from an existing text file.
    /// </summary>
    SIO.StreamReader OpenText();

    /// <summary>
    /// Creates a write-only FileStream.
    /// </summary>
    SIO.Stream OpenWrite();
}