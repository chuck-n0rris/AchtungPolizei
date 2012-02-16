using System.Windows;

namespace AchtungPolizei.Tray
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when settings button was cliecked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="System.Windows.RoutedEventArgs"/> 
        /// instance containing the event data.
        /// </param>
        private void OnSettingsClick(object sender, RoutedEventArgs e)
        {
            var settings = new Settings();
            settings.ShowDialog();
        }

        /// <summary>
        /// Called when exit button was cliecked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">
        /// The <see cref="System.Windows.RoutedEventArgs"/> instance
        /// containing the event data.
        /// </param>
        private void OnExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
