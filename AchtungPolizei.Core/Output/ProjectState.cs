using AchtungPolizei.Plugins;

namespace AchtungPolizei.Core
{
    public struct ProjectState
    {
        public BuildState BuildState { get; set; }

        public BuildStatus BuildStatus { get; set; }
    }
}