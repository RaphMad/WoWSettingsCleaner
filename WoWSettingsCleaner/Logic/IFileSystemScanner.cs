namespace WoWSettingsCleaner.Logic
{
   using System.Collections.Generic;

   /// <summary>
   /// Responsible for interaction with the file system.
   /// </summary>
   internal interface IFileSystemScanner
   {
      /// <summary>
      /// Finds the addons under a given path.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The found addons.</returns>
      IEnumerable<AddOn> FindAddOns(string path);

      /// <summary>
      /// Finds the add on settings.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The found addon settings.</returns>
      IEnumerable<AddOnSettings> FindAddOnSettings(string path);

      /// <summary>
      /// Finds the unused addon settings.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The unused addon settings.</returns>
      IEnumerable<AddOnSettings> FindUnusedAddonSettings(string path);

      /// <summary>
      /// Cleans the settings in a given path.
      /// </summary>
      /// <param name="path">The path.</param>
      void CleanSettings(string path);

       /// <summary>
       /// Checks the settings in a given path.
       /// </summary>
       /// <param name="path">The path.</param>
       void CheckSettings(string path);
    }
}