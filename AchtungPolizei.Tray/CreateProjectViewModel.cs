namespace AchtungPolizei.Tray
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Controls;

    using AchtungPolizei.Plugins;

    public class CreateProjectViewModel : ViewModelBase
    {
        private string name;
        private ObservableCollection<PluginViewModel> inputPlugins;
        private ObservableCollection<PluginViewModel> outputPlugins;
        private IConfigirationControl inputConfigurationControl;
        private ObservableCollection<PluginViewModel> outputConfigurationControls;
        private IConfigirationControl outputConfigurationControl;

        private object configuration;

        public CreateProjectViewModel(IEnumerable<IPlugin> plugins)
        {
            this.InputPlugins = new ObservableCollection<PluginViewModel>();
            this.OutputPlugins = new ObservableCollection<PluginViewModel>();
            this.OutputConfigurationControls = new ObservableCollection<PluginViewModel>();

            // var inputPlugins = plugins.OfType<IInputPlugin>();
            // var outputPlugins = plugins.OfType<IOutputPlugin>();
            this.InputPlugins.Add(new PluginViewModel { Name = "Input Test1", Configiration = new Test { Content = "Hello 1" } });
            this.InputPlugins.Add(new PluginViewModel { Name = "Input Test2", Configiration = new Test { Content = "Hello 2" } });

            this.OutputPlugins.Add(new PluginViewModel { Name = "Output Test1", Configiration = new Test { Content = "Hello 3" } });
            this.OutputPlugins.Add(new PluginViewModel { Name = "Output Test2", Configiration = new Test { Content = "Hello 4" } });
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
    }

    public class Test : Button, IConfigirationControl
    {
        public bool Validate()
        {
            return false;
        }

        public ConfigurationBase GetConfiguration()
        {
            return null;
        }
    }
}