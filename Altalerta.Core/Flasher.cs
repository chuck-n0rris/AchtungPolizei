﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Altalerta.Core
{
    public class Flasher : IOutputPlugin
    {
        public string Device
        {
            get { return Configuration["Device"]; }
        }

        public string Socket
        {
            get { return Configuration["Socket"]; }
        }

        public string ManagerPath
        {
            get { return Configuration["ManagerPath"]; }
        }

        public int Interval
        {
            get { return int.Parse(Configuration["Interval"]); }
        }

        #region IOutputPlugin Members

        public void Dispose()
        {
        }

        public string Name
        {
            get { return "Flasher"; }
        }

        public IDictionary<string, string> Configuration { get; set; }

        public Task Notify(BuildState state, BuildInfo info)
        {
            return Task.Factory.StartNew(
                () =>
                    {
                        Process.Start(ManagerPath, GetArguments(true));
                        Thread.Sleep(Interval);
                        Process.Start(ManagerPath, GetArguments(false));
                    });
        }

        #endregion

        private string GetArguments(bool on)
        {
            return string.Format("-{0} -{1} -{2}", on ? "ON" : "Off", Device, Socket);
        }
    }
}