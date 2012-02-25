using AchtungPolizei.Plugins;

namespace AchtungPolizei.Core
{
    using System;
    using System.Collections.Generic;

    public class Engine
    {
        private List<Project> projects = new List<Project>();

        private List<Type> pluginTypes = new List<Type>();

        private List<IInputPlugin> projectsInputPlugins = new List<IInputPlugin>();

        public event EventHandler<ProjectStatusChangedArgs> ProjectStatusChanged;

        public void Start(List<Project> projects)
        {
            this.projects = projects;
        }

        

        private void ActivateInputPlugin(Project project)
        {
            
        }

        public IEnumerable<Project> Projects
        {
            get { return projects; }
        }
    }
}