namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate Shortcut concerns from an concrete implementation.
/// </summary>
public interface IShortcut
{
    /// <summary>
    /// The arguments passed to the target when the shortcut is executed.
    /// </summary>
    string Arguments { get; set; }

    /// <summary>
    /// The description.
    /// </summary>
    string Description { get; set; }

    /// <summary>
    /// The full path to the shortcut file.
    /// </summary>
    string FullName { get; }

    /// <summary>
    /// The hotkey (keyboard shortcut) assigned to the shortcut.
    /// </summary>
    string Hotkey { get; set; }

    /// <summary>
    /// The location of the icon for the shortcut.
    /// </summary>
    string IconLocation { get; set; }

    /// <summary>
    /// The target path.
    /// </summary>
    string TargetPath { get; set; }

    /// <summary>
    /// The window style for the application when it starts (1 = Normal, 3 = Maximized, 7 = Minimized).
    /// </summary>
    int WindowStyle { get; set; }

    /// <summary>
    /// The working folder.
    /// </summary>
    string WorkingFolder { get; set; }

    /// <summary>
    /// Saves the Shortcut.
    /// </summary>
    void Save();

    /// <summary>
    /// Loads the Shortcut.
    /// </summary>
    void Load();
}