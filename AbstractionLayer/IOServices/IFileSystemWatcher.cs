namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    /// <summary>
    /// Interface to seperate FileSystemWatcher concerns from an concrete implementation.
    /// </summary>
    public interface IFileSystemWatcher
    {
        /// <summary>
        /// Starts/Ends the observation of the file system.
        /// </summary>
        Boolean EnableRaisingEvents { get; set; }

        /// <summary>
        /// Determines whether sub folders are included in the observation.
        /// </summary>
        Boolean IncludeSubFolders { get; set; }

        /// <summary>
        /// Is raised when a file is created.
        /// </summary>
        event System.IO.FileSystemEventHandler Created;

        /// <summary>
        /// Is raised when a file is deleted.
        /// </summary>
        event System.IO.FileSystemEventHandler Deleted;

        /// <summary>
        /// Is raised when a file is renamed.
        /// </summary>
        event System.IO.RenamedEventHandler Renamed;
    }
}