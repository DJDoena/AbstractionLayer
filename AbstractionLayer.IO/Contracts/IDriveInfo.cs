using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate DriveInfo concerns from an concrete implementation.
/// </summary>
public interface IDriveInfo : IIOServiceItem
{
    /// <summary>
    /// Returns whether tghe drive is ready for read/write operations.
    /// </summary>
    bool IsReady { get; }

    /// <summary>
    /// Returns the drive label.
    /// </summary>
    string DriveLabel { get; }

    /// <summary>
    /// Returns the original volume label.
    /// </summary>
    string VolumeLabel { get; }

    /// <summary>
    /// Returns the root folder name.
    /// </summary>
    string RootFolderName { get; }

    /// <summary>
    /// Returns the root folder.
    /// </summary>
    IFolderInfo RootFolder { get; }

    /// <summary>
    /// Returns the drive letter.
    /// </summary>
    string DriveLetter { get; }

    /// <summary>
    /// Returns the free space in bytes.
    /// </summary>
    long AvailableFreeSpace { get; }

    /// <summary>
    /// Returns the total space in bytes.
    /// </summary>
    long TotalSpace { get; }

    /// <summary>
    /// Gets the name of the drive, such as C:\.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the drive type, such as CD-ROM, removable, network, or fixed.
    /// </summary>
    SIO.DriveType DriveType { get; }

    /// <summary>
    /// Gets the name of the file system, such as NTFS or FAT32.
    /// </summary>
    string DriveFormat { get; }

    /// <summary>
    /// Gets the total amount of free space available on the drive, in bytes.
    /// </summary>
    long TotalFreeSpace { get; }
}