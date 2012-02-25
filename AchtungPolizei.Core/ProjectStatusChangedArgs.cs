namespace AchtungPolizei.Core
{
    using System;

    using AchtungPolizei.Plugins;

    public class ProjectStatusChangedArgs : EventArgs
    {
        public Project Project { get; set; }

        public BuildStatus Status { get; set; }
    }
}