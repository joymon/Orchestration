using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
    public class AsyncOperationOrchestrator_OperationContextType_New
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenObjectCreatedWithOperationListNull_ThrowArgumentNullException()
        {
            IAsyncOperationOrchestrator<int> orchestrator =
                new AsyncOperationOrchestrator<int>(null);
            _ = await orchestrator.Start(9);
        }

    }
}