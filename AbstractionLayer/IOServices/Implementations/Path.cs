namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;

    internal sealed class Path : IPath
    {
        public String Combine(params String[] parts)
        {
            String path = null;

            if ((parts != null) && (parts.Length > 0))
            {
                path = parts[0];

                for (Int32 i = 1; i < parts.Length; i++)
                {
                    path = System.IO.Path.Combine(path, parts[i]);
                }
            }

            return (path);
        }

        public String GetTempPath()
            => (System.IO.Path.GetTempPath());

        public Char[] GetInvalidFileNameChars()
            => (System.IO.Path.GetInvalidFileNameChars());
    }
}