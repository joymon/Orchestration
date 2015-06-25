using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoymonOnline.Orchestration.Core
{
    public interface IOperationsProvider<OperationContextType>
    {
        IEnumerable<IOperation<OperationContextType>> GetOperations();
    }
}
