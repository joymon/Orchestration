using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Core
{
    public interface IAsyncOperationInterceptable<OperationContextType>
    {
        Task<bool> BeforeExecute(IAsyncOperation<OperationContextType> op, OperationContextType context);
        Task AfterExecute(IAsyncOperation<OperationContextType> op,OperationContextType context);
    }
}
