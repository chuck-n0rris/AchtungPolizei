namespace AchtungPolizei.Plugins
{
    using System;

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
        /// Sets plugin configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        void SetConfiguration(ConfigurationBase configuration);

        /// <summary>
        /// Returns UI control for editing of configuration.
        /// </summary>
        /// <returns>
        /// UI control instance.
        /// </returns>
        IConfigirationControl GetConfigControl();
    }
}