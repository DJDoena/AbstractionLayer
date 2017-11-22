namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

    public interface IClipboardServices
    {
        Boolean ContainsText { get; }

        String GetText();

        Boolean SetText(String text);
    }
}