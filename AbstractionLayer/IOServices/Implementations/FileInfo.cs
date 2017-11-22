namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("Name={Name}, FullName={FullName}")]
    internal sealed class FileInfo : IFileInfo
    {
        private System.IO.FileInfo Actual { get; }

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

        public String FolderName
            => (Actual.DirectoryName);

        public String NameWithoutExtension
            => (Name.Substring(0, Name.Length - Extension.Length));

        public Boolean Exists
            => (Actual.Exists);

        public Int64 Length
            => (Actual.Length);

        public DateTime LastWriteTime
        {
            get
            {
                return (Actual.LastWriteTime);
            }
            set
            {
                Actual.LastWriteTime = value;
            }
        }

        public DateTime CreationTime
        {
            get
            {
                return (Actual.CreationTime);
            }
            set
            {
                Actual.CreationTime = value;
            }
        }
    }
}