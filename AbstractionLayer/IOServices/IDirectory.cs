using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IDirectory
    {
        String WorkingDirectory { get; }

        Boolean Exists(String path);

        void Delete(String path);

        void CreateFolder(String path);

        String[] GetFolders(String path
            , String searchPattern = "*.*"
            , System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly);

        String[] GetFiles(String path
            , String searchPattern = "*.*"
            , System.IO.SearchOption searchOption = System.IO.SearchOption.TopDirectoryOnly);

    }
}