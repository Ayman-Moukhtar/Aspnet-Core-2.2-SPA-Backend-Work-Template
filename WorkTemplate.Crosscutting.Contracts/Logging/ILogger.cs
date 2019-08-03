using System;

namespace SWE.JOIN.CrossCutting.Logging
{
  //You can implement this contrat with several frameworks.
  // .NET Diagnostics API, EntLib, Log4Net,NLog, Enterpise Library etc.
  // Logger.Write(new LogEntry()) in EntLib, or LogManager.GetLog("logger-
  public interface ILogger
  {
    /// <summary>
    ///     Log debug message
    /// </summary>
    /// <param name="item">The item with information to write in debug</param>
    void Debug(object item);

    /// <summary>
    ///     Log debug message
    /// </summary>
    /// <param name="message">The debug message</param>
    void Debug(string message);

    /// <summary>
    ///     Log debug message
    /// </summary>
    /// <param name="message">The message</param>
    /// <param name="exception">Exception to write in debug message</param>
    void Debug(string message, Exception exception);

    /// <summary>
    ///     Log message information
    /// </summary>
    /// <param name="message">The information message to write</param>
    void Info(string message);

    /// <summary>
    ///     Log warning message
    /// </summary>
    /// <param name="message">The warning message to write</param>
    void Warning(string message);

    /// <summary>
    ///     Log error message
    /// </summary>
    /// <param name="message">The error message to write</param>
    void Error(string message);

    /// <summary>
    ///     Log error message
    /// </summary>
    /// <param name="message">The error message to write</param>
    /// <param name="exception">The exception associated with this error</param>
    void Error(string message, Exception exception);

    /// <summary>
    ///     Log FATAL error
    /// </summary>
    /// <param name="message">The message of fatal error</param>
    void Fatal(string message);

    /// <summary>
    ///     log FATAL error
    /// </summary>
    /// <param name="message">The message of fatal error</param>
    /// <param name="exception">The exception to write in this fatal message</param>
    void Fatal(string message, Exception exception);
  }
}