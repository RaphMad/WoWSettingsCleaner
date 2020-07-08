namespace WoWSettingsCleaner.Infrastructure
{
   using Logic;
   using StructureMap.Configuration.DSL;
   using ViewModels;

   /// <summary>
   /// StructureMap registry for this application.
   /// </summary>
   public class AppRegistry : Registry
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="AppRegistry" /> class.
      /// </summary>
      public AppRegistry()
      {
         ForSingletonOf<ILogger>().Use<Logger>();
         ForSingletonOf<ISettingsCleanerViewModel>().Use<SettingsCleanerViewModel>();
         ForSingletonOf<IFileSystemScanner>().Use<FileSystemScanner>();
      }
   }
}