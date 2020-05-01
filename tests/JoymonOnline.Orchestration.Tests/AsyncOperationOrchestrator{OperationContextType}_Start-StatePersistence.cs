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
    abstract internal class StatePersistedAsyncOperation: IAsyncOperation<ExeContext>
    {

        protected virtual Task<bool> CanExecuteInternal(ExeContext context)
        {
            return Task.FromResult(true);
        }
        abstract protected Task<ExeContext> ExecuteInternal(ExeContext context);
        #region Interface implementation
        async Task<bool> IAsyncOperation<ExeContext>.CanExecute(ExeContext context)
        {
            return await CanExecuteInternal(context);
        }

        async Task<ExeContext> IAsyncOperation<ExeContext>.Execute(ExeContext context)
        {
            ExeContext resultContext = await ExecuteInternal(context);
            Persist(resultContext);
            return resultContext;
        }
        #endregion
        private void Persist(ExeContext context)
        {
            string serializedContext= JsonConvert.SerializeObject(context);
            File.WriteAllText("AsyncOpState.json",serializedContext);
        }

    }
    internal class ExeContext
    {
        public int Radius { get; set; }
        public double Area { get; set; }
        public bool AreaCalculated { get; set; }
        public double Perimeter { get; set; }
        public bool PerimeterCalculated { get; set; }
    }
    
    internal class AsyncCircleAreaFinder : StatePersistedAsyncOperation
    {
        internal int CanExecutionCount { get; set; }
        internal int ExecuteCount { get; set; }
        async protected override Task<bool> CanExecuteInternal(ExeContext context)
        {
            this.CanExecutionCount += 1;
            //Skip the step if already done.
            return await Task.FromResult(context.AreaCalculated == false);
        }

        async protected override Task<ExeContext> ExecuteInternal(ExeContext context)
        {
            this.ExecuteCount += 1;
            context.Area = 3.14 * context.Radius * context.Radius;
            context.AreaCalculated = true;
            return await Task.FromResult(context);
        }
    }
    internal class AsyncCirclePerimeterFinder : StatePersistedAsyncOperation
    {
        internal int CanExecutionCount { get; set; }
        internal int ExecuteCount { get; set; }
        async protected override Task<bool> CanExecuteInternal(ExeContext context)
        {
            //simulate exception only for first time.
            this.CanExecutionCount += 1;
            if (this.CanExecutionCount == 1)
            {
                throw new Exception("First time failed");
            }
            return await Task.FromResult(context.PerimeterCalculated == false);
        }

        async protected override Task<ExeContext> ExecuteInternal(ExeContext context)
        {
            this.ExecuteCount += 1;
            context.Perimeter = 2 * 3.14 * context.Radius;
            context.PerimeterCalculated = true;
            return await Task.FromResult(context);
        }
    }
}
