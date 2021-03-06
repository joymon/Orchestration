﻿using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
     public partial class AsyncOperationOrchestrator_OperationContextType_Start
    {
        [TestMethod]
        public async Task WhenInNormalCondition_ShouldWork()
        {
            IAsyncOperationOrchestrator<int> orchestrator =
                new AsyncOperationOrchestrator<int>(new List<IAsyncOperation<int>>() { new AsyncFindSquareRoot() });
            int square = await orchestrator.Start(9);
            Assert.AreEqual(3, square);
        }
        [TestMethod]
        public async Task WhenTheCanExecuteTakesMoreTime_ExecuteShouldNotExecuteBeforeCanExecute()
        {
            var asyncOp = new AsyncFindSquareRoot();
            IAsyncOperationOrchestrator<int> orchestrator =
                new AsyncOperationOrchestrator<int>(new List<IAsyncOperation<int>>() { asyncOp });
            _ = await orchestrator.Start(9);
            Console.WriteLine($"TimeCanExecute:{asyncOp.TimeWhenCanExecuteStarted},TimeExecute:{asyncOp.TimeWhenExecuteStarted}");
            
            Assert.IsTrue(asyncOp.TimeWhenExecuteStarted > asyncOp.TimeWhenCanExecuteStarted,
                $"TimeCanExecute:{asyncOp.TimeWhenCanExecuteStarted},TimeExecute:{asyncOp.TimeWhenExecuteStarted}");
        }
    }
  
}
