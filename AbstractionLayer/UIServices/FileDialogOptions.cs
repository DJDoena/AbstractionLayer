namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

    public abstract class FileDialogOptions
    {
        public String InitialFolder { get; set; }

        public String Filter { get; set; }

        public Nullable<Boolean> RestoreFolder { get; set; }

        public String Title { get; set; }

        public String FileName { get; set; }
    }
}