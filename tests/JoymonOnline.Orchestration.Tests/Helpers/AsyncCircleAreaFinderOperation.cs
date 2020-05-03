using JoymonOnline.Orchestration.Core;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    internal class AsyncCircleAreaFinderOperation : IAsyncOperation<ExeContext>
    {
        internal int CanExecutionCount { get; set; }
        internal int ExecuteCount { get; set; }
        async Task<bool> IAsyncOperation<ExeContext>.CanExecute(ExeContext context)
        {
            this.CanExecutionCount += 1;
            //Skip the step if already done.
            return await Task.FromResult(context.AreaCalculated == false);
        }

        async Task<ExeContext> IAsyncOperation<ExeContext>.Execute(ExeContext context)
        {
            this.ExecuteCount += 1;
            context.Area = 3.14 * context.Radius * context.Radius;
            context.AreaCalculated = true;
            return await Task.FromResult(context);
        }
    }
}