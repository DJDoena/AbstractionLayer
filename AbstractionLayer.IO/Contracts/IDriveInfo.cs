namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate DriveInfo concerns from an concrete implementation.
/// </summary>
public interface IDriveInfo
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
    ulong AvailableFreeSpace { get; }
}