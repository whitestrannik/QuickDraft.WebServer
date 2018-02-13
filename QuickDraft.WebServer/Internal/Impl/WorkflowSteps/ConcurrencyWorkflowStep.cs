using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickDraft.WebServer.Impl.WorkflowSteps
{
    internal sealed class ConcurrencyWorkflowStep : IWorkflowStep
    {
        private readonly BlockingCollection<IWorkUnit> _requestsList;
        private IEnumerable<Task> _tasks;

        public ConcurrencyWorkflowStep() : this(20)
        {
        }

        public ConcurrencyWorkflowStep(int concurrencyLevel)
        {
            _requestsList = new BlockingCollection<IWorkUnit>();

            CreateHandlerThreads(concurrencyLevel);
        }

        private void CreateHandlerThreads(int concurrencyLevel)
        {
            _tasks = Enumerable.Range(0, concurrencyLevel).Select(_ => Task.Run(() => HandleRequest()));
        }

        private void HandleRequest()
        {
            foreach (var item in _requestsList.GetConsumingEnumerable())
            {
                item.Process();
            }
        }

        public WorkUnitState Execute(IWorkUnit workflowUnit)
        {
            _requestsList.Add(workflowUnit);
            return WorkUnitState.Puased;
        }

        public void Dispose()
        {
            _requestsList.CompleteAdding();
            Task.WhenAll(_tasks).Wait();
            _requestsList.Dispose();
        }
    }
}
