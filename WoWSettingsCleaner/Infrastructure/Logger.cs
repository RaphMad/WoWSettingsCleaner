#region File Header

/********************************************************************
 *      PROJECT: PC Dispatcher 3
 *       AUTHOR: Raphael Mader
 *   SUBSTITUTE: Roman Zimmer
 *     LANGUAGE: C#
 *     COMPILER: MS Visual C# .NET
 *  DESCRIPTION: See class description(s)
 *
 *    COPYRIGHT: FREQUENTIS AG. All rights reserved.
 *               Registered with Commercial Court Vienna,
 *               reg.no. FN 72.115b.
 *
 *          $Id: $
 *
 ********************************************************************/

#endregion

namespace WoWSettingsCleaner.Infrastructure
{
   using System;
   using Tools;

   /// <summary>
   /// Basic logger.
   /// </summary>
   internal sealed class Logger : ILogger
   {
      /// <summary>
      /// Logs a message.
      /// </summary>
      /// <param name="message">The message.</param>
      public void Message(string message)
      {
         MessageLogged.Raise(this, new EventArgs<string>(DateTime.Now.ToString("HH:mm:ss") + ": " + message));
      }

      /// <summary>
      /// Logs an error.
      /// </summary>
      /// <param name="errorMessage">The error message.</param>
      public void Error(string errorMessage)
      {
         ErrorLogged.Raise(this, new EventArgs<string>(DateTime.Now.ToString("HH:mm:ss") + ": " + errorMessage));
      }

      /// <summary>
      /// Occurs when a message was logged.
      /// </summary>
      public event EventHandler<EventArgs<string>> MessageLogged;

      /// <summary>
      /// Occurs when an error was logged.
      /// </summary>
      public event EventHandler<EventArgs<string>> ErrorLogged;
   }
}