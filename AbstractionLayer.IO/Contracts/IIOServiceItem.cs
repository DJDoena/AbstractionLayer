namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Base interface for all types that provide access to the master IOServices interface.
/// </summary>
public interface IIOServiceItem
{
    /// <summary>
    /// The master interface.
    /// </summary>
    IIOServices IOServices { get; }
}