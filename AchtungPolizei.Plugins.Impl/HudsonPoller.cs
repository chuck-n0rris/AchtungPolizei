using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml;

namespace AchtungPolizei.Plugins.Impl
{
    /// <summary>
    /// Polls Hudson CI for status of project build.
    /// </summary>
    public class HudsonPoller : IInputPlugin
    {
        private readonly CookieAwareWebClient client = new CookieAwareWebClient();
        private readonly XmlDocument document = new XmlDocument();
        private HudsonPollerConfiguration configuration = new HudsonPollerConfiguration();
        private bool isAuthenticated;
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HudsonPoller"/> class.
        /// </summary>
        public HudsonPoller()
        {
            // authentication request completed
            client.UploadStringCompleted += AuthenticationCompleted;

            // poll request completed
            client.DownloadStringCompleted += PollCompleted;
        }

        /// <summary>
        /// Occurs when build status is received from CI.
        /// </summary>
        public event EventHandler<StatusReceivedEventArgs> StatusReceived;

        /// <summary>
        /// Plugin identifier.
        /// </summary>
        public Guid Id
        {
            get { return Guid.Parse("d207bd29-40a1-41f7-b5f2-d5144b47e99a"); }
        }

        /// <summary>
        /// Plugin name.
        /// </summary>
        public string Name
        {
            get { return "Hudson Poller"; }
        }

        /// <summary>
        /// Frees allocated resources.
        /// </summary>
        public void Dispose()
        {
            if (timer != null)
            {
                timer.Dispose();
            }

            client.Dispose();
        }

        /// <summary>
        /// Returns UI control for editing of configuration.
        /// </summary>
        /// <returns>
        /// UI control instance.
        /// </returns>
        public IConfigirationControl GetConfigControl()
        {
            var control = new HudsonPollerConfigurationControl();
            control.DataContext = new HudsonPollerConfigurationControl.ViewModel
                                      {
                                          Address = configuration.Address,
                                          Username = configuration.Username,
                                          Password = configuration.Password,
                                          PollInterval = configuration.PollInterval,
                                          Project = configuration.Project
                                      };
            return control;
        }

        /// <summary>
        /// Sets plugin configuration.
        /// </summary>
        /// <param name="instance">The configuration.</param>
        public void SetConfiguration(ConfigurationBase instance)
        {
            configuration = instance as HudsonPollerConfiguration;
            if (configuration == null)
            {
                throw new ApplicationException("Configuration can not be null.");
            }

            RestartTimer();
        }

        private void RestartTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
            }

            timer = new Timer(Poll, null, 0, configuration.PollInterval);
        }

        private void Poll(object state)
        {
            if (StatusReceived == null)
            {
                return;
            }

            if (!isAuthenticated)
            {
                Authenticate();
            }
            else
            {
                ReadStatus();                
            }
        }

        private void Authenticate()
        {
            client.Headers["Content-type"] = "application/x-www-form-urlencoded";
            client.UploadStringAsync(
                new Uri(configuration.Address + "j_acegi_security_check"),
                string.Format(
                    "j_username={0}&j_password={1}",
                    configuration.Username,
                    configuration.Password));
        }

        private void ReadStatus()
        {
            var address = string.Format(
                "{0}job/{1}/lastBuild/api/xml",
                configuration.Address,
                configuration.Project);
            client.DownloadStringAsync(new Uri(address));
        }

        private void AuthenticationCompleted(object sender, UploadStringCompletedEventArgs args)
        {
            if (args.Error != null)
            {
                throw args.Error;
            }

            isAuthenticated = true;

            Poll(null);
        }

        private void PollCompleted(object sender, DownloadStringCompletedEventArgs args)
        {
            if (StatusReceived == null)
            {
                return;
            }

            if (args.Error != null)
            {
                throw args.Error;
            }

            document.LoadXml(args.Result);

            StatusReceived(
                this,
                new StatusReceivedEventArgs(
                    new BuildState
                        {
                            Authors = GetAuthors(),
                            IsSuccessful = GetIsSuccessful(),
                            Number = GetNumber(),
                            Project = configuration.Project,
                            Time = GetTime()
                        }));
        }

        private DateTime GetTime()
        {
            return new DateTime(1970, 1, 1)
                .AddMilliseconds(double.Parse(
                    document
                        .SelectSingleNode("freeStyleBuild/timestamp")
                        .InnerText));
        }

        private int GetNumber()
        {
            return int.Parse(
                document
                    .SelectSingleNode("freeStyleBuild/number")
                    .InnerText);
        }

        private bool GetIsSuccessful()
        {
            return document
                       .SelectSingleNode("freeStyleBuild/result")
                       .InnerText == "SUCCESS";
        }

        private string[] GetAuthors()
        {
            return document
                .SelectNodes("//changeSet/item/author/fullName")
                .Cast<XmlNode>()
                .Select(x => x.InnerText)
                .ToArray();
        }
    }
}
