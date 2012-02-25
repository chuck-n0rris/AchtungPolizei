using System;
using System.Linq;
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
        private Timer timer;

        private bool isAuthenticated;
        private XmlDocument document;

        private HudsonPollerConfiguration configuration;

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
            timer.Dispose();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets plugin configuration.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public void SetConfiguration(ConfigurationBase configuration)
        {
            this.configuration = configuration as HudsonPollerConfiguration;
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

        /// <summary>
        /// Polls Hudson for status of latest project build.
        /// </summary>
        /// <param name="state">The state. Ignored.</param>
        private void Poll(object state)
        {
            if (StatusReceived == null)
            {
                return;
            }

            if (!isAuthenticated)
            {
                Authenticate();
                isAuthenticated = true;
            }

            ReadStatus();
            StatusReceived(this, new StatusReceivedEventArgs(
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

        private void ReadStatus()
        {
            document = new XmlDocument();
            document.LoadXml(client.DownloadString(string.Format(string.Format(
                "{0}job/{1}/lastBuild/api/xml", configuration.Address, configuration.Project))));
        }

        private void Authenticate()
        {
            client.Headers["Content-type"] = "application/x-www-form-urlencoded";
            client.UploadString(
                configuration.Address + "j_acegi_security_check",
                string.Format(
                    "j_username={0}&j_password={1}",
                    configuration.Username,
                    configuration.Password));
        }
    }
}
