using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using JoymonOnline.Orchestration.Triggers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace TestProject2
{
    [TestClass]
    public class PeriodicBackgroundOperationOrchestrator_OperationContextType_Tests
    {
        [TestMethod]
        public void WhenInNormalCondition_ShouldWork()
        {
            IOperationOrchestrator<int> orchestrator =
                new PeriodicBackgroundOperationOrchestrator<int>(new List<IOperation<int>>() { new FindNextNumber() },
                new TimerBasedTrigger(2000));
            orchestrator.Start(10);
            Thread.Sleep(10000);
        }
        [TestMethod]
        public void WhenBackgroundOperationsListProviderIsUsed_ShouldSucceed()
        {
            IOperationOrchestrator<int> orchestrator =
                new PeriodicBackgroundOperationOrchestrator<int>( new PeriodicBackgroundOperationsProvider(),
                new TimerBasedTrigger(2000));
            orchestrator.Start(10);
            Thread.Sleep(10000);
        }
    }

    public class FindNextNumber : IOperation<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Trace.WriteLine(string.Format("Next number of {0} is {1}", context, context +1));
        }
    }
    public class PeriodicBackgroundOperationsProvider : IOperationsProvider<int>
    {
        IEnumerable<IOperation<int>> IOperationsProvider<int>.GetOperations()
        {
            return new List<IOperation<int>>() { new FindNextNumber() };
        }
    }
}
