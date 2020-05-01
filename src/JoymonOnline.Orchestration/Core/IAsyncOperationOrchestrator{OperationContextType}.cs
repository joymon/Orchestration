using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Core
{
    public interface IAsyncOperationOrchestrator<OperationContextType>
    {
        Task<OperationContextType> Start(OperationContextType context);
    }
}
