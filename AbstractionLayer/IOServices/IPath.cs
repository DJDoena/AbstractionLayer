namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    public interface IPath
    {
        String Combine(params String[] parts);

        String GetTempPath();

        Char[] GetInvalidFileNameChars();
    }
}