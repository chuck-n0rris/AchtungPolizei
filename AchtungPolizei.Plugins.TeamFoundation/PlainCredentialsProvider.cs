namespace AchtungPolizei.Plugins.TeamFoundation
{
    using System;
    using System.Net;

    using Microsoft.TeamFoundation.Client;

    public class PlainCredentialsProvider : ICredentialsProvider
    {
        private readonly string userName;

        private readonly string password;

        public PlainCredentialsProvider(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public ICredentials GetCredentials(Uri uri, ICredentials failedCredentials)
        {
            return new NetworkCredential(userName, password);
        }

        public void NotifyCredentialsAuthenticated(Uri uri)
        {
        }
    }
}