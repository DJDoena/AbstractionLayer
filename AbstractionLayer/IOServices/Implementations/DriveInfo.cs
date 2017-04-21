using System;
using System.Diagnostics;
using System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    [DebuggerDisplay("DriveLetter={DriveLetter}, Label={Label}")]
    internal sealed class DriveInfo : IDriveInfo
    {
        private readonly System.IO.DriveInfo Actual;

        public DriveInfo(System.IO.DriveInfo driveInfo)
        {
            Actual = driveInfo;
        }

        #region  IDriveInfo

        public Boolean IsReady
            => (Actual.IsReady);

        public String DriveLetter
            => (RootDirectory.Substring(0, 2));

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

        public String RootDirectory
            => (Actual.RootDirectory.FullName);

        public Int64 AvailableFreeSpace
            => (Actual.AvailableFreeSpace);

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