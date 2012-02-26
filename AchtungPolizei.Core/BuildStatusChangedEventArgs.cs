using AchtungPolizei.Plugins;

namespace AchtungPolizei.Core
{
    using System;

    public class BuildStatusChangedEventArgs : EventArgs
    {
        public Project Project { get; set; }
        public BuildState BuildState { get; set; }
        public BuildStatus BuildStatus { get; set; }
    }
}