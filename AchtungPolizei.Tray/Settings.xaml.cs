namespace AchtungPolizei.Tray
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
        }

        private void AddProjectClick(object sender, RoutedEventArgs e)
        {
            var createDialog = new CreateProject();
            createDialog.ShowDialog();
        }
    }
}
