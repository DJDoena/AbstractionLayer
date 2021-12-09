namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Standard implementation of <see cref="IFolderInfo"/> for <see cref="System.IO.DirectoryInfo"/>.
    /// </summary>
    [DebuggerDisplay("Name={Name}, FullName={FullName}")]
    internal sealed class FolderInfo : IFolderInfo
    {
        private readonly System.IO.DirectoryInfo _actual;

        public FolderInfo(string path) : this(new System.IO.DirectoryInfo(path))
        {
        }

        public FolderInfo(System.IO.DirectoryInfo actual)
        {
            _actual = actual ?? throw new ArgumentNullException(nameof(actual));
        }

        #region IFolderInfo

        public string Name => _actual.Name;

        public IFolderInfo Root => new FolderInfo(_actual.Root.FullName);

        public bool Exists => _actual.Exists;

        public string FullName => _actual.FullName;

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

        public void Create()
        {
            _actual.Create();
        }

        public IEnumerable<IFileInfo> GetFiles(string searchPattern, System.IO.SearchOption searchOption)
        {
            var source = _actual.GetFiles(searchPattern, searchOption);

            var target = source.Select(fi => new FileInfo(fi));

            return target;
        }

        #region IEquatable<IFolderInfo>

        public bool Equals(IFolderInfo other) => other != null && string.Equals(this.FullName ?? string.Empty, other.FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        public override int GetHashCode() => this.FullName?.GetHashCode() ?? 0;

        public override bool Equals(object obj) => this.Equals(obj as IFolderInfo);

        public override string ToString() => _actual.ToString();
    }
}