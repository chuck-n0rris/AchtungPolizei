using AchtungPolizei.Plugins;

namespace AchtungPolizei.Core
{
    public class ProjectAgent
    {
        public Project Project { get; set; }

        public IInputPlugin InputPlugin { get; set; }

        public bool IsSuccessfulLastTime { get; set; }
    }
}