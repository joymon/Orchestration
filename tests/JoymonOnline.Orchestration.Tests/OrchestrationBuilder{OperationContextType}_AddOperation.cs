using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;

namespace JoymonOnline.Orchestration.Tests
{
    /// <summary>
    /// Summary description for OrchestrationBuilder_OperationContextType__AddOperation
    /// </summary>
    [TestClass]
    public class OrchestrationBuilder_OperationContextType__AddOperation
    {
        public OrchestrationBuilder_OperationContextType__AddOperation()
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
        public void WhenTargetIsOperationOrchestration_ShouldWork()
        {
            IOperationOrchestrator<int> orch = OrchestrationBuilder<int>.Create()
                   .AddOperation(new TestOperation())
            .Build();

            orch.Start(2);
        }
        [TestMethod]
        public void WhenTriedWithFluentAPIUsingExtensions_ShouldWork()
        {
            PeriodicBackgroundOperationOrchestrator<int> pOrch = OrchestrationBuilderWithExtensions.Create<PeriodicBackgroundOperationOrchestrator<int>>();
            pOrch.SetInterval<int>(34);
        }
    }
    class TestOperation : IOperation<int>
    {
        void IOperation<int>.Execute(int context)
        {
            Trace.WriteLine(context);
        }
    }
}
