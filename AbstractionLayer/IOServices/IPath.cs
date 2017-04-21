using System;
using System.Collections.Generic;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IPath
    {
        String Combine(params String[] parts);

        String GetTempPath();

        Char[] GetInvalidFileNameChars();
    }
}