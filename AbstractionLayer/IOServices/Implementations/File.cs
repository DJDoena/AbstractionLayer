namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;

    /// <summary>
    /// Standard implementation of <see cref="IFile"/> for <see cref="System.IO.File"/>.
    /// </summary>
    internal sealed class File : IFile
    {
        private ILogger Logger { get; }

        public File(ILogger logger)
        {
            Logger = logger;
        }

        public File()
            : this(null)
        { }

        #region IFile

        public Boolean Exists(String fileName)
            => System.IO.File.Exists(fileName);

        public void Copy(String sourceFileName
            , String destinationFileName
            , Boolean overwrite)
        {
            Logger?.WriteLine($"Copy file \"{sourceFileName}\"", true);
            Logger?.WriteLine($"to        \"{destinationFileName}\"");

            System.IO.File.Copy(sourceFileName, destinationFileName, overwrite);
        }

        public void Move(String oldFileName
            , String newFileName
            , Boolean overwrite = true)
        {
            Logger?.WriteLine($"Move file \"{oldFileName}\"", true);
            Logger?.WriteLine($"to        \"{newFileName}\"");

            if ((overwrite) && (Exists(newFileName)))
            {
                Delete(newFileName);
            }

            System.IO.File.Move(oldFileName, newFileName);
        }

        public void Delete(String fileName)
        {
            Logger?.WriteLine($"Delete file \"{fileName}\"");

            System.IO.File.Delete(fileName);
        }

        public void SetAttributes(String fileName
            , System.IO.FileAttributes fileAttributes)
        {
            System.IO.File.SetAttributes(fileName, fileAttributes);
        }

        #endregion
    }
}