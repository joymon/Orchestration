using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Core
{
    public interface IAsyncOperation<OperationContextType>
    {
        Task<bool> CanExecute(OperationContextType context);
        Task<OperationContextType> Execute(OperationContextType context);
    }
}
