namespace WoWSettingsCleaner.Logic
{
   using System.IO;

   /// <summary>
   /// Represents settings for an addon.
   /// </summary>
   internal class AddOnSettings
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="AddOnSettings" /> class.
      /// </summary>
      /// <param name="addOnName">Name of the add on.</param>
      /// <param name="settingsFile">The settings file.</param>
      /// <param name="backupfile">The backupfile.</param>
      public AddOnSettings(string addOnName, FileInfo settingsFile, FileInfo backupfile)
      {
         AddOnName = addOnName;
         SettingsFile = settingsFile;
         Backupfile = backupfile;
      }

      /// <summary>
      /// Gets the name of the add on.
      /// </summary>
      public string AddOnName { get; private set; }

      /// <summary>
      /// Gets the settings file.
      /// </summary>
      public FileInfo SettingsFile { get; private set; }

      /// <summary>
      /// Gets the backupfile.
      /// </summary>
      public FileInfo Backupfile { get; private set; }

      /// <summary>
      /// Returns a string that represents the current object.
      /// </summary>
      /// <returns>
      /// A string that represents the current object.
      /// </returns>
      public override string ToString()
      {
         return AddOnName;
      }
   }
}