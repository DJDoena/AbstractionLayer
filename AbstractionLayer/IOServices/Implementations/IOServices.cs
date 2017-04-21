using System;
using System.Collections.Generic;
using System.Text;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    public sealed class IOServices : IIOServices
    {
        private IPath m_Path;

        private IDirectory m_Directory;

        private IFile m_File;

        private readonly ILogger Logger;

        public IOServices(ILogger logger)
        {
            Logger = logger;
        }

        public IOServices()
            : this(null)
        { }

        #region IIOServices

        public IPath Path
        {
            get
            {
                if (m_Path == null)
                {
                    m_Path = new Path();
                }

                return (m_Path);
            }
        }

        public IDirectory Directory
        {
            get
            {
                if (m_Directory == null)
                {
                    m_Directory = new Directory(Logger);
                }

                return (m_Directory);
            }
        }

        public IFile File
        {
            get
            {
                if (m_File == null)
                {
                    m_File = new File(Logger);
                }

                return (m_File);
            }
        }

        public IDirectoryInfo GetDirectoryInfo(String path)
            => (new DirectoryInfo(path));

        public IFileInfo GetFileInfo(String fileName)
            => (new FileInfo(fileName));

        public System.IO.Stream GetFileStream(String fileName
            , System.IO.FileMode mode
            , System.IO.FileAccess access
            , System.IO.FileShare share)
            => (new System.IO.FileStream(fileName, mode, access, share));

        public System.IO.StreamWriter GetStreamWriter(System.IO.Stream stream
            , Encoding encoding)
            => (new System.IO.StreamWriter(stream, encoding ?? Encoding.UTF8));

        public IEnumerable<IDriveInfo> GetDriveInfos(Nullable<System.IO.DriveType> driveType)
        {
            System.IO.DriveInfo[] drives = System.IO.DriveInfo.GetDrives();

            foreach (System.IO.DriveInfo drive in drives)
            {
                if (driveType == null)
                {
                    yield return (new DriveInfo(drive));
                }
                else if (driveType.Value == drive.DriveType)
                {
                    yield return (new DriveInfo(drive));
                }
            }
        }

        public IDriveInfo GetDriveInfo(String driveLetter)
        {
            System.IO.DriveInfo driveInfo = new System.IO.DriveInfo(driveLetter);

            return (new DriveInfo(driveInfo));
        }

        public IFileSystemWatcher GetFileSystemWatcher(String path
            , String filter)
            => (new FileSystemWatcher(path, filter));

        #endregion
    }
}