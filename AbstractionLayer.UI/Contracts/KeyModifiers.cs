using System;

namespace DoenaSoft.AbstractionLayer.UI.Contracts;

/// <summary>
/// Specifies modifier keys that can accompany a key press.
/// </summary>
[Flags]
public enum KeyModifiers
{
    /// <summary>
    /// No modifier keys are pressed.
    /// </summary>
    None = 0,

    /// <summary>
    /// The ALT key is pressed.
    /// </summary>
    Alt = 1,

    /// <summary>
    /// The SHIFT key is pressed.
    /// </summary>
    Shift = 2,

    /// <summary>
    /// The CONTROL (Ctrl) key is pressed.
    /// </summary>
    Control = 4,
}