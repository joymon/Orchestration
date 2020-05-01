using JoymonOnline.Orchestration.Core;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace JoymonOnline.Orchestration.Orchestrators
{
    public class AsyncOperationOrchestrator<OperationContextType> : IAsyncOperationOrchestrator<OperationContextType>
    {
        private IEnumerable<IAsyncOperation<OperationContextType>> Operations { get; }
        public AsyncOperationOrchestrator(IEnumerable<IAsyncOperation<OperationContextType>> ops)
        {
            this.Operations = ops;
        }
        async Task<OperationContextType> IAsyncOperationOrchestrator<OperationContextType>.Start(OperationContextType context)
        {
            foreach(var op in this.Operations)
            {
                if(await op.CanExecute(context))
                {
                    context = await op.Execute(context);
                }
            }
            return context;
        }
    }
}