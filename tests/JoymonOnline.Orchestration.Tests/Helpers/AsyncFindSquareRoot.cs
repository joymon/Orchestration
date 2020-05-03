using JoymonOnline.Orchestration.Core;
using System;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    internal class AsyncFindSquareRoot : IAsyncOperation<int>
    {
        internal DateTime TimeWhenCanExecuteStarted { get; set; }
        internal DateTime TimeWhenExecuteStarted { get; set; }
        async Task<bool> IAsyncOperation<int>.CanExecute(int context)
        {
            this.TimeWhenCanExecuteStarted = DateTime.UtcNow;
            await Task.Delay(2000);
            return await Task.FromResult(true);
        }

        async Task<int> IAsyncOperation<int>.Execute(int context)
        {
            this.TimeWhenExecuteStarted = DateTime.UtcNow;
            return await Task.FromResult(Convert.ToInt32(Math.Sqrt(context)));
        }
    }
}
