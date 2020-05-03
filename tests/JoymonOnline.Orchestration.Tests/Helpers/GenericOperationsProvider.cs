using JoymonOnline.Orchestration.Core;
using System.Collections.Generic;

namespace JoymonOnline.Orchestration.Tests
{
    class GenericOperationsProvider<OperationContext> : IOperationsProvider<OperationContext>
    {
        IEnumerable<IOperation<OperationContext>> Operations { get; }
        internal GenericOperationsProvider(IEnumerable<IOperation<OperationContext>> ops)
        {
            this.Operations = ops;
        }
        IEnumerable<IOperation<OperationContext>> IOperationsProvider<OperationContext>.GetOperations()
        {
            return Operations;
        }
    }
}
