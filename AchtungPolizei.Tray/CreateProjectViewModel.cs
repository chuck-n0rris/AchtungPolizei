namespace AchtungPolizei.Tray
{
    using System.Collections.ObjectModel;

    using AchtungPolizei.Core;
    using AchtungPolizei.Core.Helpers;
    using AchtungPolizei.Plugins;

    public class CreateProjectViewModel : ViewModelBase<CreateProjectViewModel>
    {
        private string name;

        private ObservableCollection<PluginPreviewItem> inputPlugins;
        private ObservableCollection<PluginPreviewItem> outputPlugins;

        private PluginViewModel selectedInputPlugin;
        private PluginViewModel selectedOutputPlugin;

        private ObservableCollection<PluginViewModel> selectedOutputPlugins;
        
        public CreateProjectViewModel()
        {
            this.InputPlugins = new ObservableCollection<PluginPreviewItem>();
            this.OutputPlugins = new ObservableCollection<PluginPreviewItem>();
            this.SelectedOutputPlugins = new ObservableCollection<PluginViewModel>();
            
            this.InitializeCollections();
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

        public PluginViewModel SelectedInputPlugin
        {
            get
            {
                return this.selectedInputPlugin;
            }

            set
            {
                this.selectedInputPlugin = value;
                this.RaisePropertyChanged("SelectedInputPlugin");
            }
        }

        public PluginViewModel SelectedOutputPlugin
        {
            get
            {
                return this.selectedOutputPlugin;
            }

            set
            {
                this.selectedOutputPlugin = value;
                this.RaisePropertyChanged("SelectedOutputPlugin");
            }
        }

        public ObservableCollection<PluginViewModel> SelectedOutputPlugins
        {
            get
            {
                return this.selectedOutputPlugins;
            }
            set
            {
                this.selectedOutputPlugins = value;
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
        
        public Project GetProject()
        {
            //var project = new Project();
            //project.Name = this.Name;
            
            //project.InputPlugin = new PluginConfiguration
            //    {
            //        PluginId = InputPlugin.Id,
            //        Configuration = InputPlugin.Configuration.GetConfiguration()
            //    };

            //foreach (var pluginModel in OutputConfigurationControls)
            //{
            //    project.OutputPlugins.Add(
            //        new PluginConfiguration
            //        {
            //            PluginId = pluginModel.Id,
            //            Configuration = pluginModel.Configuration.GetConfiguration()
            //        });
            //}

            return null;
        }

        private void InitializeCollections()
        {
            foreach (var inputPlugin in PluginLocator.Current.GetAllInputPlugins())
            {
                this.InputPlugins.Add(new PluginPreviewItem(inputPlugin));
            }

            foreach (var outputPlugin in PluginLocator.Current.GetAllOutputPlugins())
            {
                this.OutputPlugins.Add(new PluginPreviewItem(outputPlugin));
            }
        }
    }
}