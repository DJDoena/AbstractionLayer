namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    public interface IShortcut
    {
        String Description { set; }

        String TargetPath { set; }

        String WorkingFolder { set; }

        void Save();
    }
}