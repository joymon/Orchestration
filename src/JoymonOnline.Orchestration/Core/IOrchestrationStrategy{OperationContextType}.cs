using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Core
{
    public interface IOrchestrationStrategy<OperationContextType>
    {
        void Stop();
        void Start(IEnumerable<IOperation<OperationContextType>> operations, OperationContextType context);
    }
}
