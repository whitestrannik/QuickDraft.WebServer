using System;

namespace QuickDraft.WebServer.Impl.WorkflowSteps
{
    internal sealed class ConsoleLoggingWorkflowStep : IWorkflowStep
    {
        public void Dispose()
        {
        }

        public WorkUnitState Execute(IWorkUnit workflowContext)
        {
            Console.WriteLine(workflowContext.HttpContext.Request.Url);
            return WorkUnitState.Run;
        }
    }
}
