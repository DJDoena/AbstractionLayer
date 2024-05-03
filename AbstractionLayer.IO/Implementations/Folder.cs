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

    public Folder()
        : this(null)
    {
    }

    #region IFolder

    public string WorkingFolderName
        => Environment.CurrentDirectory;

    public bool Exists(string folder)
        => SIO.Directory.Exists(folder);

    public string GetFullPath(string folder)
        => SIO.Path.GetFullPath(folder);

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

    public IEnumerable<string> GetFolderNames(string folder
        , string searchPattern
        , SIO.SearchOption searchOption)
        => SIO.Directory.GetDirectories(folder, searchPattern, searchOption)
            .OrderBy(item => item);

    public IEnumerable<IFolderInfo> GetFolders(string folder
        , string searchPattern
        , SIO.SearchOption searchOption)
        => this.GetFolderNames(folder, searchPattern, searchOption)
            .Select(f => new FolderInfo(f));

    public IEnumerable<IFolderInfo> GetFolderInfos(string folder
        , string searchPattern
        , SIO.SearchOption searchOption)
        => this.GetFolders(folder, searchPattern, searchOption);

    public IEnumerable<IFolderInfo> GetDirectories(string folder
        , string searchPattern
        , SIO.SearchOption searchOption)
        => this.GetFolders(folder, searchPattern, searchOption);

    public IEnumerable<IFileInfo> GetFileInfos(string folder
        , string searchPattern
        , SIO.SearchOption searchOption)
        => this.GetFiles(folder, searchPattern, searchOption);

    public IEnumerable<string> GetFileNames(string folder
        , string searchPattern
        , SIO.SearchOption searchOption)
        => SIO.Directory.GetFiles(folder, searchPattern, searchOption)
            .OrderBy(item => item);

    public IEnumerable<IFileInfo> GetFiles(string folder
        , string searchPattern
        , SIO.SearchOption searchOption)
        => this.GetFileNames(folder, searchPattern, searchOption)
            .Select(f => new FileInfo(f));

    #endregion
}