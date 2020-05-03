using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace JoymonOnline.Orchestration.Orchestrators
{
    public class TPLBasedParallelOperationsOrchestrator<OperationContextType> : OperationOrchestrator<OperationContextType>
    {
        public TPLBasedParallelOperationsOrchestrator(IOperationsProvider<OperationContextType> provider) : 
            base(provider,new TPLBasedParallelOrchestrationStrategy<OperationContextType>())
        {
            
        }
    }
}
