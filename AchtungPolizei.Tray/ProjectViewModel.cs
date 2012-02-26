namespace AchtungPolizei.Tray
{
    using AchtungPolizei.Core;
    using AchtungPolizei.Plugins;

    public class ProjectViewModel : ViewModelBase<ProjectViewModel>
    {
        private string name;

        public ProjectViewModel(Project project)
        {
            this.Name = project.Name;
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
    }
}