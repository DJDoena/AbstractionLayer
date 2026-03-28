# DoenaSoft.AbstractionLayer.IO

File I/O interface contracts to simplify unit testing of file operations. This project contains **interface definitions only** and targets .NET Standard 2.0, .NET Framework 4.7.2, and .NET 10 to be usable from multiple framework versions.

Package Id: `DoenaSoft.AbstractionLayer.IO`

Targets: netstandard2.0, net472, net10.0

## Overview

This package contains **only the interface contracts** for file I/O abstractions. For production use, you'll also need the default implementations package: **`DoenaSoft.AbstractionLayer.IO.Default`** which contains `System.IO`-based implementations of these interfaces.

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

## Usage

### In Production Code

Program against these interfaces and inject implementations at runtime. For the default `System.IO`-based implementations, reference the **`DoenaSoft.AbstractionLayer.IO.Default`** package:

```csharp
using DoenaSoft.AbstractionLayer.IOServices;

public class FileProcessor
{
    private readonly IIOServices _io;

    public FileProcessor(IIOServices io)
    {
        _io = io;
    }

    public string ReadFirstLine(string path)
    {
        var fileContent = _io.File.ReadAllText(path);
        return fileContent.Split('\n')[0];
    }
}

// In your application startup (using DoenaSoft.AbstractionLayer.IO.Default):
var io = new IOServices();
var processor = new FileProcessor(io);
```

### In Unit Tests

Reference **only** this contracts package and provide test doubles to avoid disk I/O:

```csharp
class FakeFile : IFile
{
    public IIOServices IOServices { get; }
    public bool Exists(string fileName) => true;
    public string ReadAllText(string path) => "line 1\nline 2";
    // ... implement other IFile members used by your code
}

class FakeIOServices : IIOServices
{
    public IPath Path => throw new NotImplementedException();
    public IFile File => new FakeFile();
    public IFolder Folder => throw new NotImplementedException();
    // ... implement only the members your tests need
}

// In your test
var fake = new FakeIOServices();
var processor = new FileProcessor(fake);
var firstLine = processor.ReadFirstLine("any.txt");
Assert.AreEqual("line 1", firstLine);
```

## Testing Benefits

- Replace the real file system with an in-memory `IIOServices` fake during tests to avoid touching the disk
- Set up deterministic file state for tests
- Verify that your code attempts the correct file operations
- Test error handling (file not found, access denied) without relying on platform-specific behavior
- Keep test projects lightweight by referencing only the contracts package

## Package Architecture

- **`DoenaSoft.AbstractionLayer.IO`** (this package) — Interface contracts only
- **`DoenaSoft.AbstractionLayer.IO.Default`** — Default `System.IO`-based implementations (depends on this package)

For production use, reference **`DoenaSoft.AbstractionLayer.IO.Default`**. For unit tests, reference only this contracts package.
