using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AchtungPolizei.Plugins.Impl
{
    public class LightOutputPlugin : IOutputPlugin
    {
        private readonly Guid guid = Guid.Parse("7C9641FC-A6DE-4854-B583-CCD559C6C037");
        private LightPluginConfiguration configuration;

        public string DeviceName { get; private set; }
        public string SocketName { get; private set; }
        public string ExecutablePath { get; private set; }
        public int Miliseconds { get; private set; }

        #region IOutputPlugin Members

        public void Dispose()
        {
            StopPlay();
        }

        public Guid Id
        {
            get { return guid; }
        }

        public string Name
        {
            get { return "Light output plugin"; }
        }

        public void SetConfiguration(ConfigurationBase configuration)
        {
            this.configuration = (LightPluginConfiguration) configuration;

            this.DeviceName = this.configuration.Device;
            this.ExecutablePath = this.configuration.Path;
            this.SocketName = this.configuration.Socket;
            this.Miliseconds = this.configuration.Miliseconds;
        }

        public IConfigirationControl GetConfigControl()
        {
            var model = new LightSettingsModel();

            model.Initialize(this.configuration);

            var view = new LightSettingsView
            {
                DataContext = model
            };

            return view;
        }

        public IConfigirationControl GetConfigControl(ConfigurationBase config)
        {
            var model = new LightSettingsModel();

            model.Initialize((LightPluginConfiguration)config);

            var view = new LightSettingsView
            {
                DataContext = model
            };

            return view;
        }

        public Task Start(BuildState state, BuildStatus status)
        {
            return Task.Factory.StartNew(() =>
            {
                StartPlay();
                Thread.Sleep(Miliseconds);
                StopPlay();
            });
        }

        #endregion

        private void StartPlay()
        {
            if (ExecutablePath != null)
            {
                Process.Start(ExecutablePath, BuildArguments("ON"));
            }
        }

        private string BuildArguments(string switchState)
        {
            const string cmdArgumentsPattern = @"-{0} -{1} -{2}";
            string arguments = String.Format(cmdArgumentsPattern, switchState, DeviceName, SocketName);
            return arguments;
        }

        private void StopPlay()
        {
            if (ExecutablePath != null)
            {
                Process.Start(ExecutablePath, BuildArguments("Off"));
            }
        }

        public void Close()
        {
            Dispose();
        }
    }
}