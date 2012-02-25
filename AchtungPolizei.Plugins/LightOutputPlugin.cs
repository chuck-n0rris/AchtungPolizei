using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AchtungPolizei.Plugins
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
            this.DeviceName = "Device2";
            this.SocketName = "Socket1";
            this.ExecutablePath = @"C:\Program Files\Gembird\Power Manager\pm.exe";
            this.Miliseconds = 10000;
        }

        public LightOutputPlugin(ConfigurationBase configuration)
        {
        }

        private void StartPlay()
        {
            Process.Start(ExecutablePath, BuildArguments("on"));
        }

        private string BuildArguments(string switchState)
        {
            const string cmdArgumentsPattern = @"-{0} -{1} -{2}";

            string arguments = String.Format(cmdArgumentsPattern, switchState, this.SocketName, this.DeviceName);
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
            Process.Start(ExecutablePath, BuildArguments("off"));
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
            throw new NotImplementedException();
        }

        public IConfigirationControl GetConfigControl()
        {
            throw new NotImplementedException();
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