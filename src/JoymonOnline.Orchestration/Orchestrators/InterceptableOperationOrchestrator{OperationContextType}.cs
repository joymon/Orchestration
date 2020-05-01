using JoymonOnline.Orchestration.Core;
using JoymonOnline.Orchestration.Orchestrators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Orchestrators
{
    public class InterceptableOperationOrchestrator<OperationContextType> :OperationOrchestrator<OperationContextType>
    {
        readonly IEnumerable<IOperationInterceptable<OperationContextType>> _interceptors;
        public InterceptableOperationOrchestrator(IOperationsProvider<OperationContextType> provider,
            IEnumerable<IOperationInterceptable<OperationContextType>> interceptors) :base(provider)
        {
            _interceptors = interceptors;
        }
        protected override void InternalStart(OperationContextType context)
        {
            foreach (IOperation<OperationContextType> operation in base.Operations)
            {
                CallInterceptorsBeforeExecution(context);
                operation.Execute(context);
                CallInterceptorsAfterExecution(context);
            }
        }

        private void CallInterceptorsAfterExecution(OperationContextType context)
        {
            foreach (IOperationInterceptable<OperationContextType> interceptable in _interceptors)
            {
                interceptable.After(context);
            }
        }

        private void CallInterceptorsBeforeExecution(OperationContextType context)
        {
            foreach( IOperationInterceptable<OperationContextType> interceptable in _interceptors )
            {
                interceptable.Before(context);
            }
        }
    }
}
