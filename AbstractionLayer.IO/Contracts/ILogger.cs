﻿using System;

namespace DoenaSoft.AbstractionLayer.IOServices;

/// <summary>
/// Interface to seperate logging concerns from an concrete implementation.
/// </summary>
public interface ILogger : IDisposable
{
    /// <summary>
    /// Writes the message with its parameters and creates a line break and another empty line.
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="parameters">The message parameter</param>
    void WriteLine(string message, params object[] parameters);

    /// <summary>
    /// Writes the message with its parameters and creates a line break with the option to suppress additional  empty line.
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="suppressFreeLine">whether to suppress an additional empty line</param>
    /// <param name="parameters">The message parameter</param>
    void WriteLine(string message, bool suppressFreeLine, params object[] parameters);

    /// <summary>
    /// Write the message with its parameters with the expectation that a user input will soon be required.
    /// Depending on the logger this means a different way to receive input from the user.
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="parameters">The message parameter</param>
    void WriteLineForInput(string message, params object[] parameters);

    /// <summary>
    /// Reads user input.
    /// </summary>
    /// <returns>The user input</returns>
    string ReadLine();
}