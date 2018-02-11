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

        public Boolean Exists(String folder)
            => (System.IO.Directory.Exists(folder));

        public void Delete(String folder)
        {
            Logger?.WriteLine($"Delete folder \"{folder}\"");

            System.IO.Directory.Delete(folder, true);
        }

        public void CreateFolder(String folder)
        {
            Logger?.WriteLine($"Create folder \"{folder}\"");

            System.IO.Directory.CreateDirectory(folder);
        }

        public IEnumerable<String> GetFolders(String folder
            , String searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<String> filtered = System.IO.Directory.GetDirectories(folder, searchPattern, searchOption);

            IEnumerable<String> sorted = filtered.OrderBy(item => item);

            return (sorted);
        }

        public IEnumerable<String> GetFiles(String folder
            , String searchPattern
            , System.IO.SearchOption searchOption)
        {
            IEnumerable<String> filtered = System.IO.Directory.GetFiles(folder, searchPattern, searchOption);

            IEnumerable<String> sorted = filtered.OrderBy(item => item);

            return (sorted);
        }

        #endregion
    }
}