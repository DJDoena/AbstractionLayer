namespace DoenaSoft.AbstractionLayer.UIServices
{
    using System;

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
    }
}