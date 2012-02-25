namespace AchtungPolizei.Plugins.Impl
{
    /// <summary>
    /// Represents configuration for Hudson CI system poller.
    /// </summary>
    public class HudsonPollerConfiguration : ConfigurationBase
    {
        private const string addressKey = "address";
        private const string usernameKey = "username";
        private const string passwordKey = "password";
        private const string projectKey = "project";
        private const string intervalKey = "interval";

        /// <summary>
        /// Gets or sets the address, e.g. http://hudson.example.com/ 
        /// (please, note the trailing slash).
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address
        {
            get { return GetParameter(addressKey, "http://hudson.example.com/"); }
            set { Parameters[addressKey] = value; }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get { return GetParameter(usernameKey, "username"); }
            set { Parameters[usernameKey] = value; }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get { return GetParameter(passwordKey, "password"); }
            set { Parameters[passwordKey] = value; }
        }

        /// <summary>
        /// Gets or sets the name of project.
        /// </summary>
        /// <value>
        /// The name of project.
        /// </value>
        public string Project
        {
            get { return GetParameter(projectKey, "project"); }
            set { Parameters[projectKey] = value; }
        }

        /// <summary>
        /// Gets or sets the poll interval.
        /// </summary>
        /// <value>
        /// The poll interval.
        /// </value>
        public int PollInterval
        {
            get { return GetParameter(intervalKey, 10000); } 
            set { Parameters[intervalKey] = value.ToString(); }
        }
    }
}