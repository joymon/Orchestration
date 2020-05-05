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
               new List<IAsyncOperation<ExeContext>>() { 
                   asyncAreaOp, 
                   asyncPeriOp },
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
}