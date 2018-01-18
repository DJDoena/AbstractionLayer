namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Collections.Generic;

    public sealed class IOServices : IIOServices
    {
        private IPath m_Path;

        private IFolder m_Folder;

        private IFile m_File;

        private ILogger Logger { get; }

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

        public IFolder Folder
        {
            get
            {
                if (m_Folder == null)
                {
                    m_Folder = new Folder(Logger);
                }

                return (m_Folder);
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

        public IFolderInfo GetFolderInfo(String path)
            => (new FolderInfo(path));

        public IFileInfo GetFileInfo(String fileName)
            => (new FileInfo(fileName));

        public System.IO.Stream GetFileStream(String fileName
            , System.IO.FileMode mode
            , System.IO.FileAccess access
            , System.IO.FileShare share)
            => (new System.IO.FileStream(fileName, mode, access, share));

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