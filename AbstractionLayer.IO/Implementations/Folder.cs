using System;
using System.Collections.Generic;
using System.Linq;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

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

    public bool Exists(string folder) => SIO.Directory.Exists(folder);

    public string GetFullPath(string folder) => SIO.Path.GetFullPath(folder);

    public void Delete(string folder)
    {
        _logger?.WriteLine($"Delete folder \"{folder}\"");

        SIO.Directory.Delete(folder, true);
    }

    public IFolderInfo CreateFolder(string folder)
    {
        _logger?.WriteLine($"Create folder \"{folder}\"");

        var actual = SIO.Directory.CreateDirectory(folder);

        return new FolderInfo(actual);
    }

    public IEnumerable<string> GetFolderNames(string folder, string searchPattern, SIO.SearchOption searchOption)
    {
        var filtered = SIO.Directory.GetDirectories(folder, searchPattern, searchOption);

        var sorted = filtered.OrderBy(item => item);

        return sorted;
    }

    public IEnumerable<string> GetFileNames(string folder, string searchPattern, SIO.SearchOption searchOption)
    {
        var filtered = SIO.Directory.GetFiles(folder, searchPattern, searchOption);

        var sorted = filtered.OrderBy(item => item);

        return sorted;
    }

    public IEnumerable<IFolderInfo> GetFolderInfos(string folder, string searchPattern, SIO.SearchOption searchOption)
        => this.GetFolderNames(folder, searchPattern, searchOption)
        .Select(f => new FolderInfo(f));

    public IEnumerable<IFileInfo> GetFileInfos(string folder, string searchPattern, SIO.SearchOption searchOption)
        => this.GetFileNames(folder, searchPattern, searchOption)
        .Select(f => new FileInfo(f));

    #endregion
}