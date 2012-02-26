using System;
using System.Collections.Generic;
using Altalerta.Core.Tools;
using Autofac;

namespace Altalerta.Core.Essential
{
    public class Engine : IDisposable
    {
        private readonly IPluginRepository pluginRepository;
        private readonly IProjectRepository projectRepository;

        private IEnumerable<Project> projects;

        public Engine(IPluginRepository pluginRepository, IProjectRepository projectRepository)
        {
            this.pluginRepository = pluginRepository;
            this.projectRepository = projectRepository;
        }

        #region IDisposable Members

        public void Dispose()
        {
            foreach (Project project in projects)
            {
                project.Dispose();
            }
        }

        #endregion

        public void Initialize(IContainer container)
        {
            pluginRepository.Discover(container);
            projects = projectRepository.GetAll();
            foreach (var project in projects)
            {
                project.Initialize(pluginRepository);
            }
        }
    }
}