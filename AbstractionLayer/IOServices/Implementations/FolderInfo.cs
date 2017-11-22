namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerDisplay("Name={Name}, FullName={FullName}")]
    internal sealed class FolderInfo : IFolderInfo
    {
        private System.IO.DirectoryInfo Actual { get; }

        public FolderInfo(String path)
        {
            Actual = new System.IO.DirectoryInfo(path);
        }

        public String Name
            => (Actual.Name);

        public IFolderInfo Root
            => (new FolderInfo(Actual.Root.FullName));

        public Boolean Exists
            => (Actual.Exists);

        public String FullName
            => (Actual.FullName);

        public void Create()
        {
            Actual.Create();
        }

        public IFileInfo[] GetFiles(String searchPattern
            , System.IO.SearchOption searchOption)
        {
            System.IO.FileInfo[] source = Actual.GetFiles(searchPattern, searchOption);

            IFileInfo[] target = source.Select(fi => new FileInfo(fi)).ToArray();

            return (target);
        }
    }
}