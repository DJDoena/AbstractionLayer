namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Standard implementation of <see cref="IPath"/> for <see cref="System.IO.Path"/>.
    /// </summary>
    internal sealed class Path : IPath
    {
        /// <summary>
        /// Combines multiple path segments to each other, ensuring that they are properly concatenated.
        /// </summary>
        /// <param name="parts">the path parts</param>
        /// <returns>The combined path</returns>
        public String Combine(params String[] parts)
        {
            String path = null;

            if ((parts != null) && (parts.Length > 0))
            {
                path = parts[0];

                for (Int32 i = 1; i < parts.Length; i++)
                {
                    path = System.IO.Path.Combine(path, parts[i]);
                }
            }

            return (path);
        }

        /// <summary>
        /// Returns the path of the temp folder.
        /// </summary>
        /// <returns>The path of the temp folder</returns>
        public String GetTempPath()
            => System.IO.Path.GetTempPath();

        /// <summary>
        /// Returns all characters not allowed in a file name.
        /// </summary>
        /// <returns>All characters not allowed in a file name</returns>
        public IEnumerable<Char> GetInvalidFileNameChars()
            => System.IO.Path.GetInvalidFileNameChars();

        /// <summary>
        /// Returns all characters not allowed in a path.
        /// </summary>
        /// <returns>All characters not allowed in a path</returns>
        public IEnumerable<Char> GetInvalidPathChars()
            => System.IO.Path.GetInvalidPathChars();
    }
}