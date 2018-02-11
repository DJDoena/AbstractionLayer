namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    /// <summary>
    /// Interface to seperate Shortcut concerns from an concrete implementation.
    /// </summary>
    public interface IShortcut
    {
        /// <summary>
        /// The description.
        /// </summary>
        String Description { set; }

        /// <summary>
        /// The target path.
        /// </summary>
        String TargetPath { set; }

        /// <summary>
        /// The working folder.
        /// </summary>
        String WorkingFolder { set; }

        /// <summary>
        /// Saves the Shortcut.
        /// </summary>
        void Save();
    }
}