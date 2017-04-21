using System;
using System.Collections.Generic;
using System.Linq;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    internal sealed class Directory : IDirectory
    {
        private readonly ILogger Logger;

        public Directory(ILogger logger)
        {
            Logger = logger;
        }

        public Directory()
            : this(null)
        { }

        #region IDirectory

        public String WorkingDirectory
            => (Environment.CurrentDirectory);

        public Boolean Exists(String path)
            => (System.IO.Directory.Exists(path));

        public void Delete(String path)
        {
            Logger?.WriteLine($"Delete folder \"{path}\"");

            System.IO.Directory.Delete(path, true);
        }

        public void CreateFolder(String path)
        {
            Logger?.WriteLine($"Create folder \"{path}\"");

            System.IO.Directory.CreateDirectory(path);
        }

        public String[] GetFolders(String path
            , String searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<String> filtered = System.IO.Directory.GetDirectories(path, searchPattern, searchOption);

            filtered = filtered.OrderBy(item => item);

            String[] folders = filtered.ToArray();

            return (folders);
        }

        public String[] GetFiles(String path
            , String searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<String> filtered = System.IO.Directory.GetFiles(path, searchPattern, searchOption);

            filtered = filtered.OrderBy(item => item);

            String[] files = filtered.ToArray();

            return (files);
        }

        #endregion
    }
}