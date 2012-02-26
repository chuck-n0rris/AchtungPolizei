﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
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

        private void SyncContext(WaitCallback callback, object parameter)
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, callback, parameter);
        }

        /// <summary>
        /// Raised when build status of some project was changed.
        /// </summary>
        /// <param name="sender"> The sender. </param>
        /// <param name="e"> Event arguments. </param>
        private void BuildStatusChanged(object sender, BuildStatusChangedEventArgs e)
        {
            SyncContext(state =>
                {
                    var args = (BuildStatusChangedEventArgs) state;
                    var viewModel = projectsViewModels.FirstOrDefault(vm => vm.Name == args.Project.Name);
                    if (viewModel != null)
                    {
                        viewModel.StateColor = new SolidColorBrush(buildStatusToColor[args.BuildStatus]);
                    }

                }, e);
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
            var settings = new Settings(projectsViewModels);
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
