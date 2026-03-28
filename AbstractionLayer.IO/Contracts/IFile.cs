using System;
using System.Collections.Generic;
using System.Text;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate file concerns from an concrete implementation.
/// </summary>
public interface IFile : IIOServiceItem
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
    SIO.Stream Create(string fileName);

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
    void SetAttributes(string fileName, SIO.FileAttributes fileAttributes);

    /// <summary>
    /// Creates or opens a file for writing UTF-8 encoded text. If the file already exists, its contents are overwritten.
    /// </summary>
    /// <param name="path">the file to be opened for writing</param>
    /// <returns>a <see cref="SIO.StreamWriter "/> that writes to the specified file using UTF-8 encoding</returns>
    SIO.StreamWriter CreateText(string path);

    /// <summary>
    /// Appends lines to a file, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
    /// </summary>
    void AppendAllLines(string path, IEnumerable<string> contents);

    /// <summary>
    /// Appends lines to a file by using a specified encoding, and then closes the file. If the specified file does not exist, this method creates a file, writes the specified lines to the file, and then closes the file.
    /// </summary>
    void AppendAllLines(string path, IEnumerable<string> contents, Encoding encoding);

    /// <summary>
    /// Opens a file, appends the specified string to the file, and then closes the file. If the file does not exist, this method creates a file, writes the specified string to the file, then closes the file.
    /// </summary>
    void AppendAllText(string path, string contents);

    /// <summary>
    /// Appends the specified string to the file using the specified encoding, creating the file if it does not already exist.
    /// </summary>
    void AppendAllText(string path, string contents, Encoding encoding);

    /// <summary>
    /// Creates a StreamWriter that appends UTF-8 encoded text to an existing file, or to a new file if the specified file does not exist.
    /// </summary>
    SIO.StreamWriter AppendText(string path);

    /// <summary>
    /// Gets the FileAttributes of the file on the path.
    /// </summary>
    SIO.FileAttributes GetAttributes(string path);

    /// <summary>
    /// Returns the creation date and time of the specified file or directory.
    /// </summary>
    DateTime GetCreationTime(string path);

    /// <summary>
    /// Returns the creation date and time, in coordinated universal time (UTC), of the specified file or directory.
    /// </summary>
    DateTime GetCreationTimeUtc(string path);

    /// <summary>
    /// Returns the date and time the specified file or directory was last accessed.
    /// </summary>
    DateTime GetLastAccessTime(string path);

    /// <summary>
    /// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last accessed.
    /// </summary>
    DateTime GetLastAccessTimeUtc(string path);

    /// <summary>
    /// Returns the date and time the specified file or directory was last written to.
    /// </summary>
    DateTime GetLastWriteTime(string path);

    /// <summary>
    /// Returns the date and time, in coordinated universal time (UTC), that the specified file or directory was last written to.
    /// </summary>
    DateTime GetLastWriteTimeUtc(string path);

    /// <summary>
    /// Opens a FileStream on the specified path with read/write access with no sharing.
    /// </summary>
    SIO.Stream Open(string path, SIO.FileMode mode);

    /// <summary>
    /// Opens a FileStream on the specified path, with the specified mode and access with no sharing.
    /// </summary>
    SIO.Stream Open(string path, SIO.FileMode mode, SIO.FileAccess access);

    /// <summary>
    /// Opens a FileStream on the specified path, having the specified mode with read, write, or read/write access and the specified sharing option.
    /// </summary>
    SIO.Stream Open(string path, SIO.FileMode mode, SIO.FileAccess access, SIO.FileShare share);

    /// <summary>
    /// Opens an existing file for reading.
    /// </summary>
    SIO.Stream OpenRead(string path);

    /// <summary>
    /// Opens an existing UTF-8 encoded text file for reading.
    /// </summary>
    SIO.StreamReader OpenText(string path);

    /// <summary>
    /// Opens an existing file or creates a new file for writing.
    /// </summary>
    SIO.Stream OpenWrite(string path);

    /// <summary>
    /// Opens a binary file, reads the contents of the file into a byte array, and then closes the file.
    /// </summary>
    byte[] ReadAllBytes(string path);

    /// <summary>
    /// Opens a text file, reads all lines of the file, and then closes the file.
    /// </summary>
    string[] ReadAllLines(string path);

    /// <summary>
    /// Opens a file, reads all lines of the file with the specified encoding, and then closes the file.
    /// </summary>
    string[] ReadAllLines(string path, Encoding encoding);

    /// <summary>
    /// Opens a text file, reads all the text in the file, and then closes the file.
    /// </summary>
    string ReadAllText(string path);

    /// <summary>
    /// Opens a file, reads all text in the file with the specified encoding, and then closes the file.
    /// </summary>
    string ReadAllText(string path, Encoding encoding);

    /// <summary>
    /// Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file.
    /// </summary>
    void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName);

    /// <summary>
    /// Replaces the contents of a specified file with the contents of another file, deleting the original file, and creating a backup of the replaced file and optionally ignores merge errors.
    /// </summary>
    void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);

    /// <summary>
    /// Sets the date and time the file was created.
    /// </summary>
    void SetCreationTime(string path, DateTime creationTime);

    /// <summary>
    /// Sets the date and time, in coordinated universal time (UTC), that the file was created.
    /// </summary>
    void SetCreationTimeUtc(string path, DateTime creationTimeUtc);

    /// <summary>
    /// Sets the date and time the specified file was last accessed.
    /// </summary>
    void SetLastAccessTime(string path, DateTime lastAccessTime);

    /// <summary>
    /// Sets the date and time, in coordinated universal time (UTC), that the specified file was last accessed.
    /// </summary>
    void SetLastAccessTimeUtc(string path, DateTime lastAccessTimeUtc);

    /// <summary>
    /// Sets the date and time that the specified file was last written to.
    /// </summary>
    void SetLastWriteTime(string path, DateTime lastWriteTime);

    /// <summary>
    /// Sets the date and time, in coordinated universal time (UTC), that the specified file was last written to.
    /// </summary>
    void SetLastWriteTimeUtc(string path, DateTime lastWriteTimeUtc);

    /// <summary>
    /// Creates a new file, writes the specified byte array to the file, and then closes the file. If the target file already exists, it is overwritten.
    /// </summary>
    void WriteAllBytes(string path, byte[] bytes);

    /// <summary>
    /// Creates a new file, writes a collection of strings to the file, and then closes the file.
    /// </summary>
    void WriteAllLines(string path, IEnumerable<string> contents);

    /// <summary>
    /// Creates a new file by using the specified encoding, writes a collection of strings to the file, and then closes the file.
    /// </summary>
    void WriteAllLines(string path, IEnumerable<string> contents, Encoding encoding);

    /// <summary>
    /// Creates a new file, writes the specified string array to the file, and then closes the file.
    /// </summary>
    void WriteAllLines(string path, string[] contents);

    /// <summary>
    /// Creates a new file, writes the specified string array to the file by using the specified encoding, and then closes the file.
    /// </summary>
    void WriteAllLines(string path, string[] contents, Encoding encoding);

    /// <summary>
    /// Creates a new file, writes the specified string to the file, and then closes the file. If the target file already exists, it is overwritten.
    /// </summary>
    void WriteAllText(string path, string contents);

    /// <summary>
    /// Creates a new file, writes the specified string to the file using the specified encoding, and then closes the file. If the target file already exists, it is overwritten.
    /// </summary>
    void WriteAllText(string path, string contents, Encoding encoding);
}