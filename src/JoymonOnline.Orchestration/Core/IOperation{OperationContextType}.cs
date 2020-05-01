using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Core
{
    public interface IOperation<OperationContextType>
    {
        void Execute(OperationContextType context);
    }
}
