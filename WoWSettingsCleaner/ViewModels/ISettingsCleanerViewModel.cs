namespace WoWSettingsCleaner.ViewModels
{
   /// <summary>
   /// Interface to the SettingsCleanerViewModel.
   /// </summary>
   internal interface ISettingsCleanerViewModel
   {
      /// <summary>
      /// Cleans the settings.
      /// </summary>
      /// <param name="cleanupType">Type of the cleanup.</param>
      /// <param name="path">The path.</param>
      void CleanSettings(CleanupType cleanupType, string path);
   }
}