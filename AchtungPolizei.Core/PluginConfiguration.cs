namespace AchtungPolizei.Core
{
    using System;
    using AchtungPolizei.Plugins;

    public class PluginConfiguration
    {
        public Guid PluginId { get; set; }

        public ConfigurationBase Configuration { get; set; }
    }
}