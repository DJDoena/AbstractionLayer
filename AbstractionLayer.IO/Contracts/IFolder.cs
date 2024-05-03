using System;
using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate folder concerns from an concrete implementation.
/// </summary>
public interface IFolder
{
    /// <summary>
    /// The master interface.
    /// </summary>
    IIOServices IOServices { get; }

    /// <summary>
    /// Returns the WorkingFolder.
    /// </summary>
    string WorkingFolderName { get; }

    /// <summary>
    /// Returns whether a folder exists.
    /// </summary>
    /// <param name="folder">The folder name</param>
    bool Exists(string folder);

    /// <summary>
    /// Returns the full path for a folder.
    /// </summary>
    /// <param name="folder">The folder name</param>
    string GetFullPath(string folder);

    /// <summary>
    /// Deletes a folder,
    /// </summary>
    /// <param name="folder">The folder name</param>
    void Delete(string folder);

    /// <summary>
    /// Creates a folder.
    /// </summary>
    /// <param name="folder">The folder name</param>
    IFolderInfo CreateFolder(string folder);

    /// <summary>
    /// Returns all folders in a folder according to the search pattern and option.
    /// </summary>
    /// <param name="folder">The folder name</param>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All folders in the folder according to the search pattern and option</returns>
    IEnumerable<string> GetFolderNames(string folder
        , string searchPattern = "*.*"
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Returns all folders in a folder according to the search pattern and option.
    /// </summary>
    /// <param name="folder">The folder name</param>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All folders in the folder according to the search pattern and option</returns>
    IEnumerable<IFolderInfo> GetFolders(string folder
        , string searchPattern = "*.*"
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Returns all folders in a folder according to the search pattern and option.
    /// </summary>
    /// <param name="folder">The folder name</param>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All folders in the folder according to the search pattern and option</returns>
    [Obsolete($"use {nameof(GetFolders)} instead")]
    IEnumerable<IFolderInfo> GetFolderInfos(string folder
        , string searchPattern = "*.*"
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Returns all folders in a folder according to the search pattern and option.
    /// </summary>
    /// <param name="folder">The folder name</param>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All folders in the folder according to the search pattern and option</returns>
    [Obsolete($"use {nameof(GetFolders)} instead")]
    IEnumerable<IFolderInfo> GetDirectories(string folder
        , string searchPattern = "*.*"
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Returns all files in a folder according to the search pattern and option.
    /// </summary>
    /// <param name="folder">The folder name</param>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All files in the folder according to the search pattern and option</returns>
    IEnumerable<string> GetFileNames(string folder
        , string searchPattern = "*.*"
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Returns all files in a folder according to the search pattern and option.
    /// </summary>
    /// <param name="folder">The folder name</param>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All files in the folder according to the search pattern and option</returns>

    [Obsolete($"use {nameof(GetFiles)} instead")]
    IEnumerable<IFileInfo> GetFileInfos(string folder
        , string searchPattern = "*.*"
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Returns all files in a folder according to the search pattern and option.
    /// </summary>
    /// <param name="folder">The folder name</param>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All files in the folder according to the search pattern and option</returns>
    IEnumerable<IFileInfo> GetFiles(string folder
        , string searchPattern = "*.*"
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);
}