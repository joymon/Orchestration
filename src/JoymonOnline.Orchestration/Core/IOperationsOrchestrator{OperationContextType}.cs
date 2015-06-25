using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoymonOnline.Orchestration.Core
{
    public interface IOperationOrchestrator
    {
        void Start();
        void Stop();
    }
    public interface IOperationOrchestrator<OperationContextType> :IOperationOrchestrator
    {
        void Start(OperationContextType context);
    }
}
