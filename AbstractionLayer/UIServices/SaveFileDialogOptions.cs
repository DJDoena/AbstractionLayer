namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

    /// <summary>
    /// Contains the options to configure a "save file" dialog.
    /// </summary>
    public sealed class SaveFileDialogOptions : FileDialogOptions
    {
        /// <summary>
        /// Determines if a file extension shall be added if none is provided by the user.
        /// </summary>
        public bool? AddExtension { get; set; }

        /// <summary>
        /// Determines the default file extension.
        /// </summary>
        public string DefaultExt { get; set; }

        /// <summary>
        /// Determines if the user should be asked to overwrite the file if it already exists.
        /// </summary>
        public bool? OverwritePrompt { get; set; }

        /// <summary>
        /// Determines if the file name shall be validated.
        /// </summary>
        public bool? ValidateName { get; set; }
    }
}