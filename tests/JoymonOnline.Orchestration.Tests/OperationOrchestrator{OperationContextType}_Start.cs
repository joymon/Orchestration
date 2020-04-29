using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
    public class OperationOrchestrator_OperationContextType_Start
    {
        [TestMethod]
        public void WhenInNormalCondition_ShouldWork()
        {
            IOperationOrchestrator<int> orchestrator =
                new OperationOrchestrator<int>(new List<IOperation<int>>() { new FindSquare() });
            orchestrator.Start(10);
        }
        [TestMethod]
        public void When2OperationsAreUsed_ShouldWork()
        {
            IOperationOrchestrator<int> orchestrator = new OperationOrchestrator<int>(new List<IOperation<int>>()
            {
                new FindSquare(),
                new FindSquareRoot()
            });
            orchestrator.Start(10);
        }
        [TestMethod]
        public void WhenContextIsProvided_ShouldFindSumAndAverage()
        {
            IOperationOrchestrator<CalculationContext> orchestrator = new OperationOrchestrator<CalculationContext>(
                new List<IOperation<CalculationContext>>() {
                    new FindSumOperation(),
                    new FindAverageOperation()
                });
            CalculationContext context = new CalculationContext() { Numbers = new int[] { 1, 2, 3, 6 } };
            orchestrator.Start(context);
            Console.WriteLine("Sum={0},Average={1}", context.Sum, context.Average);
        }
        [TestMethod]
        public void WhenBackgroundOperationsListProviderIsUsed_ShouldSucceed()
        {
            IOperationOrchestrator<int> orchestrator =
                new OperationOrchestrator<int>(new OperationsProvider());
            orchestrator.Start(10);
        }
    }
    internal class FindSumOperation : IOperation<CalculationContext>
    {
        void IOperation<CalculationContext>.Execute(CalculationContext context)
        {
            context.Sum = context.Numbers.Sum();
        }
    }
    internal class FindAverageOperation : IOperation<CalculationContext>
    {
        void IOperation<CalculationContext>.Execute(CalculationContext context)
        {
            context.Average = context.Sum / context.Numbers.Length;
        }
    }
    internal class CalculationContext
    {
        public int[] Numbers { get; set; }
        public int Sum { get; set; }
        public int Average { get; set; }
    }

    internal class FindSquareRoot : IOperation<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Console.WriteLine("SquareRoot of {0} is {1}", context, Math.Sqrt(context));
        }
    }

    public class FindSquare : IOperation<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Console.WriteLine("Square of {0} is {1}", context, context * context);
        }
    }
    public class OperationsProvider : IOperationsProvider<int>
    {
        IEnumerable<IOperation<int>> IOperationsProvider<int>.GetOperations()
        {
            return new List<IOperation<int>>() { new FindSquare() };
        }
    }
}
