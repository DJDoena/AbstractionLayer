using DoenaSoft.AbstractionLayer.IOServices;
using System;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IO.Default.Tests.Mocks;

internal sealed class MockFileInfo : IFileInfo
{
    private readonly MockIOServices _ioServices;
    private string _fullName;

    public MockFileInfo(MockIOServices ioServices, string fullName)
    {
        _ioServices = ioServices;
        _fullName = fullName;
    }

    public IIOServices IOServices => _ioServices;

    public string Name
        => SIO.Path.GetFileName(_fullName);

    public string Extension
        => SIO.Path.GetExtension(_fullName);

    public string FullName
        => _fullName;

    public IFolderInfo Folder
        => throw new NotImplementedException();

    public string FolderName
        => SIO.Path.GetDirectoryName(_fullName);

    public string NameWithoutExtension
        => SIO.Path.GetFileNameWithoutExtension(_fullName);

    public bool Exists
        => _ioServices.Files.ContainsKey(_fullName);

    public long Length
    {
        get
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                return fileData.Content.Length;
            }
            throw new SIO.FileNotFoundException("File not found", _fullName);
        }
    }

    public SIO.FileAttributes Attributes
    {
        get
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                return fileData.Attributes;
            }
            throw new SIO.FileNotFoundException("File not found", _fullName);
        }
        set
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                fileData.Attributes = value;
            }
            else
            {
                throw new SIO.FileNotFoundException("File not found", _fullName);
            }
        }
    }

    public bool IsReadOnly
    {
        get => (this.Attributes & SIO.FileAttributes.ReadOnly) == SIO.FileAttributes.ReadOnly;
        set
        {
            if (value)
            {
                this.Attributes |= SIO.FileAttributes.ReadOnly;
            }
            else
            {
                this.Attributes &= ~SIO.FileAttributes.ReadOnly;
            }
        }
    }

    public DateTime LastWriteTime
    {
        get
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                return fileData.LastWriteTime;
            }
            throw new SIO.FileNotFoundException("File not found", _fullName);
        }
        set
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                fileData.LastWriteTime = value;
            }
            else
            {
                throw new SIO.FileNotFoundException("File not found", _fullName);
            }
        }
    }

    public DateTime LastWriteTimeUtc
    {
        get => this.LastWriteTime.ToUniversalTime();
        set => this.LastWriteTime = value.ToLocalTime();
    }

    public DateTime CreationTime
    {
        get
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                return fileData.CreationTime;
            }
            throw new SIO.FileNotFoundException("File not found", _fullName);
        }
        set
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                fileData.CreationTime = value;
            }
            else
            {
                throw new SIO.FileNotFoundException("File not found", _fullName);
            }
        }
    }

    public DateTime CreationTimeUtc
    {
        get => this.CreationTime.ToUniversalTime();
        set => this.CreationTime = value.ToLocalTime();
    }

    public DateTime LastAccessTime
    {
        get
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                return fileData.LastAccessTime;
            }
            throw new SIO.FileNotFoundException("File not found", _fullName);
        }
        set
        {
            if (_ioServices.Files.TryGetValue(_fullName, out var fileData))
            {
                fileData.LastAccessTime = value;
            }
            else
            {
                throw new SIO.FileNotFoundException("File not found", _fullName);
            }
        }
    }

    public DateTime LastAccessTimeUtc
    {
        get => this.LastAccessTime.ToUniversalTime();
        set => this.LastAccessTime = value.ToLocalTime();
    }

    public void MoveTo(string targetFileName)
    {
        var targetFullPath = _ioServices.Path.GetFullPath(targetFileName);

        if (!_ioServices.Files.ContainsKey(_fullName))
        {
            throw new SIO.FileNotFoundException("Source file not found", _fullName);
        }

        // Check if this file should fail on move (for testing error scenarios)
        if (_ioServices.FailOnMove.Contains(_fullName))
        {
            throw new SIO.IOException($"Simulated failure moving file '{_fullName}'");
        }

        // Check if moving back (for rollback) should fail
        if (_ioServices.FailOnMoveBack.Contains(_fullName))
        {
            throw new SIO.IOException($"Simulated failure during rollback of file '{_fullName}'");
        }

        if (_ioServices.Files.ContainsKey(targetFullPath))
        {
            throw new SIO.IOException($"Cannot create a file when that file already exists: '{targetFullPath}'");
        }

        var fileData = _ioServices.Files[_fullName];
        _ioServices.Files.Remove(_fullName);
        _ioServices.Files[targetFullPath] = fileData;
        _fullName = targetFullPath;
    }

    public void Delete()
    {
        if (_ioServices.Files.ContainsKey(_fullName))
        {
            _ioServices.Files.Remove(_fullName);
        }
    }

    public IFileInfo CopyTo(string destFileName) => throw new NotImplementedException();
    public IFileInfo CopyTo(string destFileName, bool overwrite) => throw new NotImplementedException();
    public SIO.StreamWriter CreateText() => throw new NotImplementedException();
    public SIO.Stream Create() => throw new NotImplementedException();
    public SIO.Stream Open(SIO.FileMode mode) => throw new NotImplementedException();
    public SIO.Stream Open(SIO.FileMode mode, SIO.FileAccess access) => throw new NotImplementedException();
    public SIO.Stream Open(SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) => throw new NotImplementedException();
    public SIO.Stream OpenRead() => throw new NotImplementedException();
    public SIO.StreamReader OpenText() => throw new NotImplementedException();
    public SIO.Stream OpenWrite() => throw new NotImplementedException();

    public bool Equals(IFileInfo other)
    {
        if (other == null)
        {
            return false;
        }

        return string.Equals(_fullName, other.FullName, StringComparison.OrdinalIgnoreCase);
    }

    public override bool Equals(object obj) => this.Equals(obj as IFileInfo);

    public override int GetHashCode() => StringComparer.OrdinalIgnoreCase.GetHashCode(_fullName);
}