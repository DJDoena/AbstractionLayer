using System;
using System.Collections.Generic;
using System.Text;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IFile"/> for <see cref="SIO.File"/>.
/// </summary>
internal sealed class File : IOServiceItem, IFile
{
    private readonly ILogger _logger;

    public File(IIOServices ioServices
        , ILogger logger)
        : base(ioServices)
    {
        _logger = logger;
    }

    public File(IIOServices ioServices)
        : this(ioServices, null)
    {
    }

    #region IFile

    public bool Exists(string fileName)
        => SIO.File.Exists(fileName);

    public void Copy(string sourceFileName, string destinationFileName, bool overwrite)
    {
        _logger?.WriteLine($"Copy file \"{sourceFileName}\"", true);
        _logger?.WriteLine($"to        \"{destinationFileName}\"");

        SIO.File.Copy(sourceFileName, destinationFileName, overwrite);
    }

    public void Move(string oldFileName, string newFileName, bool overwrite = true)
    {
        _logger?.WriteLine($"Move file \"{oldFileName}\"", true);
        _logger?.WriteLine($"to        \"{newFileName}\"");

        if (overwrite && this.Exists(newFileName))
        {
            this.Delete(newFileName);
        }

        SIO.File.Move(oldFileName, newFileName);
    }

    public SIO.Stream Create(string fileName) => SIO.File.Create(fileName);

    public void Delete(string fileName)
    {
        _logger?.WriteLine($"Delete file \"{fileName}\"");

        SIO.File.Delete(fileName);
    }

    public void SetAttributes(string fileName, SIO.FileAttributes fileAttributes)
        => SIO.File.SetAttributes(fileName, fileAttributes);

    /// <summary>
    /// Creates or opens a file for writing UTF-8 encoded text. If the file already exists, its contents are overwritten.
    /// </summary>
    /// <param name="path">the file to be opened for writing</param>
    /// <returns>a <see cref="SIO.StreamWriter "/> that writes to the specified file using UTF-8 encoding</returns>
    public SIO.StreamWriter CreateText(string path)
        => SIO.File.CreateText(path);

    public void AppendAllLines(string path, IEnumerable<string> contents)
        => SIO.File.AppendAllLines(path, contents);

    public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        => SIO.File.AppendAllLines(path, contents, encoding);

    public void AppendAllText(string path, string contents)
        => SIO.File.AppendAllText(path, contents);

    public void AppendAllText(string path, string contents, Encoding encoding)
        => SIO.File.AppendAllText(path, contents, encoding);

    public SIO.StreamWriter AppendText(string path)
        => SIO.File.AppendText(path);

    public SIO.FileAttributes GetAttributes(string path)
        => SIO.File.GetAttributes(path);

    public DateTime GetCreationTime(string path)
        => SIO.File.GetCreationTime(path);

    public DateTime GetCreationTimeUtc(string path)
        => SIO.File.GetCreationTimeUtc(path);

    public DateTime GetLastAccessTime(string path)
        => SIO.File.GetLastAccessTime(path);

    public DateTime GetLastAccessTimeUtc(string path)
        => SIO.File.GetLastAccessTimeUtc(path);

    public DateTime GetLastWriteTime(string path)
        => SIO.File.GetLastWriteTime(path);

    public DateTime GetLastWriteTimeUtc(string path)
        => SIO.File.GetLastWriteTimeUtc(path);

    public SIO.Stream Open(string path, SIO.FileMode mode)
        => SIO.File.Open(path, mode);

    public SIO.Stream Open(string path, SIO.FileMode mode, SIO.FileAccess access)
        => SIO.File.Open(path, mode, access);

    public SIO.Stream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share)
        => SIO.File.Open(path, mode, access, share);

    public SIO.Stream OpenRead(string path)
        => SIO.File.OpenRead(path);

    public SIO.StreamReader OpenText(string path)
        => SIO.File.OpenText(path);

    public SIO.Stream OpenWrite(string path)
        => SIO.File.OpenWrite(path);

    public byte[] ReadAllBytes(string path)
        => SIO.File.ReadAllBytes(path);

    public string[] ReadAllLines(string path)
        => SIO.File.ReadAllLines(path);

    public string[] ReadAllLines(string path, Encoding encoding)
        => SIO.File.ReadAllLines(path, encoding);

    public string ReadAllText(string path)
        => SIO.File.ReadAllText(path);

    public string ReadAllText(string path, Encoding encoding)
        => SIO.File.ReadAllText(path, encoding);

    public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName)
    {
        _logger?.WriteLine($"Replace file \"{destinationFileName}\"", true);
        _logger?.WriteLine($"with file    \"{sourceFileName}\"");
        _logger?.WriteLine($"backup to    \"{destinationBackupFileName}\"");

        SIO.File.Replace(sourceFileName, destinationFileName, destinationBackupFileName);
    }

    public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors)
    {
        _logger?.WriteLine($"Replace file \"{destinationFileName}\"", true);
        _logger?.WriteLine($"with file    \"{sourceFileName}\"");
        _logger?.WriteLine($"backup to    \"{destinationBackupFileName}\"");

        SIO.File.Replace(sourceFileName, destinationFileName, destinationBackupFileName, ignoreMetadataErrors);
    }

    public void SetCreationTime(string path, DateTime creationTime)
        => SIO.File.SetCreationTime(path, creationTime);

    public void SetCreationTimeUtc(string path, DateTime creationTimeUtc)
        => SIO.File.SetCreationTimeUtc(path, creationTimeUtc);

    public void SetLastAccessTime(string path, DateTime lastAccessTime)
        => SIO.File.SetLastAccessTime(path, lastAccessTime);

    public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc)
        => SIO.File.SetLastAccessTimeUtc(path, lastAccessTimeUtc);

    public void SetLastWriteTime(string path, DateTime lastWriteTime)
        => SIO.File.SetLastWriteTime(path, lastWriteTime);

    public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc)
        => SIO.File.SetLastWriteTimeUtc(path, lastWriteTimeUtc);

    public void WriteAllBytes(string path, byte[] bytes)
        => SIO.File.WriteAllBytes(path, bytes);

    public void WriteAllLines(string path, IEnumerable<string> contents)
        => SIO.File.WriteAllLines(path, contents);

    public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding)
        => SIO.File.WriteAllLines(path, contents, encoding);

    public void WriteAllLines(string path, string[] contents)
        => SIO.File.WriteAllLines(path, contents);

    public void WriteAllLines(string path, string[] contents, Encoding encoding)
        => SIO.File.WriteAllLines(path, contents, encoding);

    public void WriteAllText(string path, string contents)
        => SIO.File.WriteAllText(path, contents);

    public void WriteAllText(string path, string contents, Encoding encoding)
        => SIO.File.WriteAllText(path, contents, encoding);

    #endregion
}