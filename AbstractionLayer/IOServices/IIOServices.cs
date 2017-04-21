using System;
using System.Collections.Generic;
using System.Text;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IIOServices
    {
        IPath Path { get; }

        IFile File { get; }

        IDirectory Directory { get; }

        IDirectoryInfo GetDirectoryInfo(String path);

        IFileInfo GetFileInfo(String fileName);

        System.IO.Stream GetFileStream(String fileName
            , System.IO.FileMode mode
            , System.IO.FileAccess access
            , System.IO.FileShare share);

        System.IO.StreamWriter GetStreamWriter(System.IO.Stream stream,
            Encoding encoding = null);

        IEnumerable<IDriveInfo> GetDriveInfos(Nullable<System.IO.DriveType> driveType = null);

        IDriveInfo GetDriveInfo(String driveLetter);

        IFileSystemWatcher GetFileSystemWatcher(String path
            , String filter = "*.*");
    }
}