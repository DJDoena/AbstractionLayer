namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Contains the options to configure a "select folder" dialog.
    /// </summary>
    public sealed class FolderBrowserDialogOptions
    {
        /// <summary>
        /// Determines the description text.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Determines the folder path.
        /// </summary>
        public string SelectedPath { get; set; }

        /// <summary>
        /// Determines the root folder of the tree.
        /// </summary>
        public System.Environment.SpecialFolder? RootFolder { get; set; }

        /// <summary>
        /// Determines whether the user has the option to create a new folder.
        /// </summary>
        public bool? ShowNewFolderButton { get; set; }
    }
}