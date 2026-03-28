using System.Diagnostics;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IFileSystemWatcher"/> for <see cref="SIO.FileSystemWatcher"/>.
/// </summary>
[DebuggerDisplay("Path={Actual.Path}, Filter={Actual.Filter}")]
internal sealed class FileSystemWatcher : IFileSystemWatcher
{
    private readonly SIO.FileSystemWatcher _actual;

    /// <summary>
    /// The master interface.
    /// </summary>
    public IIOServices IOServices { get; }

    public FileSystemWatcher(IIOServices ioServices
        , string path
        , string filter)
    {
        this.IOServices = ioServices;

        _actual = new SIO.FileSystemWatcher(path, filter);
    }

    #region IFileSystemWatcher

    public bool IncludeSubFolders
    {
        get => _actual.IncludeSubdirectories;
        set => _actual.IncludeSubdirectories = value;
    }

    public bool EnableRaisingEvents
    {
        get => _actual.EnableRaisingEvents;
        set => _actual.EnableRaisingEvents = value;
    }

    public event SIO.FileSystemEventHandler Created
    {
        add => _actual.Created += value;
        remove => _actual.Created -= value;
    }

    public event SIO.RenamedEventHandler Renamed
    {
        add => _actual.Renamed += value;
        remove => _actual.Renamed -= value;
    }

    public event SIO.FileSystemEventHandler Deleted
    {
        add => _actual.Deleted += value;
        remove => _actual.Deleted -= value;
    }

    #endregion
}