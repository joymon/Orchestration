﻿using FluentAssertions;
using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
    public class InterceptableAsyncOperationOrchestrator_OperationContextType_New
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenObjectCreatedWithOperationListNull_ThrowArgumentNullException()
        {
            IAsyncOperationOrchestrator<int> orchestrator =
                new InterceptableAsyncOperationOrchestrator<int>(null,null);
            int square = await orchestrator.Start(9);
            square.Should().Be(81);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenObjectCreatedWithInterceptorsNull_ThrowArgumentNullException()
        {
            var asyncOp = new AsyncFindSquareRoot();
            IAsyncOperationOrchestrator<int> orchestrator =
                new InterceptableAsyncOperationOrchestrator<int>(new List<IAsyncOperation<int>>() { asyncOp },null);
            _ = await orchestrator.Start(9);
            Console.WriteLine($"TimeCanExecute:{asyncOp.TimeWhenCanExecuteStarted},TimeExecute:{asyncOp.TimeWhenExecuteStarted}");
        }
    }
}