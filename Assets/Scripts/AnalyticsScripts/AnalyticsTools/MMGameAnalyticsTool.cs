using System.Collections.Generic;
using GameAnalyticsSDK;

public class MMGameAnalyticsTool : MMAnalyticToolBase
{
    public abstract class EventHandlerBase<T> : MMAnalyticsEventHandlerBase<T>
        where T : MMAnalyticsCustomEvent
    {
    }

    #region Player Progression Event Handlers

    public class LevelCompletedEventHandler : EventHandlerBase<LevelCompletedEvent>
    {
        private const string LEVEL_ID = "Level";

        public override void Handle(LevelCompletedEvent e)
        {
            string progression01 = LEVEL_ID + e.LevelID.ToString();

            GameAnalytics.NewProgressionEvent(
                GAProgressionStatus.Complete, progression01);
        }
    }
    
    public class LevelFailedEventHandler : EventHandlerBase<LevelFailedEvent>
    {
        private const string LEVEL_ID = "Level";

        public override void Handle(LevelFailedEvent e)
        {
            string progression01 = LEVEL_ID + e.LevelID.ToString();

            GameAnalytics.NewProgressionEvent(
                GAProgressionStatus.Fail, progression01);
        }
    }

    public class LevelStartedEventHandler : EventHandlerBase<LevelStartedEvent>
    {
        private const string LEVEL_ID = "Level";

        public override void Handle(LevelStartedEvent e)
        {
            string progression01 = LEVEL_ID + e.LevelID.ToString();

            GameAnalytics.NewProgressionEvent(
                GAProgressionStatus.Start, progression01);
        }
    }

    #endregion Player Progression Event Handlers

    #region Design Event Handlers

    public abstract class DesignEventHandlerBase<T> : EventHandlerBase<T>
        where T : MMAnalyticsCustomEvent
    {
        public sealed override void Handle(T e)
        {
            float parameter = GetParameter(e);

            GameAnalytics.NewDesignEvent(GetEventName(), parameter);
        }

        protected virtual float GetParameter(T e)
        {
            return 0;
        }

        protected abstract string GetEventName();
    }

    public class SessionStartedEventHandler : DesignEventHandlerBase<SessionStartedEvent>
    {
        private const string EVENT_NAME = "session_started";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }
    }

    public class SessionEndedEventHandler : DesignEventHandlerBase<SessionEndedEvent>
    {
        private const string EVENT_NAME = "session_ended";

        private const string DURATION = "duration";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }

        protected override float GetParameter(SessionEndedEvent e)
        {
            return e.SessionDuration;
        }
    }

    #endregion Design Event Handlers

    public MMGameAnalyticsTool()
    {
        GameAnalytics.Initialize();
    }

    protected override List<MMAnalyticsEventHandlerBase> GetHandlerList()
    {
        return new List<MMAnalyticsEventHandlerBase>
        {
            new LevelCompletedEventHandler(),
            new LevelStartedEventHandler(),
            new LevelFailedEventHandler(),
            new SessionStartedEventHandler(),
            new SessionEndedEventHandler(),
        };
    }
}