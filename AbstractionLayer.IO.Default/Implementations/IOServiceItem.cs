namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary />
public abstract class IOServiceItem : IIOServiceItem
{
    /// <summary>
    /// The master interface.
    /// </summary>
    public IIOServices IOServices { get; }

    /// <summary />
    protected IOServiceItem(IIOServices ioServices)
    {
        this.IOServices = ioServices;
    }
}