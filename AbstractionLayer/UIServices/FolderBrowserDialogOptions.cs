﻿namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

    public sealed class FolderBrowserDialogOptions
    {
        public String Description { get; set; }

        public String SelectedPath { get; set; }

        public Nullable<Environment.SpecialFolder> RootFolder { get; set; }

        public Nullable<Boolean> ShowNewFolderButton { get; set; }
    }
}