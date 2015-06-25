using System;

namespace JoymonOnline.Orchestration.Core
{
    public interface ITriggerPeriodicBackgroundOperation
    {
        event EventHandler Trigger;
    }
}