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

        public SettingsViewModel(IEnumerable<Project> projects)
        {
            Projects = new ObservableCollection<ProjectViewModel>(projects.Select(x => new ProjectViewModel(x)));
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