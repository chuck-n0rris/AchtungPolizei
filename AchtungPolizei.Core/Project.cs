using System.Collections.Generic;

namespace AchtungPolizei.Core
{
    public class Project
    {
        public Project()
        {
            OutputPlugins = new List<PluginConfiguration>();
        }

        public string Name { get; set; }

        public PluginConfiguration InputPlugin { get; set; }

        public List<PluginConfiguration> OutputPlugins { get; set; }
    }
}