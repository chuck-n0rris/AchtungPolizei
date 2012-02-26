using System.Collections.Generic;
using System.Windows;
using AchtungPolizei.Core;

namespace AchtungPolizei.Tray
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private IList<Project> projects;
        private readonly ProjectsRepository repository = new ProjectsRepository();
 
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            projects = repository.GetProjects();
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
            var settings = new Settings(projects);
            settings.ShowDialog();
            repository.SaveProjects(projects);
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
