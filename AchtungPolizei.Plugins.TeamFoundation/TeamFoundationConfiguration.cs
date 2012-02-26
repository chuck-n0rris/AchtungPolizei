namespace AchtungPolizei.Plugins.TeamFoundation
{
    public class TeamFoundationConfiguration : ConfigurationBase
    {
        private const string intervalKey = "interval";
        private const string usernameKey = "username";
        private const string passwordKey = "password";
        private const string collectionUriKey = "collectionUri";
        private const string projectKey = "project";
        private const string buildDefinitionKey = "buildDefinition";

        /// <summary>
        /// Gets or sets build definition name.
        /// </summary>
        /// <value>
        /// Build definition name.
        /// </value>
        public string BuildDefinition
        {
            get { return GetParameter(buildDefinitionKey, "HelloWorld"); }
            set { parameters[buildDefinitionKey] = value; }
        }

        /// <summary>
        /// Gets or sets team project.
        /// </summary>
        /// <value>
        /// Team project.
        /// </value>
        public string Project
        {
            get { return GetParameter(projectKey, "HelloWorld"); }
            set { parameters[projectKey] = value; }
        }
        
        /// <summary>
        /// Gets or sets collection URI
        /// </summary>
        /// <value>
        /// Collection URI
        /// </value>
        public string CollectionUri
        {
            get { return GetParameter(collectionUriKey, "http://bars-laptop:8080/tfs/defaultcollection"); }
            set { parameters[collectionUriKey] = value; }
        }
        
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        /// <value>
        /// The username.
        /// </value>
        public string Username
        {
            get { return GetParameter(usernameKey, "test"); }
            set { parameters[usernameKey] = value; }
        }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password
        {
            get { return GetParameter(passwordKey, "test"); }
            set { parameters[passwordKey] = value; }
        }

        /// <summary>
        /// Gets or sets the poll interval.
        /// </summary>
        /// <value>
        /// The poll interval.
        /// </value>
        public int PollInterval
        {
            get { return GetParameter(intervalKey, 5000); }
            set { parameters[intervalKey] = value.ToString(); }
        }
    }
}