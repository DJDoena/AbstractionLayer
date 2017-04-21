using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IShortcut
    {
        String Description { set; }
        
        String TargetPath { set; }
        
        String WorkingDirectory { set; }

        void Save();
    }
}
