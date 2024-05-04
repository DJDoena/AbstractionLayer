using System.Diagnostics;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Standard implementation of <see cref="IDriveInfo"/> for <see cref="SIO.DriveInfo"/>.
/// </summary>
[DebuggerDisplay("DriveLetter={DriveLetter}, Label={DriveLabel}")]
internal sealed class DriveInfo : IDriveInfo
{
    private readonly SIO.DriveInfo _actual;

    /// <summary>
    /// The master interface.
    /// </summary>
    public IIOServices IOServices { get; }

    public DriveInfo(IIOServices ioServices
        , SIO.DriveInfo driveInfo)
    {
        this.IOServices = ioServices;

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

    public ulong AvailableFreeSpace
        => (ulong)_actual.AvailableFreeSpace;

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