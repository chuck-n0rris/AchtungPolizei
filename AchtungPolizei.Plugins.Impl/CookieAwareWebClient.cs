using System;
using System.Net;

namespace AchtungPolizei.Plugins.Impl
{
    /// <summary>
    /// Web client with cookies enabled.
    /// </summary>
    class CookieAwareWebClient : WebClient
    {
        private readonly CookieContainer container = new CookieContainer();

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = container;
            }

            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = base.GetWebResponse(request);
            if (response is HttpWebResponse)
            {
                container.Add((response as HttpWebResponse).Cookies);
            }

            return response;
        }
    }
}