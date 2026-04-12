using DoenaSoft.AbstractionLayer.IOServices;
using System;
using System.Collections.Generic;
using System.Text;
using SIO = System.IO;
namespace DoenaSoft.AbstractionLayer.IO.Default.Tests.Mocks;

internal sealed class MockFile : IFile
{
    private readonly MockIOServices _ioServices;

    public MockFile(MockIOServices ioServices)
    {
        _ioServices = ioServices;
    }

    public IIOServices IOServices => _ioServices;

    public bool Exists(string fileName)
    {
        var fullPath = _ioServices.Path.GetFullPath(fileName);
        return _ioServices.Files.ContainsKey(fullPath);
    }

    public void SetAttributes(string fileName, SIO.FileAttributes fileAttributes)
    {
        var fullPath = _ioServices.Path.GetFullPath(fileName);
        if (_ioServices.Files.TryGetValue(fullPath, out var fileData))
        {
            fileData.Attributes = fileAttributes;
        }
        else
        {
            throw new SIO.FileNotFoundException("File not found", fileName);
        }
    }

    public void Copy(string sourceFileName, string destinationFileName, bool overwrite = true) => throw new NotImplementedException();
    public void Move(string oldFileName, string newFileName, bool overwrite = true) => throw new NotImplementedException();
    public SIO.Stream Create(string fileName) => throw new NotImplementedException();
    public void Delete(string fileName) => throw new NotImplementedException();
    public SIO.StreamWriter CreateText(string path) => throw new NotImplementedException();
    public void AppendAllLines(string path, IEnumerable<string> contents) => throw new NotImplementedException();
    public void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding) => throw new NotImplementedException();
    public void AppendAllText(string path, string contents) => throw new NotImplementedException();
    public void AppendAllText(string path, string contents, Encoding encoding) => throw new NotImplementedException();
    public SIO.StreamWriter AppendText(string path) => throw new NotImplementedException();
    public SIO.FileAttributes GetAttributes(string path) => throw new NotImplementedException();
    public DateTime GetCreationTime(string path) => throw new NotImplementedException();
    public DateTime GetCreationTimeUtc(string path) => throw new NotImplementedException();
    public DateTime GetLastAccessTime(string path) => throw new NotImplementedException();
    public DateTime GetLastAccessTimeUtc(string path) => throw new NotImplementedException();
    public DateTime GetLastWriteTime(string path) => throw new NotImplementedException();
    public DateTime GetLastWriteTimeUtc(string path) => throw new NotImplementedException();
    public SIO.Stream Open(string path, SIO.FileMode mode) => throw new NotImplementedException();
    public SIO.Stream Open(string path, SIO.FileMode mode, SIO.FileAccess access) => throw new NotImplementedException();
    public SIO.Stream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share) => throw new NotImplementedException();
    public SIO.Stream OpenRead(string path) => throw new NotImplementedException();
    public SIO.StreamReader OpenText(string path) => throw new NotImplementedException();
    public SIO.Stream OpenWrite(string path) => throw new NotImplementedException();
    public byte[] ReadAllBytes(string path) => throw new NotImplementedException();
    public string[] ReadAllLines(string path) => throw new NotImplementedException();
    public string[] ReadAllLines(string path, Encoding encoding) => throw new NotImplementedException();
    public string ReadAllText(string path) => throw new NotImplementedException();
    public string ReadAllText(string path, Encoding encoding) => throw new NotImplementedException();
    public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName) => throw new NotImplementedException();
    public void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors) => throw new NotImplementedException();
    public void SetCreationTime(string path, DateTime creationTime) => throw new NotImplementedException();
    public void SetCreationTimeUtc(string path, DateTime creationTimeUtc) => throw new NotImplementedException();
    public void SetLastAccessTime(string path, DateTime lastAccessTime) => throw new NotImplementedException();
    public void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc) => throw new NotImplementedException();
    public void SetLastWriteTime(string path, DateTime lastWriteTime) => throw new NotImplementedException();
    public void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc) => throw new NotImplementedException();
    public void WriteAllBytes(string path, byte[] bytes) => throw new NotImplementedException();
    public void WriteAllLines(string path, IEnumerable<string> contents) => throw new NotImplementedException();
    public void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding) => throw new NotImplementedException();
    public void WriteAllLines(string path, string[] contents) => throw new NotImplementedException();
    public void WriteAllLines(string path, string[] contents, Encoding encoding) => throw new NotImplementedException();
    public void WriteAllText(string path, string contents) => throw new NotImplementedException();
    public void WriteAllText(string path, string contents, Encoding encoding) => throw new NotImplementedException();
}