using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Strategies
{
    public class SequentialOrchestrationStrategy<OperationContextType> : IOrchestrationStrategy<OperationContextType>
    {
        void IOrchestrationStrategy<OperationContextType>.Start(IEnumerable<IOperation<OperationContextType>> operations, OperationContextType context)
        {
            foreach (IOperation<OperationContextType> operation in operations)
            {
                operation.Execute(context);
            }
        }
        void IOrchestrationStrategy<OperationContextType>.Stop()
        {
            
        }
    }
}
