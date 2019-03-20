namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface to seperate FolderInfo concerns from an concrete implementation.
    /// </summary>
    public interface IFolderInfo
    {
        /// <summary>
        /// Returns the folder name without the path.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Returns the root folder.
        /// </summary>
        IFolderInfo Root { get; }

        /// <summary>
        /// Returns whether the folder exists.
        /// </summary>
        Boolean Exists { get; }

        /// <summary>
        /// Returns the full folder name including path.
        /// </summary>
        String FullName { get; }

        /// <summary>
        /// Gets or sets the time when the current folder was last written to.
        /// </summary>
        DateTime LastWriteTime { get; set; }

        /// <summary>
        /// Gets or sets the creation time of the current folder.
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the time the current folder was last accessed.
        /// </summary>
        DateTime LastAccessTime { get; set; }

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
        IEnumerable<IFileInfo> GetFiles(String searchPattern
            , System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly);
    }
}