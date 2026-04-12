using DoenaSoft.AbstractionLayer.IOServices;
using System;
using System.Collections.Generic;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IO.Default.Tests.Mocks;

internal sealed class MockIOServices : IIOServices
{
    private readonly MockPath _path;

    private readonly MockFile _file;

    private readonly Dictionary<string, MockFileData> _files;

    private readonly HashSet<string> _failOnMove;

    private readonly HashSet<string> _failOnMoveBack;

    private readonly bool _caseSensitive;

    public MockIOServices(bool caseSensitive = false)
    {
        _caseSensitive = caseSensitive;
        var comparer = caseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;

        _files = new Dictionary<string, MockFileData>(comparer);
        _failOnMove = new HashSet<string>(comparer);
        _failOnMoveBack = new HashSet<string>(comparer);
        _path = new MockPath(this);
        _file = new MockFile(this);
    }

    public IPath Path
        => _path;

    public IFile File
        => _file;

    public IFolder Folder
        => throw new NotImplementedException();

    internal Dictionary<string, MockFileData> Files
        => _files;

    internal HashSet<string> FailOnMove
        => _failOnMove;

    internal HashSet<string> FailOnMoveBack
        => _failOnMoveBack;

    internal bool IsCaseSensitive
        => _caseSensitive;

    public IFileInfo GetFile(string fileName)
    {
        var fullPath = _path.GetFullPath(fileName);
        return new MockFileInfo(this, fullPath);
    }

    public IFolderInfo GetFolder(string folder) => throw new NotImplementedException();

    public SIO.Stream GetFileStream(string fileName, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) => throw new NotImplementedException();

    public IEnumerable<IDriveInfo> GetDrives(SIO.DriveType? driveType = null) => throw new NotImplementedException();

    public IDriveInfo GetDrive(string driveLetter) => throw new NotImplementedException();

    public IFileSystemWatcher GetFileSystemWatcher(string folder, string searchPattern = "*.*") => throw new NotImplementedException();

    public IRenameQueue CreateRenameQueue(ILogger logger = null) => new RenameQueue(this, logger);
}