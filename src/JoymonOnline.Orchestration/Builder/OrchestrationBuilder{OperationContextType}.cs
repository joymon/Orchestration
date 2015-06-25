using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoymonOnline.Orchestration.Orchestrators
{
    public class OrchestrationBuilder<OperationContextType>
    {
        IOperationOrchestrator<OperationContextType> _orchestrator;
        private OrchestrationBuilder(OperationOrchestrator<OperationContextType> orchestrator){
            _orchestrator = orchestrator;
        }
        public static OrchestrationBuilder<OperationContextType> Create()
        {
            return new OrchestrationBuilder<OperationContextType>(new OperationOrchestrator<OperationContextType>());
        }
        public OrchestrationBuilder<OperationContextType> AddOperation(IOperation<OperationContextType> step)
        {
            return this;
        }
        public IOperationOrchestrator<OperationContextType> Build( )
        {
            return _orchestrator;
        }
    }
    public static class OrchestrationBuilderWithExtensions
    {
        public static T Create<T>() where T :  IOperationOrchestrator, new()
        {
            return new T();
        }
        public static PeriodicBackgroundOperationOrchestrator<OperationContextType> SetInterval<OperationContextType>(this PeriodicBackgroundOperationOrchestrator<OperationContextType> orch,int delayInMilliSeconds)
        {
            return orch;
        }
    }
}
