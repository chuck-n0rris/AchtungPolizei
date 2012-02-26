using System.Threading.Tasks;
using AchtungPolizei.Plugins;

namespace AchtungPolizei.Core
{
    public class ExecutionStage
    {
        public BuildState BuildState { get; set; }
        public BuildStatus BuildStatus { get; set; }
        public IOutputPlugin OutputPlugin { get; set; }

        public ExecutionStage(BuildState buildState, BuildStatus buildStatus, IOutputPlugin outputPlugin)
        {
            BuildState = buildState;
            BuildStatus = buildStatus;
            OutputPlugin = outputPlugin;
        }

        public Task Run()
        {
            return OutputPlugin.Start(this.BuildState, this.BuildStatus);
        }
    }
}