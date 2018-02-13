namespace QuickDraft.WebServer
{
    public interface IWorkUnit
    {
        IHttpContext HttpContext { get; }

        void Process();

        bool IsException { get; }

        bool IsPaused { get; }

        //void SetState(WorkUnitState state);

        //void SetException(Exception ex, string message);
    }
}