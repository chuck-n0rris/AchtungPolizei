namespace AchtungPolizei.Tray
{
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    using AchtungPolizei.Core;

    /// <summary>
    /// Interaction logic for CreateUpdateProject.xaml
    /// </summary>
    public partial class UpdateProject
    {
        private readonly UpdateProjectViewModel model;

        public UpdateProject(Project project)
        {
            InitializeComponent();

            model = new UpdateProjectViewModel();
            DataContext = model;
            
            this.GetValue(project);
        }

        private void GetValue(Project project)
        {
            var foundPlugin = this.model.InputPlugins.Single(it => it.Id == project.InputPlugin.PluginId);

            model.Name = project.Name;

            foundPlugin.Source.SetConfiguration(project.InputPlugin.Configuration);
            var pluginModel = new PluginViewModel
                { Id = foundPlugin.Id, Name = foundPlugin.Name, Configuration = foundPlugin.Source.GetConfigControl() };

            this.model.SelectedInputPlugin = pluginModel;
            InputPluginsComboBox.SelectedItem = pluginModel;

            foreach (var outputPlugin1 in project.OutputPlugins)
            {
                var foundOutputPlugin = this.model.OutputPlugins.Single(it => it.Id == outputPlugin1.PluginId);

                foundOutputPlugin.Source.SetConfiguration(outputPlugin1.Configuration);
                var outputPlugin = new PluginViewModel
                    {
                        Id = foundOutputPlugin.Id,
                        Name = foundOutputPlugin.Name, 
                        Configuration = foundOutputPlugin.Source.GetConfigControl() 
                    };

                this.model.SelectedOutputPlugins.Add(outputPlugin);
            }
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
