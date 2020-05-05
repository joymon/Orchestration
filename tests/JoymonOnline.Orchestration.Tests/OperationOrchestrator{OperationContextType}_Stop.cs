using FluentAssertions;
using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
    public class OperationOrchestrator_OperationContextType_Stop
    {
        [TestMethod]
        async public Task WhenStopCalled_ShouldStopExecution()
        {
            IOperationOrchestrator<CalculationContext> or = new OperationOrchestrator<CalculationContext>(
                    new List<IOperation<CalculationContext>>()
                    {
                    
                    new FindSumOperation(){ DelayAfterOperationInMS=3000 },
                    new FindAverageOperation()
                    });

            CalculationContext context = new CalculationContext()
            {
                Numbers = new int[] { 1, 2, 3, 6 }
            };
            
            Task stopTask= Task.Run(async () =>
            {
                await Task.Delay(1000);
                or.Stop();
                Console.WriteLine($"Stopped at {DateTime.UtcNow}");
            });
            Task opTask= Task.Run(()=>or.Start(context));

            await Task.WhenAll(stopTask, opTask);

            context.Sum.Should().Be(12);
            context.Average.Should().Be(0);

        }
    }
}
