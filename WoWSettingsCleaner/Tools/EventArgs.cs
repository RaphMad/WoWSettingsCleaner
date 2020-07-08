namespace WoWSettingsCleaner.Tools
{
   using System;

   /// <summary>
   /// Event argument class that carries a data object of a specific type.
   /// </summary>
   /// <typeparam name="T">Type of the data object stored within the event arguments.</typeparam>
   internal sealed class EventArgs<T> : EventArgs
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="EventArgs{T}"/> class.
      /// </summary>
      /// <param name="value">The value.</param>
      public EventArgs(T value)
      {
         Value = value;
      }

      /// <summary>
      /// Gets the value.
      /// </summary>
      /// <value>The value.</value>
      public T Value { get; private set; }
   }
}