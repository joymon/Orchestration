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
    public class InterceptableAsyncOperationOrchestrator_OperationContextType_Start
    {
        [TestMethod]
        public async Task WhenThereIsOneOperationAndInterceptorIsUsed_BeforeExecuteOfInterceptorShouldBeCalled()
        {
            //Arrange
            var asyncOp = new AsyncFindSquareRoot();
            var asyncInterceptor = new AsyncOperationInterceptor();
            IAsyncOperationOrchestrator<int> orchestrator = new InterceptableAsyncOperationOrchestrator<int>(
                new List<IAsyncOperation<int>>() { asyncOp },
                new List<IAsyncOperationInterceptable<int>>() { asyncInterceptor });
            //Act
            int square = await orchestrator.Start(9);
            //Assert
            asyncInterceptor.BeforeExecuteCallCount.Should().BeGreaterThan(0);

        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenThereIsOneOperationAndInterceptorIsUsed_AfterExecuteOfInterceptorShouldBeCalled()
        {
            //Arrange
            var asyncOp = new AsyncFindSquareRoot();
            var asyncInterceptor = new AsyncOperationInterceptor();
            IAsyncOperationOrchestrator<int> orchestrator = new InterceptableAsyncOperationOrchestrator<int>(
                new List<IAsyncOperation<int>>() { asyncOp },
                new List<IAsyncOperationInterceptable<int>>() { asyncInterceptor });
            //Act
            int square = await orchestrator.Start(9);
            //Assert
            asyncInterceptor.BeforeExecuteCallCount.Should().BeGreaterThan(0);
        }
    }
    public class AsyncOperationInterceptor : IAsyncOperationInterceptable<int>
    {
        public int BeforeExecuteCallCount { get; set; }
        public int AfterExecuteCallCount { get; set; }
        async Task IAsyncOperationInterceptable<int>.AfterExecute(IAsyncOperation<int> op, int context)
        {
            await Task.FromResult(2); //Dummy to avoid warning
            AfterExecuteCallCount += 1;
        }

        Task<bool> IAsyncOperationInterceptable<int>.BeforeExecute(IAsyncOperation<int> op, int context)
        {
            this.BeforeExecuteCallCount += 1;
            return Task.FromResult(true);
        }
    }
}