using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
        public void WhenNonGenericStartIsUsed_ShouldWorkWithDefaultValue()
        {
            var Mock = new Mock<IOperation<int>>();

            IOperationOrchestrator<int> orchestrator =
                new OperationOrchestrator<int>(new List<IOperation<int>>() { Mock.Object });
            orchestrator.Start();
            Mock.Verify(op => op.Execute(0), Times.Once);
        }
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
        public void WhenOperationsListProviderIsUsed_ShouldSucceed()
        {
            IOperationOrchestrator<int> orchestrator =
                new OperationOrchestrator<int>(new OperationsProvider());
            orchestrator.Start(10);
        }
    }
   
}
