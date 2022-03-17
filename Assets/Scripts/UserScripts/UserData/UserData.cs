using System;

public abstract class UserData
{
    public User User { get; private set; }
    public bool IsDataLoaded { get; protected set; }
    
    private Action _onDataLoaded;

    public virtual void InitUser(User user)
    {
        User = user;
    }

    public void WaitUntilDataLoad(Action onLoadedCallback)
    {
        if (IsDataLoaded)
            onLoadedCallback?.Invoke();

        if (onLoadedCallback != null)
            _onDataLoaded += onLoadedCallback;
    }

    public abstract void LoadData(Action onLoadedCallback);
    public abstract void SaveData(Action onSavedCallback);

    protected void DataLoaded()
    {
        IsDataLoaded = true;

        _onDataLoaded?.Invoke();
        _onDataLoaded = null;
    }
}
