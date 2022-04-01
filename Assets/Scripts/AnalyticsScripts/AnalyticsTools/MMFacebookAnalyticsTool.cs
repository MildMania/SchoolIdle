using System.Collections.Generic;
using Facebook.Unity;
using System;

public class MMFacebookAnalyticsTool : MMAnalyticToolBase
{
    public abstract class EventHandlerBase<T> : MMAnalyticsEventHandlerBase<T>
        where T : MMAnalyticsCustomEvent
    {
        public sealed override void Handle(T e)
        {
            LogEvent(GetEventName(), null, GetParameterDict(e));
        }

        protected abstract string GetEventName();

        protected virtual Dictionary<string, object> GetParameterDict(T e)
        {
            return new Dictionary<string, object>();
        }
    }

    #region App General Event Handlers

    public class AppInstallEventHandler : EventHandlerBase<AppInstallEvent>
    {
        private const string EVENT_NAME = "app_install";

        private const string APP_VERSION = "app_version";
        private const string PLATFORM_ID = "platform";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }

        protected override Dictionary<string, object> GetParameterDict(AppInstallEvent e)
        {
            return new Dictionary<string, object>
            {
                {APP_VERSION, e.AppVersion },
                {PLATFORM_ID, e.PlatformName }
            };
        }
    }

    public class AppLaunchEventHandler : EventHandlerBase<AppLaunchEvent>
    {
        private const string EVENT_NAME = "app_launch";

        private const string APP_VERSION = "app_version";
        private const string PLATFORM_ID = "platform";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }

        protected override Dictionary<string, object> GetParameterDict(AppLaunchEvent e)
        {
            return new Dictionary<string, object>
            {
                {APP_VERSION, e.AppVersion },
                {PLATFORM_ID, e.PlatformName }
            };
        }
    }

    #endregion App General Event Handlers

    #region Player Progression Event Handlers

    public class LevelCompletedEventHandler : EventHandlerBase<LevelCompletedEvent>
    {
        private const string EVENT_NAME = "level_completed";

        private const string LEVEL_ID = "Level";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }

        protected override Dictionary<string, object> GetParameterDict(LevelCompletedEvent e)
        {
            return new Dictionary<string, object>
            {
                {LEVEL_ID, e.LevelID }
            };
        }
    }
    
    public class LevelFailedEventHandler : EventHandlerBase<LevelFailedEvent>
    {
        private const string EVENT_NAME = "level_failed";

        private const string LEVEL_ID = "Level";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }

        protected override Dictionary<string, object> GetParameterDict(LevelFailedEvent e)
        {
            return new Dictionary<string, object>
            {
                {LEVEL_ID, e.LevelID }
            };
        }
    }

    public class LevelStartedEventHandler : EventHandlerBase<LevelStartedEvent>
    {
        private const string EVENT_NAME = "level_started";

        private const string LEVEL_ID = "Level";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }

        protected override Dictionary<string, object> GetParameterDict(LevelStartedEvent e)
        {
            return new Dictionary<string, object>
            {
                {LEVEL_ID, e.LevelID }
            };
        }
    }

    public class SessionStartedEventHandler : EventHandlerBase<SessionStartedEvent>
    {
        private const string EVENT_NAME = "session_started";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }

        protected override Dictionary<string, object> GetParameterDict(SessionStartedEvent e)
        {
            return new Dictionary<string, object>
            {
            };
        }
    }

    public class SessionEndedEventHandler : EventHandlerBase<SessionEndedEvent>
    {
        private const string EVENT_NAME = "session_started";

        private const string DURATION = "duration";

        private const string LEVEL_ID = "level_id";

        protected override string GetEventName()
        {
            return EVENT_NAME;
        }

        protected override Dictionary<string, object> GetParameterDict(SessionEndedEvent e)
        {
            return new Dictionary<string, object>
            {
                {DURATION, e.SessionDuration },
                {LEVEL_ID, e.SessionEndLevelID }
            };
        }
    }
    #endregion Player Progression Event Handlers

    public MMFacebookAnalyticsTool()
    {
    }

    protected override List<MMAnalyticsEventHandlerBase> GetHandlerList()
    {
        return new List<MMAnalyticsEventHandlerBase>
        {
            new AppInstallEventHandler(),
            new AppLaunchEventHandler(),
            new LevelStartedEventHandler(),
            new LevelCompletedEventHandler(),
            new LevelFailedEventHandler(),
            new SessionStartedEventHandler(),
            new SessionEndedEventHandler(),
        };
    }

    public static void LogEvent(string eventName, float? valueToSum = null, Dictionary<string, object> parameters = null)
    {
        Action onInitedCallback = null;

        onInitedCallback = delegate ()
        {
            FB.LogAppEvent(eventName, valueToSum, parameters);
        };

        if (MMFacebookManager.Instance.IsInited(onInitedCallback))
            onInitedCallback();
    }
}