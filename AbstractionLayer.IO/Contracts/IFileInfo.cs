using System;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate FileInfo concerns from an concrete implementation.
/// </summary>
public interface IFileInfo : IEquatable<IFileInfo>
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
    /// Returns the path without the file name.
    /// </summary>
    [Obsolete($"use {nameof(FolderName)} instead")]
    public string GetDirectoryName { get; }

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
    ulong Length { get; }

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
}