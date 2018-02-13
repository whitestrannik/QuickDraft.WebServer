using System;

namespace QuickDraft.WebServer
{
    public interface IWebServer : IDisposable
    {
        void Start();

        void Stop();
    }
}
