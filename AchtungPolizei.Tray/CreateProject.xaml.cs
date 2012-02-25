namespace AchtungPolizei.Tray
{
    using System.Linq;
    using System.Windows;

    using AchtungPolizei.Core;
    using AchtungPolizei.Core.Helpers;

    /// <summary>
    /// Interaction logic for CreateUpdateProject.xaml
    /// </summary>
    public partial class CreateProject
    {
        private readonly CreateProjectViewModel model;

        public CreateProject()
        {
            InitializeComponent();

            var pluginsLocator = new PluginLocator("");
            var pluginTypes = pluginsLocator.FindPluginTypes();

            var plugins = pluginTypes.Select(pluginsLocator.CreatePluginInstance);

            model = new CreateProjectViewModel(plugins);
            DataContext = model;
        }

        public Project CreatedProject
        {
            get
            {
                return model.GetProject();
            }
        }

        private void AddOutputPluginClick(object sender, RoutedEventArgs e)
        {
            model.OutputConfigurationControls.Add((PluginViewModel)this.OutputPluginsComboBox.SelectedItem);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
