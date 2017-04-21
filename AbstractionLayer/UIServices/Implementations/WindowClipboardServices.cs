using System;
using System.Windows;

namespace DoenaSoft.AbstractionLayer.UIServices.Implementations
{
    public sealed class WindowClipboardServices : IClipboardServices
    {
        public Boolean ContainsText
            => (Clipboard.ContainsText(TextDataFormat.Text));

        public String GetText()
            => (Clipboard.GetText());

        public Boolean SetText(String text)
        {
            try
            {
                Clipboard.SetText(text);

                return (true);
            }
            catch
            {
                return (false);
            }
        }
    }
}