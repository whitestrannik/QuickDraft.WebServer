using System;
using System.IO;
using System.Net;

namespace QuickDraft.WebServer.Impl
{
    internal sealed class HttpRequest : IHttpRequest
    {
        private readonly HttpListenerRequest _request;

        public HttpRequest(HttpListenerRequest request)
        {
            _request = request;
        }

        public Uri Url => _request.Url;

        public Stream InputStream => _request.InputStream;
    }
}
