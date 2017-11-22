namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

    public class OpenFileDialogOptions : FileDialogOptions
    {
        public Nullable<Boolean> CheckFileExists { get; set; }
    }
}