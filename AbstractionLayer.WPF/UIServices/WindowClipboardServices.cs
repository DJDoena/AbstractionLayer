using System.Windows;

namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Standard implementation of <see cref="IClipboardServices"/> for <see cref="Clipboard"/>.
    /// </summary>
    public sealed class WindowClipboardServices : IClipboardServices
    {
        /// <summary>
        /// Returns whether the clipboard contains text.
        /// </summary>
        public bool ContainsText => Clipboard.ContainsText(TextDataFormat.Text);

        /// <summary>
        /// Returns the text from the clipboard.
        /// </summary>
        /// <returns>the text from the clipboard</returns>
        public string GetText() => Clipboard.GetText();

        /// <summary>
        /// Sets the text to the clipboard.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>If the setting succeeded</returns>
        public bool SetText(string text)
        {
            try
            {
                Clipboard.SetText(text);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}