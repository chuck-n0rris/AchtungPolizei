using System;
using System.Threading;
using Microsoft.TeamFoundation.Build.Client;
using Microsoft.TeamFoundation.Client;

namespace AchtungPolizei.Plugins.TeamFoundation
{
    public class TeamFoundationPlugin : IInputPlugin
    {
        private TeamFoundationConfiguration configuration = new TeamFoundationConfiguration();

        private Timer timer;

        private IBuildServer buildServer;

        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
            }
        }

        public Guid Id
        {
            get { return new Guid("9ee352a2-02d5-4bfb-aa7a-4da845ff52df"); }
        }

        public string Name
        {
            get { return "Team Foundation"; }
        }

        public void SetConfiguration(ConfigurationBase newConfiguration)
        {
            if (timer != null)
            {
                timer.Change(Timeout.Infinite, Timeout.Infinite);
            }

            configuration = newConfiguration as TeamFoundationConfiguration;
            if (configuration == null)
            {
                throw new ApplicationException("Configuration can not be null.");
            }


            var credentialProvider = new PlainCredentialsProvider(configuration.Username, configuration.Password);

            var teamProjectCollection = new TfsTeamProjectCollection(new Uri(configuration.CollectionUri), credentialProvider);
            buildServer = teamProjectCollection.GetService<IBuildServer>();


            if (timer == null)
            {
                timer = new Timer(Tick, null, 0, configuration.PollInterval);
            }
            else
            {
                timer.Change(0, configuration.PollInterval);
            }
            
        }

        private void Tick(object state)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);

            var buildDefinition = buildServer.GetBuildDefinition(configuration.Project, configuration.BuildDefinition);
            var buildUri = buildDefinition.LastBuildUri;
            var build = buildServer.GetBuild(buildUri);
            bool isSuccess = !build.Status.HasFlag(Microsoft.TeamFoundation.Build.Client.BuildStatus.Failed);

            OnStatusReceived(isSuccess);

            timer.Change(0, configuration.PollInterval);
        }

        public IConfigirationControl GetConfigControl()
        {
            return new TeamFoundationSettingsControl(configuration);
        }

        public IConfigirationControl GetConfigControl(ConfigurationBase config)
        {
            return new TeamFoundationSettingsControl((TeamFoundationConfiguration)config);
        }

        private void OnStatusReceived(bool isSuccess)
        {
            var buildState = new BuildState
                                 {
                                     IsSuccessful = isSuccess
                                 };
            if (StatusReceived != null)
            {
                StatusReceived(this, new StatusReceivedEventArgs(buildState));
            }
        }

        public event EventHandler<StatusReceivedEventArgs> StatusReceived;
    }
}