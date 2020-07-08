namespace WoWSettingsCleaner.Logic
{
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using Infrastructure;

   /// <summary>
   /// Responsible for interaction with the file system.
   /// </summary>
   internal class FileSystemScanner : IFileSystemScanner
   {
      /// <summary>
      /// The add on sub folder.
      /// </summary>
      private const string AddOnSubFolder = "\\Interface\\AddOns";

      /// <summary>
      /// The account sub folder.
      /// </summary>
      private const string AccountSubFolder = "\\WTF\\Account";

      /// <summary>
      /// The saved variables sub folder.
      /// </summary>
      private const string SavedVariablesSubFolder = "\\SavedVariables";

      /// <summary>
      /// The lua extension.
      /// </summary>
      private const string LuaExtension = ".lua";

      /// <summary>
      /// The backup extension.
      /// </summary>
      private const string BackupExtension = ".lua.bak";

      /// <summary>
      /// The other file prefix.
      /// </summary>
      private const string OtherFilePrefix = "FILE_";

      /// <summary>
      /// The logger.
      /// </summary>
      private readonly ILogger _logger;

      /// <summary>
      /// Initializes a new instance of the <see cref="FileSystemScanner" /> class.
      /// </summary>
      /// <param name="logger">The logger.</param>
      public FileSystemScanner(ILogger logger)
      {
         _logger = logger;
      }

      /// <summary>
      /// Finds the addons under a given path.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The found addons.</returns>
      public IEnumerable<AddOn> FindAddOns(string path)
      {
         IEnumerable<AddOn> foundAddons = Enumerable.Empty<AddOn>();

         string fullPath = path + AddOnSubFolder;

         if (Directory.Exists(fullPath))
         {
            foundAddons =
               Directory.EnumerateDirectories(fullPath).Select(directory => new AddOn(RemoveLeadingDirectories(directory)));
         }

         return foundAddons;
      }

      /// <summary>
      /// Finds the add on settings.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The found addon settings.</returns>
      public IEnumerable<AddOnSettings> FindAddOnSettings(string path)
      {
         List<AddOnSettings> foundSettings = new List<AddOnSettings>();

         string accountSettingsPath = path + AccountSubFolder;

         if (Directory.Exists(accountSettingsPath))
         {
            foreach (string accountPath in Directory.EnumerateDirectories(accountSettingsPath))
            {
               // look in global account settings
               foundSettings.AddRange(LoadAccountSettings(accountPath));

               // as well as in server specific settings
               foundSettings.AddRange(LoadServerSettings(accountPath));
            }
         }

         return foundSettings;
      }

      /// <summary>
      /// Finds the unused addon settings.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The unused addon settings.</returns>
      public IEnumerable<AddOnSettings> FindUnusedAddonSettings(string path)
      {
         IEnumerable<AddOn> addOns = FindAddOns(path);
         IEnumerable<AddOnSettings> addOnSettings = FindAddOnSettings(path);

         return addOnSettings.Where(setting => !addOns.Any(addon => addon.Name == setting.AddOnName));
      }

      /// <summary>
      /// Cleans the settings in a given path.
      /// </summary>
      /// <param name="path">The path.</param>
      public void CleanSettings(string path)
      {
         IList<AddOnSettings> unusedSettings = FindUnusedAddonSettings(path).ToList();

         if (unusedSettings.Any())
         {
            foreach (AddOnSettings settingToDelete in unusedSettings)
            {
               if (settingToDelete.SettingsFile != null && settingToDelete.SettingsFile.Exists)
               {
                  settingToDelete.SettingsFile.Delete();
                  _logger.Message("Deleted file " + settingToDelete.SettingsFile);
               }

               if (settingToDelete.Backupfile != null && settingToDelete.Backupfile.Exists)
               {
                  settingToDelete.Backupfile.Delete();
                  _logger.Message("Deleted file " + settingToDelete.Backupfile);
               }
            }
         }
         else
         {
            _logger.Message("Nothing to clean!");
         }
      }

       /// <summary>
       /// Checks the settings in a given path.
       /// </summary>
       /// <param name="path">The path.</param>
       public void CheckSettings(string path)
       {
           IList<AddOnSettings> unusedSettings = FindUnusedAddonSettings(path).ToList();

           if (unusedSettings.Any())
           {
               foreach (AddOnSettings settingToDelete in unusedSettings)
               {
                   if (settingToDelete.SettingsFile != null && settingToDelete.SettingsFile.Exists)
                   {
                       _logger.Message("Found settings file " + settingToDelete.SettingsFile);
                   }

                   if (settingToDelete.Backupfile != null && settingToDelete.Backupfile.Exists)
                   {
                       _logger.Message("Found backup file " + settingToDelete.Backupfile);
                   }
               }
           }
           else
           {
               _logger.Message("Nothing to clean!");
           }
       }


        /// <summary>
        /// Loads the account settings.
        /// </summary>
        /// <param name="accountPath">The account path.</param>
        /// <returns>All found account settings.</returns>
        private IEnumerable<AddOnSettings> LoadAccountSettings(string accountPath)
      {
         List<AddOnSettings> foundSettings = new List<AddOnSettings>();

         string fullPath = accountPath + SavedVariablesSubFolder;

         if (Directory.Exists(fullPath))
         {
            foundSettings.AddRange(FindLuaSettings(fullPath));
         }

         return foundSettings;
      }

      /// <summary>
      /// Loads the server settings.
      /// </summary>
      /// <param name="accountPath">The account path.</param>
      /// <returns>The server specific settings.</returns>
      private IEnumerable<AddOnSettings> LoadServerSettings(string accountPath)
      {
         List<AddOnSettings> foundSettings = new List<AddOnSettings>();

         foreach (string serverPath in Directory.EnumerateDirectories(accountPath).Where(path => !path.EndsWith(SavedVariablesSubFolder)))
         {
            foundSettings.AddRange(LoadCharacterSettings(serverPath));
         }

         return foundSettings;
      }

      /// <summary>
      /// Loads the character settings.
      /// </summary>
      /// <param name="serverPath">The server path.</param>
      /// <returns>The character settings.</returns>
      private IEnumerable<AddOnSettings> LoadCharacterSettings(string serverPath)
      {
         List<AddOnSettings> foundSettings = new List<AddOnSettings>();

         foreach (string characterSettingsPath in Directory.EnumerateDirectories(serverPath))
         {
            string fullPath = characterSettingsPath + SavedVariablesSubFolder;

            if (Directory.Exists(fullPath))
            {
               foundSettings.AddRange(FindLuaSettings(fullPath));
            }
         }

         return foundSettings;
      }

      /// <summary>
      /// Finds the lua settings.
      /// </summary>
      /// <param name="path">The full path.</param>
      /// <returns>All lua settings in the path.</returns>
      private IEnumerable<AddOnSettings> FindLuaSettings(string path)
      {
         IList<AddOnSettings> luaSettings = new List<AddOnSettings>();

         IList<FileInfo> allFiles = Directory.EnumerateFiles(path).Select(file => new FileInfo(file)).Where(file => !file.Name.Contains("Blizzard_")).ToList();

         foreach (FileInfo file in allFiles)
         {
            if (file.Extension == LuaExtension)
            {
               string addOnName = StripExtension(file);

               FileInfo backupFile = allFiles.FirstOrDefault(otherFile => otherFile.Name.EndsWith(addOnName + BackupExtension));
               luaSettings.Add(new AddOnSettings(addOnName, file, backupFile));
            }
         }

         foreach (FileInfo file in allFiles)
         {
            if (!luaSettings.Any(setting => setting.SettingsFile.Name == file.Name || GetBackupFileName(setting) == file.Name))
            {
               // add a dummy setting for stray files
               luaSettings.Add(new AddOnSettings(OtherFilePrefix + file.Name, file, null));
            }
         }

         return luaSettings;
      }

      /// <summary>
      /// Gets the name of the file.
      /// </summary>
      /// <param name="setting">The setting.</param>
      /// <returns>The name of the backup file if found, an empty string otherwise.</returns>
      private static string GetBackupFileName(AddOnSettings setting)
      {
         return setting.Backupfile != null ? setting.Backupfile.Name : string.Empty;
      }

      /// <summary>
      /// Removes the leading directories.
      /// </summary>
      /// <param name="path">The path.</param>
      /// <returns>The path without leading directories.</returns>
      private static string RemoveLeadingDirectories(string path)
      {
         return path.Substring(path.LastIndexOf('\\') + 1);
      }

      /// <summary>
      /// Strips the extension.
      /// </summary>
      /// <param name="file">The file.</param>
      /// <returns>The filename without its extension.</returns>
      private static string StripExtension(FileInfo file)
      {
         return file.Name.Substring(0, file.Name.Length - file.Extension.Length);
      }
   }
}