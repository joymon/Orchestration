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
    public class OperationOrchestrator_OperationContextType_Tests
    {
        [TestMethod]
        public void WhenInNormalCondition_ShouldWork()
        {
            IOperationOrchestrator<int> orchestrator = 
                new OperationOrchestrator<int>(new List<IOperation<int>>() { new FindSquare() });
            orchestrator.Start(10);
        }
        [TestMethod]
        public void WhenBackgroundOperationsListProviderIsUsed_ShouldSucceed()
        {
            IOperationOrchestrator<int> orchestrator =
                new OperationOrchestrator<int>( new OperationsProvider());
            orchestrator.Start(10);
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
