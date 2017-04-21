using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IFileInfo
    {
        String Name { get; }

        String Extension { get; }

        String FullName { get; }

        String DirectoryName { get; }

        String NameWithoutExtension { get; }

        Boolean Exists { get; }

        Int64 Length { get; }

        DateTime LastWriteTime { get; }
    }
}