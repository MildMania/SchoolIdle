using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MMAnalyticsModule
{
    public string Name;
    public bool IsActive = true;

    public MMAnalyticsTrackerBase Tracker { get; private set; }

    public MMAnalyticsModule(string name, MMAnalyticsTrackerBase tracker)
    {
        Name = name;
        Tracker = tracker;
    }
}

[System.Serializable]
public class MMAnalyticsToolInfo
{
    public string Name;
    public bool IsActive = true;

    public MMAnalyticToolBase Tool { get; private set; }

    public MMAnalyticsToolInfo(string name, MMAnalyticToolBase tool)
    {
        Name = name;
        Tool = tool;
    }
}

public class MMAnalyticsManager : MonoBehaviour
{
    const string ANALYTICS_TOOL_GAMEANALYTICS = "GameAnalytics";
    const string ANALYTICS_TOOL_FACEBOOK = "Facebook";
    const string ANALYTICS_TOOL_ELEPHANT = "Elephant";

    const string APP_GENERAL_ANALYTICS_MODULE = "General Analytics Module";
    const string PLAYER_PROGRESSION_ANALYTICS_MODULE = "Player Progression";

    public bool GameAnalytics;
    public bool FacebookAnalytics;
    public bool ElephantAnalytics;

    List<MMAnalyticsToolInfo> _analyticsTools;

    public List<MMAnalyticsModule> AnalyticsModules = new List<MMAnalyticsModule>
    {
        new MMAnalyticsModule(APP_GENERAL_ANALYTICS_MODULE, new AppGeneralAnalyticsTracker()),
        new MMAnalyticsModule(PLAYER_PROGRESSION_ANALYTICS_MODULE, new PlayerProgressionAnalyticsTracker()),
    };

    void Awake()
    {
        InitAnalyticTools();
        InitAnalyticsModules();
    }

    void OnDestroy()
    {
        DisposeAnalyticsTools();
        DisposeAnalyticsModules();
    }

    void InitAnalyticTools()
    {
        _analyticsTools = new List<MMAnalyticsToolInfo>();

        if (GameAnalytics)
            _analyticsTools.Add(new MMAnalyticsToolInfo(ANALYTICS_TOOL_GAMEANALYTICS, new MMGameAnalyticsTool()));
        if (FacebookAnalytics)
            _analyticsTools.Add(new MMAnalyticsToolInfo(ANALYTICS_TOOL_FACEBOOK, new MMFacebookAnalyticsTool()));
        if(ElephantAnalytics)
            _analyticsTools.Add(new MMAnalyticsToolInfo(ANALYTICS_TOOL_ELEPHANT, new MMElephantAnalyticsTool()));
    }

    void InitAnalyticsModules()
    {
        foreach (MMAnalyticsModule m in AnalyticsModules)
        {
            m.Tracker.AnalyticsManager = this;

            if (!m.IsActive)
                continue;

            m.Tracker.ActivateTracker();
        }
    }

    void DisposeAnalyticsModules()
    {
        foreach (MMAnalyticsModule m in AnalyticsModules)
            m.Tracker.Dispose();
    }

    void DisposeAnalyticsTools()
    {
        foreach (MMAnalyticsToolInfo t in _analyticsTools)
        {
            IDisposable dt = t as IDisposable;

            if (dt == null)
                continue;

            dt.Dispose();
        }
    }

    public void SendCustomEvent(MMAnalyticsCustomEvent e)
    {
#if !UNITY_EDITOR
        foreach(MMAnalyticsToolInfo t in _analyticsTools)
        {
            if (!t.IsActive)
                continue;

            t.Tool.TryHandleEvent(e);
        }
#endif
    }
}
