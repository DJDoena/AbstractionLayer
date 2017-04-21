using System;

namespace DoenaSoft.AbstractionLayer.UIServices
{
    public abstract class FileDialogOptions
    {
        public String InitialDirectory { get; set; }

        public String Filter { get; set; }

        public Nullable<Boolean> RestoreDirectory { get; set; }

        public String Title { get; set; }

        public String FileName { get; set; }
    }
}