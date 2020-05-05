using JoymonOnline.Orchestration.Core;
using System;
using System.Linq;
using System.Threading;

namespace JoymonOnline.Orchestration.Tests
{
    internal class FindSumOperation : IOperation<CalculationContext>
    {
        internal int DelayAfterOperationInMS { get; set; }
        void IOperation<CalculationContext>.Execute(CalculationContext context)
        {
            Console.WriteLine($"{nameof(FindSumOperation)}.Execute - Start {DateTime.UtcNow}");
            context.Sum = context.Numbers.Sum();
            Thread.Sleep(DelayAfterOperationInMS);
            Console.WriteLine($"{nameof(FindSumOperation)}.Execute - Sleep completed {DateTime.UtcNow}");
        }
    }
}
