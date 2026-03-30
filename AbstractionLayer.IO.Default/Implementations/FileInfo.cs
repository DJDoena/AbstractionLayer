using System;
using System.Diagnostics;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IFileInfo"/> for <see cref="SIO.FileInfo"/>.
/// </summary>
[DebuggerDisplay("Name={Name}, FullName={FullName}")]
internal sealed class FileInfo : IOServiceItem, IFileInfo, IEquatable<FileInfo>, IComparable<IFileInfo>, IComparable<FileInfo>
{
    private readonly SIO.FileInfo _actual;

    public FileInfo(IIOServices ioServices
        , string fileName)
        : this(ioServices, new SIO.FileInfo(fileName))
    {
    }

    internal FileInfo(IIOServices ioServices
        , SIO.FileInfo actual)
        : base(ioServices)
    {
        _actual = actual ?? throw new ArgumentNullException(nameof(actual));
    }

    #region IFileInfo

    /// <summary>
    /// Returns the file name including the extension but without the path.
    /// </summary>
    public string Name
        => _actual.Name;

    /// <summary>
    /// Returns the file extension including the leading '.'.
    /// </summary>
    public string Extension
        => _actual.Extension;

    /// <summary>
    /// Returns the full file name including path and extension.
    /// </summary>
    public string FullName
        => _actual.FullName;

    /// <summary>
    /// Returns the folder that contains the file.
    /// </summary>
    public IFolderInfo Folder
        => new FolderInfo(this.IOServices, _actual.DirectoryName);

    /// <summary>
    /// Returns the path without the file name.
    /// </summary>
    public string FolderName
        => _actual.DirectoryName;

    /// <summary>
    /// Returns the file name without path and extension.
    /// </summary>
    public string NameWithoutExtension
        => SIO.Path.GetFileNameWithoutExtension(_actual.Name);

    /// <summary>
    /// Returns whether the file exists.
    /// </summary>
    public bool Exists
        => _actual.Exists;

    /// <summary>
    /// Returns the size in bytes.
    /// </summary>
    public long Length
        => _actual.Length;

    /// <summary>
    /// Gets or sets the attributes for the current file.
    /// </summary>
    public SIO.FileAttributes Attributes
    {
        get => _actual.Attributes;
        set => _actual.Attributes = value;
    }

    /// <summary>
    /// Gets a value indicating whether the file is read-only.
    /// </summary>
    public bool IsReadOnly
    {
        get => _actual.IsReadOnly;
        set => _actual.IsReadOnly = value;
    }

    /// <summary>
    /// Gets or sets the time when the current file was last written to.
    /// </summary>
    public DateTime LastWriteTime
    {
        get => _actual.LastWriteTime;
        set => _actual.LastWriteTime = value;
    }

    /// <summary>
    /// Gets or sets the time (UTC) when the current file was last written to.
    /// </summary>
    public DateTime LastWriteTimeUtc
    {
        get => _actual.LastWriteTimeUtc;
        set => _actual.LastWriteTimeUtc = value;
    }

    /// <summary>
    /// Gets or sets the creation time of the current file.
    /// </summary>
    public DateTime CreationTime
    {
        get => _actual.CreationTime;
        set => _actual.CreationTime = value;
    }

    /// <summary>
    /// Gets or sets the creation time (UTC) of the current file.
    /// </summary>
    public DateTime CreationTimeUtc
    {
        get => _actual.CreationTimeUtc;
        set => _actual.CreationTimeUtc = value;
    }

    /// <summary>
    /// Gets or sets the time the current file was last accessed.
    /// </summary>
    public DateTime LastAccessTime
    {
        get => _actual.LastAccessTime;
        set => _actual.LastAccessTime = value;
    }

    /// <summary>
    /// Gets or sets the time (UTC) the current file was last accessed.
    /// </summary>
    public DateTime LastAccessTimeUtc
    {
        get => _actual.LastAccessTimeUtc;
        set => _actual.LastAccessTimeUtc = value;
    }

    /// <summary>
    /// Moves a specified file to a new location, providing the option to specify a new file name.
    /// </summary>
    /// <param name="targetFileName">the path to move the file to, which can specify a different file name</param>
    public void MoveTo(string targetFileName)
        => _actual.MoveTo(targetFileName);

