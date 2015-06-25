using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Threading;
using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;

namespace TestProject2
{
    /// <summary>
    /// Summary description for TPLBasedParallelOperationsOrchestrator_OperationContextType__Start
    /// </summary>
    [TestClass]
    public class TPLBasedParallelOperationsOrchestrator_OperationContextType__Start
    {
        public TPLBasedParallelOperationsOrchestrator_OperationContextType__Start()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void WhenOperationsContaingDelay_WaitForAllOperationsToComplete()
        {
            IOperationOrchestrator<int> orch = new TPLBasedParallelOperationsOrchestrator<int>(
                new ParallelOperationsProvider());
            orch.Start(30);
            Thread.Sleep(5000);
        }
    }
    class ParallelOperationsProvider : IOperationsProvider<int>
    {
        IEnumerable<IOperation<int>> IOperationsProvider<int>.GetOperations()
        {
            yield return new ParallelOp1();
            yield return new ParallelOp2();
        }
    }
    class ParallelOp1 : IOperation<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Debug.WriteLine("ParallelOp1 before waiting");
            Thread.Sleep(2000);
            Debug.WriteLine("ParallelOp1 after waiting");
        }
    }
    class ParallelOp2 : IOperation<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Debug.WriteLine("ParallelOp2 before waiting");
            Thread.Sleep(2000);
            Debug.WriteLine("ParallelOp2 after waiting");
        }
    }
}
