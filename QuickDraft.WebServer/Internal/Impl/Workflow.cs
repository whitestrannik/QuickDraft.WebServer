using System;
using System.Collections;
using System.Collections.Generic;

namespace QuickDraft.WebServer.Impl
{
    internal sealed class Workflow : IWorkflow
    {
        private readonly IList<IWorkflowStep> _items;

        public Workflow(IList<IWorkflowStep> items)
        {
            _items = items;
        }

        public IEnumerator<IWorkflowStep> GetItemEnumerator(int startIndex)
        {
            return new ItemEnumerator(this, startIndex);
        }

        private struct ItemEnumerator : IEnumerator<IWorkflowStep>
        {
            private readonly Workflow _workflow;
            private int _currentIndex;
            private IWorkflowStep _current;

            public ItemEnumerator(Workflow workflow, int startIndex)
            {
                _workflow = workflow;
                _currentIndex = startIndex - 1;
                _current = null;
            }

            public IWorkflowStep Current => _current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _currentIndex++;
                var result = _currentIndex < _workflow._items.Count;

                if (result)
                {
                    _current = _workflow._items[_currentIndex];
                }

                return result;
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }
        }
    }
}
