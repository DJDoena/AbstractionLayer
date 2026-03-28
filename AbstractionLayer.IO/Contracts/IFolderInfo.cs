using System;
using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate FolderInfo concerns from an concrete implementation.
/// </summary>
public interface IFolderInfo : IIOServiceItem, IEquatable<IFolderInfo>
{
    /// <summary>
    /// Returns the folder name without the path.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Returns the parent folder.
    /// </summary>
    IFolderInfo Parent { get; }

    /// <summary>
    /// Returns the root folder.
    /// </summary>
    IFolderInfo Root { get; }

    /// <summary>
    /// Returns the drive.
    /// </summary>
    IDriveInfo Drive { get; }

    /// <summary>
    /// Returns whether the folder exists.
    /// </summary>
    bool Exists { get; }

    /// <summary>
    /// Returns the full folder name including path.
    /// </summary>
    string FullName { get; }

    /// <summary>
    /// Gets or sets the time when the current folder was last written to.
    /// </summary>
    DateTime LastWriteTime { get; set; }

    /// <summary>
    /// Gets or sets the time (UTC) when the current folder was last written to.
    /// </summary>
    DateTime LastWriteTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the current folder.
    /// </summary>
    DateTime CreationTime { get; set; }

    /// <summary>
    /// Gets or sets the creation time (UTC) of the current folder.
    /// </summary>
    DateTime CreationTimeUtc { get; set; }

    /// <summary>
    /// Gets or sets the time the current folder was last accessed.
    /// </summary>
    DateTime LastAccessTime { get; set; }

    /// <summary>
    /// Gets or sets the time (UTC) the current folder was last accessed.
    /// </summary>
    DateTime LastAccessTimeUtc { get; set; }

    /// <summary>
    /// Creates the folder.
    /// </summary>
    void Create();

    /// <summary>
    /// Returns all files in the folder according to the search pattern and option.
    /// </summary>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All files in the folder according to the search pattern and option</returns>
    IEnumerable<IFileInfo> GetFiles(string searchPattern
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Returns all folders in the folder according to the search pattern and option.
    /// </summary>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All folders in the folder according to the search pattern and option</returns>
    IEnumerable<IFolderInfo> GetFolders(string searchPattern
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Gets or sets the attributes for the current folder.
    /// </summary>
    SIO.FileAttributes Attributes { get; set; }

    /// <summary>
    /// Deletes the folder if it is empty.
    /// </summary>
    void Delete();

    /// <summary>
    /// Deletes the folder, and, if specified, any subdirectories and files in the folder.
    /// </summary>
    /// <param name="recursive">true to delete this folder, its subfolders, and all files; otherwise, false.</param>
    void Delete(bool recursive);

    /// <summary>
    /// Moves the folder and its contents to a new path.
    /// </summary>
    /// <param name="destFolderName">The path to the new location.</param>
    void MoveTo(string destFolderName);

    /// <summary>
    /// Returns the subfolders of the current folder.
    /// </summary>
    IEnumerable<IFolderInfo> GetFolders();

    /// <summary>
    /// Returns the subfolders of the current folder matching the given search pattern.
    /// </summary>
    IEnumerable<IFolderInfo> GetFolders(string searchPattern);

    /// <summary>
    /// Returns an enumerable collection of file information in the current directory.
    /// </summary>
    IEnumerable<IFileInfo> GetFiles();

    /// <summary>
    /// Returns an enumerable collection of file information that matches a search pattern.
    /// </summary>
    IEnumerable<IFileInfo> GetFiles(string searchPattern);
}