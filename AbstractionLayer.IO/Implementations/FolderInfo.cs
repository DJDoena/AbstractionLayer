using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IFolderInfo"/> for <see cref="SIO.DirectoryInfo"/>.
/// </summary>
[DebuggerDisplay("Name={Name}, FullName={FullName}")]
internal sealed class FolderInfo : IFolderInfo, IEquatable<FolderInfo>, IComparable<IFolderInfo>, IComparable<FolderInfo>
{
    private readonly SIO.DirectoryInfo _actual;

    /// <summary>
    /// The master interface.
    /// </summary>
    public IIOServices IOServices { get; }

    public FolderInfo(IIOServices ioServices
        , string path)
        : this(ioServices, new SIO.DirectoryInfo(path))
    {
    }

    internal FolderInfo(IIOServices ioServices
        , SIO.DirectoryInfo actual)
    {
        this.IOServices = ioServices;

        _actual = actual ?? throw new ArgumentNullException(nameof(actual));
    }

    #region IFolderInfo

    /// <summary>
    /// Returns the folder name without the path.
    /// </summary>
    public string Name
        => _actual.Name;

    /// <summary>
    /// Returns the parent folder.
    /// </summary>
    public IFolderInfo Parent
        => _actual.Parent != null
            ? new FolderInfo(this.IOServices, _actual.Parent)
            : null;

    /// <summary>
    /// Returns the root folder.
    /// </summary>
    public IFolderInfo Root
        => _actual.Root != null
            ? new FolderInfo(this.IOServices, _actual.Root.FullName)
            : null;

    public IDriveInfo Drive
        => this.IOServices.GetDriveInfo(this.Root.Name);

    /// <summary>
    /// Returns whether the folder exists.
    /// </summary>
    public bool Exists
        => _actual.Exists;

    /// <summary>
    /// Returns the full folder name including path.
    /// </summary>
    public string FullName
        => _actual.FullName;

    /// <summary>
    /// Gets or sets the time when the current folder was last written to.
    /// </summary>
    public DateTime LastWriteTime
    {
        get => _actual.LastWriteTime;
        set => _actual.LastWriteTime = value;
    }

    /// <summary>
    /// Gets or sets the time (UTC) when the current folder was last written to.
    /// </summary>
    public DateTime LastWriteTimeUtc
    {
        get => _actual.LastWriteTimeUtc;
        set => _actual.LastWriteTimeUtc = value;
    }

    /// <summary>
    /// Gets or sets the creation time of the current folder.
    /// </summary>
    public DateTime CreationTime
    {
        get => _actual.CreationTime;
        set => _actual.CreationTime = value;
    }

    /// <summary>
    /// Gets or sets the creation time (UTC) of the current folder.
    /// </summary>
    public DateTime CreationTimeUtc
    {
        get => _actual.CreationTimeUtc;
        set => _actual.CreationTimeUtc = value;
    }

    /// <summary>
    /// Gets or sets the time the current folder was last accessed.
    /// </summary>
    public DateTime LastAccessTime
    {
        get => _actual.LastAccessTime;
        set => _actual.LastAccessTime = value;
    }

    /// <summary>
    /// Gets or sets the time (UTC) the current folder was last accessed.
    /// </summary>
    public DateTime LastAccessTimeUtc
    {
        get => _actual.LastAccessTimeUtc;
        set => _actual.LastAccessTimeUtc = value;
    }

    /// <summary>
    /// Creates the folder.
    /// </summary>
    public void Create()
    {
        _actual.Create();
    }

    /// <summary>
    /// Returns all files in the folder according to the search pattern and option.
    /// </summary>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All files in the folder according to the search pattern and option</returns>
    public IEnumerable<IFileInfo> GetFiles(string searchPattern
        , SIO.SearchOption searchOption)
        => _actual.GetFiles(searchPattern, searchOption)
            .Select(f => new FileInfo(this.IOServices, f));

    /// <summary>
    /// Returns all folders in the folder according to the search pattern and option.
    /// </summary>
    /// <param name="searchPattern">The search pattern</param>
    /// <param name="searchOption">The search option</param>
    /// <returns>All folders in the folder according to the search pattern and option</returns>
    public IEnumerable<IFolderInfo> GetFolders(string searchPattern
        , SIO.SearchOption searchOption = SIO.SearchOption.TopDirectoryOnly)
        => _actual.GetDirectories(searchPattern, searchOption)
            .Select(f => new FolderInfo(this.IOServices, f));

    #region IEquatable<IFolderInfo>

    /// <summary>
    /// Compares this instance with <paramref name="other"/> if they refer to the same folder.
    /// </summary>
    public bool Equals(IFolderInfo other)
    => this.GetEquality(other);

    #endregion

    #endregion

    #region IEquatable<FolderInfo>

    /// <summary>
    /// Compares this instance with <paramref name="other"/> if they refer to the same file.
    /// </summary>
    public bool Equals(FolderInfo other)
        => this.GetEquality(other);

    #endregion

    #region IComparable<IFolderInfo>

    /// <summary>
    /// Compares to this folder path to another's.
    /// </summary>
    /// <param name="other">the other folder</param>
    public int CompareTo(IFolderInfo other)
        => this.GetComparison(other);

    #endregion

    #region IComparable<FolderInfo>

    /// <summary>
    /// Compares to this file path to another's.
    /// </summary>
    /// <param name="other">the other file</param>
    public int CompareTo(FolderInfo other)
        => this.GetComparison(other);

    #endregion

    /// <summary>
    /// Returns a hashcode based on the full path of the folder.
    /// </summary>
    public override int GetHashCode()
        => this.FullName?.GetHashCode() ?? 0;

    /// <summary>
    /// Compares this instance with <paramref name="obj"/> if they refer to the same folder.
    /// </summary>
    public override bool Equals(object obj)
        => this.GetEquality(obj as IFolderInfo);

    /// <summary>
    /// Returns the path as a string.
    /// </summary>
    public override string ToString()
        => _actual.ToString();

    private bool GetEquality(IFolderInfo other)
        => ReferenceEquals(this, other)
            || (other is FolderInfo otherFI
                && ReferenceEquals(_actual, otherFI._actual))
            || (other != null
                && string.Equals(this.FullName ?? string.Empty, other.FullName ?? string.Empty, StringComparison.OrdinalIgnoreCase));

    private int GetComparison(IFolderInfo other)
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