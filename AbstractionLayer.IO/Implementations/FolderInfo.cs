using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    /// <summary>
    /// Standard implementation of <see cref="IFolderInfo"/> for <see cref="System.IO.DirectoryInfo"/>.
    /// </summary>
    [DebuggerDisplay("Name={Name}, FullName={FullName}")]
    public sealed class FolderInfo : IFolderInfo, IEquatable<FolderInfo>, IComparable<IFolderInfo>, IComparable<FolderInfo>
    {
        private readonly System.IO.DirectoryInfo _actual;

        /// <param name="path">The fully qualified name of the folder, or the relative folder name.</param>
        public FolderInfo(string path) : this(new System.IO.DirectoryInfo(path))
        {
        }

        /// <param name="actual">The actual file system wrapper for the folder</param>
        internal FolderInfo(System.IO.DirectoryInfo actual)
        {
            _actual = actual ?? throw new ArgumentNullException(nameof(actual));
        }

        #region IFolderInfo

        /// <summary>
        /// Returns the folder name without the path.
        /// </summary>
        public string Name => _actual.Name;

        /// <summary>
        /// Returns the root folder.
        /// </summary>
        public IFolderInfo Root => new FolderInfo(_actual.Root.FullName);

        /// <summary>
        /// Returns whether the folder exists.
        /// </summary>
        public bool Exists => _actual.Exists;

        /// <summary>
        /// Returns the full folder name including path.
        /// </summary>
        public string FullName => _actual.FullName;

        /// <summary>
        /// Gets or sets the time when the current folder was last written to.
        /// </summary>
        public DateTime LastWriteTime
        {
            get => _actual.LastWriteTime;
            set => _actual.LastWriteTime = value;
        }

        /// <summary>
        /// Gets or sets the time (UTC) when the current folder was last written to.
        /// </summary>
        public DateTime LastWriteTimeUtc
        {
            get => _actual.LastWriteTimeUtc;
            set => _actual.LastWriteTimeUtc = value;
        }

        /// <summary>
        /// Gets or sets the creation time of the current folder.
        /// </summary>
        public DateTime CreationTime
        {
            get => _actual.CreationTime;
            set => _actual.CreationTime = value;
        }

        /// <summary>
        /// Gets or sets the creation time (UTC) of the current folder.
        /// </summary>
        public DateTime CreationTimeUtc
        {
            get => _actual.CreationTimeUtc;
            set => _actual.CreationTimeUtc = value;
        }

        /// <summary>
        /// Gets or sets the time the current folder was last accessed.
        /// </summary>
        public DateTime LastAccessTime
        {
            get => _actual.LastAccessTime;
            set => _actual.LastAccessTime = value;
        }

        /// <summary>
        /// Gets or sets the time (UTC) the current folder was last accessed.
        /// </summary>
        public DateTime LastAccessTimeUtc
        {
            get => _actual.LastAccessTimeUtc;
            set => _actual.LastAccessTimeUtc = value;
        }

        /// <summary>
        /// Creates the folder.
        /// </summary>
        public void Create()
        {
            _actual.Create();
        }

        /// <summary>
        /// Returns all files in the folder according to the search pattern and option.
        /// </summary>
        /// <param name="searchPattern">The search pattern</param>
        /// <param name="searchOption">The search option</param>
        /// <returns>All files in the folder according to the search pattern and option</returns>
        public IEnumerable<IFileInfo> GetFileInfos(string searchPattern, System.IO.SearchOption searchOption)
        {
            var source = _actual.GetFiles(searchPattern, searchOption);

            var target = source.Select(f => new FileInfo(f));

            return target;
        }

        #region IEquatable<IFolderInfo>

        /// <summary>
        /// Compares this instance with <paramref name="other"/> if they refer to the same folder.
        /// </summary>
        public bool Equals(IFolderInfo other)
            => this.GetEquality(other);

        #endregion

        #endregion

        #region IEquatable<FolderInfo>

        /// <summary>
        /// Compares this instance with <paramref name="other"/> if they refer to the same file.
        /// </summary>
        public bool Equals(FolderInfo other)
            => this.GetEquality(other);

        #endregion

        #region IComparable<IFolderInfo>

        /// <summary>
        /// Compares to this folder path to another's.
        /// </summary>
        /// <param name="other">the other folder</param>
        public int CompareTo(IFolderInfo other)
            => this.GetComparison(other);

        #endregion

        #region IComparable<FolderInfo>

        /// <summary>
        /// Compares to this file path to another's.
        /// </summary>
        /// <param name="other">the other file</param>
        public int CompareTo(FolderInfo other)
            => this.GetComparison(other);

        #endregion

        /// <summary>
        /// Returns a hashcode based on the full path of the folder.
        /// </summary>
        public override int GetHashCode()
            => this.FullName?.GetHashCode() ?? 0;

        /// <summary>
        /// Compares this instance with <paramref name="obj"/> if they refer to the same folder.
        /// </summary>
        public override bool Equals(object obj)
            => this.GetEquality(obj as IFolderInfo);

        /// <summary>
        /// Returns the path as a string.
        /// </summary>
        public override string ToString()
            => _actual.ToString();

        private bool GetEquality(IFolderInfo other)
            => other != null && string.Equals(this.FullName ?? string.Empty, other.FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase);

        private int GetComparison(IFolderInfo other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                var result = string.Compare(this.FullName ?? string.Empty, other.FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase);

                return result;
            }
        }
    }
}