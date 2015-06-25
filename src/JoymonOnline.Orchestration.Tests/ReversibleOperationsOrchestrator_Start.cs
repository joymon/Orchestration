using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;

namespace TestProject2
{
    /// <summary>
    /// Summary description for ReversibleOperationsOrchestrator_Start
    /// </summary>
    [TestClass]
    public class ReversibleOperationsOrchestrator_Start
    {
        public ReversibleOperationsOrchestrator_Start()
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
        public void WhenSecondOperationThrowsException_FirstOperationShouldBeReverted()
        {
            IOperationOrchestrator<int> orch = new ReversibleOperationOrchestrator<int>(new ReversibleOperationProvider());
            orch.Start(20);
        }
    }
    class ReversibleOperationProvider : IOperationsProvider<int>
    {
        IEnumerable<IOperation<int>> IOperationsProvider<int>.GetOperations()
        {
            yield return new ReversibleOperation1();
            yield return new ReversibleOperation2();
        }
    }
    class ReversibleOperation1 : IOperation<int>, IReversible<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Debug.WriteLine("ReversibleOperation1");
        }

        void IReversible<int>.Reverse(int context)
        {
            Debug.WriteLine("Reverting ReversibleOperation1");
        }
    }
    class ReversibleOperation2 : IOperation<int>, IReversible<int>
    {
        void IOperation<int>.Execute(int context)
        {
            throw new Exception("test");
        }

        void IReversible<int>.Reverse(int context)
        {
            Debug.WriteLine("Reverting ReversibleOperation2");
        }
    }
}
