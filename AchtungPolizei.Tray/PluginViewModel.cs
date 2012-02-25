namespace AchtungPolizei.Tray
{
    using System;

    using AchtungPolizei.Plugins;

    public class PluginViewModel : ViewModelBase
    {
        private Guid id;
        private string name;
        private IConfigirationControl configiration;

        public PluginViewModel()
        {
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
        
        public IConfigirationControl Configiration
        {
            get
            {
                return this.configiration;
            }

            set
            {
                this.configiration = value;
                this.RaisePropertyChanged("Configiration");
            }
        }

        public Guid Id
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
                this.RaisePropertyChanged("Id");
            }
        }
    }
}