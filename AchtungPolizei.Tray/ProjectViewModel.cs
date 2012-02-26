namespace AchtungPolizei.Tray
{
    using System.Windows.Media;

    using AchtungPolizei.Core;
    using AchtungPolizei.Plugins;

    public class ProjectViewModel : ViewModelBase<ProjectViewModel>
    {
        private readonly Project internalProject;
        private SolidColorBrush stateColor;
        private BuildStatus buildStatus;

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

        public BuildStatus BuildStatus
        {
            get
            {
                return this.buildStatus;
            }

            set
            {
                this.buildStatus = value;
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

        public void SetStatus(BuildStatus status)
        {
            this.BuildStatus = status;

            switch (status)
            {
                case BuildStatus.Broken:
                    StateColor = new SolidColorBrush(Colors.Red);
                    break;

                case BuildStatus.StillBroken:
                    StateColor = new SolidColorBrush(Colors.DarkRed);
                    break;

                case BuildStatus.Fixed:
                    StateColor = new SolidColorBrush(Colors.LimeGreen);
                    break;
            }
        }
    }
}