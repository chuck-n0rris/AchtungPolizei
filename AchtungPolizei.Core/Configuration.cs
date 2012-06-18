namespace AchtungPolizei.Core
{
    using System.Collections.Generic;
    using AchtungPolizei.Plugins;

    /// <summary>
    /// This class contains an information about few bunched plugins. 
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Gets or sets the name of the configuration.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the input plugin identifier.
        /// </summary>
        public string InputPluginId { get; set; }

        /// <summary>
        /// Gets or sets the state binders.
        /// </summary>
        public List<StateBinder> StateBinders { get; set; }

        /// <summary>
        /// Gets or sets the input plugin settings.
        /// </summary>
        public SettingsModel InputPluginSettings { get; set; }
    }
}