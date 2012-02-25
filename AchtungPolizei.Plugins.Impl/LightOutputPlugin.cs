using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AchtungPolizei.Plugins.Impl
{
    public class LightOutputPlugin : IOutputPlugin
    {
        public string DeviceName { get; private set; }
        public string SocketName { get; private set; }
        public string ExecutablePath { get; private set; }
        public int Miliseconds { get; private set; }

        private readonly Guid guid = Guid.Parse("7C9641FC-A6DE-4854-B583-CCD559C6C037");
        
        public LightOutputPlugin()
        {
            this.DeviceName = "DEVICE2";
            this.SocketName = "SOCKET1";
            this.ExecutablePath = @"""C:\Program Files\Gembird\Power Manager\pm.exe""";
            this.Miliseconds = 10000;
        }

        private void StartPlay()
        {
            //Process.Start(new ProcessStartInfo())
            Process.Start(ExecutablePath, BuildArguments("ON"));
        }

        private string BuildArguments(string switchState)
        {
            const string cmdArgumentsPattern = @"-{0} -{1} -{2}";
            string arguments = String.Format(cmdArgumentsPattern, switchState, this.DeviceName, this.SocketName);
            return arguments;
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            StopPlay();
        }

        private void StopPlay()
        {
            Process.Start(ExecutablePath, BuildArguments("Off"));
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
            
        }

        public IConfigirationControl GetConfigControl()
        {
            var model = new LightSettingsModel();
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
                Thread.Sleep(this.Miliseconds);
                Close();
            });
        }
    }
}