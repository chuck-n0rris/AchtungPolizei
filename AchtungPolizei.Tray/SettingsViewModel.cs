namespace AchtungPolizei.Tray
{
    using System.Collections.ObjectModel;

    public class SettingsViewModel : ViewModelBase
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