namespace DoenaSoft.AbstractionLayer.IOServices
{
    using System;

    public interface IDriveInfo
    {
        Boolean IsReady { get; }

        String Label { get; }

        String RootFolder { get; }

        String DriveLetter { get; }

        Int64 AvailableFreeSpace { get; }
    }
}