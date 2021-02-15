namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Standard implementation of <see cref="IFileInfo"/> for <see cref="System.IO.FileInfo"/>.
    /// </summary>
    [DebuggerDisplay("Name={Name}, FullName={FullName}")]
    internal sealed class FileInfo : IFileInfo
    {
        private readonly System.IO.FileInfo _actual;

        public FileInfo(string fileName)
        {
            _actual = new System.IO.FileInfo(fileName);
        }

        public FileInfo(System.IO.FileInfo actual)
        {
            _actual = actual;
        }

        #region IFileInfo

        public string Name => _actual.Name;

        public string Extension => _actual.Extension;

        public string FullName => _actual.FullName;

        public IFolderInfo Folder => new FolderInfo(_actual.DirectoryName);

        public string FolderName => _actual.DirectoryName;

        public string NameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(_actual.Name);

        public bool Exists => _actual.Exists;

        public ulong Length => (ulong)(_actual.Length);

        public DateTime LastWriteTime
        {
            get => _actual.LastWriteTime;
            set => _actual.LastWriteTime = value;
        }

        public DateTime LastWriteTimeUtc
        {
            get => _actual.LastWriteTimeUtc;
            set => _actual.LastWriteTimeUtc = value;
        }

        public DateTime CreationTime
        {
            get => _actual.CreationTime;
            set => _actual.CreationTime = value;
        }

        public DateTime CreationTimeUtc
        {
            get => _actual.CreationTimeUtc;
            set => _actual.CreationTimeUtc = value;
        }

        public DateTime LastAccessTime
        {
            get => _actual.LastAccessTime;
            set => _actual.LastAccessTime = value;
        }

        public DateTime LastAccessTimeUtc
        {
            get => _actual.LastAccessTimeUtc;
            set => _actual.LastAccessTimeUtc = value;
        }

        #region IEquatable<IFileInfo>

        public bool Equals(IFileInfo other) => (FullName ?? string.Empty).Equals(other.FullName, StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        public override int GetHashCode() => FullName?.GetHashCode() ?? 0;

        public override bool Equals(object obj)
        {
            if (!(obj is IFileInfo other))
            {
                return false;
            }
            else
            {
                var equals = Equals(other);

                return equals;
            }
        }

        public override string ToString() => _actual.ToString();
    }
}