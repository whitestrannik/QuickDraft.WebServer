using QuickDraft.WebServer.Impl.WorkflowSteps;
using System;
using System.Collections.Generic;

namespace QuickDraft.WebServer
{
    public static class WebServerFactory
    {
        public static IWebServer CreateWebServer(IEnumerable<string> prefixes, IEnumerable<Func<IWorkflowStep>> workflowStepFactories)
        {
            return new Impl.WebServer(prefixes, workflowStepFactories);
        }

        public static IWorkflowStep CreateConsoleLoggingWorkflowStep()
        {
            return new ConsoleLoggingWorkflowStep();
        }

        public static IWorkflowStep CreateBlackListWorkflowStep(IEnumerable<string> blackList)
        {
            return new BlackListWorkflowStep(blackList);
        }

        public static IWorkflowStep CreateConcurrencyWorkflowStep()
        {
            return new ConcurrencyWorkflowStep();
        }
    }
}
