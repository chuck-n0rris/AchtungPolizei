using System.Collections.Generic;
using AchtungPolizei.Core;
using System.Linq;

namespace AchtungPolizei.Tray
{
    using System.Collections.ObjectModel;

    using AchtungPolizei.Plugins;

    public class SettingsViewModel : ViewModelBase<SettingsViewModel>
    {
        private ObservableCollection<ProjectViewModel> projects;

        public SettingsViewModel(ObservableCollection<ProjectViewModel> projects)
        {
            Projects = projects;
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