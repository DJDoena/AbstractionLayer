using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IFile
    {
        Boolean Exists(String path);

        void Copy(String sourceFileName
            , String destFileName
            , Boolean overwrite = true);

        void Move(String oldFileName
            , String newFileName
            , Boolean overwrite = true);

        void Delete(String path);

        void SetAttributes(String fullName
            , System.IO.FileAttributes fileAttributes);
    }
}