namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Interface to seperate clipboard concerns from an concrete implementation.
    /// </summary>
    public interface IClipboardServices
    {
        /// <summary>
        /// Returns whether the clipboard contains text.
        /// </summary>
        bool ContainsText { get; }

        /// <summary>
        /// Returns the text from the clipboard.
        /// </summary>
        /// <returns>the text from the clipboard</returns>
        string GetText();

        /// <summary>
        /// Sets the text to the clipboard.
        /// </summary>
        /// <param name="text">The text</param>
        /// <returns>If the setting succeeded</returns>
        bool SetText(string text);

        /// <summary>
        /// Clears the Clipboard and then attempts to place data on it the specified number
        /// of times and with the specified delay between attempts, optionally leaving the
        /// data on the Clipboard after the application exits.
        /// </summary>
        /// <param name="data">The data to place on the Clipboard.</param>
        /// <param name="copy">true if you want data to remain on the Clipboard after this application exits; otherwise, false.</param>
        /// <param name="retryTimes">The number of times to attempt placing the data on the Clipboard.</param>
        /// <param name="retryDelay">The number of milliseconds to pause between attempts.</param>
        void SetDataObject(object data, bool copy, int retryTimes, int retryDelay);
    }
}