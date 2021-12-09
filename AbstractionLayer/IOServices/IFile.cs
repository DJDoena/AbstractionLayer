namespace DoenaSoft.AbstractionLayer.IOServices
{
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
        bool Exists(string fileName);

        /// <summary>
        /// Copies a file.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file</param>
        /// <param name="destinationFileName">Name of the target file</param>
        /// <param name="overwrite">Whether an existing target file shall be overwritten</param>
        void Copy(string sourceFileName, string destinationFileName, bool overwrite = true);

        /// <summary>
        /// Moves a file.
        /// </summary>
        /// <param name="oldFileName">Existing name of the file</param>
        /// <param name="newFileName">New name of the file</param>
        /// <param name="overwrite">Whether an existing file with the new name shall be overwritten</param>
        void Move(string oldFileName, string newFileName, bool overwrite = true);

        /// <summary>
        /// Creates a file and opens a stream for writing.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns>the stream to write in</returns>
        System.IO.Stream Create(string fileName);

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="fileName">The file name</param>
        void Delete(string fileName);

        /// <summary>
        /// Sets attribute flags to a file.
        /// </summary>
        /// <param name="fileName">The file name</param>
        /// <param name="fileAttributes">File attribute flags</param>
        void SetAttributes(string fileName, System.IO.FileAttributes fileAttributes);
    }
}