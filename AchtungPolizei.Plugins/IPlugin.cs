using System;

namespace AchtungPolizei.Plugins
{
    /// <summary>
    /// Base interface for plugin.
    /// </summary>
    public interface IPlugin : IDisposable
    {
        /// <summary>
        /// Plugin identifier.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Plugin name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Plugin configuration.
        /// </summary>
        ConfigurationBase Configuration { get; set; }

        /// <summary>
        /// Returns UI control for editing of configuration.
        /// </summary>
        /// <returns>
        /// UI control instance.
        /// </returns>
        IConfigirationControl GetConfigControl();
    }
}