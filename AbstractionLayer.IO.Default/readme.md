# DoenaSoft.AbstractionLayer.IO.Default

Default `System.IO`-based implementations of the file I/O abstraction interfaces defined in `DoenaSoft.AbstractionLayer.IO`. This package provides production-ready file system operations for .NET Standard 2.0, .NET Framework 4.7.2, and .NET 10.

Package Id: `DoenaSoft.AbstractionLayer.IO.Default`

Targets: netstandard2.0, net472, net10.0

## Overview

This package contains the **default implementations** that wrap `System.IO` classes to implement the interfaces from `DoenaSoft.AbstractionLayer.IO`. Use this package in production code to perform actual file system operations.

**Dependencies:**
- `DoenaSoft.AbstractionLayer.IO` — Contains the interface contracts

License: MIT

## Implementations

This package provides concrete `System.IO`-based implementations:

### Core Implementation

- **`IOServices`** — Default implementation of `IIOServices`. Aggregates all I/O services and provides factory methods.

### File and Path Implementations

- **`File`** — Wraps `System.IO.File` static methods
- **`FileInfo`** — Wraps `System.IO.FileInfo` instance methods
- **`Path`** — Wraps `System.IO.Path` static methods

### Folder Implementations

- **`Folder`** — Wraps `System.IO.Directory` static methods
- **`FolderInfo`** — Wraps `System.IO.DirectoryInfo` instance methods

### Drive and Monitoring Implementations

- **`DriveInfo`** — Wraps `System.IO.DriveInfo`
- **`FileSystemWatcher`** — Wraps `System.IO.FileSystemWatcher`

### Supporting Implementations

- **`ConsoleLogger`** — Writes I/O operations to console
- **`FileLogger`** — Writes I/O operations to a file
- **`DualLogger`** — Combines two loggers (e.g., console + file)
- **`RenameQueue`** — Manages file rename operations

## Usage

### Basic Usage

Install both packages:
```
Install-Package DoenaSoft.AbstractionLayer.IO
Install-Package DoenaSoft.AbstractionLayer.IO.Default
```

Use the default implementations in production code:

```csharp
using DoenaSoft.AbstractionLayer.IOServices;

// Create the default IOServices instance
var io = new IOServices();

// Use the file operations
if (io.File.Exists("data.txt"))
{
    var content = io.File.ReadAllText("data.txt");
}

// Use folder operations
var folder = io.GetFolder(@"C:\MyData");
foreach (var file in folder.GetFiles("*.txt"))
{
    Console.WriteLine(file.Name);
}

// Use path operations
var combined = io.Path.Combine("folder", "subfolder", "file.txt");
```

### With Logging

The implementations support optional logging to track I/O operations:

```csharp
using DoenaSoft.AbstractionLayer.IOServices;

// Create logger (optional)
var logger = new ConsoleLogger();

// or log to file
var fileLogger = new FileLogger("operations.log");

// or combine both
var dualLogger = new DualLogger(new ConsoleLogger(), new FileLogger("ops.log"));

// Create IOServices with logger
var io = new IOServices(dualLogger);

// File operations will be logged
io.File.Copy("source.txt", "dest.txt");
// Console/file output: "Copy file "source.txt""
//                      "to        "dest.txt""
```

### Dependency Injection

Inject `IIOServices` into your classes for testability:

```csharp
public class DataProcessor
{
    private readonly IIOServices _io;

    public DataProcessor(IIOServices io)
    {
        _io = io;
    }

    public void ProcessFiles(string folder)
    {
        var folderInfo = _io.GetFolder(folder);
        foreach (var file in folderInfo.GetFiles("*.dat"))
        {
            var content = _io.File.ReadAllBytes(file.FullName);
            // process content
        }
    }
}

// In production (e.g., Program.cs or Startup.cs)
services.AddSingleton<IIOServices, IOServices>();

// In unit tests
var fakeIO = new FakeIOServices();
var processor = new DataProcessor(fakeIO);
```

## Testing Benefits

- **Production:** Use this package for real file system operations
- **Unit Tests:** Reference only `DoenaSoft.AbstractionLayer.IO` (contracts) and create test doubles
- Easily switch between real and fake implementations through dependency injection
- All implementations use the same interfaces, ensuring consistent behavior

## Package Architecture

- **`DoenaSoft.AbstractionLayer.IO`** — Interface contracts (required)
- **`DoenaSoft.AbstractionLayer.IO.Default`** (this package) — `System.IO`-based implementations

## Notes

- All file modification operations support optional logging through `ILogger`
- Implementations are thread-safe when accessing different files/folders
- `FileSystemWatcher` implementation properly wraps `System.IO.FileSystemWatcher` events


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
