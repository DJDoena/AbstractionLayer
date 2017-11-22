namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    public interface IFolderInfo
    {
        String Name { get; }

        IFolderInfo Root { get; }

        Boolean Exists { get; }

        String FullName { get; }

        void Create();

        IFileInfo[] GetFiles(String searchPattern
            , System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly);
    }
}