    /// <summary>
    /// Copies an existing file to a new file, disallowing the overwriting of an existing file.
    /// </summary>
    /// <param name="destFileName">The name of the new file to copy to.</param>
    public IFileInfo CopyTo(string destFileName)
        => new FileInfo(this.IOServices, _actual.CopyTo(destFileName));

    /// <summary>
    /// Copies an existing file to a new file, allowing the overwriting of an existing file.
    /// </summary>
    /// <param name="destFileName">The name of the new file to copy to.</param>
    /// <param name="overwrite">true to allow an existing file to be overwritten; otherwise, false.</param>
    public IFileInfo CopyTo(string destFileName, bool overwrite)
        => new FileInfo(this.IOServices, _actual.CopyTo(destFileName, overwrite));

    /// <summary>
    /// Deletes the file.
    /// </summary>
    public void Delete()
        => _actual.Delete();

    /// <summary>
    /// Creates a StreamWriter that writes a new text file.
    /// </summary>
    public SIO.StreamWriter CreateText()
        => _actual.CreateText();

    /// <summary>
    /// Creates a write-only FileStream.
    /// </summary>
    public SIO.Stream Create()
        => _actual.Create();

    /// <summary>
    /// Opens a file in the specified mode.
    /// </summary>
    public SIO.Stream Open(SIO.FileMode mode)
        => _actual.Open(mode);

    /// <summary>
    /// Opens a file in the specified mode with read, write, or read/write access.
    /// </summary>
    public SIO.Stream Open(SIO.FileMode mode, SIO.FileAccess access)
        => _actual.Open(mode, access);

    /// <summary>
    /// Opens a file in the specified mode with read, write, or read/write access and the specified sharing option.
    /// </summary>
    public SIO.Stream Open(SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share)
        => _actual.Open(mode, access, share);

    /// <summary>
    /// Creates a read-only FileStream.
    /// </summary>
    public SIO.Stream OpenRead()
        => _actual.OpenRead();

    /// <summary>
    /// Creates a StreamReader with UTF8 encoding that reads from an existing text file.
    /// </summary>
    public SIO.StreamReader OpenText()
        => _actual.OpenText();

    /// <summary>
    /// Creates a write-only FileStream.
    /// </summary>
    public SIO.Stream OpenWrite()
        => _actual.OpenWrite();

    #region IEquatable<IFileInfo>

    /// <summary>
    /// Compares this instance with <paramref name="other"/> if they refer to the same file.
    /// </summary>
    public bool Equals(IFileInfo other)
        => this.GetEquality(other);

    #endregion

    #endregion

    #region IEquatable<FileInfo>

    /// <summary>
    /// Compares this instance with <paramref name="other"/> if they refer to the same file.
    /// </summary>
    public bool Equals(FileInfo other)
        => this.GetEquality(other);

    #endregion

    #region IComparable<IFileInfo>

    /// <summary>
    /// Compares to this file path to another's.
    /// </summary>
    /// <param name="other">the other file</param>
    public int CompareTo(IFileInfo other)
        => this.GetComparison(other);

    #endregion

    #region IComparable<FileInfo>

    /// <summary>
    /// Compares to this file path to another's.
    /// </summary>
    /// <param name="other">the other file</param>
    public int CompareTo(FileInfo other)
        => this.GetComparison(other);

    #endregion

    /// <summary>
    /// Returns a hashcode based on the full path of the file.
    /// </summary>
    public override int GetHashCode()
        => this.FullName?.GetHashCode() ?? 0;

    /// <summary>
    /// Compares this instance with <paramref name="obj"/> if they refer to the same file.
    /// </summary>
    public override bool Equals(object obj)
        => this.GetEquality(obj as IFileInfo);

    /// <summary>
    /// Returns the path as a string.
    /// </summary>
    public override string ToString()
        => _actual.ToString();

    private bool GetEquality(IFileInfo other)
        => ReferenceEquals(this, other)
            || (other is FileInfo otherFI
                && ReferenceEquals(_actual, otherFI._actual))
            || (other != null
                && string.Equals(this.FullName ?? string.Empty, other.FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase));

    private int GetComparison(IFileInfo other)
    {
        if (other == null)
        {
            return 1;
        }
        else
        {
            var result = string.Compare(this.FullName ?? string.Empty, other.FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase);

            return result;
        }
    }
}