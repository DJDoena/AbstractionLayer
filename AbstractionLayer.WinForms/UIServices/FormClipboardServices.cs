namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Standard implementation of <see cref="IClipboardServices"/> for <see cref="System.Windows.Forms.Clipboard"/>.
    /// </summary>
    public sealed class FormClipboardServices : IClipboardServices
    {
        /// <summary>
        /// Returns whether the clipboard contains text.
        /// </summary>
        public bool ContainsText => System.Windows.Forms.Clipboard.ContainsText(System.Windows.Forms.TextDataFormat.Text);

        /// <summary>
        /// Returns the text from the clipboard.
        /// </summary>
        /// <returns>the text from the clipboard</returns>
        public string GetText() => System.Windows.Forms.Clipboard.GetText();

        /// <summary>
        /// Sets the text to the clipboard.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>If the setting succeeded</returns>
        public bool SetText(string text)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(text);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}