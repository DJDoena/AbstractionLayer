namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Standard implementation of <see cref="IFileInfo"/> for <see cref="System.IO.FileInfo"/>.
    /// </summary>
    [DebuggerDisplay("Name={Name}, FullName={FullName}")]
    public sealed class FileInfo : IFileInfo
    {
        private readonly System.IO.FileInfo _actual;

        /// <param name="fileName">The fully qualified name of the file, or the relative file name. Do not end the path with the directory separator character.</param>
        public FileInfo(string fileName) : this(new System.IO.FileInfo(fileName))
        {
        }

        /// <param name="actual">The actual file system wrapper for the file</param>
        public FileInfo(System.IO.FileInfo actual)
        {
            _actual = actual ?? throw new ArgumentNullException(nameof(actual));
        }

        #region IFileInfo

        /// <summary>
        /// Returns the file name including the extension but without the path.
        /// </summary>
        public string Name => _actual.Name;

        /// <summary>
        /// Returns the file extension including the leading '.'.
        /// </summary>
        public string Extension => _actual.Extension;

        /// <summary>
        /// Returns the full file name including path and extension.
        /// </summary>
        public string FullName => _actual.FullName;

        /// <summary>
        /// Returns the folder that contains the file.
        /// </summary>
        public IFolderInfo Folder => new FolderInfo(_actual.DirectoryName);

        /// <summary>
        /// Returns the path without the file name.
        /// </summary>
        public string FolderName => _actual.DirectoryName;

        /// <summary>
        /// Returns the file name without path and extension.
        /// </summary>
        public string NameWithoutExtension => System.IO.Path.GetFileNameWithoutExtension(_actual.Name);

        /// <summary>
        /// Returns whether the file exists.
        /// </summary>
        public bool Exists => _actual.Exists;

        /// <summary>
        /// Returns the size in bytes.
        /// </summary>
        public ulong Length => (ulong)_actual.Length;

        /// <summary>
        /// Gets or sets the time when the current file was last written to.
        /// </summary>
        public DateTime LastWriteTime
        {
            get => _actual.LastWriteTime;
            set => _actual.LastWriteTime = value;
        }

        /// <summary>
        /// Gets or sets the time (UTC) when the current file was last written to.
        /// </summary>
        public DateTime LastWriteTimeUtc
        {
            get => _actual.LastWriteTimeUtc;
            set => _actual.LastWriteTimeUtc = value;
        }

        /// <summary>
        /// Gets or sets the creation time of the current file.
        /// </summary>
        public DateTime CreationTime
        {
            get => _actual.CreationTime;
            set => _actual.CreationTime = value;
        }

        /// <summary>
        /// Gets or sets the creation time (UTC) of the current file.
        /// </summary>
        public DateTime CreationTimeUtc
        {
            get => _actual.CreationTimeUtc;
            set => _actual.CreationTimeUtc = value;
        }

        /// <summary>
        /// Gets or sets the time the current file was last accessed.
        /// </summary>
        public DateTime LastAccessTime
        {
            get => _actual.LastAccessTime;
            set => _actual.LastAccessTime = value;
        }

        /// <summary>
        /// Gets or sets the time (UTC) the current file was last accessed.
        /// </summary>
        public DateTime LastAccessTimeUtc
        {
            get => _actual.LastAccessTimeUtc;
            set => _actual.LastAccessTimeUtc = value;
        }

        #region IEquatable<IFileInfo>

        /// <summary>
        /// Compares this instance with <paramref name="other"/> if they refer to the same file.
        /// </summary>
        public bool Equals(IFileInfo other) => other != null && string.Equals(this.FullName ?? string.Empty, other.FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase);

        #endregion

        #endregion

        /// <summary>
        /// Returns a hashcode based on the full path of the file.
        /// </summary>
        public override int GetHashCode() => this.FullName?.GetHashCode() ?? 0;

        /// <summary>
        /// Compares this instance with <paramref name="obj"/> if they refer to the same file.
        /// </summary>
        public override bool Equals(object obj) => this.Equals(obj as IFileInfo);

        /// <summary>
        /// Returns the path as a string.
        /// </summary>
        public override string ToString() => _actual.ToString();
    }
}