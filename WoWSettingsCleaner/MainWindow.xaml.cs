namespace WoWSettingsCleaner
{
   using System;
   using System.Windows;
   using System.Windows.Documents;
   using System.Windows.Media;
   using Infrastructure;
   using StructureMap;
   using Tools;
   using ViewModels;

   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow
   {
      /// <summary>
      /// The view model.
      /// </summary>
      private readonly ISettingsCleanerViewModel _viewModel;

      /// <summary>
      /// Initializes a new instance of the <see cref="MainWindow" /> class.
      /// </summary>
      public MainWindow()
      {
         InitializeComponent();

         // populate ComboBox
         TypeComboBox.ItemsSource = Enum.GetValues(typeof(CleanupType));
         TypeComboBox.SelectedIndex = 0;

         // setup StructureMap
         var container = new Container();
         container.Configure(x => x.AddRegistry<AppRegistry>());

         // init log output to textbox
         var logger = container.GetInstance<ILogger>();
         logger.MessageLogged += HandleMessageLogged;
         logger.ErrorLogged += HandleErrorLogged;

         // assign ViewModel
         _viewModel = container.GetInstance<ISettingsCleanerViewModel>();
         DataContext = _viewModel;
      }

      /// <summary>
      /// Handles the perform clicked.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
      private void HandlePerformClicked(object sender, RoutedEventArgs e)
      {
         LogTextBox.Document.Blocks.Clear();
         _viewModel.CleanSettings((CleanupType)TypeComboBox.SelectedItem, PathTextBox.Text);
      }

      /// <summary>
      /// Handles the message logged.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="string" /> instance containing the event data.</param>
      private void HandleMessageLogged(object sender, EventArgs<string> e)
      {
         var rangeOfText1 = new TextRange(LogTextBox.Document.ContentEnd, LogTextBox.Document.ContentEnd) { Text = e.Value + "\r" };
         rangeOfText1.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
         LogTextBox.ScrollToEnd();
      }

      /// <summary>
      /// Handles the error logged.
      /// </summary>
      /// <param name="sender">The sender.</param>
      /// <param name="e">The <see cref="string" /> instance containing the event data.</param>
      private void HandleErrorLogged(object sender, EventArgs<string> e)
      {
         var rangeOfText1 = new TextRange(LogTextBox.Document.ContentEnd, LogTextBox.Document.ContentEnd) { Text = e.Value + "\r" };
         rangeOfText1.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
         LogTextBox.ScrollToEnd();
      }
   }
}