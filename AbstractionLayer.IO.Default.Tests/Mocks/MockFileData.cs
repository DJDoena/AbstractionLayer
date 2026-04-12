using System;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IO.Default.Tests.Mocks;

internal sealed class MockFileData
{
    public string Content { get; set; }
    public SIO.FileAttributes Attributes { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime LastWriteTime { get; set; }
    public DateTime LastAccessTime { get; set; }

    public MockFileData(string content = "")
    {
        this.Content = content ?? "";
        this.Attributes = SIO.FileAttributes.Normal;
        this.CreationTime = DateTime.Now;
        this.LastWriteTime = DateTime.Now;
        this.LastAccessTime = DateTime.Now;
    }
}