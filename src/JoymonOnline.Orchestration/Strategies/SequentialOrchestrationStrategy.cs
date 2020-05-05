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
        public bool IsStopped { get; set; }
        void IOrchestrationStrategy<OperationContextType>.Start(IEnumerable<IOperation<OperationContextType>> operations, OperationContextType context)
        {
            this.IsStopped = false;
            foreach (IOperation<OperationContextType> operation in operations)
            {
                if (this.IsStopped)
                {
                    break;
                }
                else
                {
                    operation.Execute(context);
                }
            }
        }
        void IOrchestrationStrategy<OperationContextType>.Stop()
        {
            this.IsStopped = true;
        }
    }
}
