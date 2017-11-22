namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    public interface IFileSystemWatcher
    {
        Boolean EnableRaisingEvents { get; set; }

        Boolean IncludeSubdirectories { get; set; }

        event System.IO.FileSystemEventHandler Created;

        event System.IO.FileSystemEventHandler Deleted;

        event System.IO.RenamedEventHandler Renamed;
    }
}