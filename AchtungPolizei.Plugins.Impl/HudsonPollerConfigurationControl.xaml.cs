using System;

namespace AchtungPolizei.Plugins.Impl
{
    /// <summary>
    /// Interaction logic for HudsonPollerConfigurationControl.xaml
    /// </summary>
    public partial class HudsonPollerConfigurationControl : IConfigirationControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HudsonPollerConfigurationControl"/> class.
        /// </summary>
        public HudsonPollerConfigurationControl()
        {
            InitializeComponent();
        }

        #region IConfigirationControl Members

        /// <summary>
        /// The validate.
        /// </summary>
        /// <returns>
        /// The validate.
        /// </returns>
        public bool Validate()
        {
            return (DataContext as ViewModel).Validate();
        }

        /// <summary>
        /// The get configuration.
        /// </summary>
        /// <returns>
        /// </returns>
        public ConfigurationBase GetConfiguration()
        {
            var model = DataContext as ViewModel;
            return new HudsonPollerConfiguration
                       {
                           Address = model.Address, 
                           Username = model.Username, 
                           Password = model.Password, 
                           PollInterval = model.PollInterval, 
                           Project = model.Project
                       };
        }

        /// <summary>
        /// Gets the type of the configuration.
        /// </summary>
        /// <returns>Configuration type.</returns>
        public Type GetConfigurationType()
        {
            return typeof (HudsonPollerConfiguration);
        }

        #endregion

        #region Nested type: ViewModel

        /// <summary>
        /// View model for current control.
        /// </summary>
        public class ViewModel : ViewModelBase<ViewModel>
        {
            private string address;
            private string username;
            private string password;
            private int pollInterval;
            private string project;

            /// <summary>
            /// Gets or sets the address.
            /// </summary>
            /// <value>
            /// The address.
            /// </value>
            public string Address
            {
                get { return address; }
                set { ChangeProperty(x => x.Address, value); }
            }

            /// <summary>
            /// Gets or sets the username.
            /// </summary>
            /// <value>
            /// The username.
            /// </value>
            public string Username
            {
                get { return username; }
                set { ChangeProperty(x => x.Username, value); }
            }

            /// <summary>
            /// Gets or sets the password.
            /// </summary>
            /// <value>
            /// The password.
            /// </value>
            public string Password
            {
                get { return password; }
                set { ChangeProperty(x => x.Password, value); }
            }

            /// <summary>
            /// Gets or sets the poll interval.
            /// </summary>
            /// <value>
            /// The poll interval.
            /// </value>
            public int PollInterval
            {
                get { return pollInterval; }
                set { ChangeProperty(x => x.PollInterval, value); }
            }

            /// <summary>
            /// Gets or sets Project.
            /// </summary>
            public string Project
            {
                get { return project; }
                set { ChangeProperty(x => x.Project, value); }
            }

            public string AddressValidator()
            {
                return Uri.IsWellFormedUriString(address, UriKind.Absolute)
                           ? null
                           : "Bad address.";
            }

            public string UsernameValidator()
            {
                return string.IsNullOrWhiteSpace(username)
                           ? "Empty username."
                           : null;
            }

            public string PasswordValidator()
            {
                return string.IsNullOrWhiteSpace(password)
                           ? "Empty password."
                           : null;
            }

            public string PollIntervalValidator()
            {
                return pollInterval < 500
                           ? "Poll interval is less than 500 ms."
                           : null;
            }

            public string ProjectValidator()
            {
                return string.IsNullOrWhiteSpace(project)
                           ? "Empty project."
                           : null;
            }
        }

        #endregion
    }
}