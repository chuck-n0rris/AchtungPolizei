namespace AchtungPolizei.Tray
{
    using System.Windows.Controls;
    using System.Windows.Media;

    using AchtungPolizei.Core;
    using AchtungPolizei.Plugins;

    public class ProjectViewModel : ViewModelBase<ProjectViewModel>
    {
        private string name;

        public ProjectViewModel(Project project)
        {
            this.Name = project.Name;
            this.StateColor = new SolidColorBrush(Colors.LimeGreen);
        }

        private SolidColorBrush stateColor;

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