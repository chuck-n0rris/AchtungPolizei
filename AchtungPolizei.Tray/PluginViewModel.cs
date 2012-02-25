namespace AchtungPolizei.Tray
{
    using AchtungPolizei.Plugins;

    public class PluginViewModel : ViewModelBase
    {
        private string name;
        private IConfigirationControl configiration;

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
    }
}