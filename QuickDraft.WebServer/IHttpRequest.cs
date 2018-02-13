using System;
using System.IO;

namespace QuickDraft.WebServer
{
    public interface IHttpRequest
    {
        Uri Url { get; }

        Stream InputStream { get; }
    }
}