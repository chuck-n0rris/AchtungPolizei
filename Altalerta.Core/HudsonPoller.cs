using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Xml;

namespace Altalerta.Core
{
    /// <summary>
    /// Polls Hudson CI for status of project build.
    /// </summary>
    public class HudsonPoller : IInputPlugin
    {
        private readonly CookieAwareWebClient client = new CookieAwareWebClient();
        private readonly XmlDocument document = new XmlDocument();
        private bool authenticated;
        private IDictionary<string, string> configuration;
        private Timer timer;

        /// <summary>
        /// Initializes a new instance of the <see cref="HudsonPoller"/> class.
        /// </summary>
        public HudsonPoller()
        {
            client.UploadStringCompleted += AuthenticationCompleted;
            client.DownloadStringCompleted += PollCompleted;
        }

        public string Address
        {
            get { return Configuration["Address"]; }
        }

        public string Username
        {
            get { return Configuration["Username"]; }
        }

        public string Password
        {
            get { return Configuration["Password"]; }
        }

        public string Project
        {
            get { return Configuration["Project"]; }
        }

        public int Interval
        {
            get { return int.Parse(Configuration["Interval"]); }
        }

        #region IInputPlugin Members

        /// <summary>
        /// Plugin name.
        /// </summary>
        public string Name
        {
            get { return "Hudson Poller"; }
        }


        public IDictionary<string, string> Configuration
        {
            get { return configuration; }
            set
            {
                configuration = value;
                RestartTimer();
            }
        }

        public event EventHandler<BuildInfoReceivedEventArgs> BuildInfoReceived;

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

        #endregion

        private void RestartTimer()
        {
            if (timer != null)
            {
                timer.Dispose();
            }

            timer = new Timer(Poll, null, 0, Interval);
        }

        private void Poll(object state)
        {
            if (BuildInfoReceived == null)
            {
                return;
            }

            if (!authenticated)
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
                new Uri(Address + "j_acegi_security_check"),
                string.Format("j_username={0}&j_password={1}", Username, Password));
        }

        private void ReadStatus()
        {
            string address = string.Format(
                "{0}job/{1}/lastBuild/api/xml", Address, Project);
            client.DownloadStringAsync(new Uri(address));
        }

        private void AuthenticationCompleted(object sender, UploadStringCompletedEventArgs args)
        {
            if (args.Error != null)
            {
                throw args.Error;
            }

            authenticated = true;

            Poll(null);
        }

        private void PollCompleted(object sender, DownloadStringCompletedEventArgs args)
        {
            if (BuildInfoReceived == null)
            {
                return;
            }

            if (args.Error != null)
            {
                throw args.Error;
            }

            document.LoadXml(args.Result);

            BuildInfoReceived(
                this,
                new BuildInfoReceivedEventArgs(
                    new BuildInfo
                        {
                            Authors = GetAuthors(),
                            IsSuccessful = GetIsSuccessful(),
                            Number = GetNumber(),
                            Project = Project,
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