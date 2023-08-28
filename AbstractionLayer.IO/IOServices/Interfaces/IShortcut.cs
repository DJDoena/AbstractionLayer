namespace DoenaSoft.AbstractionLayer.IOServices
{
    /// <summary>
    /// Interface to seperate Shortcut concerns from an concrete implementation.
    /// </summary>
    public interface IShortcut
    {
        /// <summary>
        /// The description.
        /// </summary>
        string Description { set; }

        /// <summary>
        /// The target path.
        /// </summary>
        string TargetPath { set; }

        /// <summary>
        /// The working folder.
        /// </summary>
        string WorkingFolder { set; }

        /// <summary>
        /// Saves the Shortcut.
        /// </summary>
        void Save();
    }
}