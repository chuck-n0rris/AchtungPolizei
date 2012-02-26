using AchtungPolizei.Core;

namespace AchtungPolizei.Tray
{
    /// <summary>
    /// The project view model.
    /// </summary>
    public class ProjectViewModel : ViewModelBase
    {
        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectViewModel"/> class.
        /// </summary>
        /// <param name="project">
        /// The project.
        /// </param>
        public ProjectViewModel(Project project)
        {
            Name = project.Name;
        }

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name
        {
            get { return name; }

            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
    }
}