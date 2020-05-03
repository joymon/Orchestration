using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using JoymonOnline.Orchestration.Triggers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
    public class PeriodicBackgroundOperationOrchestrator_OperationContextType_Stop
    {
        [TestMethod]
        public void WhenBackgroundOperationsListProviderIsUsed_ShouldSucceed()
        {
            IOperationOrchestrator<int> orchestrator =
                new PeriodicBackgroundOperationOrchestrator<int>(new PeriodicBackgroundOperationsProvider(),
                new TimerBasedTrigger(2000));
            orchestrator.Start(10);
            Thread.Sleep(10000);
        }
    }
}
