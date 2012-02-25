namespace AchtungPolizei.Tray
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        private SettingsViewModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            InitializeComponent();
            model = new SettingsViewModel();

            DataContext = model;
        }

        private void AddProjectClick(object sender, RoutedEventArgs e)
        {
            var createDialog = new CreateProject();
            var createDialogResult = createDialog.ShowDialog();

            if (createDialogResult.HasValue && createDialogResult.Value)
            {
                model.Projects.Add(new ProjectViewModel(createDialog.CreatedProject));
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
