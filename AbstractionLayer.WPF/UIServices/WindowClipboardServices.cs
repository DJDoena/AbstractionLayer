namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Standard implementation of <see cref="IClipboardServices"/> for <see cref="System.Windows.Clipboard"/>.
    /// </summary>
    public sealed class WindowClipboardServices : IClipboardServices
    {
        /// <summary>
        /// Returns whether the clipboard contains text.
        /// </summary>
        public bool ContainsText => System.Windows.Clipboard.ContainsText(System.Windows.TextDataFormat.Text);

        /// <summary>
        /// Returns the text from the clipboard.
        /// </summary>
        /// <returns>the text from the clipboard</returns>
        public string GetText() => System.Windows.Clipboard.GetText();

        /// <summary>
        /// Sets the text to the clipboard.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>If the setting succeeded</returns>
        public bool SetText(string text)
        {
            try
            {
                System.Windows.Clipboard.SetText(text);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Clears the Clipboard and then attempts to place data on it the specified number
        /// of times and with the specified delay between attempts, optionally leaving the
        /// data on the Clipboard after the application exits.
        /// </summary>
        /// <param name="data">The data to place on the Clipboard.</param>
        /// <param name="copy">true if you want data to remain on the Clipboard after this application exits; otherwise, false.</param>
        /// <param name="retryTimes">The number of times to attempt placing the data on the Clipboard.</param>
        /// <param name="retryDelay">The number of milliseconds to pause between attempts.</param>
        public void SetDataObject(object data, bool copy, int retryTimes, int retryDelay)
            => System.Windows.Clipboard.SetDataObject(data, copy);
    }
}