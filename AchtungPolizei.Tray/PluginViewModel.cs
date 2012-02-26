namespace AchtungPolizei.Tray
{
    using System;
    using AchtungPolizei.Plugins;

    public class PluginViewModel : ViewModelBase
    {
        private Guid id;
        private string name;
        private IConfigirationControl configuration;

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
        
        public IConfigirationControl Configuration
        {
            get
            {
                return this.configuration;
            }

            set
            {
                this.configuration = value;
                this.RaisePropertyChanged("Configuration");
            }
        }
    }
}