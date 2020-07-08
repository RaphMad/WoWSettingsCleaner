namespace WoWSettingsCleaner.Tools
{
   using System;

   /// <summary>
   /// Contains general extension methods.
   /// </summary>
   internal static class Extensions
   {
      /// <summary>
      /// Raises the specified event in a thread-safe manner.
      /// </summary>
      /// <typeparam name="TArguments">The type of the arguments.</typeparam>
      /// <param name="eventHandler">The event handler to raise.</param>
      /// <param name="sender">The sender.</param>
      /// <param name="eventArguments">The event arguments.</param>
      public static void Raise<TArguments>(this EventHandler<TArguments> eventHandler, object sender, TArguments eventArguments) where TArguments : EventArgs
      {
            eventHandler?.Invoke(sender, eventArguments);
        }
   }
}