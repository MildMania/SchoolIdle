using UnityEngine;

public class AppInstallEvent : MMAnalyticsCustomEvent
{
    public string AppVersion { get; private set; }
    public string PlatformName { get; private set; }

    public AppInstallEvent(string appVersion, string platformName)
    {
        AppVersion = appVersion;
        PlatformName = platformName;
    }
}

public class AppLaunchEvent : MMAnalyticsCustomEvent
{
    public string AppVersion { get; private set; }
    public string PlatformName { get; private set; }

    public AppLaunchEvent(string appVersion, string platformName)
    {
        AppVersion = appVersion;
        PlatformName = platformName;
    }
}

public class AppUninstalledEvent : MMAnalyticsCustomEvent
{

}

public class AppGeneralAnalyticsTracker : MMAnalyticsTrackerBase
{
    bool _isFirstTimeOpening;

    const string IS_FIRST_TIME_OPENING = "IsFirstTime";

    public override void ActivateTracker()
    {
        InitIsFirstTime();

        CheckAppInstallEvent();
        CheckAppLaunchEvent();

        base.ActivateTracker();
    }

    void InitIsFirstTime()
    {
        if (!PlayerPrefs.HasKey(IS_FIRST_TIME_OPENING))
            _isFirstTimeOpening = true;

        PlayerPrefs.SetInt(IS_FIRST_TIME_OPENING, 1);
    }

    void CheckAppInstallEvent()
    {
        if (!_isFirstTimeOpening)
            return;

        AppInstallEvent appInstallEvent = new AppInstallEvent(Application.version, GetPlatformName());

        AnalyticsManager.SendCustomEvent(appInstallEvent);
    }

    void CheckAppLaunchEvent()
    {
        if (_isFirstTimeOpening)
            return;

        AppLaunchEvent appLaunchEvent = new AppLaunchEvent(Application.version, GetPlatformName());

        AnalyticsManager.SendCustomEvent(appLaunchEvent);
    }

    string GetPlatformName()
    {
        switch(Application.platform)
        {
            case RuntimePlatform.Android:
                return "android";
            case RuntimePlatform.IPhonePlayer:
                return "ios";
            default:
                return "none";
        }
    }
}
