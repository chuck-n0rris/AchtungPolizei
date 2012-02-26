using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AchtungPolizei.Core.Helpers;
using AchtungPolizei.Plugins;

namespace AchtungPolizei.Core
{
    using System.Collections.Generic;

    public class Engine
    {
        private static Engine engine;

        private Engine()
        {
            
        }

        public static Engine Current
        {
            get { return engine ?? (engine = new Engine()); }
        }

        private readonly List<Project> projects = new List<Project>();

        private readonly List<ProjectAgent> projectAgents = new List<ProjectAgent>();

        private readonly OutputQue consumer = new OutputQue();

        private readonly ProjectsRepository projectsRepository = new ProjectsRepository();

        public string GetPluginDirectory()
        {
            var assemblyFile = new FileInfo(Assembly.GetExecutingAssembly().FullName);
            string assemblyDirectory = assemblyFile.Directory.FullName;
            return Path.Combine(assemblyDirectory, "Plugins");
        }

        public void Start()
        {
            PluginLocator.Initialize(GetPluginDirectory());
            // this.projects = projectsRepository.GetProjects().ToList();
            this.consumer.Start();

            // ActivateProjectsAgents(projects);
        }

        public void AddProject(Project project)
        {
            projects.Add(project);
            ActivateProjectAgent(project);
            projectsRepository.SaveProjects(projects);
        }

        private void ActivateProjectsAgents(IEnumerable<Project> projects)
        {
            foreach (var project in projects)
            {
                ActivateProjectAgent(project);
            }
        }

        private void ActivateProjectAgent(Project project)
        {
            var plugin = (IInputPlugin)PluginLocator.Current.GetInstanceById(project.InputPlugin.PluginId);

            var agent = new ProjectAgent
                {
                    Project = project,
                    InputPlugin = plugin,
                    IsSuccessfulLastTime = true    // Default.
                };

            plugin.StatusReceived += ProjectStatusReceived;
            plugin.SetConfiguration(project.InputPlugin.Configuration);

            projectAgents.Add(agent);
        }

        private void ProjectStatusReceived(object sender, StatusReceivedEventArgs e)
        {
            lock (this)
            {
                ProjectAgent agent = projectAgents.First(a => object.ReferenceEquals(a.InputPlugin, sender));
                bool isSuccessfulNow = e.State.IsSuccessful;
                BuildStatus? buildStatus = null;

                if (agent.IsSuccessfulLastTime && !isSuccessfulNow)
                {
                    buildStatus = BuildStatus.Broken;
                }
                else if (!agent.IsSuccessfulLastTime && !isSuccessfulNow)
                {
                    buildStatus = BuildStatus.StillBroken;
                }
                else if (!agent.IsSuccessfulLastTime && isSuccessfulNow)
                {
                    buildStatus = BuildStatus.Fixed;
                }

                agent.IsSuccessfulLastTime = isSuccessfulNow;

                if (buildStatus != null)
                {
                    QueueOutputEvent(agent.Project, e.State, buildStatus.Value);
                }
            }
        }

        private void QueueOutputEvent(Project project, BuildState buildState, BuildStatus buildStatus)
        {
            this.consumer.Add(new ProcessRequest
                                  {
                                      Project = project,
                                      ProjectState = new ProjectState
                                        {
                                            BuildState = buildState,
                                            BuildStatus = buildStatus
                                        }
                                  });
            OnBuildStatusChanged(project, buildState, buildStatus);
        }

        public event EventHandler<BuildStatusChangedEventArgs> BuildStatusChanged;

        public void OnBuildStatusChanged(Project project, BuildState buildState, BuildStatus buildStatus)
        {
            if (BuildStatusChanged != null)
            {
                BuildStatusChanged(this, new BuildStatusChangedEventArgs
                    {
                        Project = project,
                        BuildState = buildState,
                        BuildStatus = buildStatus
                    });
            }
        }

        public IEnumerable<Project> Projects
        {
            get { return projects; }
        }
    }
}