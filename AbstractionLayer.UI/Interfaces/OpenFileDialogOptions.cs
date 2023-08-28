namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Contains the options to configure a "open file" dialog.
    /// </summary>
    public class OpenFileDialogOptions : FileDialogOptions
    {
        /// <summary>
        /// Determines whether it shall be checked if the to-be-opened file exists.
        /// </summary>
        public bool? CheckFileExists { get; set; }
    }
}