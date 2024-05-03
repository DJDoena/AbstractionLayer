using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IFile"/> for <see cref="SIO.File"/>.
/// </summary>
internal sealed class File : IFile
{
    private readonly ILogger _logger;

    public File(ILogger logger)
    {
        _logger = logger;
    }

    public File() : this(null)
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

    #endregion
}