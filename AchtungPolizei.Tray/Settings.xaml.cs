using System.Collections.Generic;
using AchtungPolizei.Core;

namespace AchtungPolizei.Tray
{
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings
    {
        private readonly IList<Project> projects;
        private SettingsViewModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings(IList<Project> projects)
        {
            this.projects = projects;

            InitializeComponent();
            model = new SettingsViewModel(projects);

            DataContext = model;
        }

        private void AddProjectClick(object sender, RoutedEventArgs e)
        {
            var createDialog = new CreateProject();
            var createDialogResult = createDialog.ShowDialog();

            if (createDialogResult.HasValue && createDialogResult.Value)
            {
                projects.Add(createDialog.CreatedProject);
                model.Projects.Add(new ProjectViewModel(createDialog.CreatedProject));
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditProjectClick(object sender, RoutedEventArgs e)
        {
            var buttonModel = (ProjectViewModel) ((FrameworkElement)e.OriginalSource).DataContext;

            var updateProject = new UpdateProject(buttonModel.Source);
            var dialogResult = updateProject.ShowDialog();
            
            if (dialogResult.HasValue && dialogResult.Value)
            {
                projects.Add(updateProject.CreatedProject);
                model.Projects.Add(new ProjectViewModel(updateProject.CreatedProject));
            }
        }
    }
}
