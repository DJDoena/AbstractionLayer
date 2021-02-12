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

        public string DriveLabel
        {
            get
            {
                string driveLabel = DriveLetter;

                if (CanReadLabel())
                {
                    driveLabel += TryReadLabel();
                }

                return driveLabel;
            }
        }

        public string VolumeLabel
        {
            get
            {
                string volumeLabel;
                if (CanReadLabel())
                {
                    volumeLabel = TryReadVolumeLabel();
                }
                else
                {
                    volumeLabel = string.Empty;
                }

                return volumeLabel;
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
            string volumeLabel = TryReadVolumeLabel();

            if (!string.IsNullOrEmpty(volumeLabel))
            {
                return " [" + volumeLabel + "]";
            }
            else
            {
                return string.Empty;
            }
        }

        private String TryReadVolumeLabel()
        {
            try
            {
                string volumeLabel = Actual.VolumeLabel;

                return volumeLabel;
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }
    }
}