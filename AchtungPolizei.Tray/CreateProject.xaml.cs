namespace AchtungPolizei.Tray
{
    using System.Windows;
    using System.Windows.Controls;

    using AchtungPolizei.Core;

    /// <summary>
    /// Interaction logic for CreateUpdateProject.xaml
    /// </summary>
    public partial class CreateProject
    {
        private readonly CreateProjectViewModel model;

        public CreateProject()
        {
            InitializeComponent();
            
            model = new CreateProjectViewModel();
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
            if (model.SelectedOutputPlugin != null)
            {
                if (model.SelectedOutputPlugin.Configuration.Validate())
                {
                    model.SelectedOutputPlugins.Add(new PluginViewModel
                    {
                        Id = model.SelectedOutputPlugin.Id,
                        Name = model.SelectedOutputPlugin.Name,
                        Configuration = model.SelectedOutputPlugin.Configuration
                    });
                    
                    model.SelectedOutputPlugin = null;
                    OutputPluginsComboBox.SelectedItem = null;
                }
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
            if (InputPluginsComboBox.SelectedItem == null)
                return;

            var pluginPreview = (PluginPreviewItem)InputPluginsComboBox.SelectedItem;
            var pluginModel = new PluginViewModel
            {
                Id = pluginPreview.Id,
                Name = pluginPreview.Name,
                Configuration = pluginPreview.Source.GetConfigControl()
            };
            
            model.SelectedInputPlugin = pluginModel;
        }

        private void OutputPluginSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OutputPluginsComboBox.SelectedItem == null)
                return;
            
            var pluginPreview = (PluginPreviewItem)OutputPluginsComboBox.SelectedItem;

            var pluginModel = new PluginViewModel
            {
                Id = pluginPreview.Id,
                Name = pluginPreview.Name,
                Configuration = pluginPreview.Source.GetConfigControl()
            };

            model.SelectedOutputPlugin = pluginModel;
        }
    }
}
