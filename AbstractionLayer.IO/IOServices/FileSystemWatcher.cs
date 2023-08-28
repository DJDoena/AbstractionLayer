using System.Diagnostics;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    /// <summary>
    /// Standard implementation of <see cref="IFileSystemWatcher"/> for <see cref="System.IO.FileSystemWatcher"/>.
    /// </summary>
    [DebuggerDisplay("Path={Actual.Path}, Filter={Actual.Filter}")]
    internal sealed class FileSystemWatcher : IFileSystemWatcher
    {
        private readonly System.IO.FileSystemWatcher _actual;

        public FileSystemWatcher(string path, string filter)
        {
            _actual = new System.IO.FileSystemWatcher(path, filter);
        }

        #region IFileSystemWatcher

        public bool IncludeSubFolders
        {
            get => _actual.IncludeSubdirectories;
            set => _actual.IncludeSubdirectories = value;
        }

        public bool EnableRaisingEvents
        {
            get => _actual.EnableRaisingEvents;
            set => _actual.EnableRaisingEvents = value;
        }

        public event System.IO.FileSystemEventHandler Created
        {
            add => _actual.Created += value;
            remove => _actual.Created -= value;
        }

        public event System.IO.RenamedEventHandler Renamed
        {
            add => _actual.Renamed += value;
            remove => _actual.Renamed -= value;
        }

        public event System.IO.FileSystemEventHandler Deleted
        {
            add => _actual.Deleted += value;
            remove => _actual.Deleted -= value;
        }

        #endregion
    }
}