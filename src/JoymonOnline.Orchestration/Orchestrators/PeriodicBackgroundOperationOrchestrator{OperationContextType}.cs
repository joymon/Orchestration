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
        Timer _timer;
        ITriggerPeriodicBackgroundOperation _trigger;
        OperationContextType _context;
        public PeriodicBackgroundOperationOrchestrator() { }
        public PeriodicBackgroundOperationOrchestrator(IOperationsProvider<OperationContextType> provider,
            ITriggerPeriodicBackgroundOperation trigger) : base(provider)
        {
            _trigger = trigger;
        }
        
        public PeriodicBackgroundOperationOrchestrator(IEnumerable<IOperation<OperationContextType>> operations, 
            ITriggerPeriodicBackgroundOperation trigger) :base(operations)
        {
            _trigger = trigger;
        }
        
        

        protected override void InternalStart(OperationContextType context)
        {
            _trigger.Trigger += _trigger_Trigger;
            //_timer= new Timer(OnTick, null, 0, 2000);
            _context = context;
        }

        private void _trigger_Trigger(object sender, EventArgs e)
        {
            base.InternalStart(_context);
        }
        protected override void InternalStop()
        {
            _trigger.Trigger -= _trigger_Trigger;
            base.InternalStop();

        }
    }
}
