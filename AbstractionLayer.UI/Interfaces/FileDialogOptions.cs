namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Base class for file dialog options.
    /// </summary>
    public abstract class FileDialogOptions
    {
        /// <summary>
        /// Determines the initial folder in which the file dialog will open-
        /// </summary>
        public string InitialFolder { get; set; }

        /// <summary>
        /// Determines the file filter.
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// Determines whether the file dialog opens in the same folder as during last usage.
        /// </summary>
        public bool? RestoreFolder { get; set; }

        /// <summary>
        /// Determines the title of the file dialog.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Determines the file name.
        /// </summary>
        public string FileName { get; set; }
    }
}