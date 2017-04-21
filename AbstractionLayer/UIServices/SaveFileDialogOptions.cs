using System;

namespace DoenaSoft.AbstractionLayer.UIServices
{
    public sealed class SaveFileDialogOptions : FileDialogOptions
    {
        public Nullable<Boolean> AddExtension { get; set; }

        public String DefaultExt { get; set; }

        public Nullable<Boolean> OverwritePrompt { get; set; }

        public Nullable<Boolean> ValidateNames { get; set; }
    }
}