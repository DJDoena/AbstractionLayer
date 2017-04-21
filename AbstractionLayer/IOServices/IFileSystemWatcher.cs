using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IFileSystemWatcher
    {
        Boolean EnableRaisingEvents { get; set; }

        Boolean IncludeSubdirectories { get; set; }

        event System.IO.FileSystemEventHandler Created;

        event System.IO.FileSystemEventHandler Deleted;

        event System.IO.RenamedEventHandler Renamed;
    }
}