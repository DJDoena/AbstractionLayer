namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Standard implementation of <see cref="IDriveInfo"/> for <see cref="System.IO.DriveInfo"/>.
    /// </summary>
    [DebuggerDisplay("DriveLetter={DriveLetter}, Label={Label}")]
    internal sealed class DriveInfo : IDriveInfo
    {
        private System.IO.DriveInfo Actual { get; }

        public DriveInfo(System.IO.DriveInfo driveInfo)
        {
            Actual = driveInfo;
        }

        #region  IDriveInfo

        public Boolean IsReady
            => Actual.IsReady;

        public String DriveLetter
            => RootFolder.Substring(0, 2);

        public String Label
        {
            get
            {
                String driveLabel = DriveLetter;

                if (CanReadLabel())
                {
                    driveLabel += TryReadLabel();
                }

                return (driveLabel);
            }
        }

        public String RootFolder
            => Actual.RootDirectory.FullName;

        public UInt64 AvailableFreeSpace
            => (UInt64)(Actual.AvailableFreeSpace);

        #endregion

        private Boolean CanReadLabel()
            => ((Actual.IsReady) && (String.IsNullOrEmpty(Actual.VolumeLabel) == false));

        private String TryReadLabel()
        {
            try
            {
                String label = " [" + Actual.VolumeLabel + "]";

                return (label);
            }
            catch (IOException)
            {
                return (String.Empty);
            }
        }
    }
}