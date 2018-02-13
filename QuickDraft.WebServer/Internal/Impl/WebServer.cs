using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace QuickDraft.WebServer.Impl
{
    internal sealed class WebServer : IWebServer
    {
        private readonly HttpListener _httpListener;
        private readonly Workflow _workflow;
        private readonly List<IWorkflowStep> _workflowSteps;
        private CancellationTokenSource _cts;
        private Task _listenerTask;

        public WebServer(IEnumerable<string> prefixes, IEnumerable<Func<IWorkflowStep>> workflowStepFactories)
        {
            _workflowSteps = workflowStepFactories.Select(f => f()).ToList();

            _httpListener = new HttpListener();
            foreach (var prefix in prefixes)
            {
                _httpListener.Prefixes.Add(prefix);
            }
            
            _workflow = new Workflow(_workflowSteps);
        }

        public void Dispose()
        {
            Stop();

            foreach (var step in _workflowSteps)
            {
                step.Dispose();
            }
        }

        public void Start()
        {
            if (_httpListener.IsListening)
            {
                throw new InvalidOperationException("Server has already started.");
            }

            _httpListener.Start();

            _cts = new CancellationTokenSource();
            _listenerTask = Task.Run(() => InputRequestHandler(_cts.Token));
        }

        public void Stop()
        {
            if (!_httpListener.IsListening)
            {
                throw new InvalidOperationException("Server has already stopped.");
            }

            _httpListener.Stop();

            _cts.Cancel();
            _listenerTask.Wait();

            _cts.Dispose();
        }

        private void InputRequestHandler(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var httpContext = new HttpContext(_httpListener.GetContext());
                var workUnit = new WorkUnit(httpContext, _workflow);
                workUnit.Process();
            }
        }
    }
}
