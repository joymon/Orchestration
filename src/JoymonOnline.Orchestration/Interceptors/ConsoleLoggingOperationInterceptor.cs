using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoymonOnline.Orchestration.Interceptors
{
    public class ConsoleLoggingOperationInterceptor<OperationContextType> : IOperationInterceptable<OperationContextType>
    {
        void IOperationInterceptable<OperationContextType>.After(OperationContextType context)
        {
            Console.WriteLine("Intercepted after the execution. context is {0}",context);
        }

        void IOperationInterceptable<OperationContextType>.Before(OperationContextType context)
        {
            Console.WriteLine("Intercepted before the execution. context is {0}", context);
        }
    }
}
