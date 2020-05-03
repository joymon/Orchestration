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
    public partial class InterceptableAsyncOperationOrchestrator_OperationContextType_Start
    {
        [TestMethod]
        public async Task WhenThereIsOneOperationAndInterceptorIsUsed_BeforeExecuteOfInterceptorShouldBeCalled()
        {
            //Arrange
            var asyncOp = new AsyncFindSquareRoot();
            var asyncInterceptor = new AsyncOperationInterceptor<int>();
            IAsyncOperationOrchestrator<int> orchestrator = new InterceptableAsyncOperationOrchestrator<int>(
                new List<IAsyncOperation<int>>() { asyncOp },
                new List<IAsyncOperationInterceptable<int>>() { asyncInterceptor });
            //Act
            int square = await orchestrator.Start(9);
            //Assert
            asyncInterceptor.BeforeExecuteCallCount.Should().BeGreaterThan(0);

        }
        [TestMethod]
        public async Task WhenThereIsOneOperationAndInterceptorIsUsed_AfterExecuteOfInterceptorShouldBeCalled()
        {
            //Arrange
            var asyncOp = new AsyncFindSquareRoot();
            var asyncInterceptor = new AsyncOperationInterceptor<int>();
            IAsyncOperationOrchestrator<int> orchestrator = new InterceptableAsyncOperationOrchestrator<int>(
                new List<IAsyncOperation<int>>() { asyncOp },
                new List<IAsyncOperationInterceptable<int>>() { asyncInterceptor });
            //Act
            int square = await orchestrator.Start(9);
            //Assert
            asyncInterceptor.BeforeExecuteCallCount.Should().BeGreaterThan(0);
        }
        [TestMethod]
        public async Task WhenThereIsOneOperationAndInterceptorIsUsedWhichCancel_ExecuteShouldNotBeCalled()
        {
            //Arrange
            var asyncOp = new AsyncCircleAreaFinderOperation();
            var asyncInterceptor = new AsyncOperationInterceptor<ExeContext>() { CancelViaBeforeExecute = true };
            IAsyncOperationOrchestrator<ExeContext> orchestrator = new InterceptableAsyncOperationOrchestrator<ExeContext>(
                new List<IAsyncOperation<ExeContext>>() { asyncOp },
                new List<IAsyncOperationInterceptable<ExeContext>>(){ asyncInterceptor });
            //Act
            ExeContext square = await orchestrator.Start(new ExeContext());
            //Assert
            asyncOp.ExecuteCount.Should().Be(0);
        }

    }
    internal class AsyncOperationInterceptor<OperationContextType> : IAsyncOperationInterceptable<OperationContextType>
    {
        internal bool CancelViaBeforeExecute { get; set; }
        public int BeforeExecuteCallCount { get; set; }
        public int AfterExecuteCallCount { get; set; }
        async Task IAsyncOperationInterceptable<OperationContextType>.AfterExecute(IAsyncOperation<OperationContextType> op, OperationContextType context)
        {
            await Task.FromResult(2); //Dummy to avoid warning
            AfterExecuteCallCount += 1;
        }

        Task<bool> IAsyncOperationInterceptable<OperationContextType>.BeforeExecute(IAsyncOperation<OperationContextType> op, OperationContextType context)
        {
            this.BeforeExecuteCallCount += 1;
            if (this.CancelViaBeforeExecute)
            {
                return Task.FromResult(false);
            }
            else
            {
                return Task.FromResult(true);
            }
        }
    }
}