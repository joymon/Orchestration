using FluentAssertions;
using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using JoymonOnline.Orchestration.Triggers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace JoymonOnline.Orchestration.Tests
{
    [TestClass]
    public class PeriodicBackgroundOperationOrchestrator_OperationContextType_Start
    {
        [TestMethod]
        public void WhenIntervalIs500msAndWaitFor2100ms_ShouldWork()
        {
            //Arrange
            FindNextNumber findNextNumberOp = new FindNextNumber();
            IOperationOrchestrator<int> orchestrator =
                new PeriodicBackgroundOperationOrchestrator<int>(new List<IOperation<int>>() { findNextNumberOp },
                new TimerBasedTrigger(500));
            //Act
            orchestrator.Start(10);
            Thread.Sleep(2100);
            Console.WriteLine($"Execution count:{findNextNumberOp.ExecutionCount}");
            //Assert
            findNextNumberOp.ExecutionCount.Should().BeLessOrEqualTo(4);
        }
        [TestMethod]
        public void WhenIntervalIs1AndStoppedAfter3SecondsAndWaitsFor3MoreSeconds_ExecutionCountShouldNotExceed3()
        {
            //Arrange
            FindNextNumber findNextNumberOp = new FindNextNumber();
            IOperationOrchestrator<int> orchestrator =
                new PeriodicBackgroundOperationOrchestrator<int>(new List<IOperation<int>>() { findNextNumberOp },
                new TimerBasedTrigger(1000));
            //Act
            orchestrator.Start(10);
            Thread.Sleep(3000);
            orchestrator.Stop();
            Thread.Sleep(3000);
            //Assert
            findNextNumberOp.ExecutionCount.Should().BeLessOrEqualTo(3);

        }


    }

    public class FindNextNumber : IOperation<int>
    {
        public int ExecutionCount { get; set; }
        void IOperation<int>.Execute(int context)
        {
            ExecutionCount += 1;
            Trace.WriteLine(string.Format("Next number of {0} is {1}", context, context + 1));
        }
    }
    public class PeriodicBackgroundOperationsProvider : IOperationsProvider<int>
    {
        IEnumerable<IOperation<int>> IOperationsProvider<int>.GetOperations()
        {
            return new List<IOperation<int>>() { new FindNextNumber() };
        }
    }
}
