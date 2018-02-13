using System.Collections.Generic;

namespace QuickDraft.WebServer
{
    internal interface IWorkflow
    {
        IEnumerator<IWorkflowStep> GetItemEnumerator(int startIndex);
    }
}
