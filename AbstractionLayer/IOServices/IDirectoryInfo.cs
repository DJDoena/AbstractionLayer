using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IDirectoryInfo
    {
        String Name { get; }

        IDirectoryInfo Root { get; }

        Boolean Exists { get; }

        String FullName { get; }

        void Create();

        IFileInfo[] GetFiles(String searchPattern
            , System.IO.SearchOption option = System.IO.SearchOption.TopDirectoryOnly);
    }
}