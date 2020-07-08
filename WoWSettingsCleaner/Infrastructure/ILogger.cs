namespace WoWSettingsCleaner.Infrastructure
{
   using System;
   using Tools;

   /// <summary>
   /// Basic logger.
   /// </summary>
   internal interface ILogger
   {
      /// <summary>
      /// Logs a message.
      /// </summary>
      /// <param name="message">The message.</param>
      void Message(string message);

      /// <summary>
      /// Logs an error.
      /// </summary>
      /// <param name="errorMessage">The error message.</param>
      void Error(string errorMessage);

      /// <summary>
      /// Occurs when a message was logged.
      /// </summary>
      event EventHandler<EventArgs<string>> MessageLogged;

      /// <summary>
      /// Occurs when an error was logged.
      /// </summary>
      event EventHandler<EventArgs<string>> ErrorLogged;
   }
}