using System.Threading.Tasks;
using DoenaSoft.AbstractionLayer.UI.Contracts;

namespace DoenaSoft.AbstractionLayer.Threading
{
    internal sealed class DispatcherOperation : IDispatcherOperation
    {
        private readonly System.Windows.Threading.DispatcherOperation _actual;

        public object Result
            => _actual.Result;

        public DispatcherStatus Status
            => (DispatcherStatus)_actual.Status;

        public Task Task
            => _actual.Task;

        public DispatcherOperation(System.Windows.Threading.DispatcherOperation actual)
        {
            _actual = actual;
        }
    }
}
