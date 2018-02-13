using System;
using System.Collections.Generic;

namespace QuickDraft.WebServer.Executable
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var server = WebServerFactory.CreateWebServer(
                new string[] { "http://*:8080/" },
                new Func<IWorkflowStep>[]
                {
                    WebServerFactory.CreateConsoleLoggingWorkflowStep,
                    () => WebServerFactory.CreateBlackListWorkflowStep(EnumerateBlackAddresses),
                    WebServerFactory.CreateConcurrencyWorkflowStep
                }))
            {
                server.Start();
                Console.ReadKey();
                server.Stop();
            }
        }

        private static IEnumerable<string> EnumerateBlackAddresses
        {
            get
            {
                yield return "localhost\black1";
                yield return "localhost\black2";
            }
        }
    }
}
