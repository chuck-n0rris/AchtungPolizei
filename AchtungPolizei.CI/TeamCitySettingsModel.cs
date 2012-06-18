namespace AchtungPolizei.CI
{
    using AchtungPolizei.Plugins;

    /// <summary>
    /// Settings model for <see cref="TeamCityPlugin"/> plugin.
    /// </summary>
    public class TeamCitySettingsModel : SettingsModel
    {
        /// <summary>
        /// Gets or sets the team city URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}