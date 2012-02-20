using System;
using System.Linq;
using System.Xml;

namespace HudsonPoller
{
    /// <summary>
    /// Polls Hudson CI for status of project build.
    /// </summary>
    class HudsonPoller : IDisposable
    {
        private readonly string address;
        private readonly string username;
        private readonly string password;
        
        private readonly CookieAwareWebClient client = new CookieAwareWebClient();
        private bool isAuthenticated;

        private XmlDocument document;

        /// <summary>
        /// Initializes a new instance of the <see cref="HudsonPoller"/> class.
        /// </summary>
        /// <param name="address">The address of Hudson 
        /// (e.g. http://hudson.example.com/ - note trailing slash).</param>
        /// <param name="username">The username to authenticate.</param>
        /// <param name="password">The password to authenticate.</param>
        public HudsonPoller(string address, string username, string password)
        {
            this.address = address;
            this.username = username;
            this.password = password;
        }

        /// <summary>
        /// Polls Hudson for status of latest project build.
        /// </summary>
        /// <param name="project">Name of project.</param>
        /// <returns>Status of latest build for given project.</returns>
        public BuildStatus Poll(string project)
        {
            if (!isAuthenticated)
            {
                Authenticate();
                isAuthenticated = true;
            }

            ReadStatus(project);

            return new BuildStatus
                {
                    Authors = GetAuthors(),
                    IsSuccessful = GetIsSuccessful(),
                    Number = GetNumber(),
                    Project = project,
                    Time = GetTime()
                };
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

        private void ReadStatus(string project)
        {
            document = new XmlDocument();
            document.LoadXml(client.DownloadString(string.Format(string.Format(
                "{0}job/{1}/lastBuild/api/xml", address, project))));
        }

        private void Authenticate()
        {
            client.Headers["Content-type"] = "application/x-www-form-urlencoded";
            client.UploadString(
                address + "j_acegi_security_check", 
                string.Format("j_username={0}&j_password={1}", username, password));
        }

        /// <summary>
        /// Performs application-defined tasks associated with 
        /// freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            client.Dispose();
        }
    }
}