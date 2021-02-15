namespace DoenaSoft.AbstractionLayer.IOServices.Implementations
{
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// Standard implementation of <see cref="IDriveInfo"/> for <see cref="System.IO.DriveInfo"/>.
    /// </summary>
    [DebuggerDisplay("DriveLetter={DriveLetter}, Label={Label}")]
    internal sealed class DriveInfo : IDriveInfo
    {
        private readonly System.IO.DriveInfo _actual;

        public DriveInfo(System.IO.DriveInfo driveInfo)
        {
            _actual = driveInfo;
        }

        #region  IDriveInfo

        public bool IsReady => _actual.IsReady;

        public string DriveLetter => RootFolder.Substring(0, 2);

        public string DriveLabel
        {
            get
            {
                var driveLabel = DriveLetter;

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
                var volumeLabel = CanReadLabel()
                    ? TryReadVolumeLabel()
                    : string.Empty;

                return volumeLabel;
            }
        }

        public string RootFolder => _actual.RootDirectory.FullName;

        public ulong AvailableFreeSpace => (ulong)(_actual.AvailableFreeSpace);

        #endregion

        private bool CanReadLabel() => _actual.IsReady && (string.IsNullOrEmpty(_actual.VolumeLabel) == false);

        private string TryReadLabel()
        {
            var volumeLabel = TryReadVolumeLabel();

            if (!string.IsNullOrEmpty(volumeLabel))
            {
                return " [" + volumeLabel + "]";
            }
            else
            {
                return string.Empty;
            }
        }

        private string TryReadVolumeLabel()
        {
            try
            {
                var volumeLabel = _actual.VolumeLabel;

                return volumeLabel;
            }
            catch (IOException)
            {
                return string.Empty;
            }
        }
    }
}