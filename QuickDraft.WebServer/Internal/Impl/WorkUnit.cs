using System;

namespace QuickDraft.WebServer.Impl
{
    internal sealed class WorkUnit : IWorkUnit
    {
        private readonly IHttpContext _context;
        private readonly IWorkflow _workflow;

        private int _currentWorkflowIndex;
        private WorkUnitState _currentState;

        public WorkUnit(IHttpContext context, IWorkflow workflow)
        {
            _context = context;
            _workflow = workflow;
        }

        public IHttpContext HttpContext => _context;

        public bool IsException => _currentState == WorkUnitState.Fail;

        public bool IsPaused => _currentState == WorkUnitState.Puased;

        public void Process()
        {
            try
            {
                using (var enumerator = _workflow.GetItemEnumerator(_currentWorkflowIndex))
                {
                    while (enumerator.MoveNext())
                    {
                        _currentState = enumerator.Current.Execute(this);
                        _currentWorkflowIndex++;

                        if (_currentState != WorkUnitState.Run)
                        {
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                OnException(ex, string.Empty);
            }
        }

        private void OnException(Exception ex, string empty)
        {
            _currentState = WorkUnitState.Fail;
        }

        public void SetException(Exception ex, string message)
        {
            OnException(ex, message);
        }
    }
}
