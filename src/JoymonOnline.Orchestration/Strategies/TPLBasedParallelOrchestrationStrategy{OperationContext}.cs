using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Strategies
{
    class TPLBasedParallelOrchestrationStrategy<OperationContextType> : IOrchestrationStrategy<OperationContextType>
    {
        void IOrchestrationStrategy<OperationContextType>.Start(IEnumerable<IOperation<OperationContextType>> operations, OperationContextType context)
        {
            IList<Task> tasks = new List<Task>();
            foreach (IOperation<OperationContextType> op in operations)
            {
                Task task = new Task(() => op.Execute(context));
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
        }

        void IOrchestrationStrategy<OperationContextType>.Stop()
        {
            
        }
    }
}
