using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace JoymonOnline.Orchestration.Orchestrators
{
    public class AsyncOperationOrchestrator<OperationContextType> : IAsyncOperationOrchestrator<OperationContextType>
    {
        private IEnumerable<IAsyncOperation<OperationContextType>> Operations { get; }
        
        public AsyncOperationOrchestrator(IEnumerable<IAsyncOperation<OperationContextType>> ops)
        {
            ValidateAndThrowExceptionsIfNeeded(ops);
            this.Operations = ops;
        }
        
        #region Virtual methods
        async virtual protected Task<bool> OnCanExecute(IAsyncOperation<OperationContextType> op, OperationContextType context)
        {
            return await op.CanExecute(context);
        }
        async protected virtual  Task<OperationContextType> OnExecute(OperationContextType context, IAsyncOperation<OperationContextType> op)
        {
            return await op.Execute(context);
        }
        #endregion
        private void ValidateAndThrowExceptionsIfNeeded(IEnumerable<IAsyncOperation<OperationContextType>> ops)
        {
            if (ops == null) throw new ArgumentNullException(nameof(ops));
        }
        #region IAsyncOperationOrchestrator implementation
        async Task<OperationContextType> IAsyncOperationOrchestrator<OperationContextType>.Start(OperationContextType context)
        {
            foreach(var op in this.Operations)
            {
                if (await OnCanExecute(op, context))
                {
                    context = await OnExecute(context, op);
                }
            }
            return context;
        }

       
        #endregion

    }
}