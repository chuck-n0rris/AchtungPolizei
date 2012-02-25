namespace AchtungPolizei.Plugins.Impl
{
    /// <summary>
    /// Represents configuration for Hudson CI system poller.
    /// </summary>
    public class HudsonPollerConfiguration : ConfigurationBase
    {
        /// <summary>
        /// Gets or sets the address, e.g. http://hudson.example.com/ 
        /// (please, note the trailing slash).
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address
        {
            get { return Parameters["address"]; }
            set { Parameters["address"] = value; }
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get { return Parameters["username"]; }
            set { Parameters["username"] = value; }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get { return Parameters["password"]; }
            set { Parameters["password"] = value; }
        }

        /// <summary>
        /// Gets or sets the name of project.
        /// </summary>
        /// <value>
        /// The name of project.
        /// </value>
        public string Project
        {
            get { return Parameters["project"]; }
            set { Parameters["project"] = value; }
        }

        /// <summary>
        /// Gets or sets the poll interval.
        /// </summary>
        /// <value>
        /// The poll interval.
        /// </value>
        public int PollInterval
        {
            get { return int.Parse(Parameters["interval"]); } 
            set { Parameters["interval"] = value.ToString(); }
        }
    }
}