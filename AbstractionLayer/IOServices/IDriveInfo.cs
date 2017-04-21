using System;

namespace DoenaSoft.AbstractionLayer.IOServices
{
    public interface IDriveInfo
    {
        Boolean IsReady { get; }

        String Label { get; }

        String RootDirectory { get; }

        String DriveLetter { get; }

        Int64 AvailableFreeSpace { get; }
    }
}