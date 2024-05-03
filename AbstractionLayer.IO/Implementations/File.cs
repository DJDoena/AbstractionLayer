using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IFile"/> for <see cref="SIO.File"/>.
/// </summary>
internal sealed class File : IFile
{
    private readonly ILogger _logger;

    /// <summary>
    /// The master interface.
    /// </summary>
    public IIOServices IOServices { get; }

    public File(IIOServices ioServices
        , ILogger logger)
    {
        this.IOServices = ioServices;

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

    #endregion
}