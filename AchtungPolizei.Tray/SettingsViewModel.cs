namespace AchtungPolizei.Tray
{
    using System.Collections.ObjectModel;

    using AchtungPolizei.Plugins;

    public class SettingsViewModel : ViewModelBase<SettingsViewModel>
    {
        private ObservableCollection<ProjectViewModel> projects;

        public SettingsViewModel()
        {
            Projects = new ObservableCollection<ProjectViewModel>();
        }

        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                return this.projects;
            }

            set
            {
                this.projects = value;
                this.RaisePropertyChanged("Projects");
            }
        }
    }
}