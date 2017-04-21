using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IPath
    {
        String Combine(params String[] parts);

        String GetTempPath();

        Char[] GetInvalidFileNameChars();
    }
}