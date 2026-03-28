# DoenaSoft.AbstractionLayer.IO

File I/O abstractions to simplify unit testing of file operations. This project targets .NET Standard 2.0, .NET Framework 4.7.2, and .NET 10 to be usable from multiple framework versions.

Package Id: `DoenaSoft.AbstractionLayer.IO`

Targets: netstandard2.0, net472, net10.0

Usage:

Use the provided interfaces when writing code that performs file operations so you can inject test doubles in unit tests.

License: MIT

## Interfaces

The project provides several interfaces that represent parts of the file system. The primary entry point is `IIOServices` and the contracts include:

### Core Interfaces

- **`IIOServices`** — The main entry point that aggregates access to all I/O services. Provides factory methods for files, folders, drives, and file system watchers.

### Base Interface

- **`IIOServiceItem`** — Base interface for all types that provide access to the master `IIOServices` interface. All file system-related interfaces inherit from this.

### Path and File Operations

- **`IPath`** — Provides path manipulation helpers (combine, get extension, get folder name, change extension, etc.)
- **`IFile`** — Static file operations (exists, copy, move, delete, read all bytes/lines/text, write all bytes/lines/text, append, open streams, get/set attributes and timestamps, replace, etc.)
- **`IFileInfo`** — Instance-based file operations and metadata (name, extension, full name, folder, size, attributes, timestamps, exists, copy, move, delete, open streams, create text, etc.)

### Folder Operations

- **`IFolder`** — Static folder operations (exists, create, delete, get files/folders, move, get/set current folder, get parent, etc.)
- **`IFolderInfo`** — Instance-based folder operations and metadata (name, full name, parent, root, drive, attributes, timestamps, exists, create, delete, move, get files/folders, etc.)

### Drive and File System Watching

- **`IDriveInfo`** — Drive-specific information (name, drive type, drive format, drive letter, volume label, ready state, available free space, total free space, total size, root folder, etc.)
- **`IFileSystemWatcher`** — File system change notification abstraction with events for file creation, deletion, and renaming.

### Supporting Interfaces

- **`ILogger`** — Logging abstraction used by implementations to record I/O operations.
- **`IRenameQueue`** — Queue abstraction for managing file rename operations.
- **`IShortcut`** — Abstraction for creating and managing file shortcuts with description, target path, and working folder.

`IIOServices` is intended as the primary entry point in application code; it is typically implemented with a thin wrapper around `System.IO` in production and with an in-memory fake in tests.

Testing benefits

- Replace the real file system with an in-memory `IIOServices` fake during tests to avoid touching the disk and to deterministically set up file state.
- Verify that your code attempts the correct file operations and handles error conditions (file not found, access denied) without relying on platform-specific behavior.

Examples

Usage from application code:

```csharp
public class FileProcessor
{
    private readonly DoenaSoft.AbstractionLayer.IOServices.IIOServices _io;

    public FileProcessor(DoenaSoft.AbstractionLayer.IOServices.IIOServices io)
    {
        _io = io;
    }

    public string ReadFirstLine(string path)
    {
        using var stream = _io.GetFileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
        using var reader = new System.IO.StreamReader(stream);
        return reader.ReadLine();
    }
}
```

Simple fake for unit tests (illustrative, implement additional members as required):

```csharp
class FakeFileStream : System.IO.MemoryStream { }

class FakeFile : DoenaSoft.AbstractionLayer.IOServices.IFile
{
    public bool Exists(string fileName) => true;
    // ... implement other IFile members used by your code
}

class FakeIIOServices : DoenaSoft.AbstractionLayer.IOServices.IIOServices
{
    public DoenaSoft.AbstractionLayer.IOServices.IPath Path => throw new System.NotImplementedException();
    public DoenaSoft.AbstractionLayer.IOServices.IFile File => new FakeFile();
    public DoenaSoft.AbstractionLayer.IOServices.IFolder Folder => throw new System.NotImplementedException();

    public DoenaSoft.AbstractionLayer.IOServices.IFolderInfo GetFolder(string folder) => throw new System.NotImplementedException();
    public DoenaSoft.AbstractionLayer.IOServices.IFileInfo GetFile(string fileName) => throw new System.NotImplementedException();

    public System.IO.Stream GetFileStream(string fileName, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share)
    {
        // Return a memory stream containing test data
        var ms = new FakeFileStream();
        var writer = new System.IO.StreamWriter(ms);
        writer.WriteLine("first line");
        writer.Flush();
        ms.Position = 0;
        return ms;
    }

    public System.Collections.Generic.IEnumerable<DoenaSoft.AbstractionLayer.IOServices.IDriveInfo> GetDrives(System.IO.DriveType? driveType = null) => System.Array.Empty<DoenaSoft.AbstractionLayer.IOServices.IDriveInfo>();
    public DoenaSoft.AbstractionLayer.IOServices.IDriveInfo GetDrive(string driveLetter) => throw new System.NotImplementedException();
    public DoenaSoft.AbstractionLayer.IOServices.IFileSystemWatcher GetFileSystemWatcher(string folder, string searchPattern = "*.*") => throw new System.NotImplementedException();
}

// In a unit test
var fake = new FakeIIOServices();
var processor = new FileProcessor(fake);
var firstLine = processor.ReadFirstLine("any.txt");
// assert firstLine == "first line"
```
