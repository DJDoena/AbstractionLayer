using System;
using System.Diagnostics;
using System.Linq;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    [DebuggerDisplay("Name={Name}, FullName={FullName}")]
    internal sealed class DirectoryInfo : IDirectoryInfo
    {
        private readonly System.IO.DirectoryInfo Actual;

        public DirectoryInfo(String path)
        {
            Actual = new System.IO.DirectoryInfo(path);
        }

        public String Name
            => (Actual.Name);

        public IDirectoryInfo Root
            => (new DirectoryInfo(Actual.Root.FullName));

        public Boolean Exists
            => (Actual.Exists);

        public String FullName
            => (Actual.FullName);

        public void Create()
        {
            Actual.Create();

        }

        public IFileInfo[] GetFiles(String searchPattern
            , System.IO.SearchOption option)
        {
            System.IO.FileInfo[] source = Actual.GetFiles(searchPattern, option);

            IFileInfo[] target = source.Select(fi => new FileInfo(fi)).ToArray();

            return (target);
        }
    }
}