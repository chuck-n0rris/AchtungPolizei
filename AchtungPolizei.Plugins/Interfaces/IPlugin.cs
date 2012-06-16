namespace AchtungPolizei.Plugins.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface to be implemented by plugin components.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the plugin.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the plugin configuration parameters.
        /// </summary>
        IEnumerable<PluginParameter> Parameters { get; } 
    }
}