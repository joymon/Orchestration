using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    internal class AsyncCircleAreaFinder : StatePersistedAsyncOperation
    {
        internal int CanExecutionCount { get; set; }
        internal int ExecuteCount { get; set; }
        async protected override Task<bool> CanExecuteInternal(ExeContext context)
        {
            this.CanExecutionCount += 1;
            //Skip the step if already done.
            return await Task.FromResult(context.AreaCalculated == false);
        }

        async protected override Task<ExeContext> ExecuteInternal(ExeContext context)
        {
            this.ExecuteCount += 1;
            context.Area = 3.14 * context.Radius * context.Radius;
            context.AreaCalculated = true;
            return await Task.FromResult(context);
        }
    }
}
