using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JoymonOnline.Orchestration.Orchestrators
{
    public class ReversibleOperationOrchestrator<OperationContextType> :OperationOrchestrator<OperationContextType>
    {
        public ReversibleOperationOrchestrator(IOperationsProvider<OperationContextType> provider ) :base(provider)
        {

        }
        protected override void InternalStart(OperationContextType context)
        {
            IOperation<OperationContextType>[] ops = base.Operations.ToArray();
            int counter = 0;
            for (; counter < ops.Length; counter++)
            {
                try
                {
                    ops[counter].Execute(context);
                }
                catch(Exception ex)
                {
                    break;
                }
            }
            RevertIfRequired(ops, counter,context);
        }

        private void RevertIfRequired(IOperation<OperationContextType>[] ops, int counter,OperationContextType context)
        {
            if( counter < ops.Length )
            {
                RevertAlreadyExecutedOperations(ops, counter-1,context);
            }
        }

        private void RevertAlreadyExecutedOperations(IOperation<OperationContextType>[] ops, int counter,OperationContextType context)
        {
            for(;counter >= 0; counter--)
            {
                Revert(ops[counter] as IReversible<OperationContextType>,context);
            }
        }

        private static void Revert(IReversible<OperationContextType> reversible,OperationContextType context)
        {
            if (reversible != null)
            {
                reversible.Reverse(context);
            }
        }
    }
}
