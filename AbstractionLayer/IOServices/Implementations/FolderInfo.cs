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
        private System.IO.DirectoryInfo Actual { get; }

        public FolderInfo(String path)
        {
            Actual = new System.IO.DirectoryInfo(path);
        }

        public String Name
            => Actual.Name;

        public IFolderInfo Root
            => new FolderInfo(Actual.Root.FullName);

        public Boolean Exists
            => Actual.Exists;

        public String FullName
            => Actual.FullName;

        public DateTime LastWriteTime
        {
            get => Actual.LastWriteTime;
            set => Actual.LastWriteTime = value;
        }

        public DateTime CreationTime
        {
            get => Actual.CreationTime;
            set => Actual.CreationTime = value;
        }

        public DateTime LastAccessTime
        {
            get => Actual.LastAccessTime;
            set => Actual.LastAccessTime = value;
        }

        public void Create()
        {
            Actual.Create();
        }

        public IEnumerable<IFileInfo> GetFiles(String searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<System.IO.FileInfo> source = Actual.GetFiles(searchPattern, searchOption);

            IEnumerable<IFileInfo> target = source.Select<System.IO.FileInfo, IFileInfo>(fi => new FileInfo(fi));

            return (target);
        }
    }
}