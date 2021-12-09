namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    /// <summary>
    /// Standard implementation of <see cref="IFile"/> for <see cref="System.IO.File"/>.
    /// </summary>
    internal sealed class File : IFile
    {
        private readonly ILogger _logger;

        public File(ILogger logger)
        {
            _logger = logger;
        }

        public File() : this(null)
        { }

        #region IFile

        public bool Exists(string fileName) => System.IO.File.Exists(fileName);

        public void Copy(string sourceFileName, string destinationFileName, bool overwrite)
        {
            _logger?.WriteLine($"Copy file \"{sourceFileName}\"", true);
            _logger?.WriteLine($"to        \"{destinationFileName}\"");

            System.IO.File.Copy(sourceFileName, destinationFileName, overwrite);
        }

        public void Move(string oldFileName, string newFileName, bool overwrite = true)
        {
            _logger?.WriteLine($"Move file \"{oldFileName}\"", true);
            _logger?.WriteLine($"to        \"{newFileName}\"");

            if (overwrite && this.Exists(newFileName))
            {
                this.Delete(newFileName);
            }

            System.IO.File.Move(oldFileName, newFileName);
        }

        public void Delete(string fileName)
        {
            _logger?.WriteLine($"Delete file \"{fileName}\"");

            System.IO.File.Delete(fileName);
        }

        public void SetAttributes(string fileName, System.IO.FileAttributes fileAttributes)
        {
            System.IO.File.SetAttributes(fileName, fileAttributes);
        }

        #endregion
    }
}