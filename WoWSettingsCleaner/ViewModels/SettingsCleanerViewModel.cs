namespace WoWSettingsCleaner.ViewModels
{
   using Logic;

   /// <summary>
   /// Model for the main view.
   /// </summary>
   internal class SettingsCleanerViewModel : ISettingsCleanerViewModel
   {
      /// <summary>
      /// The _add on scanner.
      /// </summary>
      private readonly IFileSystemScanner _fileSystemScanner;

      /// <summary>
      /// Initializes a new instance of the <see cref="SettingsCleanerViewModel" /> class.
      /// </summary>
      public SettingsCleanerViewModel(IFileSystemScanner fileSystemScanner)
      {
         _fileSystemScanner = fileSystemScanner;
      }

      /// <summary>
      /// Cleans the settings.
      /// </summary>
      /// <param name="cleanupType">Type of the cleanup.</param>
      /// <param name="path">The path.</param>
      public void CleanSettings(CleanupType cleanupType, string path)
      {
         if (cleanupType == CleanupType.CleanSettings)
         {
            _fileSystemScanner.CleanSettings(path);
         }
         else if (cleanupType == CleanupType.CheckSettings)
         {
             _fileSystemScanner.CheckSettings(path);
         }
        }
   }
}