namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    /// <summary>
    /// Interface to seperate DriveInfo concerns from an concrete implementation.
    /// </summary>
    public interface IDriveInfo
    {
        /// <summary>
        /// Returns whether tghe drive is ready for read/write operations.
        /// </summary>
        Boolean IsReady { get; }

        /// <summary>
        /// Returns the label.
        /// </summary>
        String Label { get; }

        /// <summary>
        /// Returns the root folder.
        /// </summary>
        String RootFolder { get; }

        /// <summary>
        /// Returns the drive letter.
        /// </summary>
        String DriveLetter { get; }

        /// <summary>
        /// Returns the free space in bytes.
        /// </summary>
        UInt64 AvailableFreeSpace { get; }
    }
}