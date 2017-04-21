using System;

namespace DoenaSoft.AbstractionLayer.UIServices
{
    public class OpenFileDialogOptions : FileDialogOptions
    {
        public Nullable<Boolean> CheckFileExists { get; set; }
    }
}