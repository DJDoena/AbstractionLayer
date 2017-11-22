namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class Folder : IFolder
    {
        private ILogger Logger { get; }

        public Folder(ILogger logger)
        {
            Logger = logger;
        }

        public Folder()
            : this(null)
        { }

        #region IFolder

        public String WorkingFolder
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