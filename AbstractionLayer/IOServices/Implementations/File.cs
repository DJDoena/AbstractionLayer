using System;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    internal sealed class File : IFile
    {
        private readonly ILogger Logger;

        public File(ILogger logger)
        {
            Logger = logger;
        }

        public File()
            : this(null)
        { }

        #region IFile

        public Boolean Exists(String path)
            => (System.IO.File.Exists(path));

        public void Copy(String sourceFileName
            , String destFileName
            , Boolean overwrite)
        {
            Logger?.WriteLine($"Copy file \"{sourceFileName}\"", true);
            Logger?.WriteLine($"to        \"{destFileName}\"");

            System.IO.File.Copy(sourceFileName, destFileName, overwrite);
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

        public void SetAttributes(String fullName
            , System.IO.FileAttributes fileAttributes)
        {
            System.IO.File.SetAttributes(fullName, fileAttributes);
        }

        #endregion
    }
}