using System;
using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate FolderInfo concerns from an concrete implementation.
/// </summary>
public interface IFolderInfo : IEquatable<IFolderInfo>
{
    /// <summary>
    /// The master interface.
    /// </summary>
    IIOServices IOServices { get; }

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
    IEnumerable<IFileInfo> GetFileInfos(string searchPattern
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

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
    IEnumerable<IFolderInfo> GetFolderInfos(string searchPattern
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Returns all folders in the folder according to the search pattern and option.
    /// </summary>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All folders in the folder according to the search pattern and option</returns>
    IEnumerable<IFolderInfo> GetDirectories(string searchPattern
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);
}