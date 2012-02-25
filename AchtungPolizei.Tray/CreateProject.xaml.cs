namespace AchtungPolizei.Tray
{
    using System.Windows;
    using System.Windows.Controls;

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
            
            model = new CreateProjectViewModel(PluginLocator.Current.GetAllInputPlugins(), PluginLocator.Current.GetAllOutputPlugins());
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
            var plugin = (PluginViewModel)this.OutputPluginsComboBox.SelectedItem;
            if (plugin.Configiration.Validate())
            {
                model.OutputConfigurationControls.Add(plugin);
                model.OutputConfigurationControl = null;
            }
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void CreateButtonClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void InputPluginSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var pluginModel = (PluginViewModel)InputPluginsComboBox.SelectedItem;

            model.InputConfigurationControl.GetConfiguration();
        }
    }
}
