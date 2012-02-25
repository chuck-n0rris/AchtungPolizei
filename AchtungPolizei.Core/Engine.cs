namespace AchtungPolizei.Core
{
    using System;
    using System.Collections.Generic;

    public class Engine
    {
        private List<Project> projects = new List<Project>();

        public event EventHandler<ProjectStatusChangedArgs> ProjectStatusChanged;

        public void Start(List<Project> projects)
        {
            this.projects = projects;
        }

        public IEnumerable<Project> Projects
        {
            get { return projects; }
        }
    }
}