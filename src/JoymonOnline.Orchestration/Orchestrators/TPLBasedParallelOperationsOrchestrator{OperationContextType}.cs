using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JoymonOnline.Orchestration.Orchestrators
{
    public class TPLBasedParallelOperationsOrchestrator<OperationContextType> :OperationOrchestrator<OperationContextType>
    {
        public TPLBasedParallelOperationsOrchestrator(IOperationsProvider<OperationContextType> provider) :base(provider)
        {

        }
        protected override void InternalStart(OperationContextType context)
        {
            IList<Task> tasks = new List<Task>();
            foreach (IOperation<OperationContextType> op in base.Operations)
            {
                Task task = new Task(() => op.Execute(context));
                task.Start();
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
        }
    }
}
