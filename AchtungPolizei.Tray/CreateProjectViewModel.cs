namespace AchtungPolizei.Tray
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Controls;

    using AchtungPolizei.Core;
    using AchtungPolizei.Plugins;

    public class CreateProjectViewModel : ViewModelBase
    {
        private string name;
        private ObservableCollection<PluginViewModel> inputPlugins;
        private ObservableCollection<PluginViewModel> outputPlugins;
        private IConfigirationControl inputConfigurationControl;
        private ObservableCollection<PluginViewModel> outputConfigurationControls;
        private IConfigirationControl outputConfigurationControl;
        private PluginViewModel inputPlugin;

        private object configuration;

        public CreateProjectViewModel(IEnumerable<IPlugin> plugins)
        {
            this.InputPlugins = new ObservableCollection<PluginViewModel>();
            this.OutputPlugins = new ObservableCollection<PluginViewModel>();
            this.OutputConfigurationControls = new ObservableCollection<PluginViewModel>();

            // var inputPlugins = plugins.OfType<IInputPlugin>();
            // var outputPlugins = plugins.OfType<IOutputPlugin>();
            this.InputPlugins.Add(new PluginViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Input Test1", 
                    Configiration = new Test { Content = "Hello 1" }
                });
            this.InputPlugins.Add(new PluginViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Input Test2", 
                    Configiration = new Test { Content = "Hello 2" }
                });

            this.OutputPlugins.Add(new PluginViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Output Test1", 
                    Configiration = new Test { Content = "Hello 3" }
                });
            this.OutputPlugins.Add(new PluginViewModel
                {
                    Id = Guid.NewGuid(),
                    Name = "Output Test2", 
                    Configiration = new Test { Content = "Hello 4" }
                });
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

        public ObservableCollection<PluginViewModel> InputPlugins
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
        
        public ObservableCollection<PluginViewModel> OutputPlugins
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