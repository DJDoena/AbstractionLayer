using System.Diagnostics;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IDriveInfo"/> for <see cref="SIO.DriveInfo"/>.
/// </summary>
[DebuggerDisplay("DriveLetter={DriveLetter}, Label={VolumeLabel}")]
internal sealed class DriveInfo : IOServiceItem, IDriveInfo
{
    private readonly SIO.DriveInfo _actual;

    public DriveInfo(IIOServices ioServices
        , SIO.DriveInfo driveInfo)
        : base(ioServices)
    {
        _actual = driveInfo;
    }

    #region  IDriveInfo

    public bool IsReady
        => _actual.IsReady;

    public string DriveLetter
        => this.RootFolderName.Substring(0, 2);

    public string DriveLabel
    {
        get
        {
            var driveLabel = this.DriveLetter;

            if (this.CanReadLabel())
            {
                driveLabel += this.TryReadLabel();
            }

            return driveLabel;
        }
    }

    public string VolumeLabel
    {
        get
        {
            var volumeLabel = this.CanReadLabel()
                ? this.TryReadVolumeLabel()
                : string.Empty;

            return volumeLabel;
        }
    }

    public string RootFolderName
        => _actual.RootDirectory.FullName;

    public IFolderInfo RootFolder
        => new FolderInfo(this.IOServices, _actual.RootDirectory);

    public long AvailableFreeSpace
        => _actual.AvailableFreeSpace;

    public long TotalSpace
        => _actual.TotalSize;

    public string Name
        => _actual.Name;

    public SIO.DriveType DriveType
        => _actual.DriveType;

    public string DriveFormat
    {
        get
        {
            try
            {
                return _actual.DriveFormat;
            }
            catch (SIO.IOException)
            {
                return string.Empty;
            }
        }
    }

    public long TotalFreeSpace
        => _actual.TotalFreeSpace;

    #endregion

    private bool CanReadLabel()
        => _actual.IsReady && string.IsNullOrEmpty(_actual.VolumeLabel) == false;

    private string TryReadLabel()
    {
        var volumeLabel = this.TryReadVolumeLabel();

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
        catch (SIO.IOException)
        {
            return string.Empty;
        }
    }
}