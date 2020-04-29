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
            IOperationOrchestrator<int> orch = new InterceptableOperationOrchestrator<int>(
                new OperationsProvider(),GetInterceptors());
            orch.Start(20);
        }

        private IEnumerable<IOperationInterceptable<int>> GetInterceptors()
        {
            yield return new TestInterceptor();
        }
    }
    class TestInterceptor : IOperationInterceptable<int>
    {
        void IOperationInterceptable<int>.After(int context)
        {
            
        }

        void IOperationInterceptable<int>.Before(int context)
        {
            
        }
    }
}
