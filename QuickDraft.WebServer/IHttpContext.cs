namespace QuickDraft.WebServer
{
    public interface IHttpContext
    {
        IHttpRequest Request { get; }

        string ResponseHtml { get; set; }
    }
}
