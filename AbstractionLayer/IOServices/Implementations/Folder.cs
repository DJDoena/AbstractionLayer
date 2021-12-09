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
        {
        }

        #region IFolder

        public string WorkingFolder => Environment.CurrentDirectory;

        public bool Exists(string folder) => System.IO.Directory.Exists(folder);

        public string GetFullPath(string folder) => System.IO.Path.GetFullPath(folder);

        public void Delete(string folder)
        {
            _logger?.WriteLine($"Delete folder \"{folder}\"");

            System.IO.Directory.Delete(folder, true);
        }

        public IFolderInfo CreateFolder(string folder)
        {
            _logger?.WriteLine($"Create folder \"{folder}\"");

            var actual = System.IO.Directory.CreateDirectory(folder);

            return new FolderInfo(actual);
        }

        public IEnumerable<string> GetFolderNames(string folder, string searchPattern, System.IO.SearchOption searchOption)
        {
            var filtered = System.IO.Directory.GetDirectories(folder, searchPattern, searchOption);

            var sorted = filtered.OrderBy(item => item);

            return sorted;
        }

        public IEnumerable<string> GetFileNames(string folder, string searchPattern, System.IO.SearchOption searchOption)
        {
            var filtered = System.IO.Directory.GetFiles(folder, searchPattern, searchOption);

            var sorted = filtered.OrderBy(item => item);

            return sorted;
        }

        public IEnumerable<IFolderInfo> GetFolderInfos(string folder, string searchPattern, System.IO.SearchOption searchOption) => this.GetFolderNames(folder, searchPattern, searchOption).Select(f => new FolderInfo(f));

        public IEnumerable<IFileInfo> GetFileInfos(string folder, string searchPattern, System.IO.SearchOption searchOption) => this.GetFileNames(folder, searchPattern, searchOption).Select(f => new FileInfo(f));

        #endregion
    }
}