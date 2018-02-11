namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

    /// <summary>
    /// Contains the options to configure a "select folder" dialog.
    /// </summary>
    public sealed class FolderBrowserDialogOptions
    {
        /// <summary>
        /// Determines the description text.
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Determines the folder path.
        /// </summary>
        public String SelectedPath { get; set; }

        /// <summary>
        /// Determines the root folder of the tree.
        /// </summary>
        public Nullable<Environment.SpecialFolder> RootFolder { get; set; }

        /// <summary>
        /// Determines whether the user has the option to create a new folder.
        /// </summary>
        public Nullable<Boolean> ShowNewFolderButton { get; set; }
    }
}