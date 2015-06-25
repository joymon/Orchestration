using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Core
{
    public interface IOperationInterceptable<OperationContextType>
    {
        void Before(OperationContextType context);
        void After(OperationContextType context);
    }
}
