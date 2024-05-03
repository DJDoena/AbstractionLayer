using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IOServices"/>.
/// </summary>
public sealed class IOServices : IIOServices
{
    private readonly IPath _path;

    private readonly IFolder _folder;

    private readonly IFile _file;

    /// <summary />
    public IOServices(ILogger logger)
    {
        _path = new Path(this);
        _folder = new Folder(this, logger);
        _file = new File(this, logger);
    }

    /// <summary />
    public IOServices()
        : this(null)
    {
    }

    #region IIOServices

    /// <summary>
    /// Returns an object to deal with paths.
    /// </summary>
    public IPath Path
        => _path;

    /// <summary>
    /// Returns an object to deal with folders.
    /// </summary>
    public IFolder Folder
        => _folder;

    /// <summary>
    /// Returns an object to deal with files.
    /// </summary>
    public IFile File
        => _file;

    /// <summary>
    /// Returns a <see cref="IFolderInfo"/> for a given folder.
    /// </summary>
    /// <param name="folder">The folder</param>
    /// <returns>A <see cref="IFolderInfo"/> for a given folder</returns>
    public IFolderInfo GetFolderInfo(string folder)
        => new FolderInfo(this, folder);

    /// <summary>
    /// Returns a <see cref="IFileInfo"/> for a given file name.
    /// </summary>
    /// <param name="fileName">The file name </param>
    /// <returns>A <see cref="IFileInfo"/> for a given file name</returns>
    public IFileInfo GetFileInfo(string fileName)
        => new FileInfo(this, fileName);

    /// <summary>
    /// Opens a file.
    /// </summary>
    /// <param name="fileName">The file name</param>
    /// <param name="mode">The <see cref="SIO.FileMode"/></param>
    /// <param name="access">The <see cref="SIO.FileAccess"/></param>
    /// <param name="share">The <see cref="SIO.FileShare"/></param>
    /// <returns>The opened file</returns>
    public SIO.Stream GetFileStream(string fileName, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share)
        => new SIO.FileStream(fileName, mode, access, share);

    /// <summary>
    /// Returns all the <see cref="IDriveInfo"/> currently available.
    /// </summary>
    /// <param name="driveType">Filter on the <see cref="SIO.DriveType"/></param>
    /// <returns>All the <see cref="IDriveInfo"/> currently available</returns>
    public IEnumerable<IDriveInfo> GetDriveInfos(SIO.DriveType? driveType)
    {
        var drives = SIO.DriveInfo.GetDrives();

        foreach (var drive in drives)
        {
            if (driveType == null)
            {
                yield return new DriveInfo(this, drive);
            }
            else if (driveType.Value == drive.DriveType)
            {
                yield return new DriveInfo(this, drive);
            }
        }
    }

    /// <summary>
    /// Returns the <see cref="IDriveInfo"/> for the given drive letter.
    /// </summary>
    /// <param name="driveLetter">the drive letter</param>
    /// <returns>The <see cref="IDriveInfo"/> for the given drive letter</returns>
    public IDriveInfo GetDriveInfo(string driveLetter)
        => new DriveInfo(this, new SIO.DriveInfo(driveLetter));

    /// <summary>
    /// Returns a <see cref="IFileSystemWatcher"/> based on the folder and the search pattern.
    /// </summary>
    /// <param name="folder">The folder</param>
    /// <param name="searchPattern">The search pattern</param>
    /// <returns>A <see cref="IFileSystemWatcher"/> based on the folder and the search pattern</returns>
    public IFileSystemWatcher GetFileSystemWatcher(string folder, string searchPattern)
        => new FileSystemWatcher(this, folder, searchPattern);

    #endregion
}