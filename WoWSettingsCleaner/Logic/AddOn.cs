namespace WoWSettingsCleaner.Logic
{
   /// <summary>
   /// Holds data about an addon.
   /// </summary>
   internal class AddOn
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="AddOn" /> class.
      /// </summary>
      /// <param name="name">The name.</param>
      public AddOn(string name)
      {
         Name = name;
      }

      /// <summary>
      /// Gets the name.
      /// </summary>
      public string Name { get; private set; }

      /// <summary>
      /// Returns a string that represents the current object.
      /// </summary>
      /// <returns>
      /// A string that represents the current object.
      /// </returns>
      public override string ToString()
      {
         return Name;
      }
   }
}