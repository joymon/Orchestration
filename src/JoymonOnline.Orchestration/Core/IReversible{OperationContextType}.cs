using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoymonOnline.Orchestration.Core
{
    public interface IReversible<OperationContextType>
    {
        void Reverse(OperationContextType context);
    }
    
}
