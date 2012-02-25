namespace AchtungPolizei.Tray
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using AchtungPolizei.Core;
    using AchtungPolizei.Plugins;

    public class CreateProjectViewModel : ViewModelBase
    {
        private string name;
        private ObservableCollection<PluginPreviewItem> inputPlugins;
        private ObservableCollection<PluginPreviewItem> outputPlugins;
        private IConfigirationControl inputConfigurationControl;
        private ObservableCollection<PluginViewModel> outputConfigurationControls;
        private IConfigirationControl outputConfigurationControl;
        private PluginViewModel inputPlugin;
        private PluginViewModel outputPlugin;

        public CreateProjectViewModel(IEnumerable<IInputPlugin> inputPlugins, IEnumerable<IOutputPlugin> outputPlugins)
        {
            this.InputPlugins = new ObservableCollection<PluginPreviewItem>();
            this.OutputPlugins = new ObservableCollection<PluginPreviewItem>();

            foreach (var inputPlugin in inputPlugins)
            {
                this.InputPlugins.Add(new PluginViewModel(inputPlugin));
            }

            foreach (var outputPlugin in outputPlugins)
            {
                this.OutputPlugins.Add(new PluginViewModel(outputPlugin));
            }

            this.OutputConfigurationControls = new ObservableCollection<PluginViewModel>();

        }
        
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.RaisePropertyChanged("Name");
            }
        }

        public PluginViewModel InputPlugin
        {
            get
            {
                return this.inputPlugin;
            }

            set
            {
                this.inputPlugin = value;
                this.RaisePropertyChanged("InputPlugin");
            }
        }

        public PluginViewModel OutputPlugin
        {
            get
            {
                return this.outputPlugin;
            }

            set
            {
                this.outputPlugin = value;
                this.RaisePropertyChanged("OutputPlugin");
            }
        }

        public IConfigirationControl InputConfigurationControl
        {
            get
            {
                return this.inputConfigurationControl;
            }

            set
            {
                this.inputConfigurationControl = value;
                this.RaisePropertyChanged("InputConfigurationControl");
            }
        }

        public ObservableCollection<PluginViewModel> OutputConfigurationControls
        {
            get
            {
                return this.outputConfigurationControls;
            }
            set
            {
                this.outputConfigurationControls = value;
                this.RaisePropertyChanged("OutputConfigurationControls");
            }
        }

        public ObservableCollection<PluginPreviewItem> InputPlugins
        {
            get
            {
                return this.inputPlugins;
            }

            set
            {
                this.inputPlugins = value;
                this.RaisePropertyChanged("InputPlugins");
            }
        }

        public ObservableCollection<PluginPreviewItem> OutputPlugins
        {
            get
            {
                return this.outputPlugins;
            }

            set
            {
                this.outputPlugins = value;
                this.RaisePropertyChanged("OutputPlugins");
            }
        }

        public IConfigirationControl OutputConfigurationControl
        {
            get
            {
                return this.outputConfigurationControl;
            }

            set
            {
                this.outputConfigurationControl = value;
                this.RaisePropertyChanged("OutputConfigurationControl");
            }
        }

        public Project GetProject()
        {
            var project = new Project();
            project.Name = this.Name;
            project.InputPlugin = new PluginConfiguration
                {
                    PluginId = InputPlugin.Id,
                    Configuration = InputPlugin.Configiration.GetConfiguration()
                };

            foreach (var pluginModel in OutputConfigurationControls)
            {
                project.OutputPlugins.Add(
                    new PluginConfiguration
                    {
                        PluginId = pluginModel.Id,
                        Configuration = pluginModel.Configiration.GetConfiguration()
                    });
            }

            return project;
        }
    }
}