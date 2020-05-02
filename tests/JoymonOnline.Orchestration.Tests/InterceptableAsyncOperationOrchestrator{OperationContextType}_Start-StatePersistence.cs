using FluentAssertions;
using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    public partial class InterceptableAsyncOperationOrchestrator_OperationContextType_Start
    {

        [TestMethod]
        public async Task WhenThereAre2OperationsAndInterceptorIsUsedWithStatePersistence_ShouldContinueFromLastCheckPoint()
        {
            //Arrange
            var asyncAreaOp = new AsyncCircleAreaFinderOperation();
            var asyncPeriOp = new AsyncCirclePerimeterFinderOperation();
            var asyncInterceptor = new AsyncStateManagementOperationInterceptor();
            IAsyncOperationOrchestrator<ExeContext> orchestrator = new InterceptableAsyncOperationOrchestrator<ExeContext>(
               new List<IAsyncOperation<ExeContext>>() { asyncAreaOp, asyncPeriOp },
                new List<IAsyncOperationInterceptable<ExeContext>>() { asyncInterceptor });
            //Act
            ExeContext inputContext = new ExeContext() { Radius = 4 };
            ExeContext resultContext;
            try
            {
                resultContext = await orchestrator.Start(inputContext);
            }
            catch
            {
                string deserialiedContext = File.ReadAllText(AsyncStateManagementOperationInterceptor.stateFilePath);
                inputContext = JsonConvert.DeserializeObject<ExeContext>(deserialiedContext);
                resultContext = await orchestrator.Start(inputContext);
            }
            //Assert
            asyncAreaOp.CanExecutionCount.Should().Be(2, "areaOp-CanExecuteCount mismatch");
            asyncAreaOp.ExecuteCount.Should().Be(1, "areaOp-ExecuteCount mismatch");
            asyncPeriOp.CanExecutionCount.Should().Be(2, "periOp-CanExecuteCount mismatch");
            asyncPeriOp.ExecuteCount.Should().Be(1, "periOp-ExecuteCount mismatch");
            resultContext.Area.Should().Be(50.24, "Area mismatch");
            resultContext.Perimeter.Should().Be(25.12, "Perimeter mismatch");
        }
    }
    public class AsyncStateManagementOperationInterceptor : IAsyncOperationInterceptable<ExeContext>
    {
        internal const string stateFilePath = "AsyncOpStateFromInterceptable.json";
        public int BeforeExecuteCallCount { get; set; }
        public int AfterExecuteCallCount { get; set; }
        async Task IAsyncOperationInterceptable<ExeContext>.AfterExecute(IAsyncOperation<ExeContext> op, ExeContext context)
        {
            await Task.FromResult(2); //Dummy to avoid warning
            AfterExecuteCallCount += 1;
            string serializedContext = JsonConvert.SerializeObject(context);
            File.WriteAllText(stateFilePath, serializedContext);
        }

        Task<bool> IAsyncOperationInterceptable<ExeContext>.BeforeExecute(IAsyncOperation<ExeContext> op, ExeContext context)
        {
            this.BeforeExecuteCallCount += 1;
            return Task.FromResult(true);
        }
    }
    internal class AsyncCircleAreaFinderOperation : IAsyncOperation<ExeContext>
    {
        internal int CanExecutionCount { get; set; }
        internal int ExecuteCount { get; set; }
        async Task<bool> IAsyncOperation<ExeContext>.CanExecute(ExeContext context)
        {
            this.CanExecutionCount += 1;
            //Skip the step if already done.
            return await Task.FromResult(context.AreaCalculated == false);
        }

        async Task<ExeContext> IAsyncOperation<ExeContext>.Execute(ExeContext context)
        {
            this.ExecuteCount += 1;
            context.Area = 3.14 * context.Radius * context.Radius;
            context.AreaCalculated = true;
            return await Task.FromResult(context);
        }
    }
    internal class AsyncCirclePerimeterFinderOperation : IAsyncOperation<ExeContext>
    {
        internal int CanExecutionCount { get; set; }
        internal int ExecuteCount { get; set; }
        async Task<bool> IAsyncOperation<ExeContext>.CanExecute(ExeContext context)
        {
            //simulate exception only for first time.
            this.CanExecutionCount += 1;
            if (this.CanExecutionCount == 1)
            {
                throw new Exception("First time failed");
            }
            return await Task.FromResult(context.PerimeterCalculated == false);
        }
        async Task<ExeContext> IAsyncOperation<ExeContext>.Execute(ExeContext context)
        {
            this.ExecuteCount += 1;
            context.Perimeter = 2 * 3.14 * context.Radius;
            context.PerimeterCalculated = true;
            return await Task.FromResult(context);
        }
    }
}