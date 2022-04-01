using System.Collections.Generic;

public abstract class MMAnalyticToolBase
{
    List<MMAnalyticsEventHandlerBase> _handlerList;
    protected List<MMAnalyticsEventHandlerBase> HandlerList
    {
        get
        {
            if (_handlerList == null)
                _handlerList = GetHandlerList();

            return _handlerList;
        }
    }

    public void TryHandleEvent(MMAnalyticsCustomEvent e)
    {
        foreach (MMAnalyticsEventHandlerBase handler in HandlerList)
            handler.HandleEvent(e);
    }

    protected abstract List<MMAnalyticsEventHandlerBase> GetHandlerList();
}
