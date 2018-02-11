namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface to seperate path concerns from an concrete implementation.
    /// </summary>
    public interface IPath
    {
        /// <summary>
        /// Combines multiple path segments to each other, ensuring that they are properly concatenated.
        /// </summary>
        /// <param name="parts">the path parts</param>
        /// <returns>The combined path</returns>
        String Combine(params String[] parts);

        /// <summary>
        /// Returns the path of the temp folder.
        /// </summary>
        /// <returns>The path of the temp folder</returns>
        String GetTempPath();

        /// <summary>
        /// Returns all characters not allowed in a file name.
        /// </summary>
        /// <returns>All characters not allowed in a file name</returns>
        IEnumerable<Char> GetInvalidFileNameChars();

        /// <summary>
        /// Returns all characters not allowed in a path.
        /// </summary>
        /// <returns>All characters not allowed in a path</returns>
        IEnumerable<Char> GetInvalidPathChars();
    }
}