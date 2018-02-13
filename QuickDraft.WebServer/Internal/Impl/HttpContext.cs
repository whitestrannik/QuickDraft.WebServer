using System.Net;

namespace QuickDraft.WebServer.Impl
{
    internal sealed class HttpContext : IHttpContext
    {
        private readonly HttpListenerContext _context;
        private readonly IHttpRequest _request;
        private string _responseHtml;

        internal HttpContext(HttpListenerContext context)
        {
            _context = context;
            _request = new HttpRequest(context.Request);
        }

        public IHttpRequest Request => _request;

        public string ResponseHtml { get; set; }
    }
}
