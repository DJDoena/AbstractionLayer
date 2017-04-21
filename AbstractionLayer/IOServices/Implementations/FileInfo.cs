using System;
using System.Diagnostics;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    [DebuggerDisplay("Name={Name}, FullName={FullName}")]
    internal sealed class FileInfo : IFileInfo
    {
        private readonly System.IO.FileInfo Actual;

        public FileInfo(String fileName)
        {
            Actual = new System.IO.FileInfo(fileName);
        }

        public FileInfo(System.IO.FileInfo actual)
        {
            Actual = actual;
        }

        public String Name
            => (Actual.Name);

        public String Extension
            => (Actual.Extension);

        public String FullName
             => (Actual.FullName);

        public String DirectoryName
            => (Actual.DirectoryName);

        public String NameWithoutExtension
            => (Name.Substring(0, Name.Length - Extension.Length));

        public Boolean Exists
            => (Actual.Exists);

        public Int64 Length
            => (Actual.Length);

        public DateTime LastWriteTime
            => (Actual.LastWriteTime);
    }
}