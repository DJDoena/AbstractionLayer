using System.Collections.Specialized;
using SIO = System.IO;

namespace DoenaSoft.AbstractionLayer.UIServices;

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
    /// Indicates whether there is data on the Clipboard that is in the audio format or can be converted to that format.
    /// </summary>
    bool ContainsAudio();

    /// <summary>
    /// Indicates whether there is data on the Clipboard that is in the file drop list format or can be converted to that format.
    /// </summary>
    bool ContainsFileDropList();

    /// <summary>
    /// Indicates whether there is data on the Clipboard that is in the image format or can be converted to that format.
    /// </summary>
    bool ContainsImage();

    /// <summary>
    /// Indicates whether there is data on the Clipboard in the specified format.
    /// </summary>
    /// <param name="format">The format of the data to look for.</param>
    bool ContainsData(string format);

    /// <summary>
    /// Removes all data from the Clipboard.
    /// </summary>
    void Clear();

    /// <summary>
    /// Returns the text from the clipboard.
    /// </summary>
    /// <returns>the text from the clipboard</returns>
    string GetText();

    /// <summary>
    /// Retrieves an audio stream from the Clipboard.
    /// </summary>
    /// <returns>A Stream containing audio data, or null if the Clipboard does not contain any data in the audio format.</returns>
    SIO.Stream GetAudioStream();

    /// <summary>
    /// Retrieves a collection of file names from the Clipboard.
    /// </summary>
    /// <returns>A StringCollection containing file names, or null if the Clipboard does not contain any data in the file drop list format.</returns>
    StringCollection GetFileDropList();

    /// <summary>
    /// Retrieves an image from the Clipboard.
    /// </summary>
    /// <returns>An object representing the image, or null if the Clipboard does not contain any data in the image format.</returns>
    object GetImage();

    /// <summary>
    /// Retrieves data from the Clipboard in the specified format.
    /// </summary>
    /// <param name="format">The format of the data to retrieve.</param>
    /// <returns>An object representing the clipboard data, or null if the Clipboard does not contain data in the specified format.</returns>
    object GetData(string format);

    /// <summary>
    /// Retrieves data from the Clipboard in the specified format.
    /// </summary>
    /// <param name="format">The format of the data to retrieve.</param>
    /// <returns>An object representing the clipboard data, or null if the Clipboard does not contain data in the specified format.</returns>
    T GetData<T>(string format);

    /// <summary>
    /// Retrieves the data object that is currently on the Clipboard.
    /// </summary>
    /// <returns>An object that represents the data currently on the Clipboard, or null if there is no data on the Clipboard.</returns>
    object GetDataObject();

    /// <summary>
    /// Sets the text to the clipboard.
    /// </summary>
    /// <param name="text">The text</param>
    /// <returns>If the setting succeeded</returns>
    bool SetText(string text);

    /// <summary>
    /// Clears the Clipboard and then adds audio data in the specified format.
    /// </summary>
    /// <param name="audioBytes">A byte array containing the audio data.</param>
    void SetAudio(byte[] audioBytes);

    /// <summary>
    /// Clears the Clipboard and then adds audio data in the specified format.
    /// </summary>
    /// <param name="audioStream">A stream containing the audio data.</param>
    void SetAudio(SIO.Stream audioStream);

    /// <summary>
    /// Clears the Clipboard and then adds a collection of file names.
    /// </summary>
    /// <param name="filePaths">A StringCollection containing the file names.</param>
    void SetFileDropList(StringCollection filePaths);

    /// <summary>
    /// Clears the Clipboard and then adds an image.
    /// </summary>
    /// <param name="image">The image to add to the Clipboard.</param>
    void SetImage(object image);

    /// <summary>
    /// Clears the Clipboard and then adds data in the specified format.
    /// </summary>
    /// <param name="format">The format of the data to set.</param>
    /// <param name="data">The data to add to the Clipboard.</param>
    void SetData(string format, object data);

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