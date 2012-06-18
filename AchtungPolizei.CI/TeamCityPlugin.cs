namespace AchtungPolizei.CI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AchtungPolizei.Plugins;
    using AchtungPolizei.Plugins.Interfaces;

    public class TeamCityPlugin : IInputPlugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamCityPlugin"/> class.
        /// </summary>
        public TeamCityPlugin()
        {
            this.States = new[]
                {
                   new PluginState { Name = "Success" },   
                   new PluginState { Name = "Failure" },   
                   new PluginState { Name = "None" }
                };

            this.CurrentState = this.States.ElementAt(2);
        }

        /// <summary>
        /// Occurs when input plugin state changed.
        /// </summary>
        public event EventHandler<StateChangedEventArgs> StateChanged;

        /// <summary>
        /// Gets the plugin identifier.
        /// </summary>
        public string Id
        {
            get
            {
                return "5e7e1a8f-a3c8-4ef1-8449-9680b21d45ef";
            }
        }

        /// <summary>
        /// Gets the name of the plugin.
        /// </summary>
        public string Name
        {
            get
            {
                return "TeamCity";
            }
        }

        /// <summary>
        /// Gets the description of the plugin.
        /// </summary>
        public string Description
        {
            get
            {
                return "TeamCity CI Plug-in";
            }
        }

        /// <summary>
        /// Gets the current plugin state.
        /// </summary>
        public PluginState CurrentState { get; private set; }

        /// <summary>
        /// Gets the enumeration of possible input plugin states.
        /// </summary>
        public IEnumerable<PluginState> States { get; private set; }

        /// <summary>
        /// Gets the type of the plugin settings model.
        /// The type should be inherited from <see cref="SettingsModel"/>.
        /// </summary>
        /// <returns>
        /// The type of the get settings model.
        /// </returns>
        public Type GetSettingsModelType()
        {
            return typeof(TeamCitySettingsModel);
        }

        /// <summary>
        /// Launches the plugin.
        /// </summary>
        /// <param name="settings">
        ///   The plugin settings model.
        /// </param>
        public void Launch(SettingsModel settings)
        {
            // var settingsModel = (TeamCitySettingsModel)settings;
        }
    }
}