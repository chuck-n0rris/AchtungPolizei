using System;
using System.Net;

namespace Altalerta.Core.Tools.Impl
{
    /// <summary>
    /// Web client with cookies enabled.
    /// </summary>
    internal class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = container;
            }

            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);
            if (response is HttpWebResponse)
            {
                container.Add((response as HttpWebResponse).Cookies);
            }

            return response;
        }
    }
}