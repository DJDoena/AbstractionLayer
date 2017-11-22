namespace DoenaSoft.AbstractionLayer.UIServices.Implementations
{
    using System;
    using System.Windows.Forms;

    public sealed class FormClipboardServices : IClipboardServices
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