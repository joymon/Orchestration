using JoymonOnline.Orchestration.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace JoymonOnline.Orchestration.Triggers
{
    /// <summary>
    /// Trigger based on interval. The first hit will be after the interval.
    /// </summary>
    /// <seealso cref="JoymonOnline.Orchestration.Core.ITriggerPeriodicBackgroundOperation" />
    public class TimerBasedTrigger : ITriggerPeriodicBackgroundOperation
    {
        Timer _timer;
        public TimerBasedTrigger(int intervalInMilliSeconds)
        {
            _timer = new Timer(OnTick, null, intervalInMilliSeconds, intervalInMilliSeconds);
        }
        private void OnTick(object state)
        {
            OnTrigger(EventArgs.Empty);
        }
        private event EventHandler _trigger;
        event EventHandler ITriggerPeriodicBackgroundOperation.Trigger
        {
            add
            {
                _trigger += value;
            }

            remove
            {
                _trigger -= value;
            }
        }
        protected virtual void OnTrigger(EventArgs e)
        {
            
            if(_trigger !=null)
                _trigger(this, e);
        }
    }
}
