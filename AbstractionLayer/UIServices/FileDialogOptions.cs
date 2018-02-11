namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

    /// <summary>
    /// Base class for file dialog options.
    /// </summary>
    public abstract class FileDialogOptions
    {
        /// <summary>
        /// Determines the initial folder in which the file dialog will open-
        /// </summary>
        public String InitialFolder { get; set; }

        /// <summary>
        /// Determines the file filter.
        /// </summary>
        public String Filter { get; set; }

        /// <summary>
        /// Determines whether the file dialog opens in the same folder as during last usage.
        /// </summary>
        public Nullable<Boolean> RestoreFolder { get; set; }

        /// <summary>
        /// Determines the title of the file dialog.
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Determines the file name.
        /// </summary>
        public String FileName { get; set; }
    }
}