namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    /// <summary>
    /// Interface to seperate file concerns from an concrete implementation.
    /// </summary>
    public interface IFile
    {
        /// <summary>
        /// Returns whether a file exists.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <returns></returns>
        Boolean Exists(String fileName);

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file</param>
        /// <param name="destinationFileName">Name of the target file</param>
        /// <param name="overwrite">Whether an existing target file shall be overwritten</param>
        void Copy(String sourceFileName
            , String destinationFileName
            , Boolean overwrite = true);

        /// <summary>
        /// Moves a file.
        /// </summary>
        /// <param name="oldFileName">Existing name of the file</param>
        /// <param name="newFileName">New name of the file</param>
        /// <param name="overwrite">Whether an existing file with the new name shall be overwritten</param>
        void Move(String oldFileName
            , String newFileName
            , Boolean overwrite = true);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="fileName">The file name</param>
        void Delete(String fileName);

        /// <summary>
        /// Sets attribute flags to a file.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="fileAttributes">File attribute flags</param>
        void SetAttributes(String fileName
            , System.IO.FileAttributes fileAttributes);
    }
}