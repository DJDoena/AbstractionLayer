namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface to seperate I/O concerns from an concrete implementation.
    /// </summary>
    public interface IIOServices
    {
        /// <summary>
        /// Returns an object to deal with paths.
        /// </summary>
        IPath Path { get; }

        /// <summary>
        /// Returns an object to deal with files.
        /// </summary>
        IFile File { get; }

        /// <summary>
        /// Returns an object to deal with folders.
        /// </summary>
        IFolder Folder { get; }

        /// <summary>
        /// Returns a <see cref="IFolderInfo"/> for a given folder.
        /// </summary>
        /// <param name="folder">The folder</param>
        /// <returns>A <see cref="IFolderInfo"/> for a given folder</returns>
        IFolderInfo GetFolderInfo(string folder);

        /// <summary>
        /// Returns a <see cref="IFileInfo"/> for a given file name.
        /// </summary>
        /// <param name="fileName">The file name </param>
        /// <returns>A <see cref="IFileInfo"/> for a given file name</returns>
        IFileInfo GetFileInfo(string fileName);

        /// <summary>
        /// Opens a file.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="mode">The <see cref="System.IO.FileMode"/></param>
        /// <param name="access">The <see cref="System.IO.FileAccess"/></param>
        /// <param name="share">The <see cref="System.IO.FileShare"/></param>
        /// <returns>The opened file</returns>
        System.IO.Stream GetFileStream(string fileName, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share);

        /// <summary>
        /// Returns all the <see cref="IDriveInfo"/> currently available.
        /// </summary>
        /// <param name="driveType">Filter on the <see cref="System.IO.DriveType"/></param>
        /// <returns>All the <see cref="IDriveInfo"/> currently available</returns>
        IEnumerable<IDriveInfo> GetDriveInfos(System.IO.DriveType? driveType = null);

        /// <summary>
        /// Returns the <see cref="IDriveInfo"/> for the given drive letter.
        /// </summary>
        /// <param name="driveLetter">the drive letter</param>
        /// <returns>The <see cref="IDriveInfo"/> for the given drive letter</returns>
        IDriveInfo GetDriveInfo(string driveLetter);

        /// <summary>
        /// Returns a <see cref="IFileSystemWatcher"/> based on the folder and the search pattern.
        /// </summary>
        /// <param name="folder">The folder</param>
        /// <param name="searchPattern">The search pattern</param>
        /// <returns>A <see cref="IFileSystemWatcher"/> based on the folder and the search pattern</returns>
        IFileSystemWatcher GetFileSystemWatcher(string folder, string searchPattern = "*.*");
    }
}