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
    public partial class AsyncOperationOrchestrator_OperationContextType_Start
    {
        const string stateFilePath = "AsyncOpState.json";
        [TestMethod]
        public async Task WhenStatePersistedAndCrashes_ShouldResumeFromLastCheckPoint()
        {
            var areaOp = new AsyncCircleAreaFinder();
            var perimeterOp = new AsyncCirclePerimeterFinder();
            IAsyncOperationOrchestrator<ExeContext> orchestrator = new AsyncOperationOrchestrator<ExeContext>(new List<IAsyncOperation<ExeContext>>() {
                    areaOp,
                    perimeterOp  });
            ExeContext inputContext = new ExeContext() { Radius = 4 };
            ExeContext resultContext;
            try
            {
                resultContext = await orchestrator.Start(inputContext);
            }
            catch
            {
                string deserialiedContext = File.ReadAllText(stateFilePath);
                inputContext = JsonConvert.DeserializeObject<ExeContext>(deserialiedContext);
                resultContext = await orchestrator.Start(inputContext);
            }

            Assert.AreEqual(2, areaOp.CanExecutionCount,"areaOp-CanExecuteCount mismatch");
            Assert.AreEqual(1, areaOp.ExecuteCount, "areaOp-ExecuteCount mismatch");
            Assert.AreEqual(2, perimeterOp.CanExecutionCount, "periOp-CanExecuteCount mismatch");
            Assert.AreEqual(1, perimeterOp.ExecuteCount,"periOp-ExecuteCount mismatch");
            Assert.AreEqual(50.24, resultContext.Area,"Area mismatch");
            Assert.AreEqual(25.12, resultContext.Perimeter,"Perimeter mismatch");
        }
    }
}
