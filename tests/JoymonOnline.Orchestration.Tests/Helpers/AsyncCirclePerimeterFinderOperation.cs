using JoymonOnline.Orchestration.Core;
using System;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    internal class AsyncCirclePerimeterFinderOperation : IAsyncOperation<ExeContext>
    {
        internal int CanExecutionCount { get; set; }
        internal int ExecuteCount { get; set; }
        async Task<bool> IAsyncOperation<ExeContext>.CanExecute(ExeContext context)
        {
            //simulate exception only for first time.
            this.CanExecutionCount += 1;
            if (this.CanExecutionCount == 1)
            {
                throw new Exception("First time failed");
            }
            return await Task.FromResult(context.PerimeterCalculated == false);
        }
        async Task<ExeContext> IAsyncOperation<ExeContext>.Execute(ExeContext context)
        {
            this.ExecuteCount += 1;
            context.Perimeter = 2 * 3.14 * context.Radius;
            context.PerimeterCalculated = true;
            return await Task.FromResult(context);
        }
    }
}