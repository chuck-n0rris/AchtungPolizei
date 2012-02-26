namespace AchtungPolizei.Tray
{
    using System.Windows.Media;

    using AchtungPolizei.Core;
    using AchtungPolizei.Plugins;

    public class ProjectViewModel : ViewModelBase<ProjectViewModel>
    {
        private readonly Project internalProject;
        private SolidColorBrush stateColor;

        private string name;

        public ProjectViewModel(Project internalProject)
        {
            this.internalProject = internalProject;
            this.Name = internalProject.Name;

            this.StateColor = new SolidColorBrush(Colors.LimeGreen);
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

        public SolidColorBrush StateColor
        {
            get
            {
                return this.stateColor;
            }
            set
            {
                this.stateColor = value;
                this.RaisePropertyChanged("StateColor");
            }
        }

        public Project Source { get { return internalProject; } }
    }
}