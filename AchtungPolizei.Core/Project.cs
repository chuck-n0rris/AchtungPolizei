using System;
using System.Collections.Generic;
using AchtungPolizei.Plugins;

namespace AchtungPolizei.Core
{
    public class Project
    {
        public string Name { get; set; }

        public Guid InputPluginId { get; set; }

        public List<Guid> OutputPluginIds { get; set; }
    }
}