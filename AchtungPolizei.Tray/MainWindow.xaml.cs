using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using AchtungPolizei.Core;
using AchtungPolizei.Plugins;

namespace AchtungPolizei.Tray
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly IList<Project> projects;
        private readonly ProjectsRepository repository = new ProjectsRepository();
        private readonly ObservableCollection<ProjectViewModel> projectsViewModels;

        private Dictionary<BuildStatus, Color> buildStatusToColor = new Dictionary<BuildStatus, Color>
            {
                { BuildStatus.Broken, Colors.Red },
                { BuildStatus.StillBroken, Colors.DarkRed },
                { BuildStatus.Fixed, Colors.LimeGreen }
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            projects = repository.GetProjects();
            
            projectsViewModels = CreateProjectsViewModels(projects);

            Engine.Current.BuildStatusChanged += BuildStatusChanged;

            foreach (var project in projects)
            {
                Engine.Current.AddProject(project);
            }
        }

        /// <summary>
        /// Raised when build status of some project was changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> Event arguments. </param>
        private void BuildStatusChanged(object sender, BuildStatusChangedEventArgs e)
        {
            var viewModel = projectsViewModels.First(vm => vm.Name == e.Project.Name);

            viewModel.StateColor = new SolidColorBrush(buildStatusToColor[e.BuildStatus]);
        }

        /// <summary>
        /// Creates view models for projects.
        /// </summary>
        /// <param name="projects"> Loaded projects. </param>
        /// <returns> View models collection. </returns>
        private ObservableCollection<ProjectViewModel> CreateProjectsViewModels(IEnumerable<Project> projects)
        {
            var viewModels = projects.Select(project => new ProjectViewModel(project));
            return new ObservableCollection<ProjectViewModel>(viewModels);
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
