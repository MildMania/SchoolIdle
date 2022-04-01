public abstract class MMAnalyticsEventHandlerBase
{
    public abstract void HandleEvent(MMAnalyticsCustomEvent e);
}

public abstract class MMAnalyticsEventHandlerBase<T> : MMAnalyticsEventHandlerBase
    where T : MMAnalyticsCustomEvent
{
    protected bool CanHandle(MMAnalyticsCustomEvent e)
    {
        if (!(e is T))
            return false;

        return true;
    }

    public sealed override void HandleEvent(MMAnalyticsCustomEvent e)
    {
        if (!CanHandle(e))
            return;

        Handle((T)e);
    }


    public abstract void Handle(T e);
}
