namespace AchtungPolizei.Core
{
    using AchtungPolizei.Plugins;

    /// <summary>
    /// The information of output plugin.
    /// </summary>
    public class OutputPluginInfo
    {
        /// <summary>
        /// Gets or sets the output plugin identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        public SettingsModel Settings { get; set; }
    }
}