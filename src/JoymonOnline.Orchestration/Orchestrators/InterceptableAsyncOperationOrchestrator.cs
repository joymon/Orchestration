using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace JoymonOnline.Orchestration.Orchestrators
{
    public class InterceptableAsyncOperationOrchestrator<OperationContextType> : AsyncOperationOrchestrator<OperationContextType>
    {
        private IEnumerable<IAsyncOperationInterceptable<OperationContextType>> Interceptors { get; }
        public InterceptableAsyncOperationOrchestrator(IEnumerable<IAsyncOperation<OperationContextType>> ops,
            IEnumerable<IAsyncOperationInterceptable<OperationContextType>> interceptors):base(ops)
        {
            ValidateAndThrowExceptionsIfNeeded(interceptors);

            this.Interceptors = interceptors;
        }
        private void ValidateAndThrowExceptionsIfNeeded(IEnumerable<IAsyncOperationInterceptable<OperationContextType>> interceptors)
        {
            if (interceptors == null) throw new ArgumentNullException(nameof(interceptors));
        }
        async protected override Task<OperationContextType> OnExecute(OperationContextType context, IAsyncOperation<OperationContextType> op)
        {
            bool beforeExecuteResult = await CallInterceptorsBeforeExecution(this.Interceptors, op, context);
            if (beforeExecuteResult)
            {
                context = await base.OnExecute(context, op);
                await CallInterceptorsAfterExecution(this.Interceptors, op, context);
            }
            return context;
        }
        private async Task CallInterceptorsAfterExecution(IEnumerable<IAsyncOperationInterceptable<OperationContextType>> interceptors, IAsyncOperation<OperationContextType> op, OperationContextType context)
        {
            foreach (var interceptor in interceptors)
            {
                await interceptor.AfterExecute(op, context);
            }
        }
        private async Task<bool> CallInterceptorsBeforeExecution(IEnumerable<IAsyncOperationInterceptable<OperationContextType>> interceptors, IAsyncOperation<OperationContextType> op, OperationContextType context)
        {
            foreach (var interceptor in interceptors)
            {
                if (await interceptor.BeforeExecute(op, context) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}