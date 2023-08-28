namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Interface to seperate UI concerns from an concrete implementation.
    /// </summary>
    public interface IUIServices
    {
        /// <summary>
        /// Shows a MessageBox.
        /// </summary>
        /// <param name="text">The main text</param>
        /// <param name="caption">The MessageBox title</param>
        /// <param name="buttons">The buttons to be shown</param>
        /// <param name="icon">The MessageBox icon</param>
        /// <returns>Which button was pressed</returns>
        Result ShowMessageBox(string text, string caption, Buttons buttons, Icon icon);

        /// <summary>
        /// Opens a "open file" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="fileName">The selected file name</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        bool ShowOpenFileDialog(OpenFileDialogOptions options, out string fileName);

        /// <summary>
        /// Opens a "open file" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="fileNames">The selected file names</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        bool ShowOpenFileDialog(OpenFileDialogOptions options, out string[] fileNames);

        /// <summary>
        /// Opens a "save file" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="fileName">The selected file name</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        bool ShowSaveFileDialog(SaveFileDialogOptions options, out string fileName);

        /// <summary>
        /// Shows a "select folder" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="folder">The selected folder</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        bool ShowFolderBrowserDialog(FolderBrowserDialogOptions options, out string folder);
    }
}