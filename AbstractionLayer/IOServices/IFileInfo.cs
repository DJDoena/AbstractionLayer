namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    /// <summary>
    /// Interface to seperate FileInfo concerns from an concrete implementation.
    /// </summary>
    public interface IFileInfo
    {
        /// <summary>
        /// Returns the file name including the extension but without the path.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Returns the file extension including the leading '.'.
        /// </summary>
        String Extension { get; }

        /// <summary>
        /// Returns the full file name including path and extension.
        /// </summary>
        String FullName { get; }

        /// <summary>
        /// Returns the folder that contains the file.
        /// </summary>
        IFolderInfo Folder { get; }

        /// <summary>
        /// Returns the path without the file name.
        /// </summary>
        String FolderName { get; }

        /// <summary>
        /// Returns the file name without path and extension.
        /// </summary>
        String NameWithoutExtension { get; }

        /// <summary>
        /// Returns whether the file exists.
        /// </summary>
        Boolean Exists { get; }

        /// <summary>
        /// Returns the size in bytes.
        /// </summary>
        UInt64 Length { get; }

        /// <summary>
        /// Gets or sets the time when the current file was last written to.
        /// </summary>
        DateTime LastWriteTime { get; set; }

        /// <summary>
        /// Gets or sets the creation time of the current file.
        /// </summary>
        DateTime CreationTime { get; set; }

        /// <summary>
        /// Gets or sets the time the current file was last accessed.
        /// </summary>
        DateTime LastAccessTime { get; set; }
    }
}