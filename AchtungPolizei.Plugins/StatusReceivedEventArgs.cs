using System;

namespace AchtungPolizei.Plugins
{
    public class StatusReceivedEventArgs : EventArgs
    {
        private readonly BuildState state;

        public StatusReceivedEventArgs(BuildState state)
        {
            this.state = state;
        }

        public BuildState State
        {
            get { return state; }
        }
    }
}