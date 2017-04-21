using System;

namespace DoenaSoft.AbstractionLayer.UIServices
{
    public interface IClipboardServices
    {
        Boolean ContainsText { get; }

        String GetText();

        Boolean SetText(String text);
    }
}
