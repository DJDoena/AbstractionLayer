using System.Collections.Specialized;
using System.IO;

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
        /// Indicates whether there is data on the Clipboard that is in the audio format or can be converted to that format.
        /// </summary>
        public bool ContainsAudio() => System.Windows.Clipboard.ContainsAudio();

        /// <summary>
        /// Indicates whether there is data on the Clipboard that is in the file drop list format or can be converted to that format.
        /// </summary>
        public bool ContainsFileDropList() => System.Windows.Clipboard.ContainsFileDropList();

        /// <summary>
        /// Indicates whether there is data on the Clipboard that is in the image format or can be converted to that format.
        /// </summary>
        public bool ContainsImage() => System.Windows.Clipboard.ContainsImage();

        /// <summary>
        /// Indicates whether there is data on the Clipboard in the specified format.
        /// </summary>
        /// <param name="format">The format of the data to look for.</param>
        public bool ContainsData(string format) => System.Windows.Clipboard.ContainsData(format);

        /// <summary>
        /// Removes all data from the Clipboard.
        /// </summary>
        public void Clear() => System.Windows.Clipboard.Clear();

        /// <summary>
        /// Returns the text from the clipboard.
        /// </summary>
        /// <returns>the text from the clipboard</returns>
        public string GetText() => System.Windows.Clipboard.GetText();

        /// <summary>
        /// Retrieves an audio stream from the Clipboard.
        /// </summary>
        /// <returns>A Stream containing audio data, or null if the Clipboard does not contain any data in the audio format.</returns>
        public Stream GetAudioStream() => System.Windows.Clipboard.GetAudioStream();

        /// <summary>
        /// Retrieves a collection of file names from the Clipboard.
        /// </summary>
        /// <returns>A StringCollection containing file names, or null if the Clipboard does not contain any data in the file drop list format.</returns>
        public StringCollection GetFileDropList() => System.Windows.Clipboard.GetFileDropList();

        /// <summary>
        /// Retrieves an image from the Clipboard.
        /// </summary>
        /// <returns>An object representing the image, or null if the Clipboard does not contain any data in the image format.</returns>
        public object GetImage() => System.Windows.Clipboard.GetImage();

        /// <summary>
        /// Retrieves data from the Clipboard in the specified format.
        /// </summary>
        /// <param name="format">The format of the data to retrieve.</param>
        /// <returns>An object representing the clipboard data, or null if the Clipboard does not contain data in the specified format.</returns>
        public T GetData<T>(string format) => (T)System.Windows.Clipboard.GetData(format);

        /// <summary>
        /// Retrieves data from the Clipboard in the specified format.
        /// </summary>
        /// <param name="format">The format of the data to retrieve.</param>
        /// <returns>An object representing the clipboard data, or null if the Clipboard does not contain data in the specified format.</returns>
        public object GetData(string format) => System.Windows.Clipboard.GetData(format);

        /// <summary>
        /// Retrieves the data object that is currently on the Clipboard.
        /// </summary>
        /// <returns>An object that represents the data currently on the Clipboard, or null if there is no data on the Clipboard.</returns>
        public object GetDataObject() => System.Windows.Clipboard.GetDataObject();

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
        /// Clears the Clipboard and then adds audio data in the specified format.
        /// </summary>
        /// <param name="audioBytes">A byte array containing the audio data.</param>
        public void SetAudio(byte[] audioBytes) => System.Windows.Clipboard.SetAudio(audioBytes);

        /// <summary>
        /// Clears the Clipboard and then adds audio data in the specified format.
        /// </summary>
        /// <param name="audioStream">A stream containing the audio data.</param>
        public void SetAudio(Stream audioStream) => System.Windows.Clipboard.SetAudio(audioStream);

        /// <summary>
        /// Clears the Clipboard and then adds a collection of file names.
        /// </summary>
        /// <param name="filePaths">A StringCollection containing the file names.</param>
        public void SetFileDropList(StringCollection filePaths) => System.Windows.Clipboard.SetFileDropList(filePaths);

        /// <summary>
        /// Clears the Clipboard and then adds an image.
        /// </summary>
        /// <param name="image">The image to add to the Clipboard.</param>
        public void SetImage(object image) => System.Windows.Clipboard.SetImage((System.Windows.Media.Imaging.BitmapSource)image);

        /// <summary>
        /// Clears the Clipboard and then adds data in the specified format.
        /// </summary>
        /// <param name="format">The format of the data to set.</param>
        /// <param name="data">The data to add to the Clipboard.</param>
        public void SetData(string format, object data) => System.Windows.Clipboard.SetData(format, data);

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