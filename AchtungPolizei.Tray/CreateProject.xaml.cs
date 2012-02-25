namespace AchtungPolizei.Tray
{
    using System.Windows;
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

            var pluginsLocator = new PluginLocator();
            var plugins = pluginsLocator.FindPlugins();

            model = new CreateProjectViewModel(plugins);
            DataContext = model;
        }

        private void AddOutputPluginClick(object sender, RoutedEventArgs e)
        {
            model.OutputConfigurationControls.Add((PluginViewModel)this.OutputPluginsComboBox.SelectedItem);
        }
    }
}
