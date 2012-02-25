using System;

namespace AchtungPolizei.Plugins
{
    public class StatusReceivedEventArgs : EventArgs
    {
        private readonly BuildStatus status;

        public StatusReceivedEventArgs(BuildStatus status)
        {
            this.status = status;
        }

        public BuildStatus Status
        {
            get { return status; }
        }
    }
}