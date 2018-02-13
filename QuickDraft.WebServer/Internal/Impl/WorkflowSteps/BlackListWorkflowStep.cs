using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace QuickDraft.WebServer.Impl.WorkflowSteps
{
    internal sealed class BlackListWorkflowStep : IWorkflowStep
    {
        private readonly ConcurrentDictionary<string, string> _blackList;

        public BlackListWorkflowStep(IEnumerable<string> blackList)
        {
            _blackList = new ConcurrentDictionary<string, string>(blackList.Select(i => new KeyValuePair<string, string>(i, i)));
        }

        public void Dispose()
        {
        }

        public WorkUnitState Execute(IWorkUnit workflowUnit)
        {
            var result = _blackList.ContainsKey(workflowUnit.HttpContext.Request.Url.AbsolutePath);

            if (result)
            {
                throw new WebServerException("Url is in the black list.");
            }

            return WorkUnitState.Run;
        }
    }
}
