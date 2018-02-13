using System;

namespace QuickDraft.WebServer
{
    public interface IWorkflowStep : IDisposable
    {
        WorkUnitState Execute(IWorkUnit workflowContext);
    }
}
