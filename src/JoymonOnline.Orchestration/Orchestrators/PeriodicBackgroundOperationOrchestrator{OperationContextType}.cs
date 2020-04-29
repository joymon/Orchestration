using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JoymonOnline.Orchestration.Orchestrators
{
    public class PeriodicBackgroundOperationOrchestrator<OperationContextType> : OperationOrchestrator<OperationContextType>
    {
        readonly ITriggerPeriodicBackgroundOperation triggerOperation;
        OperationContextType _context;
        public PeriodicBackgroundOperationOrchestrator() { }
        public PeriodicBackgroundOperationOrchestrator(IOperationsProvider<OperationContextType> provider,
            ITriggerPeriodicBackgroundOperation trigger) : base(provider)
        {
            this.triggerOperation = trigger;
        }
        
        public PeriodicBackgroundOperationOrchestrator(IEnumerable<IOperation<OperationContextType>> operations, 
            ITriggerPeriodicBackgroundOperation trigger) :base(operations)
        {
            this.triggerOperation = trigger;
        }
        
        

        protected override void InternalStart(OperationContextType context)
        {
            triggerOperation.Trigger += TriggerOperation_Trigger;
            //_timer= new Timer(OnTick, null, 0, 2000);
            _context = context;
        }

        private void TriggerOperation_Trigger(object sender, EventArgs e)
        {
            base.InternalStart(_context);
        }
        protected override void InternalStop()
        {
            triggerOperation.Trigger -= TriggerOperation_Trigger;
            base.InternalStop();

        }
    }
}
