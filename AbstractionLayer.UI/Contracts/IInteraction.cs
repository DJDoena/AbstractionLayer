namespace DoenaSoft.AbstractionLayer.UI.Contracts;

/// <summary>
/// Provides an abstraction for console or UI-based user interaction operations such as reading and writing text.
/// </summary>
public interface IInteraction
{
    /// <summary>
    /// Writes a message followed by a line terminator to the output.
    /// </summary>
    /// <param name="message">The message to write. If null or not provided, writes an empty line.</param>
    void WriteLine(string message = null);

    /// <summary>
    /// Writes a message to the output without a line terminator.
    /// </summary>
    /// <param name="message">The message to write.</param>
    void Write(string message);

    /// <summary>
    /// Reads a line of text from the input.
    /// </summary>
    /// <returns>The line of text that was read, or null if no more lines are available.</returns>
    string ReadLine();

    /// <summary>
    /// Reads the next character or key pressed by the user.
    /// </summary>
    /// <param name="intercept">If true, the pressed key is not displayed in the output; if false, the pressed key is displayed.</param>
    /// <returns>The character that was read.</returns>
    char ReadKey(bool intercept = false);
}
