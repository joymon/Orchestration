using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JoymonOnline.Orchestration.Orchestrators;
using JoymonOnline.Orchestration.Core;
using System.Collections.Generic;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
    public class InterceptableOperationOrchestration_OperationContextType_Start
    {
        [TestMethod]
        public void WhenOneInterceptorIsGiven_ItShouldWork()
        {
            TestInterceptor interceptor = new TestInterceptor();
            IOperationOrchestrator<int> orch = new InterceptableOperationOrchestrator<int>(
                new OperationsProvider(), new List<IOperationInterceptable<int>>() { interceptor });

            orch.Start(20);

            Assert.IsTrue(interceptor.DoesInterceptedInAfterMethod, "Didnt intercepted the After method");
            Assert.IsTrue(interceptor.DoesInterceptedInAfterMethod, "Didnt intercepted the Before method");
        }
    }
    class TestInterceptor : IOperationInterceptable<int>
    {
        public bool DoesInterceptedInBeforeMethod { get; set; }
        public bool DoesInterceptedInAfterMethod { get; set; }
        void IOperationInterceptable<int>.After(int context)
        {
            this.DoesInterceptedInAfterMethod = true;
        }

        void IOperationInterceptable<int>.Before(int context)
        {
            this.DoesInterceptedInBeforeMethod = true;
        }
    }
}
