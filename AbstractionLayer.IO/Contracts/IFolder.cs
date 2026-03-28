using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate folder concerns from an concrete implementation.
/// </summary>
public interface IFolder : IIOServiceItem
{
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
    IEnumerable<IFileInfo> GetFiles(string folder
        , string searchPattern = "*.*"
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly);

    /// <summary>
    /// Moves a folder and its contents to a new location.
    /// </summary>
    /// <param name="sourceFolderName">The path of the folder to move.</param>
    /// <param name="destFolderName">The path to the new location for sourceFolderName.</param>
    void Move(string sourceFolderName, string destFolderName);

    /// <summary>
    /// Returns the current folder of the application.
    /// </summary>
    string GetCurrentFolder();

    /// <summary>
    /// Sets the application's current working folder to the specified folder.
    /// </summary>
    /// <param name="path">The path to which the current working folder is set.</param>
    void SetCurrentFolder(string path);

    /// <summary>
    /// Returns the names of subfolders (including their paths) in the specified folder.
    /// </summary>
    IEnumerable<string> GetFolders(string path);

    /// <summary>
    /// Returns the names of files (including their paths) in the specified folder.
    /// </summary>
    IEnumerable<string> GetFiles(string path);

    /// <summary>
    /// Retrieves the parent folder of the specified path.
    /// </summary>
    IFolderInfo GetParent(string path);
}