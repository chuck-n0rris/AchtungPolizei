namespace AchtungPolizei.Plugins.Interfaces
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface to be implemented by plugin components.
    /// </summary>
    public interface IInputPlugin : IPlugin
    {
        /// <summary>
        /// Occurs when input plugin state changed.
        /// </summary>
        event EventHandler<StateChangedEventArgs> StateChanged;
        
        /// <summary>
        /// Gets the enumeration of possible input plug-in states.
        /// </summary>
        IEnumerable<PluginState> States { get; }

        /// <summary>
        /// Gets the state of the current.
        /// </summary>
        PluginState CurrentState { get; }

        /// <summary>
        /// Gets the type of the plugin settings model. 
        /// The type should be inherited from <see cref="SettingsModel"/>.
        /// </summary>
        /// <returns>
        /// The type of the get settings model.
        /// </returns>
        Type GetSettingsModelType();

        /// <summary>
        /// Launches the plugin.
        /// </summary>
        /// <param name="settings">
        ///   The plugin settings model.
        /// </param>
        void Launch(SettingsModel settings);
    }
}