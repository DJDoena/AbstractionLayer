namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface to seperate folder concerns from an concrete implementation.
    /// </summary>
    public interface IFolder
    {
        /// <summary>
        /// Returns the WorkingFolder.
        /// </summary>
        String WorkingFolder { get; }

        /// <summary>
        /// Returns whether a folder exists.
        /// </summary>
        /// <param name="folder">The folder name</param>
        Boolean Exists(String folder);

        /// <summary>
        /// Deletes a folder,
        /// </summary>
        /// <param name="folder">The folder name</param>
        void Delete(String folder);

        /// <summary>
        /// Creates a folder.
        /// </summary>
        /// <param name="folder">The folder name</param>
        void CreateFolder(String folder);

        /// <summary>
        /// Returns all folders in a folder according to the search pattern and option.
        /// </summary>
        /// <param name="folder">The folder name</param>
        /// <param name="searchPattern">The search pattern</param>
        /// <param name="searchOption">The search option</param>
        /// <returns>All folders in the folder according to the search pattern and option</returns>
        IEnumerable<String> GetFolders(String folder
            , String searchPattern = "*.*"
            , System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly);

        /// <summary>
        /// Returns all files in a folder according to the search pattern and option.
        /// </summary>
        /// <param name="folder">The folder name</param>
        /// <param name="searchPattern">The search pattern</param>
        /// <param name="searchOption">The search option</param>
        /// <returns>All files in the folder according to the search pattern and option</returns>
        IEnumerable<String> GetFiles(String folder
            , String searchPattern = "*.*"
            , System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly);

    }
}