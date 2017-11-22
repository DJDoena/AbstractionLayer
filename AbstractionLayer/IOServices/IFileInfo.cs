namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    public interface IFileInfo
    {
        String Name { get; }

        String Extension { get; }

        String FullName { get; }

        String FolderName { get; }

        String NameWithoutExtension { get; }

        Boolean Exists { get; }

        Int64 Length { get; }

        DateTime LastWriteTime { get; set; }

        DateTime CreationTime { get; set; }
    }
}