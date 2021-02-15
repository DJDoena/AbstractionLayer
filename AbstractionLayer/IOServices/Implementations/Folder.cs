namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class Folder : IFolder
    {
        private readonly ILogger _logger;

        public Folder(ILogger logger)
        {
            _logger = logger;
        }

        public Folder() : this(null)
        { }

        #region IFolder

        public string WorkingFolder => Environment.CurrentDirectory;

        public bool Exists(string folder) => System.IO.Directory.Exists(folder);

        public void Delete(string folder)
        {
            _logger?.WriteLine($"Delete folder \"{folder}\"");

            System.IO.Directory.Delete(folder, true);
        }

        public void CreateFolder(string folder)
        {
            _logger?.WriteLine($"Create folder \"{folder}\"");

            System.IO.Directory.CreateDirectory(folder);
        }

        public IEnumerable<string> GetFolders(string folder, string searchPattern, System.IO.SearchOption searchOption)
        {
            var filtered = System.IO.Directory.GetDirectories(folder, searchPattern, searchOption);

            var sorted = filtered.OrderBy(item => item);

            return (sorted);
        }

        public IEnumerable<string> GetFiles(string folder, string searchPattern, System.IO.SearchOption searchOption)
        {
            var filtered = System.IO.Directory.GetFiles(folder, searchPattern, searchOption);

            var sorted = filtered.OrderBy(item => item);

            return sorted;
        }

        #endregion
    }
}