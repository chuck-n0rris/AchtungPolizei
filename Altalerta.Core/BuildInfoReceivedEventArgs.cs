using System;

namespace Altalerta.Core
{
    public class BuildInfoReceivedEventArgs : EventArgs
    {
        public BuildInfoReceivedEventArgs(BuildInfo info)
        {
            Info = info;
        }

        public BuildInfo Info { get; private set; }
    }
}