namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

    /// <summary>
    /// Contains the options to configure a "open file" dialog.
    /// </summary>
    public class OpenFileDialogOptions : FileDialogOptions
    {
        /// <summary>
        /// Determines whether it shall be checked if the to-be-opened file exists.
        /// </summary>
        public Nullable<Boolean> CheckFileExists { get; set; }
    }
}