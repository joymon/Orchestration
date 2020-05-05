using System;
using System.Collections.Generic;
using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Interceptors;
using JoymonOnline.Orchestration.Orchestrators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
    public class ConsoleLoggingOperationInterceptor_Before
    {
        [TestMethod]
        public void WhenOneInterceptorIsGiven_ItShouldWork()
        {
            TestInterceptor interceptor = new TestInterceptor();
            IOperationOrchestrator<int> orch = new InterceptableOperationOrchestrator<int> (
                new OperationsProvider(),
                new List<IOperationInterceptable<int>>() { new ConsoleLoggingOperationInterceptor<int>() }
                );

            orch.Start(20);
        }
    }
}
