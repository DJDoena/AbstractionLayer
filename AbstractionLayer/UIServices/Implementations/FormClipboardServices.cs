namespace DoenaSoft.AbstractionLayer.UIServices.Implementations
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Standard implementation of <see cref="IClipboardServices"/> for <see cref="Clipboard"/>.
    /// </summary>
    public sealed class FormClipboardServices : IClipboardServices
    {
        /// <summary>
        /// Returns whether the clipboard contains text.
        /// </summary>
        public Boolean ContainsText
            => Clipboard.ContainsText(TextDataFormat.Text);

        /// <summary>
        /// Returns the text from the clipboard.
        /// </summary>
        /// <returns>the text from the clipboard</returns>
        public String GetText()
            => Clipboard.GetText();

        /// <summary>
        /// Sets the text to the clipboard.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>If the setting succeeded</returns>
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