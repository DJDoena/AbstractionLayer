namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Path={Actual.Path}, Filter={Actual.Filter}")]
    internal sealed class FileSystemWatcher : IFileSystemWatcher
    {
        private System.IO.FileSystemWatcher Actual { get; }

        public FileSystemWatcher(String path
            , String filter)
        {
            Actual = new System.IO.FileSystemWatcher(path, filter);
        }

        #region IFileSystemWatcher

        public Boolean IncludeSubdirectories
        {
            get
            {
                return (Actual.IncludeSubdirectories);
            }
            set
            {
                Actual.IncludeSubdirectories = value;
            }
        }

        public Boolean EnableRaisingEvents
        {
            get
            {
                return (Actual.EnableRaisingEvents);
            }
            set
            {
                Actual.EnableRaisingEvents = value;
            }
        }

        public event System.IO.FileSystemEventHandler Created
        {
            add
            {
                Actual.Created += value;
            }
            remove
            {
                Actual.Created -= value;
            }
        }

        public event System.IO.RenamedEventHandler Renamed
        {
            add
            {
                Actual.Renamed += value;
            }
            remove
            {
                Actual.Renamed -= value;
            }
        }

        public event System.IO.FileSystemEventHandler Deleted
        {
            add
            {
                Actual.Deleted += value;
            }
            remove
            {
                Actual.Deleted -= value;
            }
        }

        #endregion
    }
}