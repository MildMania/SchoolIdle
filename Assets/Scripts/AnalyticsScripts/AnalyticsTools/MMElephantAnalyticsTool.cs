using System;
using System.Collections.Generic;
using ElephantSDK;

public class MMElephantAnalyticsTool : MMAnalyticToolBase
{
    public abstract class EventHandlerBase<T> : MMAnalyticsEventHandlerBase<T>
        where T : MMAnalyticsCustomEvent
    {
    }

    public class LevelStartedEventHandler : EventHandlerBase<LevelStartedEvent>
    {
        public override void Handle(LevelStartedEvent e)
        {
            Elephant.LevelStarted(e.LevelID);
        }
    }

    public class LevelCompletedEventHandler : EventHandlerBase<LevelCompletedEvent>
    {
        public override void Handle(LevelCompletedEvent e)
        {
            Elephant.LevelCompleted(
                e.LevelID);
        }
    }
    
    public class LevelFailedEventHandler : EventHandlerBase<LevelFailedEvent>
    {
        public override void Handle(LevelFailedEvent e)
        {
            Elephant.LevelFailed(
                e.LevelID);
        }
    }
    protected override List<MMAnalyticsEventHandlerBase> GetHandlerList()
    {
        return new List<MMAnalyticsEventHandlerBase>
        {
            new LevelStartedEventHandler(),
            new LevelCompletedEventHandler(),
            new LevelFailedEventHandler()
        };
    }
}
