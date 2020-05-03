using System;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    internal class AsyncCirclePerimeterFinder : StatePersistedAsyncOperation
    {
        internal int CanExecutionCount { get; set; }
        internal int ExecuteCount { get; set; }
        async protected override Task<bool> CanExecuteInternal(ExeContext context)
        {
            //simulate exception only for first time.
            this.CanExecutionCount += 1;
            if (this.CanExecutionCount == 1)
            {
                throw new Exception("First time failed");
            }
            return await Task.FromResult(context.PerimeterCalculated == false);
        }

        async protected override Task<ExeContext> ExecuteInternal(ExeContext context)
        {
            this.ExecuteCount += 1;
            context.Perimeter = 2 * 3.14 * context.Radius;
            context.PerimeterCalculated = true;
            return await Task.FromResult(context);
        }
    }
}
